using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private Button _restartButton;
    
    private void OnEnable()
    {
        _restartButton.onClick.AddListener(HandleRestart);
    }
    
    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(HandleRestart);
    }
    
    public void HideAllScreens()
    {
        _victoryScreen.SetActive(false);
        _defeatScreen.SetActive(false);
        _restartButton.gameObject.SetActive(false);
    }
    
    public void ShowVictoryScreen()
    {
        _victoryScreen.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }
    
    public void ShowDefeatScreen()
    {
        _defeatScreen.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }
    
    private void HandleRestart()
    {
        _gameController.StartGame();
    }
}