using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerDaoAnh : GameManager
{
    public static GameManagerDaoAnh Instance;
    public Action onPlayerTouchingAction;
    public Action onResetGameState;
    [SerializeField] private List<Vocabulary> currentVocabularies = new List<Vocabulary>();
    [SerializeField] private TextMeshProUGUI currentVocaTextEasy;
    [SerializeField] private TextMeshProUGUI currentVocaTextMedium;
    [SerializeField] private TextMeshProUGUI currentVocaTextHard;
    [SerializeField] private List<Image> vocaPulledImagesEasy = new List<Image>();
    [SerializeField] private List<Image> vocaPulledImagesMedium = new List<Image>();
    [SerializeField] private List<Image> vocaPulledImagesHard = new List<Image>();
    [SerializeField] private GameObject rightArrowEndLv;
    [SerializeField] private GameObject leftArrowEndLv;
    [SerializeField] private Sprite questionMarkSprite;
    private int currentVocaOnEndLv = 0;
    private int currentVocaNum = 0;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject); 
        else
            Instance = this;
        leftArrowEndLv.SetActive(false);
        rightArrowEndLv.SetActive(true);
    }
    public void OnTouching() => onPlayerTouchingAction?.Invoke();
    public override void OnSelectDifficulty(int diff)
    {
        base.OnSelectDifficulty(diff);
        SpawnNewRandomVoca();
        RacoonSpawner.Instance.SpawnRacoonOnInitializeLv();
        PowerUpSpawner.Instance.SpawnPowerUpOnInitializeLv();
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                timer = 60f;
                SetQuizHolderAndVocaPullImages(1);
                break;
            case Difficulty.normal:
                timer = 80f;
                SetQuizHolderAndVocaPullImages(2);
                break;
            case Difficulty.hard:
                timer = 100f;
                SetQuizHolderAndVocaPullImages(3);
                break;
        }
        startTimer = true;
        PlayWordAudio();
        SetVocaForEndLvUI();
        ResetPulledImage();
    }
    private void SetQuizHolderAndVocaPullImages(int diff)
    {
        if(diff == 1)
        {
            vocaPulledImagesEasy[0].transform.parent.parent.gameObject.SetActive(true);
            vocaPulledImagesMedium[0].transform.parent.parent.gameObject.SetActive(false);
            vocaPulledImagesHard[0].transform.parent.parent.gameObject.SetActive(false);
            currentVocaTextEasy.gameObject.SetActive(true);
            currentVocaTextMedium.gameObject.SetActive(false);
            currentVocaTextHard.gameObject.SetActive(false);
        } else if(diff == 2)
        {
            vocaPulledImagesEasy[0].transform.parent.parent.gameObject.SetActive(false);
            vocaPulledImagesMedium[0].transform.parent.parent.gameObject.SetActive(true);
            vocaPulledImagesHard[0].transform.parent.parent.gameObject.SetActive(false);
            currentVocaTextEasy.gameObject.SetActive(false);
            currentVocaTextMedium.gameObject.SetActive(true);
            currentVocaTextHard.gameObject.SetActive(false);
        } else
        {
            vocaPulledImagesEasy[0].transform.parent.parent.gameObject.SetActive(false);
            vocaPulledImagesMedium[0].transform.parent.parent.gameObject.SetActive(false);
            vocaPulledImagesHard[0].transform.parent.parent.gameObject.SetActive(true);
            currentVocaTextEasy.gameObject.SetActive(false);
            currentVocaTextMedium.gameObject.SetActive(false);
            currentVocaTextHard.gameObject.SetActive(true);
        }
    }
    public override void OnClickNextLv()
    {
        base.OnClickNextLv();
        SpawnNewRandomVoca();
        RacoonSpawner.Instance.SpawnRacoonOnInitializeLv();
        PowerUpSpawner.Instance.SpawnPowerUpOnInitializeLv();
        ResetPulledImage();
        currentVocaNum = 0;
        currentVocaOnEndLv = 0;
        leftArrowEndLv.SetActive(false);
        rightArrowEndLv.SetActive(true);
        SetVocaForEndLvUI();
        onResetGameState?.Invoke();
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                timer = 60f;
                break;
            case Difficulty.normal:
                timer = 80f;
                break;
            case Difficulty.hard:
                timer = 100f;
                break;
        }
        if (!timeOut)
            lv++;
        else
            timeOut = false;
        startTimer = true;
    }
    protected void SetVocaForEndLvUI()
    {
        vocaImageEndLv.sprite = currentVocabularies[0].image;
        vocaTextEndLv.text = currentVocabularies[0].vocabulary;
        vocaMeaningText.text = currentVocabularies[0].mean;
        AudioManager.instance.SetCurrentWordAudio(currentVocabularies[0].audio);
    }
    protected override void Update()
    {
        base.Update();
        currentVocaTextEasy.text = currentVocabulary.vocabulary;
        currentVocaTextMedium.text = currentVocabulary.vocabulary;
        currentVocaTextHard.text = currentVocabulary.vocabulary;
    }
    private void SpawnNewRandomVoca()
    {
        currentVocabularies.Clear();
        switch(DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                for(int i=0; i<5; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomEasyVocabulary());
                }
                break;
            case Difficulty.normal:
                for (int i = 0; i < 6; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomMediumVocabulary());
                }
                break;
            case Difficulty.hard:
                for (int i = 0; i < 7; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomHardVocabulary());
                }
                break;
        }
        currentVocabulary = currentVocabularies[0];
        AudioManager.instance.SetCurrentWordAudio(currentVocabulary.audio);
    }
    public Sprite GetImageForRacoon(int num)
    {
        return currentVocabularies[num].image;
    }
    public bool CheckImagePulled(Image image)
    {
        if (image.sprite != currentVocabulary.image)
            return false;
        return true;
    }
    public void SetNewImageAfterPulledSucess(Image image)
    {
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                vocaPulledImagesEasy[currentVocaNum - 1].sprite = image.sprite;
                vocaPulledImagesEasy[currentVocaNum - 1].rectTransform.localScale = new Vector3(.9f, .8f, 1f);
                vocaPulledImagesEasy[currentVocaNum - 1].transform.parent.GetComponent<Animator>().SetTrigger("Open");
                break;
            case Difficulty.normal:
                vocaPulledImagesMedium[currentVocaNum - 1].sprite = image.sprite;
                vocaPulledImagesMedium[currentVocaNum - 1].rectTransform.localScale = new Vector3(.9f, .8f, 1f);
                vocaPulledImagesMedium[currentVocaNum - 1].transform.parent.GetComponent<Animator>().SetTrigger("Open");
                break;
            case Difficulty.hard:
                vocaPulledImagesHard[currentVocaNum - 1].sprite = image.sprite;
                vocaPulledImagesHard[currentVocaNum - 1].rectTransform.localScale = new Vector3(.9f, .8f, 1f);
                vocaPulledImagesHard[currentVocaNum - 1].transform.parent.GetComponent<Animator>().SetTrigger("Open");
                break;
        }
    }
    public void SetNextVocaAfterPulledSucess()
    {
        currentVocaNum++;
        if(currentVocaNum == currentVocabularies.Count)
        {
            SetFxAfterEndLv();
            return;
        }
        currentVocabulary = currentVocabularies[currentVocaNum];
        AudioManager.instance.SetCurrentWordAudio(currentVocabulary.audio);
        PlayWordAudio();
    }
    protected override void ResetGameState()
    {
        base.ResetGameState();
        startTimer = true;
        currentVocaNum = 0;
        currentVocaOnEndLv = 0;
        leftArrowEndLv.SetActive(false);
        rightArrowEndLv.SetActive(true);
        onResetGameState?.Invoke();
    }
    private void ResetPulledImage()
    {
        switch(DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                foreach(Image im in vocaPulledImagesEasy)
                {
                    im.sprite = questionMarkSprite;
                    im.transform.localScale = new Vector2(.6f, .6f);
                }
                break;
            case Difficulty.normal:
                foreach (Image im in vocaPulledImagesMedium)
                {
                    im.sprite = questionMarkSprite;
                    im.transform.localScale = new Vector2(.6f, .6f);
                }
                break;
            case Difficulty.hard:
                foreach (Image im in vocaPulledImagesHard)
                {
                    im.sprite = questionMarkSprite;
                    im.transform.localScale = new Vector2(.6f, .6f);
                }
                break;
        }
    }
    public void SetFxAfterEndLv()
    {
        startTimer = false;
        scoreFx.gameObject.SetActive(true);
        titleEndLvText.text = "Wonderful!";
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                scoreFx.GetComponent<ScoreFx>().scoreText.text = "+10";
                break;
            case Difficulty.normal:
                scoreFx.GetComponent<ScoreFx>().scoreText.text = "+20";
                break;
            case Difficulty.hard:
                scoreFx.GetComponent<ScoreFx>().scoreText.text = "+30";
                break;
        }
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                foreach (Image im in vocaPulledImagesEasy)
                {
                    im.transform.parent.GetComponent<Animator>().SetTrigger("Open");
                }
                break;
            case Difficulty.normal:
                foreach (Image im in vocaPulledImagesMedium)
                {
                    im.transform.parent.GetComponent<Animator>().SetTrigger("Open");
                }
                break;
            case Difficulty.hard:
                foreach (Image im in vocaPulledImagesHard)
                {
                    im.transform.parent.GetComponent<Animator>().SetTrigger("Open");
                }
                break;
        }
        Invoke("EnablePassLvUI", 3.5f);
    }
    protected override void EnablePassLvUI()
    {
        AudioManager.instance.SetCurrentWordAudio(currentVocabularies[0].audio);
        base.EnablePassLvUI();
    }
    public void OnClickNextImageOnEndLv()
    {
        if (currentVocaOnEndLv < currentVocabularies.Count - 1)
        {
            leftArrowEndLv.SetActive(true);
            rightArrowEndLv.SetActive(true);
            currentVocaOnEndLv++;
            if(currentVocaOnEndLv == currentVocabularies.Count - 1)
                rightArrowEndLv.SetActive(false);
        }
        else
        {
            leftArrowEndLv.SetActive(true);
            rightArrowEndLv.SetActive(false);
            return;
        }
        vocaImageEndLv.sprite = currentVocabularies[currentVocaOnEndLv].image;
        vocaTextEndLv.text = currentVocabularies[currentVocaOnEndLv].vocabulary;
        vocaMeaningText.text = currentVocabularies[currentVocaOnEndLv].mean;
        AudioManager.instance.SetCurrentWordAudio(currentVocabularies[currentVocaOnEndLv].audio);
        PlayWordAudio();
    }
    public void OnClickPreImageOnEndLv()
    {
        if (currentVocaOnEndLv > 0)
        {
            leftArrowEndLv.SetActive(true);
            rightArrowEndLv.SetActive(true);
            currentVocaOnEndLv--;
            if (currentVocaOnEndLv == 0)
                leftArrowEndLv.SetActive(false);
        }
        else
        {
            leftArrowEndLv.SetActive(false);
            rightArrowEndLv.SetActive(true);
            return;
        }
        vocaImageEndLv.sprite = currentVocabularies[currentVocaOnEndLv].image;
        vocaTextEndLv.text = currentVocabularies[currentVocaOnEndLv].vocabulary;
        vocaMeaningText.text = currentVocabularies[currentVocaOnEndLv].mean;
        AudioManager.instance.SetCurrentWordAudio(currentVocabularies[currentVocaOnEndLv].audio);
        PlayWordAudio();
    }
    public override void OnClickOkExitOrReplayBtn()
    {
        base.OnClickOkExitOrReplayBtn();
        startTimer = false;
        //playerData data = Resources.Load<playerData>("playerData");
        //data.SetScoreGame2(score);
    }
    public void AddBonusTime() => timer += 15;
}
