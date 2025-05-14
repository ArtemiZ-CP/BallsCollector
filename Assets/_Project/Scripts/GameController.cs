using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private BoardGenerator _boardGenerator;
    [SerializeField] private ActionBar _actionBar;
    [SerializeField] private UIController _uiController;
    [SerializeField] private Button _reshuffleButton;

    private bool _isGameActive;

    private void Start()
    {
        StartGame();
    }

    private void OnEnable()
    {
        _actionBar.OnBarFull += HandleGameOver;
        _actionBar.OnMatchFound += HandleMatchFound;
        _reshuffleButton.onClick.AddListener(HandleReshuffle);
        GameEvents.OnVictory += HandleVictory;
    }

    private void OnDisable()
    {
        _actionBar.OnBarFull -= HandleGameOver;
        _actionBar.OnMatchFound -= HandleMatchFound;
        _reshuffleButton.onClick.RemoveListener(HandleReshuffle);
        GameEvents.OnVictory -= HandleVictory;
    }

    public void StartGame()
    {
        _isGameActive = true;
        _uiController.HideAllScreens();
        _boardGenerator.GenerateBoard();
    }

    private void HandleReshuffle()
    {
        if (_isGameActive == false)
        {
            return;
        }

        _boardGenerator.ReshuffleBoard();
    }

    private void HandleMatchFound(Figure figure)
    {
        _boardGenerator.RemoveFigure(figure);
    }

    private void HandleGameOver()
    {
        _isGameActive = false;
        _uiController.ShowDefeatScreen();
    }

    private void HandleVictory()
    {
        _isGameActive = false;
        _uiController.ShowVictoryScreen();
    }
}