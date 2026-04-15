// ================================================================
//  SmartStudentSystem – Forms/MainForm.Designer.cs
//  Professional Windows Forms UI layout
//  Q#1(iii) – Labels, TextBoxes, at least 2 Buttons
//  Q#2(iii) – Prediction shown using Label / TextBox
// ================================================================

using System.Drawing;
using System.Windows.Forms;

namespace SmartStudentSystem.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer? components = null;

        // ── Input panel controls ──────────────────────────────────────────────
        private Panel   pnlHeader    = null!;
        private Label   lblAppTitle  = null!;
        private Label   lblSubtitle  = null!;
        private Panel   pnlInput     = null!;
        private Label   lblInputTitle = null!;

        private Label   lblId        = null!;  private TextBox txtId        = null!;
        private Label   lblName      = null!;  private TextBox txtName      = null!;
        private Label   lblAge       = null!;  private TextBox txtAge       = null!;
        private Label   lblMath      = null!;  private TextBox txtMath      = null!;
        private Label   lblEnglish   = null!;  private TextBox txtEnglish   = null!;
        private Label   lblScience   = null!;  private TextBox txtScience   = null!;
        private Label   lblUrdu      = null!;  private TextBox txtUrdu      = null!;
        private Label   lblIslamiat  = null!;  private TextBox txtIslamiat  = null!;

        private Button  btnAdd       = null!;
        private Button  btnClear     = null!;
        private Button  btnViewAll   = null!;
        private Button  btnShowResult= null!;
        private Button  btnRetrain   = null!;

        // ── Student list ──────────────────────────────────────────────────────
        private Panel   pnlList      = null!;
        private Label   lblListTitle = null!;
        private ListBox lstStudents  = null!;

        // ── Result panel controls ─────────────────────────────────────────────
        private Panel   pnlResult       = null!;
        private Label   lblResultTitle  = null!;
        private Label   lblResultId     = null!;
        private Label   lblResultName   = null!;

        private Label   lblRMath        = null!;  private TextBox txtRMath       = null!;
        private Label   lblREnglish     = null!;  private TextBox txtREnglish    = null!;
        private Label   lblRScience     = null!;  private TextBox txtRScience    = null!;
        private Label   lblRUrdu        = null!;  private TextBox txtRUrdu       = null!;
        private Label   lblRIslamiat    = null!;  private TextBox txtRIslamiat   = null!;
        private Label   lblAvgH         = null!;  private TextBox txtAverage     = null!;
        private Label   lblGradeH       = null!;  private TextBox txtGrade       = null!;

        private Label   lblRuleH        = null!;
        private Label   lblRuleResult   = null!;   // Q#1(iv) result label
        private Label   lblMLH          = null!;
        private Label   lblMLResult     = null!;   // Q#2(iii) ML label
        private Label   lblConfH        = null!;  private TextBox txtMLConfidence = null!;
        private Label   lblScoreH       = null!;  private TextBox txtMLScore      = null!;

        // ── Status strip ─────────────────────────────────────────────────────
        private StatusStrip  statusStrip = null!;
        private ToolStripStatusLabel ssLabel = null!;

        // ── Metrics label ─────────────────────────────────────────────────────
        private Label lblMetrics = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Form ─────────────────────────────────────────────────────────
            Text          = "🎓 Smart Student Result Prediction System";
            Size          = new Size(1280, 780);
            MinimumSize   = new Size(1100, 700);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor     = Color.FromArgb(245, 248, 252);
            Font          = new Font("Segoe UI", 9f);

            // ══════════════════════════════════════════════════════════════════
            //  HEADER
            // ══════════════════════════════════════════════════════════════════
            pnlHeader = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 70,
                BackColor = Color.FromArgb(22, 88, 143),
                Padding   = new Padding(20, 5, 20, 5)
            };

            lblAppTitle = new Label
            {
                Text      = "🎓  Smart Student Result Prediction System",
                Font      = new Font("Segoe UI", 17f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize  = true,
                Location  = new Point(20, 8)
            };

            lblSubtitle = new Label
            {
                Text      = "Department of Computer Science  |  ML.NET Powered  |  Windows Forms",
                Font      = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(189, 215, 238),
                AutoSize  = true,
                Location  = new Point(23, 44)
            };

            pnlHeader.Controls.AddRange(new Control[] { lblAppTitle, lblSubtitle });

            // ══════════════════════════════════════════════════════════════════
            //  LEFT – Input Panel
            // ══════════════════════════════════════════════════════════════════
            pnlInput = new Panel
            {
                Width     = 340,
                Dock      = DockStyle.Left,
                BackColor = Color.White,
                Padding   = new Padding(18, 15, 18, 10)
            };

            lblInputTitle = MakeSectionLabel("➕  Student Data Entry", new Point(18, 12));

            int y = 48;
            (lblId,       txtId)       = MakeRow("Student ID *",   new Point(18, y)); y += 54;
            (lblName,     txtName)     = MakeRow("Full Name *",    new Point(18, y)); y += 54;
            (lblAge,      txtAge)      = MakeRow("Age *",          new Point(18, y)); y += 54;

            var lblMarks = new Label
            {
                Text     = "Subject Marks  (0 – 100)",
                Font     = new Font("Segoe UI", 8.5f, FontStyle.Bold | FontStyle.Italic),
                ForeColor= Color.FromArgb(100, 100, 120),
                AutoSize = true,
                Location = new Point(18, y)
            };
            y += 24;

            (lblMath,     txtMath)     = MakeRow("Mathematics *",  new Point(18, y)); y += 50;
            (lblEnglish,  txtEnglish)  = MakeRow("English *",      new Point(18, y)); y += 50;
            (lblScience,  txtScience)  = MakeRow("Science *",      new Point(18, y)); y += 50;
            (lblUrdu,     txtUrdu)     = MakeRow("Urdu *",         new Point(18, y)); y += 50;
            (lblIslamiat, txtIslamiat) = MakeRow("Islamiat *",     new Point(18, y)); y += 60;

            // Buttons – Q#1(iii): at least two buttons
            btnAdd = MakeButton("✅  Add Student",   new Point(18, y), Color.FromArgb(39, 174, 96));
            btnAdd.Click += btnAdd_Click;
            y += 44;

            btnClear = MakeButton("🗑  Clear Form",   new Point(18, y), Color.FromArgb(127, 140, 141));
            btnClear.Click += btnClear_Click;
            y += 44;

            btnShowResult = MakeButton("🔍  Show Selected", new Point(18, y), Color.FromArgb(41, 128, 185));
            btnShowResult.Click += btnShowResult_Click;
            y += 44;

            btnViewAll = MakeButton("📋  View All Records", new Point(18, y), Color.FromArgb(142, 68, 173));
            btnViewAll.Click += btnViewAll_Click;
            y += 44;

            btnRetrain = MakeButton("🔄  Retrain Model", new Point(18, y), Color.FromArgb(211, 84, 0));
            btnRetrain.Click += btnRetrain_Click;

            pnlInput.Controls.AddRange(new Control[]
            {
                lblInputTitle, lblMarks,
                lblId, txtId, lblName, txtName, lblAge, txtAge,
                lblMath, txtMath, lblEnglish, txtEnglish,
                lblScience, txtScience, lblUrdu, txtUrdu,
                lblIslamiat, txtIslamiat,
                btnAdd, btnClear, btnShowResult, btnViewAll, btnRetrain
            });

            // ══════════════════════════════════════════════════════════════════
            //  MIDDLE – Student List
            // ══════════════════════════════════════════════════════════════════
            pnlList = new Panel
            {
                Width     = 260,
                Dock      = DockStyle.Left,
                BackColor = Color.FromArgb(250, 252, 255),
                Padding   = new Padding(10, 15, 10, 10)
            };

            lblListTitle = MakeSectionLabel("👥  Students Added", new Point(10, 12));

            lstStudents = new ListBox
            {
                Location     = new Point(10, 45),
                Size         = new Size(238, 580),
                Font         = new Font("Segoe UI", 9f),
                BorderStyle  = BorderStyle.FixedSingle,
                BackColor    = Color.White,
                SelectionMode= SelectionMode.One,
                IntegralHeight = false
            };
            lstStudents.SelectedIndexChanged += (s, e) =>
            {
                btnShowResult.Enabled = lstStudents.SelectedIndex >= 0;
            };

            pnlList.Controls.AddRange(new Control[] { lblListTitle, lstStudents });

            // ══════════════════════════════════════════════════════════════════
            //  RIGHT – Result Panel  (Q#1-iv display + Q#2-iii ML prediction)
            // ══════════════════════════════════════════════════════════════════
            pnlResult = new Panel
            {
                Dock      = DockStyle.Fill,
                BackColor = Color.White,
                Padding   = new Padding(20, 15, 20, 15)
            };

            lblResultTitle = MakeSectionLabel("📊  Prediction Results", new Point(20, 12));

            lblResultId   = MakeValueLabel("—", new Point(20, 48), 14f, FontStyle.Bold, Color.FromArgb(22, 88, 143));
            lblResultName = MakeValueLabel("Select or add a student", new Point(20, 70), 11f, FontStyle.Regular, Color.Gray);

            int ry = 105;
            (lblRMath,    txtRMath)    = MakeReadonlyRow("Mathematics",  new Point(20, ry)); ry += 40;
            (lblREnglish, txtREnglish) = MakeReadonlyRow("English",      new Point(20, ry)); ry += 40;
            (lblRScience, txtRScience) = MakeReadonlyRow("Science",      new Point(20, ry)); ry += 40;
            (lblRUrdu,    txtRUrdu)    = MakeReadonlyRow("Urdu",         new Point(20, ry)); ry += 40;
            (lblRIslamiat,txtRIslamiat)= MakeReadonlyRow("Islamiat",     new Point(20, ry)); ry += 52;

            var sep = new Label
            {
                Location  = new Point(20, ry),
                Size      = new Size(600, 1),
                BackColor = Color.FromArgb(210, 220, 230)
            };
            ry += 12;

            (lblAvgH,   txtAverage) = MakeReadonlyRow("Average %",  new Point(20, ry)); ry += 40;
            (lblGradeH, txtGrade)   = MakeReadonlyRow("Grade",       new Point(20, ry)); ry += 52;

            // Rule-based result
            lblRuleH = MakeSectionLabel("📐  Rule-Based Result", new Point(20, ry)); ry += 35;
            lblRuleResult = MakeValueLabel("—", new Point(20, ry), 22f, FontStyle.Bold, Color.Gray);
            ry += 55;

            // ML Prediction  (Q#2-iii – Prediction must be shown using Label/TextBox)
            lblMLH = MakeSectionLabel("🤖  ML.NET Prediction  (FastTree Binary Classification)", new Point(20, ry)); ry += 35;
            lblMLResult = MakeValueLabel("—", new Point(20, ry), 22f, FontStyle.Bold, Color.Gray);
            ry += 52;

            (lblConfH,  txtMLConfidence) = MakeReadonlyRow("Confidence", new Point(20, ry)); ry += 40;
            (lblScoreH, txtMLScore)      = MakeReadonlyRow("Raw Score",  new Point(20, ry)); ry += 50;

            lblMetrics = new Label
            {
                Location  = new Point(20, ry),
                AutoSize  = true,
                Font      = new Font("Segoe UI", 8.5f, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 120, 140),
                Text      = "Model metrics will appear here after training…"
            };

            pnlResult.Controls.AddRange(new Control[]
            {
                lblResultTitle, lblResultId, lblResultName, sep,
                lblRMath, txtRMath, lblREnglish, txtREnglish,
                lblRScience, txtRScience, lblRUrdu, txtRUrdu,
                lblRIslamiat, txtRIslamiat,
                lblAvgH, txtAverage, lblGradeH, txtGrade,
                lblRuleH, lblRuleResult,
                lblMLH, lblMLResult,
                lblConfH, txtMLConfidence, lblScoreH, txtMLScore,
                lblMetrics
            });

            // ══════════════════════════════════════════════════════════════════
            //  STATUS BAR
            // ══════════════════════════════════════════════════════════════════
            statusStrip = new StatusStrip { BackColor = Color.FromArgb(41, 128, 185) };
            ssLabel     = new ToolStripStatusLabel
            {
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 8.5f),
                Text      = "Initialising…"
            };
            statusStrip.Items.Add(ssLabel);

            // ── Assemble form ─────────────────────────────────────────────────
            Controls.AddRange(new Control[]
            {
                pnlResult, pnlList, pnlInput, pnlHeader, statusStrip
            });
        }

        // ── UI factory helpers ────────────────────────────────────────────────

        private static Label MakeSectionLabel(string text, Point loc) => new Label
        {
            Text      = text,
            Location  = loc,
            AutoSize  = true,
            Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = Color.FromArgb(44, 62, 80)
        };

        private static Label MakeValueLabel(string text, Point loc,
            float size, FontStyle style, Color color) => new Label
        {
            Text      = text,
            Location  = loc,
            AutoSize  = true,
            Font      = new Font("Segoe UI", size, style),
            ForeColor = color
        };

        private static (Label, TextBox) MakeRow(string caption, Point loc)
        {
            var lbl = new Label
            {
                Text      = caption,
                Location  = loc,
                AutoSize  = true,
                Font      = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(60, 80, 100)
            };
            var txt = new TextBox
            {
                Location  = new Point(loc.X, loc.Y + 18),
                Width     = 290,
                Font      = new Font("Segoe UI", 10f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(252, 253, 255)
            };
            return (lbl, txt);
        }

        private static (Label, TextBox) MakeReadonlyRow(string caption, Point loc)
        {
            var lbl = new Label
            {
                Text      = caption,
                Location  = loc,
                Size      = new Size(130, 20),
                Font      = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(80, 100, 120),
                TextAlign = ContentAlignment.MiddleLeft
            };
            var txt = new TextBox
            {
                Location  = new Point(loc.X + 135, loc.Y),
                Width     = 200,
                Font      = new Font("Segoe UI", 9.5f),
                ReadOnly  = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(245, 248, 252),
                ForeColor = Color.FromArgb(30, 50, 80)
            };
            return (lbl, txt);
        }

        private static Button MakeButton(string text, Point loc, Color bg)
        {
            var btn = new Button
            {
                Text      = text,
                Location  = loc,
                Size      = new Size(295, 36),
                Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}
