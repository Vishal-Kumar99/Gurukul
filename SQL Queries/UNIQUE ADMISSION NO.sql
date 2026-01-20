ALTER TABLE Student
ADD CONSTRAINT UQ_Student_AdmissionNo_Year
UNIQUE (AdmissionNo);