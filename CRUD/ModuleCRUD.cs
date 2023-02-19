using Npgsql;
using StudentResultsAPI.Models.Modules;

namespace StudentResultsAPI.CRUD;

internal class ModuleCRUD
{
    /// <summary>
    /// Selects all modules form the database and returns an array of Module objects
    /// </summary>
    /// <returns>Module[]</returns>
    public static Module[] ReadAllModules()
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        List<Module> modulesList = new List<Module>();
        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM modules", connectDb.connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Module module = Module.MapToModule(reader);
                    modulesList.Add(module);
                }
        }
        connectDb.CloseConnection();
        return modulesList.ToArray();
    }

    /// <summary>
    /// Selects module entry by ID from database and returns a Student object
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Module</returns>
    public static Module ReadModuleByID(int id)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = @"SELECT * FROM modules WHERE moduleid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Module module = Module.MapToModule(reader);
                    return module;
                }
        }
        connectDb.CloseConnection();
        return new Module(-1, "");
    }

    /// <summary>
    /// Inserts a module into Modules table of StudentResultDB
    /// </summary>
    /// <param name="module"></param>
    /// <returns>int</returns>
    public static int CreateModule(ModuleWithoutID module)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        int moduleID;
        string commandText = @"INSERT INTO modules (modulename) VALUES (@moduleName) RETURNING moduleid";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("moduleName", module.moduleName);

            moduleID = (int)cmd.ExecuteScalar();
        }
        connectDb.CloseConnection();
        return moduleID;
    }

    /// <summary>
    /// Updates the module entry corresponding to the Module instance passed as an argument.
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="module">ModuleWithoutID></param>
    /// <returns>int</returns>
    public static int UpdateModule(int id, ModuleWithoutID module)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();

        int rowsAffected = 0;

        Dictionary<string, object> columnValueDictionary = module.mapDictionaryValues();

        List<string> columnNames = columnValueDictionary.Keys.ToList();

        KeyValuePair<string, string> idKeyValuePair = new("moduleid", id.ToString());

        string commandText = UpdateUtility.generateUpdateQuery("modules", columnNames, idKeyValuePair);

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
    /// Deletes a module entry from Modules table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>int</returns>
    public static int DeleteModuleByID(int id)
    {
        int rowsAffected = 0;
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = $@"DELETE FROM modules WHERE moduleid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }
}

