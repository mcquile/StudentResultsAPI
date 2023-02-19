using Npgsql;
using StudentResultsAPI.Models.StudentModels;

namespace StudentResultsAPI.CRUD;

internal class StudentCRUD
{
    /// <summary>
    /// Selects all students from the database and returns an array of Student objects
    /// </summary>
    /// <returns>Student[]</returns>
    public static Student[] ReadAllStudents()
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        List<Student> studentsList = new List<Student> ();
        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM students", connectDb.connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Student student = Student.MapToStudent(reader);
                    studentsList.Add(student);
                }
        }
        connectDb.CloseConnection();
        return studentsList.ToArray();
    }


    /// <summary>
    /// Selects student entry by ID from database and returns a Student object
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>Student</returns>
    public static Student ReadStudentByID(int id)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = @"SELECT * FROM students WHERE studentid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Student student = Student.MapToStudent(reader);
                    return student;
                }
        }
        connectDb.CloseConnection();
        return new Student(-1, "", "", DateTime.Today);
    }

    /// <summary>
    /// Inserts a student into Students table of StudentResultDB
    /// </summary>
    /// <param name="student">Student</param>
    /// <returns>int</returns>
    public static int CreateStudent(StudentWithoutID student)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        int studentID;
        string commandText = @"INSERT INTO students (firstname, lastname, dateofbirth) VALUES (@firstName, @lastName, @dateOfBirth) RETURNING studentid";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("firstName", student.firstName);
            cmd.Parameters.AddWithValue("lastName", student.lastName);
            cmd.Parameters.AddWithValue("dateOfBirth", student.dateOfBirth);

            studentID = (int)cmd.ExecuteScalar();
        }
        connectDb.CloseConnection();
        return studentID;
    }

    /// <summary>
    /// Updates the student entry corresponding to the Student instance passed as an argument.
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="student">StudentWithoutID></param>
    /// <returns>int</returns>
    public static int UpdateStudent(int id,StudentWithoutID student)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();

        int rowsAffected = 0;

        Dictionary<string, object> columnValueDictionary = student.mapDictionaryValues();

        List<string> columnNames = columnValueDictionary.Keys.ToList();

        KeyValuePair<string, string> idKeyValuePair = new("studentid", id.ToString());

        string commandText = UpdateUtility.generateUpdateQuery("students", columnNames, idKeyValuePair);

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
    /// Deletes a student entry from Students table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>int</returns>
    public static int DeleteStudentByID(int id)
    {
        int rowsAffected = 0;
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = $@"DELETE FROM students WHERE studentid = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            rowsAffected = cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
        return rowsAffected;
    }
}