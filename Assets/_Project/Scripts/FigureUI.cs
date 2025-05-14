using UnityEngine;
using UnityEngine.UI;

public class FigureUI : MonoBehaviour
{
    [SerializeField] private Image _frameBack;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _animal;

    private FigureInfo _figureInfo;
    private bool _isEmpty = true;

    public bool IsEmpty => _isEmpty;

    public void Initialize(FigureInfo info)
    {
        gameObject.SetActive(true);
        _figureInfo = info;
        _frame.sprite = info.ShapeIcon;
        _frameBack.sprite = info.ShapeIcon;
        _frame.color = info.Color;
        _animal.sprite = info.AnimalIcon;
        _isEmpty = false;
    }

    public void Clear()
    {
        _figureInfo = null;
        _frameBack.sprite = null;
        _frame.sprite = null;
        _animal.sprite = null;
        _isEmpty = true;
        gameObject.SetActive(false);
    }

    public bool IsMatch(Figure figure)
    {
        if (_figureInfo == null || figure == null)
        {
            return false;
        }

        return _figureInfo.IsMatch(figure.Info);
    }
}
