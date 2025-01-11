using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private GameObject gameOverPanel, mainMenuPanel, gameMenuPanel;
    [SerializeField] private Color correctCol, wrongCol, normalCol;
    [SerializeField] private Image questionImage;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;
    [SerializeField] private AudioSource questionAudio;
    [SerializeField] private TMPro.TextMeshProUGUI questionText, scoreText, timerText;
    [SerializeField] private List<Button> options, uiButtons;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private AudioClip touchSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject Star1;
    [SerializeField] private GameObject Star2;
    [SerializeField] private GameObject Star3;

    [SerializeField] private MusicManager musicManager;
    [SerializeField] private playerData playerdata;

    [SerializeField] private GameObject numberPrefab;  // Prefab của UI Image cho số
    [SerializeField] private Transform scoreContainer; // GameObject chứa các ảnh số
    [SerializeField] private Sprite[] numberSprites;   // Các sprite từ 0 đến 9


    [SerializeField] private LevelUnlockSystem.GameUI gameUI;


    private Question question;
    private bool answered;
    private float audioLength;

    public TextMeshProUGUI ScoreText { get { return scoreText; } }
    public TextMeshProUGUI TimerText { get { return timerText; } }
    public GameObject GameOverPanel { get { return gameOverPanel; } }


    public void Play()
    {
        mainMenuPanel.SetActive(false);
        gameMenuPanel.SetActive(true);
        audioSource.PlayOneShot(touchSFX);
    }
    void Awake()
    {

        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        for (int i = 0; i < uiButtons.Count; i++)
        {
            Button localBtn = uiButtons[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

    }

    public void SetQuestion(Question question)
    {
        //set the question
        this.question = question;
        //check for questionType
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImage.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionType.IMAGE:
                ImageHolder();
                questionImage.transform.gameObject.SetActive(true);
                questionImage.sprite = question.qustionImage;
                break;
            case QuestionType.VIDEO:
                ImageHolder();
                questionVideo.transform.gameObject.SetActive(true);
                questionVideo.clip = question.qustionVideo;
                questionVideo.Play();
                break;
            case QuestionType.AUDIO:
                ImageHolder();
                questionAudio.transform.gameObject.SetActive(true);
                audioLength = question.qustionClip.length;
                StartCoroutine(PlayAudio());
                break;

        }


        questionText.text = question.questionInfor;
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalCol;
        }

        answered = false;
    }

    IEnumerator PlayAudio()
    {
        int playCount = 0;

        while (playCount < 2)
        {
            if (question.questionType == QuestionType.AUDIO)
            {
                questionAudio.PlayOneShot(question.qustionClip);
                playCount++;
                yield return new WaitForSeconds(audioLength + 0.5f);
            }
            else
            {
                break;
            }
        }

        yield return null;
    }


    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);
        questionAudio.transform.gameObject.SetActive(false);
        questionVideo.transform.gameObject.SetActive(false);
    }

    private void OnClick(Button btn)
    {
        if (quizManager.GameStatus == QuizManager.QuizGameStatus.Playing)
        {
            if (!answered)
            {
                answered = true;

                bool val = quizManager.Answer(btn.name);

                if (val)
                {
                    btn.image.color = Color.green;
                }
                else
                {
                    btn.image.color = Color.red;
                }
            }
        }

        switch (btn.name)
        {
            case "Animal":
                quizManager.StartGame(0);
                mainMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                break;
            case "Bird":
                quizManager.StartGame(1);
                mainMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                break;
            case "Mix":
                quizManager.StartGame(2);
                // mainMenuPanel.SetActive(false);
                // gameMenuPanel.SetActive(true);
                break;

        }
    }


    public void ShowGameOverPanel(int score)
    {

        foreach (Transform child in scoreContainer)
        {
            Destroy(child.gameObject);
        }

        string scoreString = score.ToString();

        if(playerdata.GetScoreGame1() < score)
        {
            playerdata.SetScoreGame1(score);
        }
        foreach (char digit in scoreString)
        {
            int number = digit - '0'; // Chuyển ký tự thành số nguyên

            // Tạo một UI Image mới từ Prefab
            GameObject newNumber = Instantiate(numberPrefab, scoreContainer);

            // Gán sprite tương ứng cho số
            Image numberImage = newNumber.GetComponent<Image>();
            numberImage.sprite = numberSprites[number];
        }

        int stars = 0;
        if (score >= 100) stars = 3;
        else if (score >= 50) stars = 2;
        else if (score >= 10) stars = 1;

        gameOverPanel.SetActive(true);

        if (musicManager != null)
        {
            musicManager.StopMusic();
        }

        if (gameUI != null)
        {
            gameUI.GameOver(stars);
        }

        if (score < 10)
        {
            Star1.SetActive(false);
            Star2.SetActive(false);
            Star3.SetActive(false);
        }
        else if (score <= 50)
        {
            Star1.SetActive(true);
            Star2.SetActive(false);
            Star3.SetActive(false);
        }
        else if (score <= 100)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(false);
        }
        else // scoreString > 100
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(true);
        }

        Debug.Log("" + scoreString);
    }


    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReduceLife(int index)
    {
        lifeImageList[index].color = Color.black;
    }


}
