using System;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public static event Action<Figure> OnFigureClicked;

    [SerializeField] private SpriteRenderer _frameBackRenderer;
    [SerializeField] private SpriteRenderer _frameRenderer;
    [SerializeField] private SpriteRenderer _animalRenderer;

    private FigureInfo _figureInfo;
    private bool _isClickable;

    public FigureInfo Info => _figureInfo;

    private void OnMouseDown()
    {
        if (_isClickable == false)
        {
            return;
        }

        OnFigureClicked?.Invoke(this);
        _isClickable = false;
    }

    public void Initialize(FigureInfo info)
    {
        _isClickable = true;
        _figureInfo = info;
        _frameRenderer.sprite = info.ShapeIcon;
        _frameBackRenderer.sprite = info.ShapeIcon;
        _frameRenderer.color = info.Color;
        _animalRenderer.sprite = info.AnimalIcon;
    }

    public bool IsMatch(Figure other)
    {
        return _figureInfo.IsMatch(other._figureInfo);
    }
}
