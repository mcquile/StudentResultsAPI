using Npgsql;

namespace StudentResultsAPI.Models.StudentResultsModels;

/// <summary>
/// Class that serves as a model for the StudentResults table in the StudentResultsDB
/// </summary>
public class StudentResultWithoutID
{
    public int studentID { get; set; }
    public int moduleExaminationsID { get; set; }
    public decimal mark { get; set; }


    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="studentID">int</param>
    /// <param name="moduleExaminationsID">int</param>
    /// <param name="mark">decimal</param>
    public StudentResultWithoutID(
        int studentID,
        int moduleExaminationsID,
        decimal mark)
    {
        this.studentID = studentID;
        this.moduleExaminationsID = moduleExaminationsID;
        this.mark = mark;
    }

    /// <summary>
    /// Creates a dictionary entry for properties which are not null or empty.
    /// </summary>
    /// <returns>Dictionary</returns>
    public Dictionary<string, string> mapDictionaryValues()
    {
        Dictionary<string, string> setDictionary = new Dictionary<string, string>();

        if (this.studentID > 0)
        {
            setDictionary.Add("studentid", this.studentID.ToString());
        }

        if (this.moduleExaminationsID > 0)
        {
            setDictionary.Add("moduleexaminationsid", this.moduleExaminationsID.ToString());
        }

        if (this.mark >= 0)
        {
            setDictionary.Add("mark", this.mark.ToString());
        }

        return setDictionary;
    }
}