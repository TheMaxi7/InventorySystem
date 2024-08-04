using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public static GameUiManager Instance;
    [SerializeField] private GameObject pausePanel;
    [HideInInspector] public bool isGamePaused;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        isGamePaused = false;
    }
    private void Update()
    {
        // Toggle pause panel visibility when Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
                isGamePaused = false;
            }
            else
            {
                pausePanel.SetActive(true);
                isGamePaused = true;
                Cursor.visible = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    /// <summary>
    /// Method to quit game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
