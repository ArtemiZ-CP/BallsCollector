using UnityEngine;

public class ActionBarView : MonoBehaviour
{
    [SerializeField] private ActionBar _actionBar;
    [SerializeField] private ActionBarSlot _barSlotPrefab;

    private ActionBarSlot[] _barSlots;

    private void Awake()
    {
        _barSlots = new ActionBarSlot[_actionBar.MaxCapacity];

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < _actionBar.MaxCapacity; i++)
        {
            _barSlots[i] = Instantiate(_barSlotPrefab, transform);
        }
    }

    private void OnEnable()
    {
        _actionBar.OnFigureClicked += HandleFigureClicked;
        _actionBar.OnMatchFound += HandleMatchFound;
        _actionBar.OnFigureCleared += HandleFigureCleared;
    }

    private void OnDisable()
    {
        _actionBar.OnFigureClicked -= HandleFigureClicked;
        _actionBar.OnMatchFound -= HandleMatchFound;
        _actionBar.OnFigureCleared -= HandleFigureCleared;
    }

    private void HandleFigureCleared()
    {
        for (int i = 0; i < _barSlots.Length; i++)
        {
            ActionBarSlot slot = _barSlots[i];
            slot.ClearFigure();
        }
    }

    private void HandleFigureClicked(Figure figure)
    {
        for (int i = 0; i < _barSlots.Length; i++)
        {
            if (_barSlots[i].IsEmpty)
            {
                _barSlots[i].SetFigure(figure);
                break;
            }
        }
    }

    private void HandleMatchFound(Figure figure)
    {
        for (int i = 0; i < _barSlots.Length; i++)
        {
            ActionBarSlot slot = _barSlots[i];

            if (slot.IsMatch(figure))
            {
                slot.ClearFigure();
            }
        }
    }
}
