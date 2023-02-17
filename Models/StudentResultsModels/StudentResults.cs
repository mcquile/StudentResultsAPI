using Npgsql;

namespace StudentResultsAPI.Models.StudentResultsModels;

/// <summary>
/// Class that serves as a model for the StudentResults table in the StudentResultsDB
/// </summary>
public class StudentResults
{
    public int id { get; set; }
    public int studentID { get; set; }
    public int moduleExaminationsID { get; set; }
    public double mark { get; set; }


    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="studentID">int</param>
    /// <param name="moduleExaminationsID">int</param>
    /// <param name="mark">double</param>
    public StudentResults(
        int id,
        int studentID,
        int moduleExaminationsID,
        double mark)
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
    public static StudentResults MapToStudentResults(NpgsqlDataReader reader)
    {
        int id = (int)reader["studentresultsid"];
        int studentID = (int)reader["studentid"];
        int moduleExaminationsID = (int)reader["moduleexaminationsid"];
        double mark = (double)reader["mark"];

        return new StudentResults(id, studentID, moduleExaminationsID, mark);
    }
}