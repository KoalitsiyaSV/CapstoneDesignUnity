using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MySql.Data.MySqlClient;
using TMPro;

public class LoginManager : MonoBehaviour {
    public string host;
    public string database;
    public string userId;
    public string password;
    public string tableName;

    //Login Panel
    [Header("Login Panel")]
    public GameObject LoginPanel;
    public TMP_InputField IDInputField;
    public TMP_InputField PasswordInputField;

    //Create Account Panel
    [Header("Create Account Panel")]
    public GameObject CreateAccountPanel;
    public TMP_InputField CreateIDInputField;
    public TMP_InputField CreatePasswordInputFIeld;

    void Start() {
        
    }

    public void LoginBtn() {


        //string connString = string.Format("Server={0};Database={1};User ID={2};Password={3};Pooling=false", host, database, userId, password);
        //MySqlConnection conn = new MySqlConnection(connString);

        //try
        //{
        //    conn.Open();

        //    string query = string.Format("SELECT * FROM {0} WHERE ID='{1}' AND PASSWORD='{2}'", tableName, IDInputField, PasswordInputField);

        //    MySqlCommand command = new MySqlCommand(query, conn);
        //    MySqlDataReader reader = command.ExecuteReader();

        //    if (reader.HasRows)
        //    {
        //        Debug.Log("Login Success");
        //        // �α��� ���� ó��
        //    }
        //    else
        //    {
        //        Debug.Log("Login Failed");
        //        // �α��� ���� ó��
        //    }

        //    reader.Close();
        //    conn.Close();
        //}
        //catch (System.Exception e)
        //{
        //    Debug.Log(e.Message);
        //}
    }

    IEnumerator LoginCo()
    {
        Debug.Log(IDInputField.text);
        Debug.Log(PasswordInputField.text);

        yield return null;
    }

    public void CreateAccountBtn()
    {
        LoginPanel.SetActive(false);
        CreateAccountPanel.SetActive(true);
        // StartCoroutine(LoginCo());
    }

    public void CreateNewAccountBtn()
    {
        LoginPanel.SetActive(true);
        CreateAccountPanel.SetActive(false);
    }
}
