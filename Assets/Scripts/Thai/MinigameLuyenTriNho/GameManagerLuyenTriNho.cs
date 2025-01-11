using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLuyenTriNho : GameManager
{
    public static GameManagerLuyenTriNho Instance;
    [SerializeField] private List<Vocabulary> currentVocabularies = new List<Vocabulary>();
    [SerializeField] private GameObject cardHolderEasy;
    [SerializeField] private GameObject cardHolderMedium;
    [SerializeField] private GameObject cardHolderHard;
    [SerializeField] private List<Image> vocaImageCards = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> vocaTextCards = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject rightArrowEndLv;
    [SerializeField] private GameObject leftArrowEndLv;
    [SerializeField] private GameObject rightArrowGoUI;
    [SerializeField] private GameObject leftArrowGoUI;
    private List<int> vocaIndexsRemain = new List<int>();
    private Transform card1_Selected = null;
    private Transform card2_Selected = null;
    private int finishCardPairNum = 0;
    private int currentVocaOnEndLv = 0;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public override void OnSelectDifficulty(int diff)
    {
        base.OnSelectDifficulty(diff);
        SpawnRandomVocaPairs();
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                timer = 20f;
                break;
            case Difficulty.normal:
                timer = 30f;
                break;
            case Difficulty.hard:
                timer = 40f;
                break;
        }
        startTimer = true;
        SetVocaForEndLvAndGoUI();
        leftArrowEndLv.SetActive(false);
        rightArrowEndLv.SetActive(true);
        leftArrowGoUI.SetActive(false);
        rightArrowGoUI.SetActive(true);
        AudioManager.instance.PlayBgm(2);
        card1_Selected = null;
        card2_Selected = null;
    }
    protected override void ResetGameState()
    {
        base.ResetGameState();
        finishCardPairNum = 0;
        currentVocaOnEndLv = 0;
        ResetCardState();
    }

    private void ResetCardState()
    {
        foreach (Image image in vocaImageCards)
        {
            image.transform.parent.GetComponent<Card>().ResetStateOnResetGame();
        }
    }

    public override void OnClickNextLv()
    {
        base.OnClickNextLv();
        ResetCardState();
        SpawnRandomVocaPairs();
        SetVocaForEndLvAndGoUI();
        leftArrowEndLv.SetActive(false);
        rightArrowEndLv.SetActive(true);
        leftArrowGoUI.SetActive(false);
        rightArrowGoUI.SetActive(true);
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                timer = 20f;
                break;
            case Difficulty.normal:
                timer = 30f;
                break;
            case Difficulty.hard:
                timer = 40f;
                break;
        }
        if (!timeOut)
            lv++;
        else
            timeOut = false;
        startTimer = true;
        card1_Selected = null;
        card2_Selected = null;
        currentVocaOnEndLv = 0;
        finishCardPairNum = 0;
    }
    protected void SetVocaForEndLvAndGoUI()
    {
        vocaImageEndLv.sprite = currentVocabularies[0].image;
        vocaTextEndLv.text = currentVocabularies[0].vocabulary;
        vocaMeaningText.text = currentVocabularies[0].mean;

        vocaImageGOVUI.sprite = currentVocabularies[0].image;
        vocaTextGOVUI.text = currentVocabularies[0].vocabulary;
        vocaMeaningTextGOVUI.text = currentVocabularies[0].mean;
        AudioManager.instance.SetCurrentWordAudio(currentVocabularies[0].audio);
    }
    private void SpawnRandomVocaPairs()
    {
        currentVocabularies.Clear();
        switch (DifficultyManager.instance.Mode) 
        {
            case Difficulty.easy:
                ActiveHolderByDifficulty(1);
                for (int i=0; i<4; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomEasyVocabulary());
                }
                AssignVocaImages(8);
                break;
            case Difficulty.normal:
                ActiveHolderByDifficulty(2);
                for (int i = 0; i < 5; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomMediumVocabulary());
                }
                AssignVocaImages(10);
                break;
            case Difficulty.hard:
                ActiveHolderByDifficulty(3);
                for (int i = 0; i < 6; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomHardVocabulary());
                }
                AssignVocaImages(12);
                break;
        }
    }
    private void ActiveHolderByDifficulty(int diff)
    {
        if(diff == 1)
        {
            cardHolderEasy.SetActive(true);
            cardHolderMedium.SetActive(false);
            cardHolderHard.SetActive(false);
        } else if(diff == 2) 
        {
            cardHolderEasy.SetActive(false);
            cardHolderMedium.SetActive(true);
            cardHolderHard.SetActive(false);
        } else
        {
            cardHolderEasy.SetActive(false);
            cardHolderMedium.SetActive(false);
            cardHolderHard.SetActive(true);
        }
        GetVocasUI(diff);
    }
    private void GetVocasUI(int diff)
    {
        vocaImageCards.Clear();
        vocaTextCards.Clear();
        if (diff == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                vocaImageCards.Add(cardHolderEasy.transform.GetChild(i).Find("Image").GetComponent<Image>());
                vocaTextCards.Add(cardHolderEasy.transform.GetChild(i).Find("Text").GetComponent<TextMeshProUGUI>());
            }
        }
        else if (diff == 2)
        {
            for (int i = 0; i < 10; i++)
            {
                vocaImageCards.Add(cardHolderMedium.transform.GetChild(i).Find("Image").GetComponent<Image>());
                vocaTextCards.Add(cardHolderMedium.transform.GetChild(i).Find("Text").GetComponent<TextMeshProUGUI>());
            }
        }
        else
        {
            for (int i = 0; i < 12; i++)
            {
                vocaImageCards.Add(cardHolderHard.transform.GetChild(i).Find("Image").GetComponent<Image>());
                vocaTextCards.Add(cardHolderHard.transform.GetChild(i).Find("Text").GetComponent<TextMeshProUGUI>());
            }
        }

    }
    private void AssignVocaImages(int num)
    {
        vocaIndexsRemain.Clear();
        for (int i = 0; i < num; i++)
        {
            vocaIndexsRemain.Add(i);
        }
        for (int i = 0; i < num / 2; i++)
        {
            int rdId = Random.Range(0, vocaIndexsRemain.Count);
            vocaImageCards[vocaIndexsRemain[rdId]].sprite = currentVocabularies[i].image;
            vocaImageCards[vocaIndexsRemain[rdId]].gameObject.SetActive(true);
            vocaImageCards[vocaIndexsRemain[rdId]].transform.localScale = new Vector2(.9f, .8f);
            vocaTextCards[vocaIndexsRemain[rdId]].gameObject.SetActive(false);
            vocaIndexsRemain.Remove(vocaIndexsRemain[rdId]);
            rdId = Random.Range(0, vocaIndexsRemain.Count);
            vocaTextCards[vocaIndexsRemain[rdId]].text = currentVocabularies[i].vocabulary;
            vocaTextCards[vocaIndexsRemain[rdId]].gameObject.SetActive(true);
            vocaImageCards[vocaIndexsRemain[rdId]].gameObject.SetActive(false);
            vocaIndexsRemain.Remove(vocaIndexsRemain[rdId]);
        }
    }
    public void OnClickCard(Transform cardClicked)
    {
        if ((card1_Selected != null && card2_Selected != null) || cardClicked.GetComponent<Card>().isFlipped
            || card1_Selected == cardClicked)
            return;
        cardClicked.GetComponent<Card>().SetOpenCardAnim();
        if (cardClicked.Find("Text").gameObject.activeInHierarchy)
            PlayClickedVocaTextAudio(cardClicked.Find("Text").GetComponent<TextMeshProUGUI>().text);
        if (card1_Selected == null)
            card1_Selected = cardClicked;
        else
            card2_Selected = cardClicked;
        if (card1_Selected != null && card2_Selected != null)
        {
            GameObject card1Image = card1_Selected.Find("Image").gameObject;
            GameObject card2Image = card2_Selected.Find("Image").gameObject;
            GameObject card1Text = card1_Selected.Find("Text").gameObject;
            GameObject card2Text = card2_Selected.Find("Text").gameObject;
            if ((card1Image.activeInHierarchy && card2Image.activeInHierarchy)
                || (card1Text.activeInHierarchy && card2Text.activeInHierarchy))
            {
                OnSelectWrongCardPair();
            }
            else
                CheckSelectedCardPair(card1Image, card2Image, card1Text, card2Text);
        }
    }
    private void PlayClickedVocaTextAudio(string vocaText)
    {
        foreach(Vocabulary voca in currentVocabularies)
        {
            if(voca.vocabulary == vocaText)
            {
                AudioManager.instance.SetCurrentWordAudio(voca.audio);
                break;
            }
        }
        PlayWordAudio();
    }
    private void OnSelectWrongCardPair()
    {
        card1_Selected.GetComponent<Card>().EnableRedFx();
        card2_Selected.GetComponent<Card>().EnableRedFx();    
        Invoke("CardHolderImpulseFx", .5f);
        Invoke("CloseWrongCardPair", .75f);
    }
    private void CheckSelectedCardPair(GameObject card1Image, GameObject card2Image, GameObject card1Text, GameObject card2Text)
    {
        Image selectedImage = null;
        string selectedText = null;
        bool result = false;
        if (card1Image.activeInHierarchy)
        {
            selectedImage = card1Image.GetComponent<Image>();
            selectedText = card2Text.GetComponent<TextMeshProUGUI>().text;
        }
        else
        {
            selectedImage = card2Image.GetComponent<Image>();
            selectedText = card1Text.GetComponent<TextMeshProUGUI>().text;
        }
        foreach (Vocabulary voca in currentVocabularies)
        {
            if (voca.image == selectedImage.sprite)
            {
                if (voca.vocabulary == selectedText)
                {
                    result = true;
                    break;
                }
            }
        }
        if (result)
        {
            card1_Selected.GetComponent<Card>().SetOpenedState();
            card2_Selected.GetComponent<Card>().SetOpenedState();
            card1_Selected = null;
            card2_Selected = null;
            finishCardPairNum++;
            if (finishCardPairNum < currentVocabularies.Count)
            {
                Player.Instance.SetAnim("Pick");
                AudioManager.instance.PlaySfx(0);
            } else 
            {
                SetFxAfterEndLv();
            }
        }
        else
        {
            OnSelectWrongCardPair();
        }
    }
    private void CloseWrongCardPair()
    {
        card1_Selected.GetComponent<Card>().SetCloseCardAnim();
        card2_Selected.GetComponent<Card>().SetCloseCardAnim();
        card1_Selected = null;
        card2_Selected = null;
    }
    private void CardHolderImpulseFx()
    {
        card1_Selected.parent.GetComponent<Animator>().SetTrigger("Impulse");
        AudioManager.instance.PlaySfx(6);
    }
    public void SetFxAfterEndLv()
    {
        startTimer = false;
        finishCardPairNum = 0;
        scoreFx.gameObject.SetActive(true);
        titleEndLvText.text = "Wonderful!";
        Player.Instance.SetAnim("Victory");
        AudioManager.instance.PlaySfx(1);
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
    protected override void EnablePassLvUI()
    {
        AudioManager.instance.SetCurrentWordAudio(currentVocabularies[0].audio);
        base.EnablePassLvUI();
    }
    public override void EnableEndGameUI()
    {
        AudioManager.instance.SetCurrentWordAudio(currentVocabularies[0].audio);
        base.EnableEndGameUI();
    }
    public override void OnClickOkExitOrReplayBtn()
    {
        base.OnClickOkExitOrReplayBtn();
        startTimer = false;
    }
    public void OnClickNextImageOnEndLv()
    {
        if (currentVocaOnEndLv < currentVocabularies.Count - 1)
        {
            leftArrowEndLv.SetActive(true);
            rightArrowEndLv.SetActive(true);
            leftArrowGoUI.SetActive(true);
            rightArrowGoUI.SetActive(true);
            currentVocaOnEndLv++;
            if (currentVocaOnEndLv == currentVocabularies.Count - 1)
            {
                rightArrowEndLv.SetActive(false);
                rightArrowGoUI.SetActive(false);
            }
        }
        else
        {
            leftArrowEndLv.SetActive(true);
            rightArrowEndLv.SetActive(false);
            leftArrowGoUI.SetActive(true);
            rightArrowGoUI.SetActive(false);
            return;
        }
        vocaImageEndLv.sprite = currentVocabularies[currentVocaOnEndLv].image;
        vocaTextEndLv.text = currentVocabularies[currentVocaOnEndLv].vocabulary;
        vocaMeaningText.text = currentVocabularies[currentVocaOnEndLv].mean;

        vocaImageGOVUI.sprite = currentVocabularies[currentVocaOnEndLv].image;
        vocaTextGOVUI.text = currentVocabularies[currentVocaOnEndLv].vocabulary;
        vocaMeaningTextGOVUI.text = currentVocabularies[currentVocaOnEndLv].mean;
        AudioManager.instance.SetCurrentWordAudio(currentVocabularies[currentVocaOnEndLv].audio);
        PlayWordAudio();
    }
    public void OnClickPreImageOnEndLv()
    {
        if (currentVocaOnEndLv > 0)
        {
            leftArrowEndLv.SetActive(true);
            rightArrowEndLv.SetActive(true);
            leftArrowGoUI.SetActive(true);
            rightArrowGoUI.SetActive(true);
            currentVocaOnEndLv--;
            if (currentVocaOnEndLv == 0)
            {
                leftArrowEndLv.SetActive(false);
                leftArrowGoUI.SetActive(false);
            }
        }
        else
        {
            leftArrowEndLv.SetActive(false);
            rightArrowEndLv.SetActive(true);
            leftArrowGoUI.SetActive(false);
            rightArrowGoUI.SetActive(true);
            return;
        }
        vocaImageEndLv.sprite = currentVocabularies[currentVocaOnEndLv].image;
        vocaTextEndLv.text = currentVocabularies[currentVocaOnEndLv].vocabulary;
        vocaMeaningText.text = currentVocabularies[currentVocaOnEndLv].mean;

        vocaImageGOVUI.sprite = currentVocabularies[currentVocaOnEndLv].image;
        vocaTextGOVUI.text = currentVocabularies[currentVocaOnEndLv].vocabulary;
        vocaMeaningTextGOVUI.text = currentVocabularies[currentVocaOnEndLv].mean;
        AudioManager.instance.SetCurrentWordAudio(currentVocabularies[currentVocaOnEndLv].audio);
        PlayWordAudio();
    }
}
