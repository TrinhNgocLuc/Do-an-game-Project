using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vocabulary
{
    public string vocabulary;
    public string mean;
    public Sprite image;
    public AudioClip audio;
    public Vocabulary(string _vocabulary, string _mean, Sprite _image, AudioClip _audio)
    {
        this.vocabulary = _vocabulary;
        this.mean = _mean;
        this.image = _image;
        this.audio = _audio;
    }
}
public class VocabularyManager : MonoBehaviour
{
    public static VocabularyManager instance { get; private set; }
    private CsvReader csvReader;
    [SerializeField] private List<Vocabulary> easyVocabularies = new List<Vocabulary>();
    [SerializeField] private List<Vocabulary> mediumVocabularies = new List<Vocabulary>();
    [SerializeField] private List<Vocabulary> hardVocabularies = new List<Vocabulary>();

    private List<Vocabulary> easyVocabulariesRemain;
    private List<Vocabulary> mediumVocabulariesRemain;
    private List<Vocabulary> hardVocabulariesRemain;
    [SerializeField] private int easyVocaRemainQuantity;
    [SerializeField] private int mediumVocaRemainQuantity;
    public Dictionary<string, string> easyVocabulary_mean { get; private set; } = new Dictionary<string, string>();
    public Dictionary<string, string> mediumVocabulary_mean { get; private set; } = new Dictionary<string, string>();
    public Dictionary<string, string> hardVocabulary_mean { get; private set; } = new Dictionary<string, string>();
    private List<Sprite> easyVocaImages;
    private List<Sprite> mediumVocaImages;
    private List<Sprite> hardVocaImages;

    private List<AudioClip> easyAudios;
    private List<AudioClip> mediumAudios;
    private List<AudioClip> hardAudios;
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;
        csvReader = GetComponent<CsvReader>();
    }
    private void Start()
    {
        LoadVocabularyMean();
        LoadVocabularyImage();
        LoadVocabularyAudio();
        LoadVocabularies();
        easyVocabulariesRemain = new List<Vocabulary>(easyVocabularies);
        mediumVocabulariesRemain = new List<Vocabulary>(mediumVocabularies);
        hardVocabulariesRemain = hardVocabularies;
        easyVocaRemainQuantity = easyVocabulariesRemain.Count;
        mediumVocaRemainQuantity = mediumVocabulariesRemain.Count;
    }
    public Vocabulary GetRandomEasyVocabulary()
    {
        if (easyVocabulariesRemain.Count < 1)
        {
            Debug.Log("Out of voca!");
            return null;
        }
        int rd = UnityEngine.Random.Range(0, easyVocabulariesRemain.Count);
        int index = easyVocabularies.IndexOf(easyVocabulariesRemain[rd]);
        easyVocabulariesRemain.Remove(easyVocabulariesRemain[rd]);
        easyVocaRemainQuantity = easyVocabulariesRemain.Count;
        return easyVocabularies[index];
    }
    public Vocabulary GetRandomMediumVocabulary()
    {
        if (mediumVocabulariesRemain.Count < 1)
        {
            Debug.Log("Out of voca!");
            return null;
        }
        int rd = UnityEngine.Random.Range(0, mediumVocabulariesRemain.Count);
        int index = mediumVocabularies.IndexOf(mediumVocabulariesRemain[rd]);
        mediumVocabulariesRemain.Remove(mediumVocabulariesRemain[rd]);
        mediumVocaRemainQuantity = mediumVocabulariesRemain.Count;
        return mediumVocabularies[index];
    }
    public Vocabulary GetRandomHardVocabulary()
    {
        int rd = UnityEngine.Random.Range(0, hardVocabularies.Count);
        return hardVocabularies[rd];
    }
    private void LoadVocabularies()
    {
        int cnt = 0;
        foreach (var item in easyVocabulary_mean)
        {
            easyVocabularies.Add(new Vocabulary(item.Key, item.Value, easyVocaImages[cnt], easyAudios[cnt++]));
        }
        cnt = 0;
        foreach (var item in mediumVocabulary_mean)
        {
            mediumVocabularies.Add(new Vocabulary(item.Key, item.Value, mediumVocaImages[cnt], mediumAudios[cnt++]));
        }
        //cnt = 0;
        //foreach (var item in hardVocabulary_mean)
        //{
        //    hardVocabularies.Add(new Vocabulary(item.Key, item.Value, hardVocaImages[cnt], hardAudios[cnt++]));
        //}
    }

    private void LoadVocabularyImage()
    {
        easyVocaImages = Resources.Load<Vocabulary_imageSO>("Vocabulary_image").easyVocaImages;
        mediumVocaImages = Resources.Load<Vocabulary_imageSO>("Vocabulary_image").mediumVocaImages;
        hardVocaImages = Resources.Load<Vocabulary_imageSO>("Vocabulary_image").hardVocaImages;
    }
    private void LoadVocabularyAudio()
    {
        easyAudios = Resources.Load<Vocabulary_audioSO>("Vocabulary_audio").easyAudios;
        mediumAudios = Resources.Load<Vocabulary_audioSO>("Vocabulary_audio").mediumAudios;
        hardAudios = Resources.Load<Vocabulary_audioSO>("Vocabulary_audio").hardAudios;
    }
    private void LoadVocabularyMean()
    {
        int cnt = 0;
        foreach (string s in csvReader.easyVocabularyData)
        {
            if (cnt == 0 || cnt % 2 == 0)
            {
                if(!easyVocabulary_mean.ContainsKey(s))
                    easyVocabulary_mean.Add(s, csvReader.easyVocabularyData[cnt + 1]);
            }
            cnt++;
        }
        cnt = 0;
        foreach (string s in csvReader.mediumVocabularyData)
        {
            if (cnt == 0 || cnt % 2 == 0)
                mediumVocabulary_mean.Add(s, csvReader.mediumVocabularyData[cnt + 1]);
            cnt++;
        }
        cnt = 0;
        foreach (string s in csvReader.hardVocabularyData)
        {
            if (cnt == 0 || cnt % 2 == 0)
                hardVocabulary_mean.Add(s, csvReader.hardVocabularyData[cnt + 1]);
            cnt++;
        }
    }
}
