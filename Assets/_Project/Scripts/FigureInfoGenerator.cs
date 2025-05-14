using System.Collections.Generic;
using UnityEngine;

public class FigureInfoGenerator : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private Sprite[] _animalIcons;
    [SerializeField] private Sprite[] _shapeIcons;

    public List<FigureInfo> GetAllCombinations()
    {
        int id = 0;
        var result = new List<FigureInfo>();

        foreach (var color in _colors)
        {
            foreach (var animal in _animalIcons)
            {
                foreach (var shape in _shapeIcons)
                {
                    result.Add(new FigureInfo(color, animal, shape, id++));
                }
            }
        }

        return result;
    }
}
