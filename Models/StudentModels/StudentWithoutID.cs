namespace StudentResultsAPI.Models.StudentModels;

/// <summary>
/// Class that serves as a model for the Students table in the StudentResultsDB
/// </summary>
public class StudentWithoutID
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime dateOfBirth { get; set; }
    public StudentWithoutID() { }

    /// <summary>
    /// Constructor to instantiate student object with a default id of 0. Useful in cases where the ID field is not needed, such as Insert for example.
    /// </summary>
    /// <param name="firstName">string</param>
    /// <param name="lastName">string</param>
    /// <param name="dateOfBirth">DateTime</param>
    public StudentWithoutID(
        string firstName,
        string lastName,
        DateTime? dateOfBirth)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateOfBirth = dateOfBirth ?? DateTime.Today.AddYears(1);
        
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

        if (this.dateOfBirth < DateTime.Now)
        {
            setDictionary.Add("dateofbirth", this.dateOfBirth);
        }

        return setDictionary;
    }
}