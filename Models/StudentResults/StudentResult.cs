using Npgsql;

namespace StudentResultsAPI.Models.StudentResults;

/// <summary>
/// Class that serves as a model for the StudentResults table in the StudentResultsDB
/// </summary>
public class StudentResult
{
    public int id { get; set; }
    public int studentID { get; set; }
    public int moduleExaminationsID { get; set; }
    public decimal mark { get; set; }


    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="studentID">int</param>
    /// <param name="moduleExaminationsID">int</param>
    /// <param name="mark">decimal</param>
    public StudentResult(
        int id,
        int studentID,
        int moduleExaminationsID,
        decimal mark)
    {
        this.id = id;
        this.studentID = studentID;
        this.moduleExaminationsID = moduleExaminationsID;
        this.mark = mark;
    }

    /// <summary>
    /// Maps the reader provide to a ModuleExamination object and returns the object.
    /// </summary>
    /// <param name="reader">NpgsqlDataReader</param>
    /// <returns>Lecturer</returns>
    public static StudentResult MapToStudentResults(NpgsqlDataReader reader)
    {
        int id = (int)reader["studentresultsid"];
        int studentID = (int)reader["studentid"];
        int moduleExaminationsID = (int)reader["moduleexaminationsid"];
        decimal mark = (decimal)reader["mark"];

        return new StudentResult(id, studentID, moduleExaminationsID, mark);
    }
}