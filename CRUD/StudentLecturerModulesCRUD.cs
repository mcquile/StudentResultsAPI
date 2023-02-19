using Npgsql;
using StudentResultsAPI.Models.ModuleExaminations;

namespace StudentResultsAPI.CRUD;

internal class StudentLecturerModulesCRUD
{
    /// <summary>
    /// Selects all StudentLecturerModules from the database and returns an array of StudentLecturerModules objects
    /// </summary>
    /// <returns>StudentLecturerModules[]</returns>
    public static StudentLecturerModules[] ReadAllStudentLecturerModules()
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        List<StudentLecturerModules> studentLecturerModulesList = new List<StudentLecturerModules> ();
        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM studentlecturermodules", connectDb.connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    StudentLecturerModules studentLecturerModules = StudentLecturerModules.MapToStudentLecturerModules(reader);
                    studentLecturerModulesList.Add(studentLecturerModules);
                }
        }
        connectDb.CloseConnection();
        return studentLecturerModulesList.ToArray();
    }


    /// <summary>
    /// Selects StudentLecturerModules entry by ID from database and returns a StudentLecturerModules object
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>StudentLecturerModules</returns>
    public static StudentLecturerModules ReadStudentLecturerModulesByID(int id)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = @"SELECT * FROM studentlecturermodules WHERE slmid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    StudentLecturerModules studentLecturerModules = StudentLecturerModules.MapToStudentLecturerModules(reader);
                    return studentLecturerModules;
                }
        }
        connectDb.CloseConnection();
        return new StudentLecturerModules(-1, -1, -1);
    }

    /// <summary>
    /// Inserts a studentLecturerModules entry into StudentLecturerModules table of StudentResultDB
    /// </summary>
    /// <param name="studentLecturerModule">ModuleExamination</param>
    /// <returns>int</returns>
    public static int CreateStudentLecturerModules(StuLecModsWithoutID studentLecturerModule)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        int studentLecturerModulesID;
        string commandText = @"INSERT INTO studentlecturermodules (studentid, lecturermodulesid) VALUES (@studentid, @lecturermodulesid) RETURNING slmid";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("studentid", studentLecturerModule.studentID);
            cmd.Parameters.AddWithValue("lecturermodulesid", studentLecturerModule.lecturerModulesID);

            studentLecturerModulesID = (int)cmd.ExecuteScalar();
        }
        connectDb.CloseConnection();
        return studentLecturerModulesID;
    }

    /// <summary>
    /// Updates the moduleexamination entry corresponding to the StudentLecturerModules instance passed as an argument.
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="studentLecturerModule">StuLecModsWithoutID></param>
    /// <returns>int</returns>
    public static int UpdateStudentLecturerModules(int id, StuLecModsWithoutID studentLecturerModule)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();

        int rowsAffected = 0;

        Dictionary<string, object> columnValueDictionary = studentLecturerModule.mapDictionaryValues();

        List<string> columnNames = columnValueDictionary.Keys.ToList();

        KeyValuePair<string, string> idKeyValuePair = new("slmid", id.ToString());

        string commandText = UpdateUtility.generateUpdateQuery("studentlecturermodules", columnNames, idKeyValuePair);

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
    /// Deletes a studentLecturerModule entry from StudentLecturerModules table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>int</returns>
    public static int DeleteStudentLecturerModulesByID(int id)
    {
        int rowsAffected = 0;
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = $@"DELETE FROM studentlecturermodules WHERE slmid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }
}