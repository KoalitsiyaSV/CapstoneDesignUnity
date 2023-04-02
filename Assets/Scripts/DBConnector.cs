using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class DBConnector : MonoBehaviour
{
    private string connectionString;
    private MySqlConnection connection;

    public void Connect(string host, string database, string username, string password)
    {
        connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", host, database, username, password);
        connection = new MySqlConnection(connectionString);
        connection.Open();
    }

    public void Close()
    {
        if (connection != null)
        {
            connection.Close();
            connection = null;
        }
    }

    public bool Authenticate(string username, string password)
    {
        bool isAuthenticated = false;

        MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE username=@username AND password=@password", connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);

        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            isAuthenticated = true;
        }
        reader.Close();

        return isAuthenticated;
    }
}