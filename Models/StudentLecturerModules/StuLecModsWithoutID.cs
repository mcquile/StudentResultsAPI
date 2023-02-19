using Npgsql;

namespace StudentResultsAPI.Models.ModuleExaminations;

/// <summary>
/// Class that serves as a model for the StudentLecturerModules table in the StudentResultsDB
/// </summary>
public class StuLecModsWithoutID
{
    public int studentID { get; set; }
    public int lecturerModulesID { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="studentId">int</param>
    /// <param name="lecturerModulesId">int</param>
    public StuLecModsWithoutID(
        int studentId,
        int lecturerModulesId)
    {
        this.studentID = studentId;
        this.lecturerModulesID = lecturerModulesId;
    }

    /// <summary>
    /// Creates a dictionary entry for properties which are not null or empty.
    /// </summary>
    /// <returns>Dictionary</returns>
    public Dictionary<string, object> mapDictionaryValues()
    {
        Dictionary<string, object> setDictionary = new Dictionary<string, object>();

        if (this.studentID > 0)
        {
            setDictionary.Add("studentid", this.studentID);
        }

        if (this.lecturerModulesID > 0)
        {
            setDictionary.Add("lecturermodulesid", this.lecturerModulesID);
        }

        return setDictionary;
    }

}