using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Npgsql;

namespace StudentResultsAPI.Models.StudentModels;

/// <summary>
/// Class that serves as a model for the Students table in the StudentResultsDB
/// </summary>
public class Student
{

    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime dateOfBirth { get; set; }

    /// <summary>
    /// Constructor to instantiate all attributes of class
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="firstName">string</param>
    /// <param name="lastName">string</param>
    /// <param name="dateOfBirth">DateTime</param>
    public Student(
        int id,
        string firstName,
        string lastName,
        DateTime dateOfBirth)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateOfBirth = dateOfBirth;
    }

    /// <summary>
    /// Constructor to instantiate student object with a default id of 0. Useful in cases where the ID field is not needed, such as Insert for example.
    /// </summary>
    /// <param name="firstName">string</param>
    /// <param name="lastName">string</param>
    /// <param name="dateOfBirth">DateTime</param>
    public Student(
        string firstName,
        string lastName,
        DateTime dateOfBirth)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateOfBirth = dateOfBirth;
    }

    /// <summary>
    /// Maps the reader provide to a student object and returns the object.
    /// </summary>
    /// <param name="reader">NpgsqlDataReader</param>
    /// <returns>Student</returns>
    public static Student MapToStudent(NpgsqlDataReader reader)
    {
        var id = reader["StudentID"] as int?;
        var firstName = reader["FirstName"] as string;
        var lastName = reader["LastName"] as string;
        var dateOfBirth = reader["DateOfBirth"] as DateTime?;

        return new Student(id.Value, firstName, lastName, dateOfBirth.Value);
    }

    /// <summary>
    /// More legible toString()
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        return $@"ID: {id}\nfirstName: {firstName}\nlastName: {lastName}\ndateOfBirth: {dateOfBirth}";
    }
}