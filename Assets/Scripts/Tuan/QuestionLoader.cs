using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Video;


public class QuestionLoader : MonoBehaviour
{
    [SerializeField] private QuizDataScriptable quizDataScriptable;

    void Start()
    {
        LoadQuestionsForCurrentScene();
    }

    void LoadQuestionsForCurrentScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string jsonFileName = $"{sceneName}.json";
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        if (System.IO.File.Exists(filePath))
        {
            string jsonData = System.IO.File.ReadAllText(filePath);


            QuestionList loadedQuestions = JsonUtility.FromJson<QuestionList>(jsonData);

            quizDataScriptable.questions.Clear();

            foreach (var question in loadedQuestions.questions)
            {
                Debug.Log($"Question Type: {question.questionType}");

                switch (question.questionType)
                {
                    case QuestionType.TEXT:
                        question.qustionImage = null;
                        question.qustionClip = null;
                        question.qustionVideo = null;
                        break;

                    case QuestionType.IMAGE:
                        Debug.Log($"Loading image: {question.qustionImage}");
                        question.qustionImage = Resources.Load<Sprite>("Images/" + question.qustionImage);
                        if (question.qustionImage == null)
                            Debug.LogError($"Image not found: {question.qustionImage}");
                        question.qustionClip = null;
                        question.qustionVideo = null;
                        break;

                    case QuestionType.AUDIO:
                        Debug.Log($"Loading audio: {question.qustionClip}");
                        question.qustionClip = Resources.Load<AudioClip>("Audio/" + question.qustionClip);
                        if (question.qustionClip == null)
                            Debug.LogError($"Audio not found: {question.qustionClip}");
                        question.qustionImage = null;
                        question.qustionVideo = null;
                        break;

                    case QuestionType.VIDEO:
                        Debug.Log($"Loading video: {question.qustionVideo}");
                        question.qustionVideo = Resources.Load<VideoClip>("Video/" + question.qustionVideo);
                        if (question.qustionVideo == null)
                            Debug.LogError($"Video not found: {question.qustionVideo}");
                        question.qustionImage = null;
                        question.qustionClip = null;
                        break;
                }

                quizDataScriptable.questions.Add(question);
            }



            Debug.Log($"Questions loaded for {sceneName}");
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
        }
    }
}

[System.Serializable]
public class QuestionList
{
    public List<Question> questions;
}
