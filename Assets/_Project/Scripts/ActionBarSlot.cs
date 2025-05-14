using UnityEngine;

public class ActionBarSlot : MonoBehaviour
{
    [SerializeField] private FigureUI _figureUI;

    public bool IsEmpty => _figureUI.IsEmpty;

    private void Awake()
    {
        ClearFigure();
    }

    public void SetFigure(Figure figure)
    {
        _figureUI.Initialize(figure.Info);
    }

    public void ClearFigure()
    {
        _figureUI.Clear();
    }

    public bool IsMatch(Figure other)
    {
        return _figureUI.IsMatch(other);
    }
}
