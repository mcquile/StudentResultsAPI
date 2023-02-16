using Npgsql;

namespace WebApiExample;

internal class ConnectDB
{
    public ConnectDB()
    {
        connection = new NpgsqlConnection();
    }

    internal NpgsqlConnection connection { get; }

    /// <summary>
    ///     Opens connection to database and returns the current instance
    /// </summary>
    /// <returns>ConnectDB</returns>
    public ConnectDB OpenConnection()
    {
        connection.ConnectionString = Constants._ConnectionString;
        connection.Open();
        return this;
    }

    /// <summary>
    ///     Closes the current connection to database
    /// </summary>
    public void CloseConnection()
    {
        try
        {
            connection.Dispose();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}