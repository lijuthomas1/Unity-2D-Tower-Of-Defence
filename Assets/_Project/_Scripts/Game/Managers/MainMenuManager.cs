using TowerOfDefence.Game;
using UnityEngine;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject instructionUI;
    [SerializeField]
    private GameObject mainMenuUI;
    private static MainMenuManager instance;
    public static MainMenuManager Instance {  get { return instance; } }
    private void Awake()
    {
        instance = this;
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
        LevelManager.Instance.GameStartRequest();
    }
}
