using Npgsql;

namespace StudentResultsAPI.Models.LecturerModulesModels;

/// <summary>
/// Class that serves as a model for the LecturerModules table in the StudentResultsDB
/// </summary>
public class LecturerModule
{
    public int id { get; set; }
    public int lecturerID { get; set; }
    public int moduleID { get; set; }
    public bool activeStatus { get; set; }


    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="lecturerId">int</param>
    /// <param name="moduleId">int</param>
    /// <param name="activeStatus">bool</param>
    public LecturerModule(
        int id,
        int lecturerId,
        int moduleId,
        bool activeStatus)
    {
        this.id = id;
        this.lecturerID = lecturerId;
        this.moduleID = moduleId;
        this.activeStatus = activeStatus;
    }

    /// <summary>
    /// Maps the reader provide to a LecturerModule object and returns the object.
    /// </summary>
    /// <param name="reader">NpgsqlDataReader</param>
    /// <returns>LecturerModules</returns>
    public static LecturerModule MapToLecturerModules(NpgsqlDataReader reader)
    {
        int id = (int)reader["lecturermodulesid"];
        int lecturerID = (int)reader["lecturerid"];
        int moduleID = (int)reader["moduleid"];
        bool activeStatus = (bool)reader["activestatus"];

        return new LecturerModule(id, lecturerID, moduleID, activeStatus);
    }
}