using System.Text;
namespace StudentResultsAPI;

public class UpdateUtility
{
    /// <summary>
    /// Dynamically generates a parameter based Update query based upon the table name, a list of columns to modify as well as a key-value pair relating to the primary key of the table
    /// </summary>
    /// <param name="tableName">string - Name of the table you're altering</param>
    /// <param name="columnNames">List<string> of column that require updating</string></param>
    /// <param name="idKeyValuePair">Key-Value pair to generate the appropriate Where clause</param>
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
            removeTrailingCommaAndSpace(ref updateStringBuilder);
        }
        
        updateStringBuilder.Append($" WHERE {idKeyValuePair.Key} = {idKeyValuePair.Value}");

        return updateStringBuilder.ToString();
    }

    private static void removeTrailingCommaAndSpace(ref StringBuilder stringBuilder)
    {
        stringBuilder.Length -= 2;
    }

    
}