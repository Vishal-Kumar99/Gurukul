-- =========================
-- Academic Year
-- =========================
CREATE TABLE AcademicYear (
    AcademicYearId INT IDENTITY PRIMARY KEY,
    YearName NVARCHAR(20) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsActive BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- =========================
-- Class
-- =========================
CREATE TABLE Class (
    ClassId INT IDENTITY PRIMARY KEY,
    ClassName NVARCHAR(50) NOT NULL UNIQUE
);

-- =========================
-- Section
-- =========================
CREATE TABLE Section (
    SectionId INT IDENTITY PRIMARY KEY,
    SectionName NVARCHAR(10) NOT NULL,
    ClassId INT NOT NULL,
    FOREIGN KEY (ClassId) REFERENCES Class(ClassId) ON DELETE CASCADE
);

-- =========================
-- Parent
-- =========================
CREATE TABLE Parent (
    ParentId INT IDENTITY PRIMARY KEY,
    FatherName NVARCHAR(100),
    MotherName NVARCHAR(100),
    FatherOccupation NVARCHAR(100),
    MotherOccupation NVARCHAR(100),
    AnnualIncome DECIMAL(12,2),
    MobileNo NVARCHAR(15),
    Email NVARCHAR(100),
    Address NVARCHAR(250)
);

-- =========================
-- Student
-- =========================
CREATE TABLE Student (
    StudentId INT IDENTITY PRIMARY KEY,
    AdmissionNo NVARCHAR(50) UNIQUE,
    FullName NVARCHAR(150) NOT NULL,
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    AadhaarNo NVARCHAR(20),
    Religion NVARCHAR(50),
    CasteCategory NVARCHAR(20),
    BloodGroup NVARCHAR(10),
    Nationality NVARCHAR(50),
    MotherTongue NVARCHAR(50),
    PreviousSchool NVARCHAR(150),
    TCNumber NVARCHAR(50),

    ClassId INT NOT NULL,
    SectionId INT NOT NULL,
    ParentId INT NOT NULL,
    AcademicYearId INT NOT NULL,

    FOREIGN KEY (ClassId) REFERENCES Class(ClassId),
    FOREIGN KEY (SectionId) REFERENCES Section(SectionId),
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId) ON DELETE CASCADE,
    FOREIGN KEY (AcademicYearId) REFERENCES AcademicYear(AcademicYearId)
);

-- =========================
-- Subject
-- =========================
CREATE TABLE Subject (
    SubjectId INT IDENTITY PRIMARY KEY,
    SubjectName NVARCHAR(100) NOT NULL,
    SubjectCode NVARCHAR(20),
    ClassId INT NOT NULL,
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (ClassId) REFERENCES Class(ClassId) ON DELETE CASCADE
);

-- =========================
-- Exam
-- =========================
CREATE TABLE Exam (
    ExamId INT IDENTITY PRIMARY KEY,
    ExamName NVARCHAR(100) NOT NULL,
    AcademicYearId INT NOT NULL,
    FOREIGN KEY (AcademicYearId) REFERENCES AcademicYear(AcademicYearId) ON DELETE CASCADE
);

-- =========================
-- Result
-- =========================
CREATE TABLE Result (
    ResultId INT IDENTITY PRIMARY KEY,
    StudentId INT NOT NULL,
    ExamId INT NOT NULL,
    SubjectId INT NOT NULL,
    MarksObtained DECIMAL(6,2),
    MaxMarks DECIMAL(6,2),

    FOREIGN KEY (StudentId) REFERENCES Student(StudentId) ON DELETE CASCADE,
    FOREIGN KEY (ExamId) REFERENCES Exam(ExamId) ON DELETE CASCADE,
    FOREIGN KEY (SubjectId) REFERENCES Subject(SubjectId)
);

-- =========================
-- FeeStructure
-- =========================
CREATE TABLE FeeStructure (
    FeeStructureId INT IDENTITY PRIMARY KEY,
    ClassId INT NOT NULL,
    AcademicYearId INT NOT NULL,
    TuitionFee DECIMAL(10,2),
    ExamFee DECIMAL(10,2),
    MiscFee DECIMAL(10,2),
    TotalFee DECIMAL(10,2),
    IsActive BIT DEFAULT 1,

    FOREIGN KEY (ClassId) REFERENCES Class(ClassId) ON DELETE CASCADE,
    FOREIGN KEY (AcademicYearId) REFERENCES AcademicYear(AcademicYearId) ON DELETE CASCADE
);

-- =========================
-- FeePayment
-- =========================
CREATE TABLE FeePayment (
    PaymentId INT IDENTITY PRIMARY KEY,
    StudentId INT NOT NULL,
    PaidAmount DECIMAL(10,2),
    ConcessionAmount DECIMAL(10,2),
    DueAmount DECIMAL(10,2),
    PaymentDate DATE,
    PaymentMode NVARCHAR(50),

    FOREIGN KEY (StudentId) REFERENCES Student(StudentId) ON DELETE CASCADE
);

-- =========================
-- StudentAttendance
-- =========================
CREATE TABLE StudentAttendance (
    AttendanceId INT IDENTITY PRIMARY KEY,
    StudentId INT NOT NULL,
    AcademicYearId INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    IsPresent BIT,

    FOREIGN KEY (StudentId) REFERENCES Student(StudentId) ON DELETE CASCADE,
    FOREIGN KEY (AcademicYearId) REFERENCES AcademicYear(AcademicYearId)
);

-- =========================
-- Teacher
-- =========================
CREATE TABLE Teacher (
    TeacherId INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(150),
    MobileNo NVARCHAR(15),
    Qualification NVARCHAR(100),
    ExperienceYears INT,
    AadhaarNo NVARCHAR(20),
    PAN NVARCHAR(20),
    JoiningDate DATE,
    ClassTeacherOfClassId INT NULL,

    FOREIGN KEY (ClassTeacherOfClassId) REFERENCES Class(ClassId)
);

-- =========================
-- TeacherAttendance
-- =========================
CREATE TABLE TeacherAttendance (
    AttendanceId INT IDENTITY PRIMARY KEY,
    TeacherId INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    IsPresent BIT,

    FOREIGN KEY (TeacherId) REFERENCES Teacher(TeacherId) ON DELETE CASCADE
);

-- =========================
-- Role
-- =========================
CREATE TABLE Role (
    RoleId INT IDENTITY PRIMARY KEY,
    RoleName NVARCHAR(50) UNIQUE
);

-- =========================
-- User
-- =========================
CREATE TABLE [User] (
    UserId INT IDENTITY PRIMARY KEY,
    UserName NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(250),
    RoleId INT NOT NULL,

    FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);
