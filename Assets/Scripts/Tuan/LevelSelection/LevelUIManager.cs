using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelUnlockSystem
{
    public class LevelUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelBtnGridHolder;
        [SerializeField] private LevelBtnScript levelBtnPrefab;

        private void Start()
        {
            InitializeUI();
        }

        public void InitializeUI()
        {
            LevelItem[] levelItemArray = LevelSystemManager.Instance.LevelData.levelItemArray;

            for (int i = 0; i < levelItemArray.Length; i++)
            {
                LevelBtnScript levelButton = Instantiate(levelBtnPrefab, levelBtnGridHolder.transform);    //create button for each element in array

                levelButton.SetLevelButton(levelItemArray[i], i, i == LevelSystemManager.Instance.LevelData.lastUnlockLevel);
            }
        }
    }
}
