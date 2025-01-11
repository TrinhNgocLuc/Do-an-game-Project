using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class GameManager : MonoBehaviour
{
    protected int lv = 1;
    [SerializeField] protected int score = 0;
    [Header("UI")]
    [SerializeField] protected Transform selectDiffUI;
    [SerializeField] protected GameObject pauseUI;
    [SerializeField] protected Transform blurBlackScreen;
    [SerializeField] protected TextMeshProUGUI titleEndLvText;
    [SerializeField] protected GameObject exitOrReplayNoti;
    [SerializeField] protected TextMeshProUGUI exitOrReplayTitleTxt;
    [SerializeField] protected TextMeshProUGUI scoreNotiOnExitOrReplayNoti;
    [SerializeField] protected Transform endLvUI;
    [SerializeField] protected Transform gameOverUI;
    [SerializeField] protected TextMeshProUGUI timerText;
    [SerializeField] protected List<Transform> hearts;
    [SerializeField] protected Transform timeOutNoti;
    [SerializeField] protected TextMeshProUGUI scoreText;
    [SerializeField] protected Transform scoreFx;
    [SerializeField] protected Transform addScoreFx;
    [SerializeField] protected TextMeshProUGUI lvText;
    [SerializeField] protected Image vocaImageEndLv;
    [SerializeField] protected TextMeshProUGUI vocaTextEndLv;
    [SerializeField] protected TextMeshProUGUI vocaMeaningText;
    [SerializeField] protected Image vocaImageGOVUI;
    [SerializeField] protected TextMeshProUGUI vocaTextGOVUI;
    [SerializeField] protected TextMeshProUGUI vocaMeaningTextGOVUI;
    [SerializeField] protected int addScore = 0;
    public Vocabulary currentVocabulary;
    public List<Sprite> allAlphabetSprites;
    protected int life = 3;
    protected float timer = 0;
    protected bool timeOut = false;
    protected bool outOfVoca = false;
    protected bool startTimer = false;
    protected virtual void Start()
    {
        selectDiffUI.gameObject.SetActive(true);
        pauseUI.SetActive(false);
        endLvUI.gameObject.SetActive(false);
        scoreFx.gameObject.SetActive(false);
        addScoreFx.gameObject.SetActive(false);
        exitOrReplayNoti.SetActive(false);
    }
    protected virtual void Update()
    {
        if (timer > 0f && startTimer && !timeOut)
        {
            timer -= Time.deltaTime;
        }
        timerText.text = (int)timer + "";
        scoreText.text = score + "";
        scoreNotiOnExitOrReplayNoti.text = score + "";
        lvText.text = "Level " + lv;
        if (addScoreFx.gameObject.activeInHierarchy)
            addScoreFx.GetComponent<AddScoreFx>().addScoreText.text = "+" + addScore;
        CheckTimeOut();
    }
    protected void CheckTimeOut()
    {
        if (!timeOut && timer <= 0f && startTimer)
        {
            Debug.Log("Time Out!");
            timeOut = true;
            startTimer = false;
            life--;
            if(life <= 0)
            {
                AudioManager.instance.StopAllBgm();
                AudioManager.instance.PlaySfx(4);
            }

            blurBlackScreen.gameObject.SetActive(true);
            timeOutNoti.gameObject.SetActive(true);
            titleEndLvText.text = "Ouch!";
            Invoke("TimeOutFx", 1f);
        }
    }
    protected void TimeOutFx()
    {
        if (life > 0) 
        { 
            Player.Instance.SetAnim("Hit");
            AudioManager.instance.PlaySfx(5);
        }
        else
        {
            Player.Instance.SetAnim("Dead");
        }
        hearts[life].Find("Heart_RedFx").gameObject.SetActive(true);
    }
    public virtual void OnSelectDifficulty(int diff)
    {
        switch (diff)
        {
            case 0:
                DifficultyManager.instance.Mode = Difficulty.easy;
                break;
            case 1:
                DifficultyManager.instance.Mode = Difficulty.normal;
                break;
            case 2:
                DifficultyManager.instance.Mode = Difficulty.hard;
                break;
        }
        selectDiffUI.gameObject.SetActive(false);
        Player.Instance.SetAnim("Idle");
    }
    public virtual void OnClickNextLv()
    {
        endLvUI.gameObject.SetActive(false);
        timeOutNoti.gameObject.SetActive(false);
        blurBlackScreen.gameObject.SetActive(false);
        addScore = 0;
        AudioManager.instance.IncreaseBGMVolumeAfterContinueGame();
    }
    public void OnSelectExitBtn()
    {
        exitOrReplayTitleTxt.text = "Bạn muốn chơi lại?";
        exitOrReplayNoti.SetActive(true);
    }
    public void OnSelectQuitGameBtn()
    {
        exitOrReplayTitleTxt.text = "Bạn muốn thoát game?";
        exitOrReplayNoti.SetActive(true);
    }
    public virtual void OnClickOkExitOrReplayBtn()
    {
        endLvUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        selectDiffUI.gameObject.SetActive(true); 
        ResetGameState();
    }
    public void OnClickNoExitOrReplayBtn()
    {
        exitOrReplayNoti.SetActive(false);
    }
    protected virtual void EnablePassLvUI()
    {
        endLvUI.gameObject.SetActive(true);
        AudioManager.instance.DecreaseBGMVolumeWhilePausedGame();
        PlayWordAudio();
    }
    public virtual void EnableEndGameUI()
    {
        if (life != 0)
            endLvUI.gameObject.SetActive(true);
        else
        {
            gameOverUI.gameObject.SetActive(true);
        }
        AudioManager.instance.DecreaseBGMVolumeWhilePausedGame();
        PlayWordAudio();
    }
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
    protected IEnumerator AddScoreByTimerRemain()
    {
        int intTimer = (int)timer;
        while (timer > 0)
        {
            yield return new WaitForSeconds(.05f);
            timer -= 1;
            intTimer -= 1;
            if (intTimer >= 0)
                addScore += 1;
            if (timer < 0)
                timer = 0;
        }
    }
    public void PlayWordAudio() => AudioManager.instance.PlayCurrentWordAudio();
    public void AddBonusScore() => score += addScore;
    public void OnClickPauseGame()
    {
        if (!startTimer)
            return;
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
        AudioManager.instance.DecreaseBGMVolumeWhilePausedGame();
    }
    public void OnClickContinueGame()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        AudioManager.instance.IncreaseBGMVolumeAfterContinueGame();
    }
    protected virtual void ResetGameState()
    {
        life = 3;
        score = 0;
        lv = 1;
        addScore = 0;
        timeOut = false;
        timeOutNoti.gameObject.SetActive(false);
        blurBlackScreen.gameObject.SetActive(false);
        pauseUI.SetActive(false);
        exitOrReplayNoti.SetActive(false);
        Time.timeScale = 1;
        AudioManager.instance.IncreaseBGMVolumeAfterContinueGame();
        for (int i = 0; i < 3; i++)
        {
            hearts[i].GetComponent<Image>().color = Color.white;
            hearts[i].Find("Heart_RedFx").gameObject.SetActive(false);
            hearts[i].Find("Heart_BlurFx").gameObject.SetActive(false);
        }
    }
    public void OnClickBackToMainEnvironment()
    {
        SceneManager.LoadScene("Screen Main");
    }
}
