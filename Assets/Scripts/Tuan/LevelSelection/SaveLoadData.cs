using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace LevelUnlockSystem
{
    public class SaveLoadData : MonoBehaviour
    {
        private static SaveLoadData instance;

        public static SaveLoadData Instance { get => instance; }

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

        public void Initialize()
        {
            //ClearData();
            if (PlayerPrefs.GetInt("GameStartFirstTime") == 1)
            {
                LoadData();
            }
            else
            {
                SaveData();
                PlayerPrefs.SetInt("GameStartFirstTime", 1);
            }
        }


        private void OnApplicationPause(bool pause)
        {
            SaveData();
        }

        /// <summary>
        /// Method used to save the data
        /// </summary>
        public void SaveData()
        {
            string directoryPath = Application.streamingAssetsPath;
            string fullPath = Path.Combine(directoryPath, "LevelData.json");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Debug.Log($"Directory created: {directoryPath}");
            }

            string levelDataString = JsonUtility.ToJson(LevelSystemManager.Instance.LevelData);

            try
            {
                System.IO.File.WriteAllText(fullPath, levelDataString); // Ghi dữ liệu vào file JSON
                Debug.Log($"Data Saved to: {fullPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error Saving Data: " + e);
            }
        }

        /// <summary>
        /// Hàm tải dữ liệu từ StreamingAssets
        /// </summary>
        private void LoadData()
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, "LevelData.json");

            try
            {
                if (System.IO.File.Exists(fullPath))
                {
                    string levelDataString = System.IO.File.ReadAllText(fullPath);
                    LevelData levelData = JsonUtility.FromJson<LevelData>(levelDataString);

                    if (levelData != null)
                    {
                        LevelSystemManager.Instance.LevelData.levelItemArray = levelData.levelItemArray;
                        LevelSystemManager.Instance.LevelData.lastUnlockLevel = levelData.lastUnlockLevel;
                    }

                    Debug.Log($"Data Loaded from: {fullPath}");
                }
                else
                {
                    Debug.LogWarning($"File not found at: {fullPath}");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error Loading Data: " + e);
            }
        }
    }
}
