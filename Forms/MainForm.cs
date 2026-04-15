// ================================================================
//  SmartStudentSystem – Forms/MainForm.cs
//  Q#1(iii)(iv)(v) – Windows Forms UI + Event handling + Multi-form
//  Q#2(iii)        – ML.NET prediction displayed via Label/TextBox
// ================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartStudentSystem.ML;
using SmartStudentSystem.Models;

namespace SmartStudentSystem.Forms
{
    /// <summary>
    /// Main application window.
    /// Handles: student data entry, rule-based result calculation,
    /// ML.NET Pass/Fail prediction, and navigation to AllStudentsForm.
    /// </summary>
    public partial class MainForm : Form
    {
        // ── Fields ───────────────────────────────────────────────────────────
        private readonly ModelTrainer      _trainer;
        private readonly List<Student>     _students = new();

        // ── Constructor ──────────────────────────────────────────────────────
        public MainForm(ModelTrainer trainer)
        {
            _trainer = trainer;
            InitializeComponent();
            UpdateStatusBar();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  Q#1(iv) – Event: Add a Student
        // ─────────────────────────────────────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryReadInputs(out var student)) return;

            // ML prediction
            var mlInput = new StudentData
            {
                MathMarks     = student.MathMarks,
                EnglishMarks  = student.EnglishMarks,
                ScienceMarks  = student.ScienceMarks,
                UrduMarks     = student.UrduMarks,
                IslamiatMarks = student.IslamiatMarks
            };
            var pred = _trainer.Predict(mlInput);
            student.MLPrediction  = pred.Prediction ? "Pass" : "Fail";
            student.MLProbability = pred.Probability;

            // Save
            _students.Add(student);

            // Update list
            lstStudents.Items.Add(student.ToString());

            // Show results in result panel (Q#1-iv)
            ShowResult(student, pred);

            // Clear form
            ClearInputs();
            UpdateStatusBar();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  Q#1(iv) – Event: Display Student Result (from list selection)
        // ─────────────────────────────────────────────────────────────────────
        private void btnShowResult_Click(object sender, EventArgs e)
        {
            if (lstStudents.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a student from the list.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var s    = _students[lstStudents.SelectedIndex];
            var pred = _trainer.Predict(new StudentData
            {
                MathMarks     = s.MathMarks,
                EnglishMarks  = s.EnglishMarks,
                ScienceMarks  = s.ScienceMarks,
                UrduMarks     = s.UrduMarks,
                IslamiatMarks = s.IslamiatMarks
            });

            ShowResult(s, pred);
        }

        // ─────────────────────────────────────────────────────────────────────
        //  Q#1(v) – Open All Students Form (multi-form interaction)
        // ─────────────────────────────────────────────────────────────────────
        private void btnViewAll_Click(object sender, EventArgs e)
        {
            if (_students.Count == 0)
            {
                MessageBox.Show("No students added yet. Please add at least one student.",
                    "Empty List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Pass data from this form to the child form (Q#1-v)
            using var allForm = new AllStudentsForm(_students);
            allForm.ShowDialog(this);
        }

        // ─────────────────────────────────────────────────────────────────────
        //  Clear button
        // ─────────────────────────────────────────────────────────────────────
        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        // ─────────────────────────────────────────────────────────────────────
        //  Retrain ML model
        // ─────────────────────────────────────────────────────────────────────
        private void btnRetrain_Click(object sender, EventArgs e)
        {
            btnRetrain.Enabled = false;
            btnRetrain.Text    = "Training…";
            Application.DoEvents();

            try
            {
                _trainer.Retrain();
                lblMetrics.Text = _trainer.MetricsSummary();
                MessageBox.Show($"Model retrained successfully!\n\n{_trainer.MetricsSummary()}",
                    "Training Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Training failed:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRetrain.Enabled = true;
                btnRetrain.Text    = "🔄 Retrain Model";
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        //  Helpers
        // ─────────────────────────────────────────────────────────────────────

        private bool TryReadInputs(out Student student)
        {
            student = null!;

            // Validate ID & Name
            if (string.IsNullOrWhiteSpace(txtId.Text))
            { ShowValidationError("Student ID is required.", txtId); return false; }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            { ShowValidationError("Student Name is required.", txtName); return false; }

            if (!int.TryParse(txtAge.Text, out int age) || age < 5 || age > 50)
            { ShowValidationError("Age must be a number between 5 and 50.", txtAge); return false; }

            // Validate marks
            float[] marks = new float[5];
            TextBox[] boxes = { txtMath, txtEnglish, txtScience, txtUrdu, txtIslamiat };
            string[] names  = { "Math", "English", "Science", "Urdu", "Islamiat" };

            for (int i = 0; i < boxes.Length; i++)
            {
                if (!float.TryParse(boxes[i].Text, out float m) || m < 0 || m > 100)
                {
                    ShowValidationError($"{names[i]} marks must be between 0 and 100.", boxes[i]);
                    return false;
                }
                marks[i] = m;
            }

            try
            {
                student = new Student(
                    txtId.Text.Trim(),
                    txtName.Text.Trim(),
                    age,
                    marks[0], marks[1], marks[2], marks[3], marks[4]);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Q#1(iv) + Q#2(iii): Populates all result Labels and TextBoxes.
        /// </summary>
        private void ShowResult(Student s, StudentPrediction pred)
        {
            // Identity
            lblResultId.Text   = s.Id;
            lblResultName.Text = s.Name;

            // Marks
            txtRMath.Text      = s.MathMarks.ToString("F0");
            txtREnglish.Text   = s.EnglishMarks.ToString("F0");
            txtRScience.Text   = s.ScienceMarks.ToString("F0");
            txtRUrdu.Text      = s.UrduMarks.ToString("F0");
            txtRIslamiat.Text  = s.IslamiatMarks.ToString("F0");

            // Computed results
            txtAverage.Text    = $"{s.Average:F2}%";
            txtGrade.Text      = s.Grade;

            // Rule-based result
            lblRuleResult.Text     = s.ResultLabel;
            lblRuleResult.ForeColor = s.IsPassing ? Color.DarkGreen : Color.DarkRed;

            // ML prediction (Q#2-iii – shown via Label/TextBox)
            lblMLResult.Text      = pred.Verdict;
            lblMLResult.ForeColor = pred.Prediction ? Color.DarkGreen : Color.DarkRed;
            txtMLConfidence.Text  = pred.ConfidenceText;
            txtMLScore.Text       = pred.Score.ToString("F4");

            // Panel background
            pnlResult.BackColor = s.IsPassing
                ? Color.FromArgb(235, 255, 235)
                : Color.FromArgb(255, 235, 235);
        }

        private void ClearInputs()
        {
            foreach (Control c in pnlInput.Controls)
                if (c is TextBox tb) tb.Clear();
            txtId.Focus();
        }

        private void UpdateStatusBar()
        {
            ssLabel.Text = $"Students: {_students.Count}  |  " +
                           $"ML Model: {(_trainer.IsReady ? "Ready ✅" : "Not Loaded ❌")}  |  " +
                           $"{_trainer.MetricsSummary()}";
        }

        private static void ShowValidationError(string msg, Control focus)
        {
            MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focus.Focus();
        }
    }
}
