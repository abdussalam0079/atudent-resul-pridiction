// ================================================================
//  SmartStudentSystem – Models/GraduateStudent.cs
//  Q#1(ii) – Polymorphism: GraduateStudent overrides key behaviours
// ================================================================

namespace SmartStudentSystem.Models
{
    /// <summary>
    /// Represents a postgraduate / master's student.
    /// Demonstrates Polymorphism by overriding <see cref="Person.GetRole"/>,
    /// <see cref="Person.CalculateResult"/>, and <see cref="Person.GetInfo"/>.
    /// Graduate students have a higher pass threshold and a thesis component.
    /// </summary>
    public class GraduateStudent : Student
    {
        // ── Graduate-specific constants ──────────────────────────────────────
        public new const float PassThreshold = 50f; // stricter for postgrads

        // ── Graduate-specific properties (Encapsulation) ─────────────────────

        /// <summary>Thesis / dissertation marks (0-100).</summary>
        public float ThesisMarks
        {
            get => _thesisMarks;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(ThesisMarks),
                        "Thesis marks must be between 0 and 100.");
                _thesisMarks = value;
            }
        }
        private float _thesisMarks;

        /// <summary>Research supervisor's name.</summary>
        public string Supervisor { get; set; }

        /// <summary>Graduate-level GPA on a 4.0 scale.</summary>
        public double GPA => CalculateResult() / 25.0; // 100 → 4.0

        // ── Overridden Pass criteria (Polymorphism) ──────────────────────────

        /// <summary>
        /// A graduate student passes when all subjects AND thesis exceed 50.
        /// </summary>
        public new bool IsPassing =>
            MathMarks     >= PassThreshold &&
            EnglishMarks  >= PassThreshold &&
            ScienceMarks  >= PassThreshold &&
            UrduMarks     >= PassThreshold &&
            IslamiatMarks >= PassThreshold &&
            ThesisMarks   >= PassThreshold;

        public new string ResultLabel => IsPassing ? "Pass" : "Fail";

        // ── Constructor ──────────────────────────────────────────────────────

        public GraduateStudent(
            string id,
            string name,
            int    age,
            float  mathMarks,
            float  englishMarks,
            float  scienceMarks,
            float  urduMarks,
            float  islamiatMarks,
            float  thesisMarks,
            string supervisor = "TBA")
            : base(id, name, age,
                   mathMarks, englishMarks, scienceMarks,
                   urduMarks, islamiatMarks)
        {
            ThesisMarks = thesisMarks;
            Supervisor  = supervisor;
        }

        // ── Polymorphic Overrides ────────────────────────────────────────────

        public override string GetRole() => "Graduate Student (MS/MPhil)";

        /// <summary>
        /// Weighted average: coursework 70 % + thesis 30 %.
        /// </summary>
        public override double CalculateResult()
        {
            double courseworkAvg =
                (MathMarks + EnglishMarks + ScienceMarks +
                 UrduMarks + IslamiatMarks) / 5.0;

            return (courseworkAvg * 0.70) + (ThesisMarks * 0.30);
        }

        public override string GetInfo()
            => $"{base.GetInfo()} | Thesis: {ThesisMarks} | GPA: {GPA:F2} | Supervisor: {Supervisor}";

        public override string ToString()
            => $"{Id} – {Name} [Grad] ({ResultLabel})";
    }
}
