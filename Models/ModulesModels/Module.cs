using Npgsql;

namespace StudentResultsAPI.Models.ModulesModels;

/// <summary>
/// Class that serves as a model for the Modules table in the StudentResultsDB
/// </summary>
public class Module
{
    public int id { get; set; }
    public string moduleName { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="moduleName">string</param>
    public Module(
        int id,
        string moduleName)
    {
        this.id = id;
        this.moduleName = moduleName;
    }

    /// <summary>
    /// Maps the reader provide to a Module object and returns the object.
    /// </summary>
    /// <param name="reader">NpgsqlDataReader</param>
    /// <returns>Module</returns>
    public static Module MapToModule(NpgsqlDataReader reader)
    {
        int id = (int)reader["moduleid"];
        string moduleName = (string)reader["modulename"];

        return new Module(id, moduleName);
    }
}