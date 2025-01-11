using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace LevelUnlockSystem
{
    public class LevelBtnScript : MonoBehaviour
    {
        [SerializeField] private GameObject lockObj, unlockObj;
        [SerializeField] private Image[] starsArray;
        [SerializeField] private Text levelIndexText;
        [SerializeField] private Color lockColor, unlockColor;
        [SerializeField] private Button btn;
        [SerializeField] private GameObject activeLevelIndicator;
        [SerializeField] AudioSource audioSource;

        public AudioClip SFX;
        private int levelIndex;

        private void Start()
        {
            btn.onClick.AddListener(() => OnClick());
        }

        public void SetLevelButton(LevelItem value, int index, bool activeLevel)
        {
            if (value.unlocked)
            {
                activeLevelIndicator.SetActive(activeLevel);
                levelIndex = index + 1;
                btn.interactable = true;
                lockObj.SetActive(false);
                unlockObj.SetActive(true);
                SetStar(value.starAchieved);
                levelIndexText.text = "" + levelIndex;

            }
            else
            {
                btn.interactable = false;
                lockObj.SetActive(true);
                unlockObj.SetActive(false);
            }

        }
        private void SetStar(int starAchieved)
        {
            for (int i = 0; i < starsArray.Length; i++)
            {

                if (i < starAchieved)
                {
                    starsArray[i].color = unlockColor;
                }
                else
                {
                    starsArray[i].color = lockColor;
                }
            }
        }

        void OnClick()
        {
            LevelSystemManager.Instance.CurrentLevel = levelIndex - 1;
            SceneManager.LoadScene("Level" + levelIndex);
            audioSource.PlayOneShot(SFX);
        }
    }
}
