// ================================================================
//  SmartStudentSystem – Forms/AllStudentsForm.cs
//  Q#1(v) – Second form: displays all student records passed from MainForm
// ================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartStudentSystem.Models;

namespace SmartStudentSystem.Forms
{
    /// <summary>
    /// Secondary form that receives a list of <see cref="Student"/> objects
    /// from <see cref="MainForm"/> and presents them in a styled DataGridView.
    /// Demonstrates multi-form interaction and data passing (Q#1-v).
    /// </summary>
    public partial class AllStudentsForm : Form
    {
        private readonly List<Student> _students;

        // ── Constructor ──────────────────────────────────────────────────────

        public AllStudentsForm(List<Student> students)
        {
            _students = students ?? throw new ArgumentNullException(nameof(students));
            InitializeComponent();
            PopulateGrid();
        }

        // ── Data binding ─────────────────────────────────────────────────────

        private void PopulateGrid()
        {
            dgvStudents.Rows.Clear();

            foreach (var s in _students)
            {
                int row = dgvStudents.Rows.Add(
                    s.Id,
                    s.Name,
                    s.Age,
                    $"{s.MathMarks:F0}",
                    $"{s.EnglishMarks:F0}",
                    $"{s.ScienceMarks:F0}",
                    $"{s.UrduMarks:F0}",
                    $"{s.IslamiatMarks:F0}",
                    $"{s.Average:F1}%",
                    s.Grade,
                    s.ResultLabel,
                    s.MLPrediction);

                // Colour-code row by result
                var rowObj = dgvStudents.Rows[row];
                if (s.IsPassing)
                {
                    rowObj.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                    rowObj.DefaultCellStyle.ForeColor = Color.DarkGreen;
                }
                else
                {
                    rowObj.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    rowObj.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
            }

            lblTotal.Text =
                $"Total: {_students.Count}  |  " +
                $"Pass: {_students.FindAll(s => s.IsPassing).Count}  |  " +
                $"Fail: {_students.FindAll(s => !s.IsPassing).Count}";
        }

        // ── Event handlers ───────────────────────────────────────────────────

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnExport_Click(object sender, EventArgs e)
        {
            using var dlg = new SaveFileDialog
            {
                Filter   = "CSV Files|*.csv",
                FileName = "students_export.csv"
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var sw = new System.IO.StreamWriter(dlg.FileName);
                sw.WriteLine("ID,Name,Age,Math,English,Science,Urdu,Islamiat,Average,Grade,Result,ML Prediction");

                foreach (var s in _students)
                    sw.WriteLine(
                        $"{s.Id},{s.Name},{s.Age}," +
                        $"{s.MathMarks},{s.EnglishMarks},{s.ScienceMarks}," +
                        $"{s.UrduMarks},{s.IslamiatMarks}," +
                        $"{s.Average:F2},{s.Grade},{s.ResultLabel},{s.MLPrediction}");

                MessageBox.Show("Export successful!", "Exported",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
