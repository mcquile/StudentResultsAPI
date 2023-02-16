using Npgsql;

namespace StudentResultsAPI.Models.LecturerModels;

/// <summary>
/// Class that serves as a model for the Lecturers table in the StudentResultsDB
/// </summary>
public class Lecturer
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="firstName">string</param>
    /// <param name="lastName">string</param>
    public Lecturer(
        int id,
        string firstName,
        string lastName)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
    }

    /// <summary>
    /// Maps the reader provide to a Lecturer object and returns the object.
    /// </summary>
    /// <param name="reader">NpgsqlDataReader</param>
    /// <returns>Lecturer</returns>
    public static Lecturer MapToLecturer(NpgsqlDataReader reader)
    {
        int id = (int)reader["lecturerid"];
        string firstName = (string)reader["firstname"];
        string lastName = (string)reader["lastname"];

        return new Lecturer(id, firstName, lastName);
    }
}