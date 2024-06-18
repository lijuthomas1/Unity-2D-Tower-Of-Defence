using TowerOfDefence.Game;
using UnityEngine;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject instructionUI;
    [SerializeField]
    private GameObject mainMenuUI;
    [SerializeField]
    private GameObject gameOverUI;

    private void Start()
    {
        LevelManager.OnGameStateChange += OnGameStateChange;
    }
    private void OnDestroy()
    {
        LevelManager.OnGameStateChange -= OnGameStateChange;
    }

    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.GameOver:
                gameOverUI.SetActive(true);
                break;
        }
    }


    public void OnClickExit()
    {
        Application.Quit();
    }

    
    public void OpenInstructionPage() {
        instructionUI.SetActive(true);
    }
    public void CloseInstructionPage()
    {
        instructionUI.SetActive(false);
    }

    public void OnClickStartButton()
    {
        print("OnClickStartButton");
        mainMenuUI.SetActive(false);
        instructionUI.SetActive(false);
        gameOverUI.SetActive(false);
        LevelManager.Instance.GameStartRequest();
    }

    public void OnShowMenuButton()
    {
        mainMenuUI.SetActive(true);
        instructionUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

 
}
