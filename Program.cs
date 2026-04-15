// ================================================================
//  SmartStudentSystem – Program.cs
//  Application entry point: console demo → WinForms UI
// ================================================================

using System;
using System.Threading;
using System.Windows.Forms;
using SmartStudentSystem.Console;
using SmartStudentSystem.Forms;
using SmartStudentSystem.ML;

namespace SmartStudentSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // ── Q#1(i) – Run console prototype first ─────────────────────────
            ConsoleDemo.Run();

            // ── Q#2(ii) – Initialise ML.NET model ───────────────────────────
            var trainer = new ModelTrainer();

            System.Console.WriteLine("\nInitialising ML.NET model (this may take a moment)…");
            try
            {
                trainer.Initialise();
                System.Console.WriteLine($"Model ready.  {trainer.MetricsSummary()}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[WARNING] ML model failed: {ex.Message}");
                System.Console.WriteLine("The application will continue without ML predictions.");
            }

            System.Console.WriteLine("\nLaunching Windows Forms UI…");
            Thread.Sleep(800); // brief pause so console output is visible

            // ── Q#1(iii) – Launch Windows Forms application ──────────────────
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(trainer));
        }
    }
}
