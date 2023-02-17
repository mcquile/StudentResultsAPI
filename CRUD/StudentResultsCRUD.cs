using Npgsql;
using StudentResultsAPI.Models.ExaminationModels;
using StudentResultsAPI.Models.StudentResultsModels;

namespace StudentResultsAPI.CRUD;

internal class StudentResultsCRUD
{
    /// <summary>
    /// Selects all StudentResults from the database and returns an array of StudentResult objects
    /// </summary>
    /// <returns>StudentResults[]</returns>
    public static StudentResult[] ReadAllStudentResults()
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        List<StudentResult> studentResultsList = new List<StudentResult> ();
        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM studentresults", connectDb.connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    StudentResult studentResult = StudentResult.MapToStudentResults(reader);
                    studentResultsList.Add(studentResult);
                }
        }
        connectDb.CloseConnection();
        return studentResultsList.ToArray();
    }


    /// <summary>
    /// Selects student result entry by ID from database and returns a StudentResult object
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>StudentResult</returns>
    public static StudentResult ReadStudentResultByID(int id)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = @"SELECT * FROM studentresults WHERE studentresultsid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    StudentResult studentResult = StudentResult.MapToStudentResults(reader);
                    return studentResult;
                }
        }
        connectDb.CloseConnection();
        return new StudentResult(-1, -1,  -1, -1);
    }

    /// <summary>
    /// Inserts a examination into Examinations table of StudentResultDB
    /// </summary>
    /// <param name="studentResult">StudentResultWithoutID</param>
    /// <returns>int</returns>
    public static int CreateStudentResult(StudentResultWithoutID studentResult)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        int newStudentResultsID;
        string commandText = @"INSERT INTO studentresults (studentid, moduleexaminationsid, mark) VALUES (@studentid, @moduleexaminationsid, @mark) RETURNING studentresultsid";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("studentid", studentResult.studentID);
            cmd.Parameters.AddWithValue("moduleexaminationsid", studentResult.moduleExaminationsID);
            cmd.Parameters.AddWithValue("mark", studentResult.mark);

            newStudentResultsID = (int)cmd.ExecuteScalar();
        }
        connectDb.CloseConnection();
        return newStudentResultsID;
    }

    /// <summary>
    /// Updates the student result entry corresponding to the StudentResult instance passed as an argument.
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="studentResult">StudentResultWithoutID></param>
    /// <returns>int</returns>
    public static int UpdateStudentResult(int id, StudentResultWithoutID studentResult)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();

        int rowsAffected = 0;

        Dictionary<string, string> columnValueDictionary = studentResult.mapDictionaryValues();

        List<string> columnNames = columnValueDictionary.Keys.ToList();

        KeyValuePair<string, string> idKeyValuePair = new("studentresultsid", id.ToString());

        string commandText = UpdateUtility.generateUpdateQuery("studentresults", columnNames, idKeyValuePair);

        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            foreach (KeyValuePair<string, string> columnValue in columnValueDictionary)
            {
                cmd.Parameters.AddWithValue(columnValue.Key, columnValue.Value);
            }
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }

    /// <summary>
    /// Deletes a student result entry from StudentResults table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>int</returns>
    public static int DeleteStudentResultByID(int id)
    {
        int rowsAffected = 0;
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = $@"DELETE FROM studentresults WHERE studentresultsid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }
}