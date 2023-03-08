using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nicknameText;

    private void Awake()
    {
        nicknameText.text = $"Pelaajan nimi: {Login.nickname}";
    }
}
