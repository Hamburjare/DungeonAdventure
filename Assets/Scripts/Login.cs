using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using TMPro;
using System.Data;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // GUIlta saatu pinkoodi
    [SerializeField]
    private TMP_InputField pincode;

    // GUIn virhekooditeksti
    [SerializeField]
    private TMP_Text errorText;

    // Missä tietokanta majailee
    private string dbConnectionString;

    // Pelaajan lempinimi. Haetaan tietokannasta. Käytetään pelissä.
    public static string nickname = "";

    // Auto-ominaisuus. Lippu joka kertto onnistuiko tietokantaoperaatio(true=onnistui)
    public bool LoginOK { get; private set; }

    private void Awake()
    {
        dbConnectionString = "URI=file:Database.db";
        LoginOK = false;
        DontDestroyOnLoad(this);
    }

    

    public void LogIn()
    {
        // print($"pincode:{pincode.text}");
        using (var connection = new SqliteConnection(dbConnectionString))
        {
            // Avataan yhteys tietokantaan
            connection.Open();
            // Valmistellaan tietokantahaku
            using (var command = connection.CreateCommand())
            {
                // SQL-lause, jolla haetaan pinkoodi
                command.CommandText = $"select * from login where pincode = '{pincode.text}'";
                // Suoritetaan haku
                using (IDataReader reader = command.ExecuteReader())
                {
                    // Selvitetään löytyykö pinkoodi
                    while (reader.Read())
                    { // Löytyi, joten otetaan lempinimi talteen
                        nickname = reader["nickname"].ToString();
                        // Nostetaan lippu merkiksi että tietokantaoperaatio onnistui.
                        LoginOK = true;
                    }
                    // Suljetaan haku
                    reader.Close();
                }
            }
            // Suljetaan tietokantayhtetys
            connection.Close();
        }
        if (!LoginOK)
        {
            errorText.gameObject.SetActive(true);
            return;
        }
        //print("Kirjautuminen onnistui");
        errorText.gameObject.SetActive(false);
        // Onnistuneen kirjatumisen jälkeen siirrytään pelialueelle.
        SceneManager.LoadScene(1);
    }
}
