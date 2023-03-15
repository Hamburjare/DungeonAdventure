using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static bool hasUIOpen = false;

        // private void Awake()
        // {
        //     DontDestroyOnLoad(this);
        // }

        public static void ChangeCursorState(bool newState)
        {
            hasUIOpen = newState;
            Cursor.lockState = newState ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = newState;
            GameObject
                .Find("PlayerCapsule")
                .GetComponent<StarterAssets.FirstPersonController>()
                .LockCameraPosition = newState;
        }

        // Method to close all ui
        public void CloseAllUI()
        {
            // GameManager.Instance.pauseMenu.SetActive(false);
            ChangeCursorState(false);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        public void LoadScene(int scene)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void Update()
        {
            if (hasUIOpen)
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
                {
                    CloseAllUI();
                }
            }

            else if (!hasUIOpen)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeCursorState(true);
                    // GameManager.Instance.pauseMenu.SetActive(true);
                }
            }
        }
    }
}