using Npgsql;

namespace StudentResultsAPI.Models.ModuleExaminations;

/// <summary>
/// Class that serves as a model for the StudentLecturerModules table in the StudentResultsDB
/// </summary>
public class StudentLecturerModules
{
    public int id { get; set; }
    public int studentID { get; set; }
    public int lecturerModulesID { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="studentId">int</param>
    /// <param name="lecturerModulesId">int</param>
    public StudentLecturerModules(
        int id,
        int studentId,
        int lecturerModulesId)
    {
        this.id = id;
        this.studentID = studentId;
        this.lecturerModulesID = lecturerModulesId;
    }

    /// <summary>
    /// Maps the reader provide to a StudentLecturerModules object and returns the object.
    /// </summary>
    /// <param name="reader">NpgsqlDataReader</param>
    /// <returns>StudentLecturerModules</returns>
    public static StudentLecturerModules MapToStudentLecturerModules(NpgsqlDataReader reader)
    {
        int id = (int)reader["slmid"];
        int moduleID = (int)reader["studentid"];
        int examinationID = (int)reader["lecturermodulesid"];

        return new StudentLecturerModules(id, moduleID, examinationID);
    }
}