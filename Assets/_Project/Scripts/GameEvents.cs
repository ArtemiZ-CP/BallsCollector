using System;

public static class GameEvents
{
    public static event Action OnVictory;
    
    public static void TriggerVictory()
    {
        OnVictory?.Invoke();
    }
}