ALTER TABLE Class
ADD NextClassId INT NULL;

ALTER TABLE Class
ADD CONSTRAINT FK_Class_NextClass
FOREIGN KEY (NextClassId) REFERENCES Class(ClassId);

ALTER TABLE Student
ADD IsTransferred BIT DEFAULT 0,
    IsTCIssued BIT DEFAULT 0;

CREATE TABLE StudentPromotion (
    PromotionId INT IDENTITY PRIMARY KEY,
    StudentId INT NOT NULL,
    FromClassId INT NOT NULL,
    ToClassId INT NOT NULL,
    FromAcademicYearId INT NOT NULL,
    ToAcademicYearId INT NOT NULL,
    PromotionDate DATE DEFAULT GETDATE(),

    FOREIGN KEY (StudentId) REFERENCES Student(StudentId) ON DELETE CASCADE,
    FOREIGN KEY (FromClassId) REFERENCES Class(ClassId),
    FOREIGN KEY (ToClassId) REFERENCES Class(ClassId),
    FOREIGN KEY (FromAcademicYearId) REFERENCES AcademicYear(AcademicYearId),
    FOREIGN KEY (ToAcademicYearId) REFERENCES AcademicYear(AcademicYearId)
);


CREATE TABLE TransferCertificate (
    TCId INT IDENTITY PRIMARY KEY,
    StudentId INT NOT NULL,
    TCNumber NVARCHAR(50) UNIQUE,
    IssueDate DATE NOT NULL,
    Reason NVARCHAR(250),
    LastClassStudied NVARCHAR(50),
    Conduct NVARCHAR(50),
    IsMigrated BIT DEFAULT 0,

    FOREIGN KEY (StudentId) REFERENCES Student(StudentId) ON DELETE CASCADE
);

CREATE TABLE AppSettings (
    AppSettingsId INT IDENTITY PRIMARY KEY,
    CurrentAcademicYearId INT NOT NULL,
    SidebarBehavior NVARCHAR(50) DEFAULT 'Expanded',

    FOREIGN KEY (CurrentAcademicYearId) REFERENCES AcademicYear(AcademicYearId)
);

CREATE INDEX IX_Student_AcademicYear ON Student(AcademicYearId);
CREATE INDEX IX_Attendance_Date ON StudentAttendance(AttendanceDate);
CREATE INDEX IX_FeePayment_Date ON FeePayment(PaymentDate);
