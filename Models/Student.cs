// ================================================================
//  SmartStudentSystem – Models/Student.cs
//  Q#1(ii) – Student class: Encapsulation + Inheritance from Person
// ================================================================

namespace SmartStudentSystem.Models
{
    /// <summary>
    /// Represents a regular undergraduate student.
    /// Inherits from <see cref="Person"/> and adds academic marks,
    /// GPA computation, and Pass/Fail determination.
    /// </summary>
    public class Student : Person
    {
        // ── Constants ────────────────────────────────────────────────────────
        public const float PassThreshold   = 40f;   // minimum per-subject mark
        public const float TotalMarks      = 100f;

        // ── Encapsulated properties ──────────────────────────────────────────

        public float MathMarks      { get => _mathMarks;     set => _mathMarks     = Validate(value, nameof(MathMarks));     }
        public float EnglishMarks   { get => _englishMarks;  set => _englishMarks  = Validate(value, nameof(EnglishMarks));  }
        public float ScienceMarks   { get => _scienceMarks;  set => _scienceMarks  = Validate(value, nameof(ScienceMarks));  }
        public float UrduMarks      { get => _urduMarks;     set => _urduMarks     = Validate(value, nameof(UrduMarks));     }
        public float IslamiatMarks  { get => _islamiatMarks; set => _islamiatMarks = Validate(value, nameof(IslamiatMarks)); }

        // ── Private backing fields ───────────────────────────────────────────
        private float _mathMarks;
        private float _englishMarks;
        private float _scienceMarks;
        private float _urduMarks;
        private float _islamiatMarks;

        // ── Computed read-only properties ────────────────────────────────────

        /// <summary>Average percentage across all five subjects.</summary>
        public double Average => CalculateResult();

        /// <summary>True when the student passes all subjects.</summary>
        public bool IsPassing =>
            MathMarks     >= PassThreshold &&
            EnglishMarks  >= PassThreshold &&
            ScienceMarks  >= PassThreshold &&
            UrduMarks     >= PassThreshold &&
            IslamiatMarks >= PassThreshold;

        /// <summary>"Pass" or "Fail" string label.</summary>
        public string ResultLabel => IsPassing ? "Pass" : "Fail";

        /// <summary>Letter grade based on average percentage.</summary>
        public string Grade => Average switch
        {
            >= 90 => "A+",
            >= 80 => "A",
            >= 70 => "B",
            >= 60 => "C",
            >= 50 => "D",
            >= 40 => "E",
            _      => "F"
        };

        // ── ML prediction fields (populated at runtime) ──────────────────────
        public string MLPrediction  { get; set; } = "N/A";
        public float  MLProbability { get; set; }

        // ── Constructor ──────────────────────────────────────────────────────

        /// <summary>
        /// Full constructor – sets all identity and academic fields.
        /// </summary>
        public Student(
            string id,
            string name,
            int    age,
            float  mathMarks,
            float  englishMarks,
            float  scienceMarks,
            float  urduMarks,
            float  islamiatMarks)
            : base(id, name, age)
        {
            MathMarks     = mathMarks;
            EnglishMarks  = englishMarks;
            ScienceMarks  = scienceMarks;
            UrduMarks     = urduMarks;
            IslamiatMarks = islamiatMarks;
        }

        // ── Overrides ────────────────────────────────────────────────────────

        public override string GetRole() => "Undergraduate Student";

        /// <summary>
        /// Q#1(i) required method: calculates average percentage.
        /// </summary>
        public override double CalculateResult()
            => (MathMarks + EnglishMarks + ScienceMarks +
                UrduMarks + IslamiatMarks) / 5.0;

        public override string GetInfo()
            => $"{base.GetInfo()} | Avg: {Average:F1}% | Grade: {Grade} | Result: {ResultLabel}";

        public override string ToString() => $"{Id} – {Name} ({ResultLabel})";

        // ── Helper ───────────────────────────────────────────────────────────

        /// <summary>Validates that a mark is within [0, 100].</summary>
        private static float Validate(float value, string propName)
        {
            if (value < 0 || value > TotalMarks)
                throw new ArgumentOutOfRangeException(propName,
                    $"{propName} must be between 0 and {TotalMarks}.");
            return value;
        }
    }
}
