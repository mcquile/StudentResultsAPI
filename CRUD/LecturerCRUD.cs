using Npgsql;
using StudentResultsAPI.Models.Lecturers;

namespace StudentResultsAPI.CRUD;

internal class LecturerCRUD
{
    /// <summary>
    /// Selects all lecturers from the database and returns an array of Lecturer objects
    /// </summary>
    /// <returns>Lecturer[]</returns>
    public static Lecturer[] ReadAllLecturers()
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        List<Lecturer> lecturerList = new List<Lecturer>();
        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM lecturers", connectDb.connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Lecturer lecturer = Lecturer.MapToLecturer(reader);
                    lecturerList.Add(lecturer);
                }
        }
        connectDb.CloseConnection();
        return lecturerList.ToArray();
    }

    /// <summary>
    /// Selects lecturer entry by ID from database and returns a Lecturer object
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Lecturer</returns>
    public static Lecturer ReadLecturerByID(int id)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = @"SELECT * FROM lecturers WHERE lecturerid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Lecturer lecturer = Lecturer.MapToLecturer(reader);
                    return lecturer;
                }
        }
        connectDb.CloseConnection();
        return new Lecturer(-1, "", "");
    }

    /// <summary>
    /// Inserts a lecturer into Lecturers table of StudentResultDB
    /// </summary>
    /// <param name="lecturer"></param>
    /// <returns>int</returns>
    public static int CreateLecturer(LecturerWithoutID lecturer)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        int lecturerID;
        string commandText = @"INSERT INTO lecturers (firstname, lastname) VALUES (@firstName, @lastName) RETURNING lecturerid";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("firstName", lecturer.firstName);
            cmd.Parameters.AddWithValue("lastName", lecturer.lastName);

            lecturerID = (int)cmd.ExecuteScalar();
        }
        connectDb.CloseConnection();
        return lecturerID;
    }

    /// <summary>
    /// Updates the lecturer entry corresponding to the Lecturer instance passed as an argument.
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="lecturer">LecturerWithoutID></param>
    /// <returns>int</returns>
    public static int UpdateLecturer(int id, LecturerWithoutID lecturer)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();

        int rowsAffected = 0;

        Dictionary<string, object> columnValueDictionary = lecturer.mapDictionaryValues();

        List<string> columnNames = columnValueDictionary.Keys.ToList();

        KeyValuePair<string, string> idKeyValuePair = new("lecturerid", id.ToString());

        string commandText = UpdateUtility.generateUpdateQuery("lecturers", columnNames, idKeyValuePair);

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
    /// Deletes a lecturer entry from Lecturers table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>int</returns>
    public static int DeleteLecturertByID(int id)
    {
        int rowsAffected = 0;
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = $@"DELETE FROM Lecturers WHERE lecturerid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }
}

