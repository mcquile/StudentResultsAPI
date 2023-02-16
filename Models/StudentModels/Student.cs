using Npgsql;

namespace StudentResultsAPI.Models.StudentModels;

/// <summary>
/// Class that serves as a model for the Students table in the StudentResultsDB
/// </summary>
public class Student
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime dateOfBirth { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="firstName">string</param>
    /// <param name="lastName">string</param>
    /// <param name="dateOfBirth">DateTime</param>
    public Student(
        int id,
        string firstName,
        string lastName,
        DateTime dateOfBirth)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateOfBirth = dateOfBirth;
    }

    /// <summary>
    /// Maps the reader provide to a student object and returns the object.
    /// </summary>
    /// <param name="reader">NpgsqlDataReader</param>
    /// <returns>Student</returns>
    public static Student MapToStudent(NpgsqlDataReader reader)
    {
        int id = (int)reader["StudentID"];
        string firstName = (string)reader["FirstName"];
        string lastName = (string)reader["LastName"];
        DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];

        return new Student(id, firstName, lastName, dateOfBirth);
    }
}