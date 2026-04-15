// ================================================================
//  SmartStudentSystem – Console/ConsoleDemo.cs
//  Q#1(i) – Console-based prototype demonstrating Person / Student
// ================================================================

using System;
using SmartStudentSystem.Models;

namespace SmartStudentSystem.Console
{
    /// <summary>
    /// Static helper that runs a console-based demonstration of the
    /// OOP class hierarchy before the Windows Forms UI starts.
    /// Satisfies Q#1(i): console prototype with constructor +
    /// calculation method, and Q#1(ii): OOP concepts.
    /// </summary>
    public static class ConsoleDemo
    {
        public static void Run()
        {
            PrintBanner();

            // ── Undergraduate student ────────────────────────────────────────
            var ug = new Student(
                id:            "S001",
                name:          "Ali Hassan",
                age:           20,
                mathMarks:     72,
                englishMarks:  65,
                scienceMarks:  80,
                urduMarks:     58,
                islamiatMarks: 90);

            PrintSection("Undergraduate Student – Q#1(i) & (ii)");
            System.Console.WriteLine(ug.GetInfo());
            System.Console.WriteLine($"  Role          : {ug.GetRole()}");
            System.Console.WriteLine($"  Average       : {ug.CalculateResult():F2}%  ← Q#1(i) method");
            System.Console.WriteLine($"  Grade         : {ug.Grade}");
            System.Console.WriteLine($"  Result        : {ug.ResultLabel}");

            // ── Graduate student (Polymorphism) ──────────────────────────────
            var grad = new GraduateStudent(
                id:            "G001",
                name:          "Sara Malik",
                age:           24,
                mathMarks:     78,
                englishMarks:  82,
                scienceMarks:  75,
                urduMarks:     70,
                islamiatMarks: 88,
                thesisMarks:   85,
                supervisor:    "Dr. Khalid");

            PrintSection("Graduate Student – Polymorphism override");
            System.Console.WriteLine(grad.GetInfo());
            System.Console.WriteLine($"  Role          : {grad.GetRole()}");
            System.Console.WriteLine($"  Weighted Avg  : {grad.CalculateResult():F2}%  ← overridden method");
            System.Console.WriteLine($"  GPA           : {grad.GPA:F2} / 4.00");
            System.Console.WriteLine($"  Result        : {grad.ResultLabel}");

            // ── Failing student ──────────────────────────────────────────────
            var fail = new Student(
                id:            "S002",
                name:          "Usman Ahmed",
                age:           19,
                mathMarks:     35,
                englishMarks:  42,
                scienceMarks:  50,
                urduMarks:     30,
                islamiatMarks: 60);

            PrintSection("Failing Student example");
            System.Console.WriteLine(fail.GetInfo());
            System.Console.WriteLine($"  Result        : {fail.ResultLabel}");

            // ── Polymorphism summary ─────────────────────────────────────────
            PrintSection("Polymorphism Demo – Person references");
            Person[] people = { ug, grad, fail };
            foreach (var p in people)
            {
                // Same call – different behaviour per type (polymorphism)
                System.Console.WriteLine(
                    $"  {p.Id,-6} {p.Name,-20} Role: {p.GetRole(),-30} Avg: {p.CalculateResult():F1}%");
            }

            PrintSection("Console demo complete – launching Windows Forms UI…");
        }

        // ── Formatting helpers ────────────────────────────────────────────────

        private static void PrintBanner()
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine("╔══════════════════════════════════════════════════╗");
            System.Console.WriteLine("║   Smart Student Result Prediction System          ║");
            System.Console.WriteLine("║   Q#1 Console Prototype  |  Department of CS     ║");
            System.Console.WriteLine("╚══════════════════════════════════════════════════╝");
            System.Console.ResetColor();
        }

        private static void PrintSection(string title)
        {
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine($"── {title}");
            System.Console.ResetColor();
        }
    }
}
