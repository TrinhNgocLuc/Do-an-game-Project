using Firebase.Database;
using Firebase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class saveData: MonoBehaviour
{

    public playerData _playerData;
    private DatabaseReference Database;
    private string currentUserId;

    void Start()
    {
        Database = FirebaseDatabase.GetInstance(FirebaseApp.DefaultInstance,
            "https://game-3d-english-study-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
    }
    void Update()
    {
        SaveGame();

    }

    public async void SaveGame()
    {
        currentUserId = PlayerPrefs.GetString("currentUserId");
        var updatedData = new Dictionary<string, object>
        {
            { "ScoreGame1", _playerData.scoreGame1 },
            { "ScoreGame2", _playerData.scoreGame2 },
            { "ScoreGame3", _playerData.scoreGame3 },
            { "ScoreGame4", _playerData.scoreGame4 },
            { "ScoreGame5", _playerData.scoreGame5 },
            { "ScoreSum", _playerData.GetScoreSum()},
        };
        await Database.Child("user").Child(currentUserId).UpdateChildrenAsync(updatedData);
        Debug.Log("Game Saved!");
    }


    void OnApplicationQuit()
    {
        SaveGame();
    }

}
