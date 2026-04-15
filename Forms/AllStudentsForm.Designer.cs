// ================================================================
//  SmartStudentSystem – Forms/AllStudentsForm.Designer.cs
//  Q#1(v) – Designer code for the All Students record form
// ================================================================

using System.Drawing;
using System.Windows.Forms;

namespace SmartStudentSystem.Forms
{
    partial class AllStudentsForm
    {
        private System.ComponentModel.IContainer? components = null;

        // ── Controls ─────────────────────────────────────────────────────────
        private Panel              pnlHeader    = null!;
        private Label              lblTitle     = null!;
        private Label              lblTotal     = null!;
        private DataGridView       dgvStudents  = null!;
        private Panel              pnlFooter    = null!;
        private Button             btnExport    = null!;
        private Button             btnClose     = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Form ─────────────────────────────────────────────────────────
            Text            = "📋 All Student Records — Smart Result Prediction System";
            Size            = new Size(1100, 620);
            StartPosition   = FormStartPosition.CenterParent;
            MinimumSize     = new Size(900, 500);
            BackColor       = Color.FromArgb(245, 248, 252);
            Font            = new Font("Segoe UI", 9f);

            // ── Header panel ─────────────────────────────────────────────────
            pnlHeader = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 60,
                BackColor = Color.FromArgb(33, 97, 140),
                Padding   = new Padding(15, 0, 15, 0)
            };

            lblTitle = new Label
            {
                Text      = "📋  All Student Records",
                Font      = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize  = false,
                Dock      = DockStyle.Left,
                TextAlign = ContentAlignment.MiddleLeft,
                Width     = 400
            };

            lblTotal = new Label
            {
                Text      = "Loading…",
                Font      = new Font("Segoe UI", 10f),
                ForeColor = Color.LightCyan,
                AutoSize  = false,
                Dock      = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                Width     = 400
            };

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblTotal });

            // ── DataGridView ─────────────────────────────────────────────────
            dgvStudents = new DataGridView
            {
                Dock                  = DockStyle.Fill,
                ReadOnly              = true,
                AllowUserToAddRows    = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor       = Color.White,
                BorderStyle           = BorderStyle.None,
                RowHeadersVisible     = false,
                GridColor             = Color.FromArgb(220, 230, 241),
                EnableHeadersVisualStyles = false
            };

            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(41, 128, 185);
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor  = Color.White;
            dgvStudents.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvStudents.ColumnHeadersHeight                      = 36;
            dgvStudents.RowTemplate.Height                       = 28;
            dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);

            // Add columns
            string[] cols = { "ID", "Name", "Age", "Math", "English", "Science", "Urdu", "Islamiat", "Average", "Grade", "Result", "ML Pred." };
            foreach (var col in cols)
            {
                dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = col,
                    SortMode   = DataGridViewColumnSortMode.Automatic
                });
            }

            // ── Footer panel ─────────────────────────────────────────────────
            pnlFooter = new Panel
            {
                Dock      = DockStyle.Bottom,
                Height    = 55,
                BackColor = Color.FromArgb(236, 240, 245),
                Padding   = new Padding(15, 10, 15, 10)
            };

            btnExport = new Button
            {
                Text      = "⬇  Export CSV",
                Size      = new Size(130, 35),
                Location  = new Point(15, 10),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor    = Cursors.Hand
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += btnExport_Click;

            btnClose = new Button
            {
                Text      = "✖  Close",
                Size      = new Size(110, 35),
                Location  = new Point(160, 10),
                BackColor = Color.FromArgb(192, 57, 43),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor    = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += btnClose_Click;

            pnlFooter.Controls.AddRange(new Control[] { btnExport, btnClose });

            // ── Assemble ─────────────────────────────────────────────────────
            Controls.AddRange(new Control[] { dgvStudents, pnlHeader, pnlFooter });
        }
    }
}
