using Npgsql;
using StudentResultsAPI.Models.ModuleExaminations;

namespace StudentResultsAPI.CRUD;

internal class ModuleExaminationCRUD
{
    /// <summary>
    /// Selects all ModuleExamination from the database and returns an array of ModuleExamination objects
    /// </summary>
    /// <returns>ModuleExamination[]</returns>
    public static ModuleExamination[] ReadAllModuleExamination()
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        List<ModuleExamination> examinationsList = new List<ModuleExamination> ();
        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM moduleexaminations", connectDb.connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    ModuleExamination moduleExamination = ModuleExamination.MapToModuleExamination(reader);
                    examinationsList.Add(moduleExamination);
                }
        }
        connectDb.CloseConnection();
        return examinationsList.ToArray();
    }


    /// <summary>
    /// Selects ModuleExamination entry by ID from database and returns a ModuleExamination object
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>ModuleExamination</returns>
    public static ModuleExamination ReadModuleExaminationByID(int id)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = @"SELECT * FROM moduleexaminations WHERE moduleexaminationsID = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    ModuleExamination moduleExamination = ModuleExamination.MapToModuleExamination(reader);
                    return moduleExamination;
                }
        }
        connectDb.CloseConnection();
        return new ModuleExamination(-1, -1, -1);
    }

    /// <summary>
    /// Inserts a moduleexamination into ModuleExamination table of StudentResultDB
    /// </summary>
    /// <param name="moduleExamination">ModuleExamination</param>
    /// <returns>int</returns>
    public static int CreateModuleExamination(ModuleExaminationWithoutID moduleExamination)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        int moduleExaminationID;
        string commandText = @"INSERT INTO moduleexaminations (moduleid, examinationid) VALUES (@moduleid, @examinationid) RETURNING moduleexaminationsid";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("moduleid", moduleExamination.moduleID);
            cmd.Parameters.AddWithValue("examinationid", moduleExamination.examinationID);

            moduleExaminationID = (int)cmd.ExecuteScalar();
        }
        connectDb.CloseConnection();
        return moduleExaminationID;
    }

    /// <summary>
    /// Updates the moduleexamination entry corresponding to the ModuleExamination instance passed as an argument.
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="moduleExamination">ModuleExaminationWithoutID></param>
    /// <returns>int</returns>
    public static int UpdateModuleExamination(int id,ModuleExaminationWithoutID examination)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();

        int rowsAffected = 0;

        Dictionary<string, object> columnValueDictionary = examination.mapDictionaryValues();

        List<string> columnNames = columnValueDictionary.Keys.ToList();

        KeyValuePair<string, string> idKeyValuePair = new("moduleexaminationsid", id.ToString());

        string commandText = UpdateUtility.generateUpdateQuery("moduleexaminations", columnNames, idKeyValuePair);

        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            foreach (KeyValuePair<string, object> columnValue in columnValueDictionary)
            {
                cmd.Parameters.AddWithValue(columnValue.Key, columnValue.Value);
            }
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }

    /// <summary>
    /// Deletes a moduleexamination entry from ModuleExaminations table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>int</returns>
    public static int DeleteModuleExaminationByID(int id)
    {
        int rowsAffected = 0;
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = $@"DELETE FROM moduleexaminations WHERE moduleexaminationsid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }
}