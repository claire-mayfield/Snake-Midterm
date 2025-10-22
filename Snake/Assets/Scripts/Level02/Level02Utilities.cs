using System.Collections.Generic;
using UnityEngine;

public static class Level02Utilities
{
    private static float _num = 0.25f;
    
    public enum GameState {
        Play,
        Pause,
		GameOver,
    }
 
    public static float GetNonZeroRandomFloat(float min = -1.0f, float max = 1.0f)
    {
        do
        {
            _num = Random.Range(min, max);
        } while (_num > 0.25f);

        return _num;
    }
}