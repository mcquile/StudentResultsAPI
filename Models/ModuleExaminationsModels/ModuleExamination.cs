using Npgsql;

namespace StudentResultsAPI.Models.ModuleExaminationsModels;

/// <summary>
/// Class that serves as a model for the ModuleExaminations table in the StudentResultsDB
/// </summary>
public class ModuleExamination
{
    public int id { get; set; }
    public int moduleID { get; set; }
    public int examinationID { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="moduleID">int</param>
    /// <param name="examinationID">int</param>
    public ModuleExamination(
        int id,
        int moduleID,
        int examinationID)
    {
        this.id = id;
        this.moduleID = moduleID;
        this.examinationID = examinationID;
    }

    /// <summary>
    /// Maps the reader provide to a ModuleExamination object and returns the object.
    /// </summary>
    /// <param name="reader">NpgsqlDataReader</param>
    /// <returns>Lecturer</returns>
    public static ModuleExamination MapToModuleExamination(NpgsqlDataReader reader)
    {
        int id = (int)reader["moduleexaminationid"];
        int moduleID = (int)reader["moduleid"];
        int examinationID = (int)reader["examinationid"];

        return new ModuleExamination(id, moduleID, examinationID);
    }
}