namespace StudentResultsAPI.Models.ModulesModels;

/// <summary>
/// Class that serves as a model for the Lecturers table in the StudentResultsDB
/// </summary>
public class ModuleWithoutID
{
    public string moduleName { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="moduleName">string</param>m>
    public ModuleWithoutID(
        string moduleName)
    {
        this.moduleName = moduleName;
    }

    /// <summary>
    /// Creates a dictionary entry for properties which are not null or empty.
    /// </summary>
    /// <returns>Dictionary</returns>
    public Dictionary<string, object> mapDictionaryValues()
    {
        Dictionary<string, object> setDictionary = new Dictionary<string, object>();

        if (this.moduleName.Length != 0)
        {
            setDictionary.Add("modulename", this.moduleName);
        }
        return setDictionary;
    }
}