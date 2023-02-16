namespace StudentResultsAPI.Models.LecturerModels;

/// <summary>
/// Class that serves as a model for the Lecturers table in the StudentResultsDB
/// </summary>
public class LecturerWithoutID
{
    public string firstName { get; set; }
    public string lastName { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="firstName">string</param>
    /// <param name="lastName">string</param>
    public LecturerWithoutID(
        string firstName,
        string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    /// <summary>
    /// Creates a dictionary entry for properties which are not null or empty.
    /// </summary>
    /// <returns>Dictionary</returns>
    public Dictionary<string, string> mapDictionaryValues()
    {
        Dictionary<string, string> setDictionary = new Dictionary<string, string>();

        if (this.firstName.Length != 0)
        {
            setDictionary.Add("firstname", this.firstName);
        }

        if (this.lastName.Length != 0)
        {
            setDictionary.Add("lastname", this.lastName);
        }

        return setDictionary;
    }
}