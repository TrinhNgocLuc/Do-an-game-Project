using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelUnlockSystem
{

    public class LevelSystemManager : MonoBehaviour
    {
        private static LevelSystemManager instance;
        [SerializeField] private LevelData levelData;

        public static LevelSystemManager Instance { get => instance; }
        public LevelData LevelData { get => levelData; }
        private int currentLevel;
        public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void OnEnable()
        {
            SaveLoadData.Instance.Initialize();
        }


        public void LevelComplete(int starAchieved)
        {
            levelData.levelItemArray[currentLevel].starAchieved = starAchieved;
            if (levelData.lastUnlockLevel < (currentLevel + 1))
            {
                levelData.lastUnlockLevel = currentLevel + 1;

                levelData.levelItemArray[levelData.lastUnlockLevel].unlocked = true;
            }
            SaveLoadData.Instance.SaveData();
        }
    }

    [System.Serializable]
    public class LevelData
    {
        public int lastUnlockLevel = 0;
        public LevelItem[] levelItemArray;
    }

    [System.Serializable]
    public class LevelItem
    {
        public bool unlocked;
        public int starAchieved;
    }
}