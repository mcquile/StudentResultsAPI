using Npgsql;

namespace StudentResultsAPI.Models.LecturerModulesModels;

/// <summary>
/// Class that serves as a model for the LecturerModulesWithoutID table in the StudentResultsDB
/// </summary>
public class LecturerModulesWithoutID
{
    public int lecturerID { get; set; }
    public int moduleID { get; set; }
    public bool activeStatus { get; set; }


    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="lecturerId">int</param>
    /// <param name="moduleId">int</param>
    /// <param name="activeStatus">bool</param>
    public LecturerModulesWithoutID(
        int lecturerId,
        int moduleId,
        bool activeStatus)
    {
        this.lecturerID = lecturerId;
        this.moduleID = moduleId;
        this.activeStatus = activeStatus;
    }

}