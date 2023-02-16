using System.Text;
using StudentResultsAPI.Models.StudentModels;

namespace StudentResultsAPI;

public class UpdateUtility
{

    public static string generateUpdateQuery(string tableName, Dictionary<string, string> setDictionary, KeyValuePair<string, string> whereKeyValuePair)
    {
        if (setDictionary.Count==0)
        {
            return "";
        }
        StringBuilder updateStringBuilder = new StringBuilder();
        updateStringBuilder.Append("UPDATE ");
        updateStringBuilder.Append(tableName+ " SET ");
        foreach (KeyValuePair<string, string> kvp in setDictionary)
        {
            updateStringBuilder.Append(kvp.Key + " = " + kvp.Value + ", ");
        }

        if (setDictionary.Count>0)
        {
            updateStringBuilder.Length-=2;
        }
        
        updateStringBuilder.Append(" ");
        updateStringBuilder.Append("WHERE " + whereKeyValuePair.Key + " = " + whereKeyValuePair.Value);

        return updateStringBuilder.ToString();
    }

    public static void mapDictionaryValues(ref Dictionary<string, string> setDictionary, StudentWithoutID student)
    {
        if (student.firstName.Length != 0)
        {
            setDictionary.Add("firstName", student.firstName);
        }

        if (student.lastName.Length != 0)
        {
            setDictionary.Add("lastName", student.lastName);
        }

        if (student.dateOfBirth != null && student.dateOfBirth < DateTime.Now)
        {  
            setDictionary.Add("dateOfBirth", student.dateOfBirth.ToString("yyyy-MM-dd"));
        }
    }
}