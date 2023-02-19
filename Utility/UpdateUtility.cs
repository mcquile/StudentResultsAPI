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
        // If no columns require an update, return an empty string.
        if (columnNames.Count==0)
        {
            return "";
        }

        // Declare stringbuilder and start it with "UPDATE *table name here* SET ".
        StringBuilder updateStringBuilder = new StringBuilder();
        updateStringBuilder.Append($"UPDATE {tableName.ToLower()} SET ");

        // Loop through all columns provided and add it with a parameter and a comma. Example: name = @name, -> stringBuilder now holds: "UPDATE *table name here* SET name = @name, "
        foreach (string columnName in columnNames)
        {
            updateStringBuilder.Append($"{columnName} = @{columnName}, ");
        }

        // If columns were added, the new stringbuilder will end with a comma and space. This statement removes the trailing comma and space.
        if (columnNames.Count>0)
        {
            removeTrailingCommaAndSpace(ref updateStringBuilder);
        }

        // Ends the stringBuilder with the WHERE statement holding the provided key-value pair. Example: The final string will be:  "UPDATE *table name here* SET name = @name WHERE *id name* = *id value*
        updateStringBuilder.Append($" WHERE {idKeyValuePair.Key} = {idKeyValuePair.Value}");

        return updateStringBuilder.ToString();
    }

    private static void removeTrailingCommaAndSpace(ref StringBuilder stringBuilder)
    {
        stringBuilder.Length -= 2;
    }

    
}