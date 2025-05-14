using UnityEngine;

[System.Serializable]
public class FigureInfo
{
    public int Id;
    public Color Color;
    public Sprite AnimalIcon;
    public Sprite ShapeIcon;

    public FigureInfo(Color color, Sprite animalIcon, Sprite shapeIcon, int id)
    {
        Id = id;
        Color = color;
        AnimalIcon = animalIcon;
        ShapeIcon = shapeIcon;
    }

    public bool IsMatch(FigureInfo other)
    {
        return Id == other.Id;
    }
}