// ================================================================
//  SmartStudentSystem – Models/StudentData.cs
//  Q#2(i) – ML data model: features + label for binary classification
// ================================================================

using Microsoft.ML.Data;

namespace SmartStudentSystem.Models
{
    /// <summary>
    /// Flat feature row consumed by the ML.NET training pipeline.
    /// Column indices map to the CSV layout in Data/students.csv.
    /// </summary>
    public class StudentData
    {
        [LoadColumn(0)] public float MathMarks     { get; set; }
        [LoadColumn(1)] public float EnglishMarks  { get; set; }
        [LoadColumn(2)] public float ScienceMarks  { get; set; }
        [LoadColumn(3)] public float UrduMarks     { get; set; }
        [LoadColumn(4)] public float IslamiatMarks { get; set; }

        /// <summary>
        /// Ground-truth label: <c>true</c> = Pass, <c>false</c> = Fail.
        /// A student passes when every subject ≥ 40 marks.
        /// </summary>
        [LoadColumn(5)]
        [ColumnName("Label")]
        public bool Result { get; set; }
    }

    /// <summary>
    /// Output produced by the binary-classification model.
    /// </summary>
    public class StudentPrediction
    {
        /// <summary>Predicted class: <c>true</c> = Pass, <c>false</c> = Fail.</summary>
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        /// <summary>Calibrated probability of the positive (Pass) class.</summary>
        public float Probability { get; set; }

        /// <summary>Raw model score before sigmoid calibration.</summary>
        public float Score { get; set; }

        /// <summary>Human-readable verdict.</summary>
        public string Verdict        => Prediction ? "✅ PASS" : "❌ FAIL";
        public string ConfidenceText => $"{Probability * 100:F1}% confidence";
    }
}
