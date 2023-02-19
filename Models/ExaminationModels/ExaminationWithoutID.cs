using Npgsql;

namespace StudentResultsAPI.Models.ExaminationModels;

/// <summary>
/// Class that serves as a model for the Examinations table in the StudentResultsDB
/// </summary>
public class ExaminationWithoutID
{
    public string title { get; set; }
    public DateTime dateAndTime { get; set; }
    public string buildingName { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="title">string</param>
    /// <param name="dateAndTime">DateTime</param>
    /// <param name="buildingName">string</param>
    public ExaminationWithoutID(
        string title,
        DateTime? dateAndTime,
        string buildingName
    )
    {
        this.title = title;
        this.dateAndTime = dateAndTime ?? DateTime.Now.Subtract(TimeSpan.FromDays(365));
        this.buildingName = buildingName;
    }


    /// <summary>
    /// Creates a dictionary entry for properties which are not null or empty.
    /// </summary>
    /// <returns>Dictionary</returns>
    public Dictionary<string, object> mapDictionaryValues()
        {
            Dictionary<string, object> setDictionary = new Dictionary<string, object>();

            if (this.title.Length != 0)
            {
                setDictionary.Add("title", this.title);
            }

            if (this.buildingName.Length != 0)
            {
                setDictionary.Add("buildingname", this.buildingName);
            }

            if (this.dateAndTime < DateTime.Now)
            {
                setDictionary.Add("dateandtime", this.dateAndTime);
            }

            return setDictionary;
        }
}

