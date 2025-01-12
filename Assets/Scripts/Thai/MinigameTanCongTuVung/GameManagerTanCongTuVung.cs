using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManagerTanCongTuVung : GameManager
{
    public static GameManagerTanCongTuVung Instance;

    [SerializeField] private List<Sprite> allVocaSpriteOnARound = new List<Sprite>();
    [SerializeField] private List<Sprite> vocasSpriteRemainOnARound = new List<Sprite>();
    [SerializeField] private List<string> allVocaTextOnARound = new List<string>();
    [SerializeField] private List<string> vocasTextRemainOnARound = new List<string>();
    [SerializeField] private float spawnImageCd;
    [SerializeField] private Transform perfectChecker;
    [SerializeField] private GameObject encouragingText;
    [SerializeField] private Transform vocaQuizHolder;
    [SerializeField] private Transform textHolder;
    [SerializeField] private Transform imageHolder;
    [SerializeField] private playerData playerScore;
    [SerializeField] private GameObject rank;
    [SerializeField] private GameObject gamePlay;
    [SerializeField] private AudioSource SFXbutton;
    private bool statusRank = false;
    private int allVocaNum = 0;
    private int spawnedNum = 0;
    private bool finishVocaText = false;
    private bool canAttack = true;
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
        SetLogicForNewLv(diff);
        AudioManager.instance.PlayBgm(3);
    }
    private void GetVocabularyForEndLv()
    {
        vocaImageEndLv.sprite = currentVocabulary.image;
        vocaImageGOVUI.sprite = currentVocabulary.image;
        vocaTextEndLv.text = currentVocabulary.vocabulary;
        vocaTextGOVUI.text = currentVocabulary.vocabulary;
        vocaMeaningText.text = currentVocabulary.mean;
        vocaMeaningTextGOVUI.text = currentVocabulary.mean;
    }
    private void SetLogicForNewLv(int diff)
    {
        allVocaSpriteOnARound.Clear();
        allVocaTextOnARound.Clear();
        spawnedNum = 0;
        finishVocaText = false;
        canAttack = true;
        if (!timeOut)
            lv++;
        else
            timeOut = false;
        startTimer = true;
        CancelInvoke("SpawnVocaText");
        CancelInvoke("SpawnVocaSprite");
        Player.Instance.SetAnim("Idle");
        Player.Instance.Flip(false);
        encouragingText.SetActive(false);
        textHolder.GetComponent<CanvasGroup>().alpha = 1.0f;
        imageHolder.GetComponent<CanvasGroup>().alpha = 1.0f;
        int rdRightVocaIndex;
        foreach (Transform tran in ImageGameplaySpawner.Instance.holder)
        {
            if (!tran.gameObject.activeInHierarchy || (tran.name != "VocaImage(Clone)" && tran.name != "VocaText(Clone)"))
                continue;
            ImageGameplaySpawner.Instance.Despawn(tran);
        }
        switch (diff)
        {
            case 0:
                timer = 30f;
                allVocaNum = 7;
                spawnImageCd = 1f;
                rdRightVocaIndex = Random.Range(0, allVocaNum);
                currentVocabulary = VocabularyManager.instance.GetRandomEasyVocabulary();
                for (int i = 0; i < allVocaNum; i++)
                {
                    if (i == rdRightVocaIndex)
                    {
                        allVocaSpriteOnARound.Add(currentVocabulary.image);
                        allVocaTextOnARound.Add(currentVocabulary.vocabulary);
                        continue;
                    }
                    Vocabulary newVoca = VocabularyManager.instance.GetRandomEasyVocabulary();
                    allVocaSpriteOnARound.Add(newVoca.image);
                    allVocaTextOnARound.Add(newVoca.vocabulary);
                }
                break;
            case 1:
                timer = 40f;
                allVocaNum = 10;
                spawnImageCd = .9f;
                rdRightVocaIndex = Random.Range(0, allVocaNum);
                currentVocabulary = VocabularyManager.instance.GetRandomMediumVocabulary();
                for (int i = 0; i < allVocaNum; i++)
                {
                    if (i == rdRightVocaIndex)
                    {
                        allVocaSpriteOnARound.Add(currentVocabulary.image);
                        allVocaTextOnARound.Add(currentVocabulary.vocabulary);
                        continue;
                    }
                    Vocabulary newVoca = VocabularyManager.instance.GetRandomMediumVocabulary();
                    allVocaSpriteOnARound.Add(newVoca.image);
                    allVocaTextOnARound.Add(newVoca.vocabulary);
                }
                break;
            case 2:
                timer = 50f;
                allVocaNum = 13;
                spawnImageCd = .8f;
                rdRightVocaIndex = Random.Range(0, allVocaNum);
                currentVocabulary = VocabularyManager.instance.GetRandomHardVocabulary();
                for (int i = 0; i < allVocaNum; i++)
                {
                    if (i == rdRightVocaIndex)
                    {
                        allVocaSpriteOnARound.Add(currentVocabulary.image);
                        allVocaTextOnARound.Add(currentVocabulary.vocabulary);
                        continue;
                    }
                    Vocabulary newVoca = VocabularyManager.instance.GetRandomHardVocabulary();
                    allVocaSpriteOnARound.Add(newVoca.image);
                    allVocaTextOnARound.Add(newVoca.vocabulary);
                }
                break;
        }
        GetVocabularyForEndLv();
        DisableQuizHolderText();
        vocasSpriteRemainOnARound = new List<Sprite>(allVocaSpriteOnARound);
        vocasTextRemainOnARound = new List<string>(allVocaTextOnARound);
        InvokeRepeating("SpawnVocaText", spawnImageCd, spawnImageCd);
        AudioManager.instance.SetCurrentWordAudio(currentVocabulary.audio);
        PlayWordAudio();
    }
    public override void OnClickNextLv()
    {
        base.OnClickNextLv();
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                SetLogicForNewLv(0);
                break;
            case Difficulty.normal:
                SetLogicForNewLv(1);
                break;
            case Difficulty.hard:
                SetLogicForNewLv(2);
                break;
        }
    }
    private void DisableQuizHolderText()
    {
        vocaQuizHolder.Find("TextEasy").gameObject.SetActive(false);
        vocaQuizHolder.Find("TextMedium").gameObject.SetActive(false);
        vocaQuizHolder.Find("TextHard").gameObject.SetActive(false);
        vocaQuizHolder.Find("QstMark").gameObject.SetActive(true);
    }
    private void GetNewVocasSpriteListForNewRound()
    {
        vocasSpriteRemainOnARound.Clear();
        List<int> indexOfvocasRemain = new List<int>();
        for (int i = 0; i < allVocaNum; i++)
        {
            indexOfvocasRemain.Add(i);
        }
        while (indexOfvocasRemain.Count >= 1)
        {
            int rdIndex = Random.Range(0, indexOfvocasRemain.Count);
            vocasSpriteRemainOnARound.Add(allVocaSpriteOnARound[indexOfvocasRemain[rdIndex]]);
            indexOfvocasRemain.Remove(indexOfvocasRemain[rdIndex]);
        }
    }
    private void GetNewVocasTextListForNewRound()
    {
        vocasTextRemainOnARound.Clear();
        List<int> indexOfvocasRemain = new List<int>();
        for (int i = 0; i < allVocaNum; i++)
        {
            indexOfvocasRemain.Add(i);
        }
        while (indexOfvocasRemain.Count >= 1)
        {
            int rdIndex = Random.Range(0, indexOfvocasRemain.Count);
            vocasTextRemainOnARound.Add(allVocaTextOnARound[indexOfvocasRemain[rdIndex]]);
            indexOfvocasRemain.Remove(indexOfvocasRemain[rdIndex]);
        }
    }
    public Sprite GetSpriteFromVocaList()
    {
        Sprite result = vocasSpriteRemainOnARound[spawnedNum];
        spawnedNum++;
        if (spawnedNum >= allVocaNum)
        {
            GetNewVocasSpriteListForNewRound();
            spawnedNum = 0;
        }
        return result;
    }
    public string GetTextFromVocaList()
    {
        string result = vocasTextRemainOnARound[spawnedNum];
        spawnedNum++;
        if (spawnedNum >= allVocaNum)
        {
            GetNewVocasTextListForNewRound();
            spawnedNum = 0;
        }
        return result;
    }
    private void SpawnVocaSprite()
    {
        ImageGameplaySpawner.Instance.SpawnVocaImage();
    }
    private void SpawnVocaText()
    {
        ImageGameplaySpawner.Instance.SpawnVocaText();
    }
    public void OnClickAttack()
    {
        if (!canAttack || scoreFx.gameObject.activeInHierarchy || addScoreFx.gameObject.activeInHierarchy)
            return;
        Player.Instance.SetAnim("Attack");
        Transform nearest = null;
        float distanceY = 999f;
        if (!finishVocaText)
        {
            nearest = GetVocaNearest("VocaText(Clone)");
            if (nearest == null)
                return;
            distanceY = Mathf.Abs(nearest.localPosition.y - perfectChecker.localPosition.y);
            if (distanceY > 125f)
                return;
            if (currentVocabulary.vocabulary != nearest.Find("Text").GetComponent<TextMeshProUGUI>().text)
            {
                SetFxWhenAttackWrongVoca(nearest);
                return;
            }
        }
        else
        {
            nearest = GetVocaNearest("VocaImage(Clone)");
            if (nearest == null)
                return;
            distanceY = Mathf.Abs(nearest.localPosition.y - perfectChecker.localPosition.y);
            if (distanceY > 125f)
                return;
            if (currentVocabulary.image != nearest.Find("Image").GetComponent<Image>().sprite)
            {
                SetFxWhenAttackWrongVoca(nearest);
                return;
            }
        }
        encouragingText.SetActive(true);
        if (distanceY < 125f && distanceY > 75f)
        {
            encouragingText.GetComponent<TextMeshProUGUI>().text = "Good!";
        } else if (distanceY < 75f && distanceY > 30f)
        {
            encouragingText.GetComponent<TextMeshProUGUI>().text = "Great!!";
        } else if (distanceY < 30f)
        {
            encouragingText.GetComponent<TextMeshProUGUI>().text = "Perfect!!";
        }
        nearest.Find("ExplosionFx").gameObject.SetActive(true);
        AudioManager.instance.PlaySfx(0);
        if (!finishVocaText)
        {
            vocaQuizHolder.Find("QstMark").gameObject.SetActive(false);
            switch (DifficultyManager.instance.Mode)
            {
                case Difficulty.easy:
                    vocaQuizHolder.Find("TextEasy").GetComponent<TextMeshProUGUI>().text = currentVocabulary.vocabulary;
                    vocaQuizHolder.Find("TextEasy").gameObject.SetActive(true);
                    break;
                case Difficulty.normal:
                    vocaQuizHolder.Find("TextMedium").GetComponent<TextMeshProUGUI>().text = currentVocabulary.vocabulary;
                    vocaQuizHolder.Find("TextMedium").gameObject.SetActive(true);
                    break;
                case Difficulty.hard:
                    vocaQuizHolder.Find("TextHard").GetComponent<TextMeshProUGUI>().text = currentVocabulary.vocabulary;
                    vocaQuizHolder.Find("TextHard").gameObject.SetActive(true);
                    break;
            }
            StopSpawningAndMovingVocaText();
        } else
        {
            StopSpawningAndMovingVocaSprite();
        }
    }
    private void StopSpawningAndMovingVocaText()
    {
        CancelInvoke("SpawnVocaText");
        foreach(Transform tran in ImageGameplaySpawner.Instance.holder)
        {
            if (!tran.gameObject.activeInHierarchy || tran.name != "VocaText(Clone)")
                continue;
            tran.GetComponent<CardMovementable>().StopMoving();
            tran.GetComponent<CardMovementable>().CancelInvokeRunning();
        }
        Invoke("SetNextAttackVocaSprite", 1f);
    }
    private void SetNextAttackVocaSprite()
    {
        finishVocaText = true;
        Player.Instance.Flip(true);
        textHolder.GetComponent<CanvasGroup>().alpha = .5f;
        foreach (Transform tran in ImageGameplaySpawner.Instance.holder)
        {
            if (!tran.gameObject.activeInHierarchy || tran.name != "VocaText(Clone)")
                continue;
            tran.GetComponent<CanvasGroup>().alpha = .5f;
        }
        InvokeRepeating("SpawnVocaSprite", spawnImageCd, spawnImageCd);
    }
    private void StopSpawningAndMovingVocaSprite()
    {
        CancelInvoke("SpawnVocaSprite");
        foreach (Transform tran in ImageGameplaySpawner.Instance.holder)
        {
            if (!tran.gameObject.activeInHierarchy || tran.name != "VocaImage(Clone)")
                continue;
            tran.GetComponent<CardMovementable>().StopMoving();
            tran.GetComponent<CardMovementable>().CancelInvokeRunning();
        }
        Invoke("SetFxAfterEndLv", 1f);
    }
    public void SetFxAfterEndLv()
    {
        imageHolder.GetComponent<CanvasGroup>().alpha = .5f;
        foreach (Transform tran in ImageGameplaySpawner.Instance.holder)
        {
            if (!tran.gameObject.activeInHierarchy || tran.name != "VocaImage(Clone)")
                continue;
            tran.GetComponent<CanvasGroup>().alpha = .5f;
        }
        startTimer = false;
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
    private Transform GetVocaNearest(string vocaObjName)
    {
        float minDistance = 9999f;
        Transform result = null;
        foreach (Transform tran in ImageGameplaySpawner.Instance.holder)
        {
            if (!tran.gameObject.activeInHierarchy || tran.name != vocaObjName)
                continue;
            if (Vector2.Distance(tran.position, perfectChecker.position) < minDistance)
            {
                minDistance = Vector2.Distance(tran.position, perfectChecker.position);
                result = tran;
            }
        }
        return result;
    }
    private void SetFxWhenAttackWrongVoca(Transform nearestVoca)
    {
        canAttack = false;
        Player.Instance.SetAnim("Stun");
        AudioManager.instance.PlaySfx(7);
        Invoke("SetPlayerBackToIdle", 2f);
        nearestVoca.Find("RedFx").gameObject.SetActive(true);
        encouragingText.gameObject.SetActive(true);
        encouragingText.GetComponent<TextMeshProUGUI>().text = "Ouch!";
    }
    private void SetPlayerBackToIdle()
    {
        Player.Instance.SetAnim("Idle");
        canAttack = true;
    }
    public override void OnClickOkExitOrReplayBtn()
    {

        startTimer = false;
        if (playerScore.scoreGame5 < score)
        {
            playerScore.SetScoreGame5(score);
        }
        base.OnClickOkExitOrReplayBtn();
    }
    public void controllRank()
    {
        SFXbutton.Play();
        statusRank = !statusRank;
        if (statusRank == true)
        {
            rank.SetActive(true);
            gamePlay.SetActive(false);

        }
        else if (statusRank == false)
        {
            rank.SetActive(false);
            gamePlay.SetActive(true);

        }
    }
}
