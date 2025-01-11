using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_SXChuCai : GameManager
{
    public static GameManager_SXChuCai instance { get; private set; }
    public int currentWordLength;
    public int currentAlphabetNumOnSlot = 0;
    [Header("UI")]
    [SerializeField] private Image vocaImageGamePlay;
    [SerializeField] private TextMeshProUGUI vocaTextGamePlayTest;
    [SerializeField] private List<GameObject> perfectWordHolders;
    [SerializeField] private BackgroundMoving bg;
    [SerializeField] private PlayPartUI playPartUI;
    
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
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
        vocaImageGOVUI.sprite = currentVocabulary.image;
        vocaTextGamePlayTest.text = currentVocabulary.vocabulary;
        vocaTextEndLv.text = currentVocabulary.vocabulary;
        vocaTextGOVUI.text = currentVocabulary.vocabulary;
        vocaMeaningText.text = currentVocabulary.mean;
        vocaMeaningTextGOVUI.text = currentVocabulary.mean;
        currentWordLength = currentVocabulary.vocabulary.Length;
        AudioManager.instance.SetCurrentWordAudio(currentVocabulary.audio);
        for(int i=0; i<=3; i++)
        {
            perfectWordHolders[i].SetActive(false);
        }
        if (DifficultyManager.instance.Mode != Difficulty.hard)
        {
            perfectWordHolders[0].SetActive(true);
            perfectWordHolders[0].GetComponent<PerfectWordHolder>().ActiveSlots();
        }
        else
        {
            for(int i=1; i<=GetCurrentVocaPartQuantity(); i++)
            {
                perfectWordHolders[i].SetActive(true);
                perfectWordHolders[i].GetComponent<PerfectWordHolder>().ActiveSlotsOnHardMode();
            }
        }
        AlphabetHolder.instance.GetAlphabets();
    }
    public int GetVocaPartLength(int partIndex)
    {
        return GetCurrentVocaPartsOnHardMode()[partIndex-1].Length;
    }
    private string[] GetCurrentVocaPartsOnHardMode()
    {
        string[] vocaParts = currentVocabulary.vocabulary.Split(new string[] { " " }, System.StringSplitOptions.None);
        return vocaParts;
    }
    public int GetCurrentVocaPartQuantity()
    {   
        Debug.Log(GetCurrentVocaPartsOnHardMode().Length);
        return GetCurrentVocaPartsOnHardMode().Length;
    }
    public override void OnSelectDifficulty(int diff)
    {
        base.OnSelectDifficulty(diff);
        switch(diff)
        {
            case 0:
                timer = 16f;
                GetRandomEasyVocabulary();
                break;
            case 1:
                timer = 26f;
                GetRandomMediumVocabulary();
                break;
            case 2:
                timer = 41f;
                GetRandomHardVocabulary();
                break;
        }
        VocabularyManager.instance.ResetVocabulariesRemain();
        SetRunToNextLvState();
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
    public bool CheckAlphabet(char alphabet, int index, int holderLocation)
    {
        if (holderLocation != 0)
        {
            return this.GetCurrentVocaPartsOnHardMode()[holderLocation-1][index] == alphabet;
        }
        string voca = currentVocabulary.vocabulary;
        return voca[index] == alphabet;
    }
    public override void OnClickNextLv()
    {
        base.OnClickNextLv();
        currentAlphabetNumOnSlot = 0;
        for(int i=0; i<=3; i++)
        {
            perfectWordHolders[i].GetComponent<PerfectWordHolder>().ReturnAllAlphabetToHolder();
        }
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                GetRandomEasyVocabulary();
                timer = 16f;
                break;
            case Difficulty.normal:
                GetRandomMediumVocabulary();
                timer = 26f;
                break;
            case Difficulty.hard:
                GetRandomHardVocabulary();
                timer = 41f;
                break;
        }
        if(outOfVoca)
        {
            // enable win UI
            return;
        }
        if (!timeOut)
        {
            lv++;
            SetRunToNextLvState();
        }    
        else
        {
            SetPlayState();
            timeOut = false;
        }
    }
    private bool CheckPerfectWordWhenFullSlot()
    {
        if(DifficultyManager.instance.Mode != Difficulty.hard) 
            return perfectWordHolders[0].GetComponent<PerfectWordHolder>().CheckPerfectWordWhenFullSlot();
        else
        {
            for(int i=1;i<=GetCurrentVocaPartQuantity(); i++)
            {
                if (!perfectWordHolders[i].GetComponent<PerfectWordHolder>().CheckPerfectWordWhenFullSlot())
                {
                    Debug.Log("hàng "+i+" sai!");        
                    return false;
                }
            }
            return true;
        }
    }
    public void CheckEndLv()
    {
        if (currentWordLength != currentAlphabetNumOnSlot && DifficultyManager.instance.Mode != Difficulty.hard)
            return;
        if (this.CheckPerfectWordWhenFullSlot())
        {
            startTimer = false;
            PassLvEffect();
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
            Invoke("EnablePassLvUI", 3.5f);
        }
    }
    public void PassLvEffect()
    {
        for (int i = 0; i <= 3; i++)
        {
            perfectWordHolders[i].GetComponent<PerfectWordHolder>().CreateFx();
        }
        Player.Instance.SetAnim("Victory");
    }
    protected override void EnablePassLvUI()
    {
        base.EnablePassLvUI();
        playPartUI.SetOnState(false);
    }
    protected override void ResetGameState()
    {
        base.ResetGameState();
        startTimer = false;
        currentAlphabetNumOnSlot = 0;
        playPartUI.SetOnState(false);
        for (int i = 0; i <= 3; i++)
        {
            perfectWordHolders[i].GetComponent<PerfectWordHolder>().ReturnAllAlphabetToHolder();
        }
    }
    private void SetRunToNextLvState()
    {
        startTimer = false;
        playPartUI.SetOnState(false);
        Player.Instance.SetAnim("Run");
        bg.MoveBG();
        Invoke("SetPlayState", 2f);
    }
    private void SetPlayState()
    {
        startTimer = true;
        playPartUI.SetOnState(true);
        Player.Instance.SetAnim("Idle");
        bg.StopMoveBG();
        Invoke("PlayWordAudio", .5f);
    }
    public override void OnClickOkExitOrReplayBtn()
    {
        base.OnClickOkExitOrReplayBtn();
        playerData data = Resources.Load<playerData>("playerData");
        data.SetScoreGame1(score);
    }
}
