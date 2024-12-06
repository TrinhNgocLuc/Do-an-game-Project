using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_SXChuCai : MonoBehaviour
{
    public static GameManager_SXChuCai instance { get; private set; }
    public int currentWordLength;
    public List<Sprite> allAlphabetSprites;
    public Vocabulary currentVocabulary;
    public int currentAlphabetNumOnSlot = 0;
    [Header("UI")]
    [SerializeField] private Image vocaImageGamePlay;
    [SerializeField] private Image vocaImageEndLv;
    [SerializeField] private TextMeshProUGUI vocaTextGamePlayTest;
    [SerializeField] private TextMeshProUGUI vocaTextEndLv;
    [SerializeField] private TextMeshProUGUI vocaMeaningText;
    [SerializeField] private Transform selectDiffUI;
    [SerializeField] private Transform endLvUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private List<Transform> hearts;
    [SerializeField] private Transform blurBlackScreen;
    [SerializeField] private Transform timeOutNoti;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform scoreFx;
    [SerializeField] private Transform addScoreFx;
    [SerializeField] private int score = 0;
    [SerializeField] private int addScore = 0;
    private int life = 3;
    private float timer = 0;
    private bool passLv = false;
    private bool timeOut = false;
    private bool outOfVoca = false;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        selectDiffUI.gameObject.SetActive(true);
        endLvUI.gameObject.SetActive(false);
        scoreFx.gameObject.SetActive(false);
        addScoreFx.gameObject.SetActive(false);
    }
    public void GetRandomEasyVocabulary()
    {
        currentVocabulary = VocabularyManager.instance.GetRandomEasyVocabulary();
        if (currentVocabulary == null)
        {
            outOfVoca = true;
            return;
        }
        GetVocabulary();
    }
    public void GetRandomMediumVocabulary()
    {
        currentVocabulary = VocabularyManager.instance.GetRandomMediumVocabulary();
        GetVocabulary();
    }
    public void GetRandomHardVocabulary()
    {
        currentVocabulary = VocabularyManager.instance.GetRandomHardVocabulary();
        GetVocabulary();
    }
    private void GetVocabulary()
    {
        vocaImageGamePlay.sprite = currentVocabulary.image;
        vocaImageEndLv.sprite = currentVocabulary.image;
        vocaTextGamePlayTest.text = currentVocabulary.vocabulary;
        vocaTextEndLv.text = currentVocabulary.vocabulary;
        vocaMeaningText.text = currentVocabulary.mean;
        currentWordLength = currentVocabulary.vocabulary.Length;
        AudioManager.instance.SetCurrentWordAudio(currentVocabulary.audio);
        PerfectWordHolder.instance.ActiveSlots();
        AlphabetHolder.instance.GetAlphabets();
    }
    private void Update()
    {
        if(timer > 0f && !timeOut && !endLvUI.gameObject.activeInHierarchy && !passLv)
        {
            timer -= Time.deltaTime;
        }
        timerText.text = (int)timer + "";
        scoreText.text = score+"";
        if(addScoreFx.gameObject.activeInHierarchy)
            addScoreFx.GetComponent<AddScoreFx>().addScoreText.text = "+"+addScore;
        CheckTimeOut();
    }
    private void CheckTimeOut()
    {
        if(!timeOut && timer <= 0f && !selectDiffUI.gameObject.activeInHierarchy && !passLv)
        {
            Debug.Log("Time Out!");
            timeOut = true;
            life--;
            blurBlackScreen.gameObject.SetActive(true);
            timeOutNoti.gameObject.SetActive(true);
            Invoke("DisappearAHeart", 1f);
        }
    }
    private void DisappearAHeart()
    {
        hearts[life].Find("Heart_RedFx").gameObject.SetActive(true);
    }
    public void OnSelectDifficulty(int diff)
    {
        switch(diff)
        {
            case 0:
                DifficultyManager.instance.Mode = Difficulty.easy;
                timer = 10f;
                GetRandomEasyVocabulary();
                break;
            case 1:
                DifficultyManager.instance.Mode = Difficulty.normal;
                timer = 20f;
                GetRandomMediumVocabulary();
                break;
            case 2:
                DifficultyManager.instance.Mode = Difficulty.hard;
                timer = 30f;
                GetRandomHardVocabulary();
                break;
        }
        selectDiffUI.gameObject.SetActive(false);
        AudioManager.instance.PlayCurrentWordAudio();
    }
    public Sprite GetSpriteByName(char alphabet)
    {
        Debug.Log(alphabet);
        foreach(Sprite sprite in allAlphabetSprites)
        {
            if(sprite.name == alphabet.ToString())
                return sprite;
        }
        return null;
    } 
    public Sprite GetRandomAlphabetSprite()
    {
        int rdIndex = Random.Range(1, allAlphabetSprites.Count);
        return allAlphabetSprites[rdIndex];
    }
    public bool CheckAlphabet(char alphabet, int index)
    {
        return currentVocabulary.vocabulary[index] == alphabet;
    }
    public void OnClickNextLv()
    {
        PerfectWordHolder.instance.ReturnAllAlphabetToHolder();
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                GetRandomEasyVocabulary();
                timer = 10f;
                break;
            case Difficulty.normal:
                GetRandomMediumVocabulary();
                timer = 20f;
                break;
            case Difficulty.hard:
                GetRandomHardVocabulary();
                timer = 30f;
                break;
        }
        if(outOfVoca)
        {
            // enable win UI
            return;
        }
        endLvUI.gameObject.SetActive(false);
        timeOutNoti.gameObject.SetActive(false);
        blurBlackScreen.gameObject.SetActive(false);
        currentAlphabetNumOnSlot = 0;
        timeOut = false;
        passLv = false;
        addScore = 0;
        AudioManager.instance.PlayCurrentWordAudio();
    }
    public void EnableEndGameUI() => endLvUI.gameObject.SetActive(true);
    public void CheckEndLv()
    {
        if (currentWordLength != currentAlphabetNumOnSlot)
            return;
        if (PerfectWordHolder.instance.CheckPerfectWordWhenFullSlot())
        {
            passLv = true;
            PassLvEffect();
            scoreFx.gameObject.SetActive(true);
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
            Invoke("EnablePassLvUI", 3.5f);
        }
    }
    public void PassLvEffect()
    {
        PerfectWordHolder.instance.CreateFx();
        Player.Instance.SetAnim("Victory");
    }
    private void EnablePassLvUI() => endLvUI.gameObject.SetActive(true);
    public void AddScore()
    {
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                score += 10;
                break;
            case Difficulty.normal:
                score += 20;
                break;
            case Difficulty.hard:
                score += 30;
                break;
        }
        addScoreFx.gameObject.SetActive(true);
        StartCoroutine(AddScoreByTimerRemain());
    }
    private IEnumerator AddScoreByTimerRemain()
    {
        float timerToSubstract = timer / 10f;
        while (timer > 0)
        {
            yield return new WaitForSeconds(.1f);
            int currentTimer = (int)timer;
            timer -= timerToSubstract;
            if ((int)timer < currentTimer)
                addScore += 1;
        }
    }
    public void AddBonusScore() => score += addScore;
}
