using Npgsql;

namespace StudentResultsAPI.Models.ExaminationModels;

/// <summary>
/// Class that serves as a model for the Examinations table in the StudentResultsDB
/// </summary>
public class Examination
{
    public int id { get; set; }
    public string title { get; set; }
    public DateTime dateAndTime { get; set; }
    public string buildingName { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="title">string</param>
    /// <param name="dateAndTime">DateTime</param>
    /// <param name="buildingName">string</param>
    public Examination(
        int id,
        string title,
        DateTime dateAndTime,
        string buildingName
        )
    {
        this.id = id;
        this.title = title;
        this.dateAndTime = dateAndTime;
        this.buildingName = buildingName;
    }

    /// <summary>
    /// Maps the reader provide to a student object and returns the object.
    /// </summary>
    /// <param name="reader">NpgsqlDataReader</param>
    /// <returns>Examination</returns>
    public static Examination MapToStudent(NpgsqlDataReader reader)
    {
        int id = (int)reader["examinationid"];
        string title = (string)reader["title"];
        DateTime dateAndTime = (DateTime)reader["dateandtime"];
        string buildingName = (string)reader["buildingname"];

        return new Examination(id, title, dateAndTime, buildingName);
    }
}