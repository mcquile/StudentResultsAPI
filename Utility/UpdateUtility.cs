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
        updateStringBuilder.Append($"UPDATE {tableName.ToLower()} SET ");
        foreach (KeyValuePair<string, string> kvp in setDictionary)
        {
            updateStringBuilder.Append($"{kvp.Key} = {kvp.Value}, ");
        }

        if (setDictionary.Count>0)
        {
            updateStringBuilder.Length-=2;
        }
        
        updateStringBuilder.Append($" WHERE {whereKeyValuePair.Key} = {whereKeyValuePair.Value}");

        string updateQueryString = updateStringBuilder.ToString();

        return updateQueryString;
    }

    public static void mapDictionaryValues(ref Dictionary<string, string> setDictionary, StudentWithoutID student)
    {
        if (student.firstName.Length != 0)
        {
            setDictionary.Add("FirstName", $@"""{student.firstName}""");
        }

        if (student.lastName.Length != 0)
        {
            setDictionary.Add("lastname", $@"""{student.lastName}""");
        }

        if (student.dateOfBirth != null && student.dateOfBirth < DateTime.Now)
        {  
            setDictionary.Add("DateOfBirth", student.dateOfBirth.ToString("yyyy-MM-dd"));
        }
    }
}