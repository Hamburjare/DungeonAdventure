using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nicknameText;

    [SerializeField]
    public GameObject closedText;

    public static GameManager Instance { get; private set; }

    public static bool s_hasKey = false;
    

    private void Awake()
    {
        nicknameText.text = $"Pelaajan nimi: {Login.nickname}";
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        UI.UIManager.ChangeCursorState(false);
    }

}
