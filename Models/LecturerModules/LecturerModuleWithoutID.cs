namespace StudentResultsAPI.Models.LecturerModules;

/// <summary>
/// Class that serves as a model for the LecturerModulesWithoutID table in the StudentResultsDB
/// </summary>
public class LecturerModuleWithoutID
{
    public int lecturerID { get; set; }
    public int moduleID { get; set; }


    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="lecturerId">int</param>
    /// <param name="moduleId">int</param>
    public LecturerModuleWithoutID(
        int lecturerId,
        int moduleId)
    {
        this.lecturerID = lecturerId;
        this.moduleID = moduleId;
    }

    public Dictionary<string, object> mapDictionaryValues()
    {
        Dictionary<string, object> setDictionary = new Dictionary<string, object>();

        if (this.lecturerID > 0)
        {
            setDictionary.Add("lecturerid", this.lecturerID);
        }

        if (this.moduleID > 0)
        {
            setDictionary.Add("moduleid", this.moduleID);
        }


        return setDictionary;
    }
}