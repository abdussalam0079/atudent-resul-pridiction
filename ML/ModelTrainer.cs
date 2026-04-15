// ================================================================
//  SmartStudentSystem – ML/ModelTrainer.cs
//  Q#2(ii) – ML.NET binary classification: Pass / Fail prediction
// ================================================================

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using SmartStudentSystem.Models;

namespace SmartStudentSystem.ML
{
    /// <summary>
    /// Handles all ML.NET concerns:
    ///   • Synthetic training-data generation  
    ///   • FastTree binary-classification pipeline  
    ///   • Model evaluation, persistence and hot-loading  
    ///   • Single-row prediction via <see cref="PredictionEngine{TSrc,TDst}"/>
    /// </summary>
    public class ModelTrainer
    {
        // ── Constants ────────────────────────────────────────────────────────
        private const string ModelFile   = "student_model.zip";
        private const string DataFile    = "Data/students.csv";
        private const float  PassMark    = 40f;
        private const int    SampleCount = 1200;   // synthetic training rows

        // ── Fields ───────────────────────────────────────────────────────────
        private readonly MLContext _ctx;
        private ITransformer?      _model;
        private PredictionEngine<StudentData, StudentPrediction>? _engine;

        // ── Evaluation metrics (read after Initialise) ────────────────────────
        public double Accuracy { get; private set; }
        public double F1Score  { get; private set; }
        public double AucRoc   { get; private set; }
        public bool   IsReady  => _engine is not null;

        // ── Constructor ──────────────────────────────────────────────────────
        public ModelTrainer() => _ctx = new MLContext(seed: 42);

        // ── Public API ───────────────────────────────────────────────────────

        /// <summary>
        /// Loads cached model if present; otherwise trains and saves a new one.
        /// </summary>
        public void Initialise()
        {
            if (File.Exists(ModelFile))
                Load();
            else
                TrainAndSave();
        }

        /// <summary>Forces re-training (called from the UI's "Retrain" button).</summary>
        public void Retrain() => TrainAndSave();

        /// <summary>
        /// Runs inference on a single student's marks.
        /// </summary>
        public StudentPrediction Predict(StudentData input)
        {
            if (_engine is null)
                throw new InvalidOperationException(
                    "Model not initialised. Call Initialise() first.");
            return _engine.Predict(input);
        }

        // ── Training ─────────────────────────────────────────────────────────

        private void TrainAndSave()
        {
            // 1. Generate or load training data
            var rows = File.Exists(DataFile)
                ? LoadCsv()
                : GenerateSyntheticData();

            var view  = _ctx.Data.LoadFromEnumerable(rows);
            var split = _ctx.Data.TrainTestSplit(view, testFraction: 0.20);

            // 2. Build pipeline
            var pipeline = BuildPipeline();

            // 3. Fit
            _model = pipeline.Fit(split.TrainSet);

            // 4. Evaluate
            Evaluate(split.TestSet);

            // 5. Persist
            _ctx.Model.Save(_model, view.Schema, ModelFile);

            // 6. Create prediction engine
            CreateEngine();
        }

        private void Load()
        {
            _model = _ctx.Model.Load(ModelFile, out _);
            CreateEngine();
        }

        // ── Pipeline ─────────────────────────────────────────────────────────

        private IEstimator<ITransformer> BuildPipeline()
        {
            string[] features =
            {
                nameof(StudentData.MathMarks),
                nameof(StudentData.EnglishMarks),
                nameof(StudentData.ScienceMarks),
                nameof(StudentData.UrduMarks),
                nameof(StudentData.IslamiatMarks)
            };

            return _ctx.Transforms
                       .Concatenate("Features", features)
                .Append(
                    _ctx.BinaryClassification.Trainers.FastTree(
                        labelColumnName:        "Label",
                        featureColumnName:      "Features",
                        numberOfLeaves:         20,
                        numberOfTrees:          100,
                        minimumExampleCountPerLeaf: 5,
                        learningRate:           0.2));
        }

        // ── Evaluation ───────────────────────────────────────────────────────

        private void Evaluate(IDataView testSet)
        {
            var predictions = _model!.Transform(testSet);
            var metrics     = _ctx.BinaryClassification
                                  .Evaluate(predictions, "Label");
            Accuracy = metrics.Accuracy;
            F1Score  = metrics.F1Score;
            AucRoc   = metrics.AreaUnderRocCurve;
        }

        // ── Prediction engine ─────────────────────────────────────────────────

        private void CreateEngine()
            => _engine = _ctx.Model
                             .CreatePredictionEngine<StudentData, StudentPrediction>(_model!);

        // ── Synthetic data generation ────────────────────────────────────────

        private static List<StudentData> GenerateSyntheticData()
        {
            var rng  = new Random(99);
            var rows = new List<StudentData>(SampleCount);

            for (int i = 0; i < SampleCount; i++)
            {
                // 60 % passing students, 40 % failing – realistic class distribution
                bool pass = rng.NextDouble() < 0.60;

                float RandMark(float min, float max)
                    => (float)(min + rng.NextDouble() * (max - min));

                var row = pass
                    ? new StudentData
                    {
                        MathMarks     = RandMark(40, 100),
                        EnglishMarks  = RandMark(40, 100),
                        ScienceMarks  = RandMark(40, 100),
                        UrduMarks     = RandMark(40, 100),
                        IslamiatMarks = RandMark(40, 100),
                        Result        = true
                    }
                    : new StudentData
                    {
                        // At least one subject below pass mark
                        MathMarks     = RandMark(0,  rng.NextDouble() < 0.5 ? 39 : 100),
                        EnglishMarks  = RandMark(0,  rng.NextDouble() < 0.4 ? 39 : 100),
                        ScienceMarks  = RandMark(0,  rng.NextDouble() < 0.3 ? 39 : 100),
                        UrduMarks     = RandMark(30, 100),
                        IslamiatMarks = RandMark(30, 100),
                        Result        = false
                    };

                rows.Add(row);
            }

            return rows;
        }

        private List<StudentData> LoadCsv()
        {
            var view = _ctx.Data.LoadFromTextFile<StudentData>(
                path:              DataFile,
                hasHeader:         true,
                separatorChar:     ',');
            return new List<StudentData>(
                _ctx.Data.CreateEnumerable<StudentData>(view, reuseRowObject: false));
        }

        // ── Summary string ───────────────────────────────────────────────────

        public string MetricsSummary()
            => $"Accuracy: {Accuracy:P1}  |  F1: {F1Score:P1}  |  AUC-ROC: {AucRoc:P1}";
    }
}
