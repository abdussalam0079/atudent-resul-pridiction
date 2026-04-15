# ================================================================
#  SmartStudentSystem – Dockerfile
#  Q#2(iv) – Multi-stage Docker build for .NET 8 Windows Forms App
#
#  ╔════════════════════════════════════════════════════════════╗
#  ║  NOTE: Windows Forms applications require a Windows        ║
#  ║  container host. Use:                                      ║
#  ║    docker build -t smartstudentsystem .                    ║
#  ║    docker run --isolation=process smartstudentsystem       ║
#  ╚════════════════════════════════════════════════════════════╝
# ================================================================

# ── Stage 1: Build ───────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build

# Set working directory inside the container
WORKDIR /src

# Copy project file first (layer caching – NuGet restore only reruns on .csproj change)
COPY SmartStudentSystem.csproj ./
RUN dotnet restore SmartStudentSystem.csproj

# Copy remaining source files
COPY . .

# Publish a self-contained, trimmed release build
RUN dotnet publish SmartStudentSystem.csproj \
    --configuration Release \
    --runtime win-x64 \
    --self-contained true \
    --output /app/publish \
    -p:PublishSingleFile=false \
    -p:PublishReadyToRun=true

# ── Stage 2: Runtime ─────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/runtime:8.0-windowsservercore-ltsc2022 AS runtime

# Labels (OCI standard)
LABEL maintainer="Department of Computer Science"
LABEL description="Smart Student Result Prediction System – ML.NET + WinForms"
LABEL version="1.0.0"

WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Ensure the Data directory exists (training CSV)
RUN mkdir Data 2>NUL | exit 0
COPY --from=build /src/Data/students.csv Data/students.csv

# Environment variable for display (RDP / VNC)
ENV DISPLAY=:0

# ── Run command ──────────────────────────────────────────────────
ENTRYPOINT ["SmartStudentSystem.exe"]
