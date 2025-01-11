using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelUnlockSystem
{

    public class GameUI : MonoBehaviour
    {
        public void GameOver(int starCount)
        {

            if (starCount > 0)
            {
                LevelSystemManager.Instance.LevelComplete(starCount);
            }
            
            // gameOverPanel.SetActive(true);
        }
    }
}