// ================================================================
//  SmartStudentSystem – Models/Person.cs
//  Q#1(i) – Base class with constructor + at least one method
// ================================================================

namespace SmartStudentSystem.Models
{
    /// <summary>
    /// Abstract base class representing any person in the system.
    /// Demonstrates OOP foundation: constructor, encapsulation, abstraction.
    /// </summary>
    public abstract class Person
    {
        // ── Private backing fields (Encapsulation) ───────────────────────────
        private string _name  = string.Empty;
        private int    _age;
        private string _id    = string.Empty;

        // ── Properties (Q#1-ii: Encapsulation via properties) ────────────────

        /// <summary>Full name of the person.</summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value.Trim();
            }
        }

        /// <summary>Age – must be a positive integer.</summary>
        public int Age
        {
            get => _age;
            set
            {
                if (value < 1 || value > 120)
                    throw new ArgumentOutOfRangeException(nameof(Age),
                        "Age must be between 1 and 120.");
                _age = value;
            }
        }

        /// <summary>Unique identifier (e.g., student roll number).</summary>
        public string Id
        {
            get => _id;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ID cannot be empty.");
                _id = value.Trim().ToUpper();
            }
        }

        // ── Constructor ──────────────────────────────────────────────────────

        /// <summary>
        /// Initialises a Person with mandatory identity fields.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <param name="name">Full name.</param>
        /// <param name="age">Age in years.</param>
        protected Person(string id, string name, int age)
        {
            Id   = id;
            Name = name;
            Age  = age;
        }

        // ── Abstract method (Polymorphism hook) ──────────────────────────────

        /// <summary>
        /// Returns a description of this person's role.
        /// Each subclass must override to supply its own definition.
        /// </summary>
        public abstract string GetRole();

        /// <summary>
        /// Core calculation method required by Q#1(i).
        /// Computes a numeric result relevant to the person type.
        /// </summary>
        public abstract double CalculateResult();

        // ── Virtual methods ──────────────────────────────────────────────────

        /// <summary>Returns a formatted summary string.</summary>
        public virtual string GetInfo()
            => $"[{GetRole()}] ID: {Id} | Name: {Name} | Age: {Age}";

        /// <summary>String representation for list-box / console display.</summary>
        public override string ToString() => GetInfo();
    }
}
