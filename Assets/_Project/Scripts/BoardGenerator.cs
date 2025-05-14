using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    private const int FigureMatchCount = 3;
    private readonly List<Figure> ActiveFigures = new();

    [SerializeField] private FigureInfoGenerator _figureInfoGenerator;
    [SerializeField] private Figure _figurePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnDelay = 0.1f;
    [SerializeField] private int _figureCount = 9;
    [SerializeField] private float _randomSpawnRange = 1;

    private List<FigureInfo> _figureInfoes;
    private bool _isGenerating;

    public event Action<int> OnBoardGenerated;
    public event Action OnBoardCleared;

    private void Awake()
    {
        _figureInfoes = _figureInfoGenerator.GetAllCombinations();
    }

    public void GenerateBoard()
    {
        if (_isGenerating)
        {
            return;
        }

        GenerateBoard(_figureCount);
    }

    public void ReshuffleBoard()
    {
        if (_isGenerating)
        {
            return;
        }
        
        GenerateBoard(ActiveFigures.Count);
    }

    public void RemoveFigure(Figure figure)
    {
        ActiveFigures.Remove(figure);

        if (ActiveFigures.Count == 0)
        {
            GameEvents.TriggerVictory();
        }
    }

    private void GenerateBoard(int figureCount)
    {
        _isGenerating = true;
        ClearBoard();
        Dictionary<FigureInfo, int> typeCounts = CalculateTypeCounts(figureCount);
        List<FigureInfo> shuffledFigures = new();

        foreach (var pair in typeCounts)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                shuffledFigures.Add(pair.Key);
            }
        }

        shuffledFigures = shuffledFigures.OrderBy(x => UnityEngine.Random.value).ToList();

        StartCoroutine(SpawnFiguresWithDelay(shuffledFigures));
    }

    private Dictionary<FigureInfo, int> CalculateTypeCounts(int figureCount)
    {
        Dictionary<FigureInfo, int> typeCounts = new();
        int typesCount = _figureInfoes.Count;
        int remainingSlots = figureCount / FigureMatchCount;

        if (remainingSlots <= 0)
        {
            remainingSlots = 1;
        }

        for (int i = 0; i < remainingSlots; i++)
        {
            if (typesCount == 0)
            {
                break;
            }

            int randomIndex = UnityEngine.Random.Range(0, typesCount);
            FigureInfo type = _figureInfoes[randomIndex];
            typeCounts[type] = 3;
            _figureInfoes.RemoveAt(randomIndex);
            typesCount--;
        }

        return typeCounts;
    }

    private IEnumerator SpawnFiguresWithDelay(List<FigureInfo> figures)
    {
        foreach (var type in figures)
        {
            SpawnFigure(type);
            yield return new WaitForSeconds(_spawnDelay);
        }

        _isGenerating = false;
        OnBoardGenerated?.Invoke(figures.Count);
    }

    private void SpawnFigure(FigureInfo type)
    {
        Vector3 position = _spawnPoint.position + new Vector3(
            UnityEngine.Random.Range(-_randomSpawnRange, _randomSpawnRange),
            UnityEngine.Random.Range(-_randomSpawnRange, _randomSpawnRange),
            0);

        Figure figure = Instantiate(_figurePrefab, position, Quaternion.identity, _spawnPoint);
        figure.Initialize(type);

        ActiveFigures.Add(figure);
    }

    private void ClearBoard()
    {
        foreach (var figure in ActiveFigures)
        {
            Destroy(figure.gameObject);
        }

        ActiveFigures.Clear();
        OnBoardCleared?.Invoke();
    }
}
