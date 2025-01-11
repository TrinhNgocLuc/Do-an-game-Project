using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private List<QuizDataScriptable> quizData;
    [SerializeField] private float timeLimit = 60f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correctAnswerSFX;
    // [SerializeField] private AudioClip touchSFX;
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioClip errorSFX;

    private List<Question> questions;
    private Question selectedQuestion;
    private int scoreCount = 0;
    private float currentTime;
    private int lifeRemaining = 3;

    private QuizGameStatus gameStatus = QuizGameStatus.Next;
    public QuizGameStatus GameStatus { get { return gameStatus; } }


    public void StartGame(int index)
    {
        scoreCount = 0;
        currentTime = timeLimit;
        lifeRemaining = 3;

        questions = new List<Question>();
        for (int i = 0; i < quizData[index].questions.Count; i++)
        {
            questions.Add(quizData[index].questions[i]);
        }

        SelectQuestion();
        gameStatus = QuizGameStatus.Playing;
    }


    private void SelectQuestion()
    {
        if (questions.Count > 0)
        {
            int val = UnityEngine.Random.Range(0, questions.Count);
            selectedQuestion = questions[val];
            quizUI.SetQuestion(selectedQuestion);
            questions.RemoveAt(val);

        }
        else
        {
            Debug.LogWarning("No questions available!");
        }
    }

    private void Update()
    {
        if (gameStatus == QuizGameStatus.Playing)
        {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }
    }

    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUI.TimerText.text = "Time:" + time.ToString("mm':'ss");

        if (currentTime <= 0)
        {
            gameStatus = QuizGameStatus.Next;
            quizUI.GameOverPanel.SetActive(true);
            quizUI.ShowGameOverPanel(scoreCount);
        }
    }


    public bool Answer(string answered)
    {
        bool correctAns = false;
        if (answered == selectedQuestion.correctAns)
        {
            correctAns = true;
            scoreCount += 10;
            quizUI.ScoreText.text = "Score: " + scoreCount;
            audioSource.PlayOneShot(correctAnswerSFX);
        }
        else
        {
            audioSource.PlayOneShot(errorSFX);
            lifeRemaining--;
            quizUI.ReduceLife(lifeRemaining);

            if (lifeRemaining <= 0 || questions.Count == 0)
            {
                gameStatus = QuizGameStatus.Next;
                quizUI.ShowGameOverPanel(scoreCount);
                audioSource.PlayOneShot(gameOverSFX);

            }
        }

        if (questions.Count > 0)
        {
            Invoke("SelectQuestion", .5f);
        }
        else
        {
            gameStatus = QuizGameStatus.Next;
            quizUI.ShowGameOverPanel(scoreCount);
        }

        return correctAns;
    }



    [System.Serializable]
    public enum QuizGameStatus
    {
        Next,
        Playing
    }

}
