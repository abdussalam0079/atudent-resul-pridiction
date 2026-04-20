# 🎓 Smart Student Result Prediction System

**Department of Computer Science**  
CLO-01 / CLO-02 | PLO-02 / PLO-03 | P3 / P4

---
## 📋 Project Overview

A complete intelligent, deployable application built in **C# .NET 8** that:

| Feature | Technology |
|---|---|
| OOP class hierarchy | C# – `Person → Student → GraduateStudent` |
| Console prototype | .NET Console – Q#1(i) |
| Windows Forms UI | WinForms – Q#1(iii), (iv), (v) |
| ML Pass/Fail prediction | **ML.NET** – FastTree Binary Classifier |
| Containerisation | **Docker** – multi-stage Windows container |

---
## 🗂️ Project Structure

```
SmartStudentSystem/
├── SmartStudentSystem.csproj       ← .NET 8 WinForms + ML.NET packages
├── Program.cs                      ← Entry point (console demo → UI)
│
├── Models/
│   ├── Person.cs                   ← Q#1(i)(ii) – Abstract base class
│   ├── Student.cs                  ← Q#1(ii) – Encapsulation + Inheritance
│   ├── GraduateStudent.cs          ← Q#1(ii) – Polymorphism override
│   └── StudentData.cs              ← Q#2(i)  – ML feature + label model
│
├── ML/
│   └── ModelTrainer.cs             ← Q#2(ii) – FastTree binary classifier
│
├── Console/
│   └── ConsoleDemo.cs              ← Q#1(i)  – Console prototype
│
├── Forms/
│   ├── MainForm.cs                 ← Q#1(iii)(iv)(v) + Q#2(iii) – Main UI
│   ├── MainForm.Designer.cs        ← UI layout (Labels, TextBoxes, Buttons)
│   ├── AllStudentsForm.cs          ← Q#1(v) – Second form, all records
│   └── AllStudentsForm.Designer.cs ← Second form layout + DataGridView
│
├── Data/
│   └── students.csv                ← Training data (marks + Pass/Fail labels)
│
├── Dockerfile                      ← Q#2(iv) – Multi-stage Windows container
├── docker-compose.yml              ← Q#2(v)  – Deployment orchestration
└── README.md                       ← This file
```

---

## ✅ Requirements Coverage

### Q#1 – Smart Student Result Prediction (OOP + WinForms)

| # | Requirement | Implementation |
|---|---|---|
| i | Console prototype – `Person` class with constructor + method | `ConsoleDemo.cs` → `Person.cs` |
| ii | OOP: Encapsulation (properties) + Inheritance / Polymorphism | `Student.cs`, `GraduateStudent.cs` |
| iii | Windows Forms – Labels, TextBoxes, ≥ 2 Buttons | `MainForm.Designer.cs` |
| iv | Events: Add student, Display result | `btnAdd_Click`, `btnShowResult_Click` |
| v | Multi-form: second form for all records | `AllStudentsForm.cs` |

### Q#2 – ML.NET + Docker Deployment

| # | Requirement | Implementation |
|---|---|---|
| i | `StudentData` / `StudentPrediction` model class | `Models/StudentData.cs` |
| ii | ML.NET FastTree binary classification | `ML/ModelTrainer.cs` |
| iii | WinForms prediction display via Label/TextBox | `MainForm.cs → ShowResult()` |
| iv | Dockerfile – base image, build steps, run command | `Dockerfile` |
| v | Docker build, run, verify | See commands below |

---

## 🚀 How to Build & Run

### Option A – Run locally (without Docker)

```bash
# Restore packages
dotnet restore

# Build
dotnet build -c Release

# Run
dotnet run
```

### Option B – Docker (Q#2-v)

> **Prerequisite:** Docker Desktop in Windows container mode.

```bash
# 1. Build the Docker image
docker build -t smartstudentsystem:1.0 .

# 2. Run the container
docker run --isolation=process smartstudentsystem:1.0

# 3. Verify application execution (check container logs)
docker logs smart_student_system

# Using docker-compose (recommended)
docker-compose up --build        # build + start
docker-compose ps                # verify running
docker-compose down              # stop
```

---

## 🤖 ML.NET Model Details

| Parameter | Value |
|---|---|
| Algorithm | `FastTreeBinaryTrainer` |
| Features | Math, English, Science, Urdu, Islamiat (5 floats) |
| Label | Pass = `true` / Fail = `false` |
| Training samples | 1 200 (synthetic) or from `Data/students.csv` |
| Test split | 20 % |
| Typical Accuracy | ~97% |
| Pass threshold | 40 marks per subject |

---

## 🏗️ OOP Concepts

```
Person  (abstract)
│  + Id, Name, Age  [Encapsulation via properties]
│  + GetRole()      [abstract – Polymorphism]
│  + CalculateResult() [abstract – Polymorphism]
│
├── Student  (Inheritance)
│       + MathMarks … IslamiatMarks  [Encapsulation]
│       + Average, Grade, IsPassing  [computed properties]
│       + CalculateResult() → simple average
│
└── GraduateStudent  (Polymorphism override)
        + ThesisMarks, Supervisor
        + CalculateResult() → weighted avg (70% course + 30% thesis)
        + PassThreshold raised to 50
```

---

## 📝 License

Academic project – Department of Computer Science.
