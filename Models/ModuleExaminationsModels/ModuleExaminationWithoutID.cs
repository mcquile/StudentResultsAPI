using Npgsql;

namespace StudentResultsAPI.Models.ModuleExaminationsModels;

/// <summary>
/// Class that serves as a model for the ModuleExaminations table in the StudentResultsDB
/// </summary>
public class ModuleExaminationWithoutID
{
    public int moduleID { get; set; }
    public int examinationID { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="moduleID">int</param>
    /// <param name="examinationID">int</param>
    public ModuleExaminationWithoutID(
        int moduleID,
        int examinationID)
    {
        this.moduleID = moduleID;
        this.examinationID = examinationID;
    }

    /// <summary>
    /// Creates a dictionary entry for properties which are not null or empty.
    /// </summary>
    /// <returns>Dictionary</returns>
    public Dictionary<string, string> mapDictionaryValues()
    {
        Dictionary<string, string> setDictionary = new Dictionary<string, string>();

        if (this.moduleID > 0)
        {
            setDictionary.Add("moduleid", this.moduleID.ToString());
        }

        if (this.examinationID > 0)
        {
            setDictionary.Add("examinationid", this.examinationID.ToString());
        }

        return setDictionary;
    }