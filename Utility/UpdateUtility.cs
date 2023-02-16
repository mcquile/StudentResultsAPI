using System.Text;
namespace StudentResultsAPI;

public class UpdateUtility
{
    /// <summary>
    /// Dynamically generates a parameter based Update query based upon the table name, a list of columns to modify as well as a key-value pair relating to the primary key of the table
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="columnNames"></param>
    /// <param name="idKeyValuePair"></param>
    /// <returns></returns>
    public static string generateUpdateQuery(string tableName, List<string> columnNames, KeyValuePair<string, string> idKeyValuePair)
    {
        if (columnNames.Count==0)
        {
            return "";
        }
        StringBuilder updateStringBuilder = new StringBuilder();
        updateStringBuilder.Append($"UPDATE {tableName.ToLower()} SET ");
        foreach (string columnName in columnNames)
        {
            updateStringBuilder.Append($"{columnName} = @{columnName}, ");
        }

        if (columnNames.Count>0)
        {
            updateStringBuilder.Length-=2;
        }
        
        updateStringBuilder.Append($" WHERE {idKeyValuePair.Key} = {idKeyValuePair.Value}");

        string updateQueryString = updateStringBuilder.ToString();

        return updateQueryString;
    }

    
}