CREATE TABLE AdmissionSequence
(
    AcademicYearId INT PRIMARY KEY,
    LastNumber INT NOT NULL
);

CREATE PROCEDURE GetNextAdmissionNo
    @AcademicYearId INT,
    @NextAdmissionNo NVARCHAR(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NextNo INT;

    IF EXISTS (SELECT 1 FROM AdmissionSequence WHERE AcademicYearId = @AcademicYearId)
    BEGIN
        UPDATE AdmissionSequence
        SET LastNumber = LastNumber + 1
        WHERE AcademicYearId = @AcademicYearId;

        SELECT @NextNo = LastNumber
        FROM AdmissionSequence
        WHERE AcademicYearId = @AcademicYearId;
    END
    ELSE
    BEGIN
        INSERT INTO AdmissionSequence VALUES (@AcademicYearId, 1);
        SET @NextNo = 1;
    END

    SET @NextAdmissionNo =
        CONCAT('ADM-', YEAR(GETDATE()), '-', FORMAT(@NextNo, '0000'));
END