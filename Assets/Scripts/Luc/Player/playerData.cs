using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using static Cinemachine.DocumentationSortingAttribute;

[CreateAssetMenu(fileName = "playerData")]
public class playerData : ScriptableObject
{
    public Vector3 position;
    public int scoreGame1;
    public int scoreGame2;
    public int scoreGame3;
    public int scoreGame4;
    public int scoreGame5;
    public int scoreSum;
    public string Name;


    public Vector3 GetPosition()
    {
        return position;
    }
    public int GetScoreGame1()
    {
        return scoreGame1;
    }
    public void SetScoreGame1(int newScore)
    {
        scoreGame1 = newScore;
        UpdateScoreSum();
    }

    public int GetScoreGame2()
    {
        return scoreGame2;
    }
    public void SetScoreGame2(int newScore)
    {
        scoreGame2 = newScore;
        UpdateScoreSum();
    }

    public int GetScoreGame3()
    {
        return scoreGame3;
    }
    public void SetScoreGame3(int newScore)
    {
        scoreGame3 = newScore;
        UpdateScoreSum();
    }
    public void SetScoreGame4(int newScore)
    {
        scoreGame4 = newScore;
        UpdateScoreSum();
    }
    public int GetScoreGame4()
    {
        return scoreGame3;
    }
    public void SetScoreGame5(int newScore)
    {
        scoreGame5 = newScore;
        UpdateScoreSum();
    }
    public int GetScoreGame5()
    {
        return scoreGame3;
    }
    public int GetScoreSum()
    {
        return scoreSum;
    }

    public string GetName()
    {
        return Name;
    }
    private void UpdateScoreSum()
    {
        scoreSum = scoreGame1 + scoreGame2 + scoreGame3 + scoreGame4 + scoreGame5;
    }
}
