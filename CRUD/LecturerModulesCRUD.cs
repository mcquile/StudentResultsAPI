using Npgsql;
using StudentResultsAPI.Models.LecturerModulesModels;

namespace StudentResultsAPI.CRUD;

internal class LecturerModuleCRUD
{
    /// <summary>
    /// Selects all LecturerModules from the database and returns an array of LecturerModule objects
    /// </summary>
    /// <returns>LecturerModule[]</returns>
    public static LecturerModule[] ReadAllLecturerModules()
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        List<LecturerModule> lecturerModulesList = new List<LecturerModule> ();
        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM lecturermodules", connectDb.connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    LecturerModule lecturerModule = LecturerModule.MapToLecturerModules(reader);
                    lecturerModulesList.Add(lecturerModule);
                }
        }
        connectDb.CloseConnection();
        return lecturerModulesList.ToArray();
    }


    /// <summary>
    /// Selects lecturerModule entry by ID from database and returns a Examinaiton object
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>LecturerModule</returns>
    public static LecturerModule ReadLecturerModuleByID(int id)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = @"SELECT * FROM lecturermodules WHERE lecturermodulesid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    LecturerModule lecturerModule = LecturerModule.MapToLecturerModules(reader);
                    return lecturerModule;
                }
        }
        connectDb.CloseConnection();
        return new LecturerModule(-1, -1,  -1);
    }

    /// <summary>
    /// Inserts a lecturerModule into LecturerModules table of StudentResultDB
    /// </summary>
    /// <param name="lecturerModule">LecturerModule</param>
    /// <returns>int</returns>
    public static int CreateLecturerModule(LecturerModuleWithoutID lecturerModule)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        int lecturerModuleID;
        string commandText = @"INSERT INTO lecturermodules (lecturerid, moduleid) VALUES (@lecturerid, @moduleid) RETURNING lecturermodulesid";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("lecturerid", lecturerModule.lecturerID);
            cmd.Parameters.AddWithValue("moduleid", lecturerModule.moduleID);

            lecturerModuleID = (int)cmd.ExecuteScalar();
        }
        connectDb.CloseConnection();
        return lecturerModuleID;
    }

    /// <summary>
    /// Updates the lecturerModule entry corresponding to the LecturerModule instance passed as an argument.
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="lecturerModule">LecturerModuleWithoutID></param>
    /// <returns>int</returns>
    public static int UpdateLecturerModule(int id,LecturerModuleWithoutID lecturerModule)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        
        int rowsAffected = 0;

        Dictionary<string, object> columnValueDictionary = lecturerModule.mapDictionaryValues();

        List<string> columnNames = columnValueDictionary.Keys.ToList();

        KeyValuePair<string, string> idKeyValuePair = new("lecturermodulesid", id.ToString());

        string commandText = UpdateUtility.generateUpdateQuery("lecturermodules", columnNames, idKeyValuePair);

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
    /// Deletes an lecturerModule entry from LecturerModules table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>int</returns>
    public static int DeleteLecturerModuleByID(int id)
    {
        int rowsAffected = 0;
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = $@"DELETE FROM lecturerModules WHERE lecturermodulesid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }
}