using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] Button newGameButton;
    [SerializeField] Button continueGameButton;

    private void Start()
    {
        if (!DataPersistanceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        DisableMenuButtons();

        //Create a new game by initializing new game data
        DataPersistanceManager.instance.NewGame();

        //Load the scene
        SceneManager.LoadSceneAsync("TestingWorld");
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();

        SceneManager.LoadSceneAsync("TestingWorld");
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
