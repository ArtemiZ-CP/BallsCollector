using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    private readonly List<Figure> Figures = new();

    [SerializeField] private BoardGenerator _boardGenerator;
    [SerializeField] private int _maxCapacity = 7;

    public event Action OnBarFull;
    public event Action<Figure> OnMatchFound;
    public event Action<Figure> OnFigureClicked;
    public event Action OnFigureCleared;

    public int MaxCapacity => _maxCapacity;

    private void OnEnable()
    {
        Figure.OnFigureClicked += HandleFigureClicked;
        _boardGenerator.OnBoardCleared += HandleBoardCleared;
    }

    private void OnDisable()
    {
        Figure.OnFigureClicked -= HandleFigureClicked;
        _boardGenerator.OnBoardCleared -= HandleBoardCleared;
    }

    public IReadOnlyList<Figure> GetFigures() => Figures.AsReadOnly();

    private void HandleBoardCleared()
    {
        Figures.Clear();
        OnFigureCleared?.Invoke();
    }

    private void HandleFigureClicked(Figure figure)
    {
        if (Figures.Count >= _maxCapacity)
        {
            return;
        }

        Figures.Add(figure);
        figure.gameObject.SetActive(false);
        OnFigureClicked?.Invoke(figure);
        CheckMatches();

        if (Figures.Count >= _maxCapacity)
        {
            OnBarFull?.Invoke();
        }
    }

    private void CheckMatches()
    {
        for (int i = 0; i < Figures.Count; i++)
        {
            List<int> matchIndices = new() { i };

            for (int j = i + 1; j < Figures.Count; j++)
            {
                if (Figures[i].IsMatch(Figures[j]))
                {
                    matchIndices.Add(j);
                }
            }

            if (matchIndices.Count >= 3)
            {
                HandleMatchFound(matchIndices);
                return;
            }
        }
    }

    private void HandleMatchFound(List<int> indices)
    {
        indices.Sort((a, b) => b.CompareTo(a));

        foreach (int index in indices)
        {
            Figure matchFigure = Figures[index];
            OnMatchFound?.Invoke(matchFigure);
            Figures.RemoveAt(index);
        }
    }
}