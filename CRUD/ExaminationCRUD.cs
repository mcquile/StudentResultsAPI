using Npgsql;
using StudentResultsAPI.Models.ExaminationModels;

namespace StudentResultsAPI.CRUD;

internal class ExaminationCRUD
{
    /// <summary>
    /// Selects all Examinations from the database and returns an array of Examination objects
    /// </summary>
    /// <returns>Examination[]</returns>
    public static Examination[] ReadAllExaminations()
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        List<Examination> examinationsList = new List<Examination> ();
        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM examinations", connectDb.connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Examination examination = Examination.MapToExamination(reader);
                    examinationsList.Add(examination);
                }
        }
        connectDb.CloseConnection();
        return examinationsList.ToArray();
    }


    /// <summary>
    /// Selects examination entry by ID from database and returns a Examinaiton object
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>Examination</returns>
    public static Examination ReadExaminationByID(int id)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = @"SELECT * FROM examinations WHERE examinationid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Examination examination = Examination.MapToExamination(reader);
                    return examination;
                }
        }
        connectDb.CloseConnection();
        return new Examination(-1, "",  DateTime.Today, "");
    }

    /// <summary>
    /// Inserts a examination into Examinations table of StudentResultDB
    /// </summary>
    /// <param name="examination">Examination</param>
    /// <returns>int</returns>
    public static int CreateExamination(ExaminationWithoutID examination)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        int examinationID;
        string commandText = @"INSERT INTO examinations (title, dateandtime, buildingname) VALUES (@title, @dateandtime, @buildingname) RETURNING examinationid";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("title", examination.title);
            cmd.Parameters.AddWithValue("dateandtime", examination.dateAndTime);
            cmd.Parameters.AddWithValue("buildingname", examination.buildingName);

            examinationID = (int)cmd.ExecuteScalar();
        }
        connectDb.CloseConnection();
        return examinationID;
    }

    /// <summary>
    /// Updates the examination entry corresponding to the Examination instance passed as an argument.
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="examination">ExaminationWithoutID></param>
    /// <returns>int</returns>
    public static int UpdateExamination(int id,ExaminationWithoutID examination)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();

        int rowsAffected = 0;

        Dictionary<string, object> columnValueDictionary = examination.mapDictionaryValues();

        List<string> columnNames = columnValueDictionary.Keys.ToList();

        KeyValuePair<string, string> idKeyValuePair = new("examinationid", id.ToString());

        string commandText = UpdateUtility.generateUpdateQuery("examinations", columnNames, idKeyValuePair);

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
    /// Deletes an examination entry from Examinations table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>int</returns>
    public static int DeleteExaminationByID(int id)
    {
        int rowsAffected = 0;
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = $@"DELETE FROM examinations WHERE examinationid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }
}