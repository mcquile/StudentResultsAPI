using Npgsql;

namespace WebApiExample;

internal class StudentCRUD
{
    public static Student[] ReadAllStudents()
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        List<Student> studentsList = new List<Student> ();
        string commandText = $@"SELECT * FROM ""{Constants._StudentTableName}""";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connectDb.connection))
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
        string commandText = $@"SELECT * FROM ""{Constants._StudentTableName}"" WHERE ""StudentID"" = @id";
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
        return new Student(0,"", "", DateTime.Now);
    }

    /// <summary>
    /// Inserts a student into Students table of StudentResultDB
    /// </summary>
    /// <param name="student">Student</param>

    public static int CreateStudent(Student student)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        int studentID;
        string commandText = $@"INSERT INTO ""{Constants._StudentTableName}"" (""FirstName"", ""LastName"", ""DateOfBirth"") VALUES (@firstName, @lastName, @dateOfBirth) RETURNING ""StudentID""";
        using (var cmd = new NpgsqlCommand(commandText, connectDb.connection))
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
    /// Updates the student entry corresponding to the Student instance passed as an argument
    /// </summary>
    /// <param name="student">Student</param>
    public static void UpdateStudent(Student student)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        var commandText = $@"UPDATE ""{Constants._StudentTableName}""
                SET ""FirstName"" = @firstName, ""LastName"" = @lastName, ""DateOfBirth"" = @dateOfBirth
                WHERE ""StudentID"" = @studentID";

        using (var cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("studentID", student.id);
            cmd.Parameters.AddWithValue("firstName", student.firstName);
            cmd.Parameters.AddWithValue("lastName", student.lastName);
            cmd.Parameters.AddWithValue("dateOfBirth", student.dateOfBirth);
                
            cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
    }

    /// <summary>
    /// Deletes a student entry from Students table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="connection">NpgsqlConnection</param>
    public static void DeleteStudentByID(int id)
    {
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        string commandText = $@"DELETE FROM ""{Constants._StudentTableName}"" WHERE ""StudentID"" = @id";
        using (var cmd = new NpgsqlCommand(commandText, connectDb.connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
        connectDb.CloseConnection();
    }
}