using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Difficulty
{
    easy,
    normal,
    hard
}

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;
    public Difficulty Mode = Difficulty.easy;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
}
