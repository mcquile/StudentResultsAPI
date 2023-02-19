namespace StudentResultsAPI.Models.Lecturers;

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
    public Dictionary<string, object> mapDictionaryValues()
    {
        Dictionary<string, object> setDictionary = new Dictionary<string, object>();

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