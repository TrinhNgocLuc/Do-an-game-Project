using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestGetData : MonoBehaviour
{
    [SerializeField] private Image vocaImage;
    [SerializeField] private TextMeshProUGUI vocaText;
    [SerializeField] private TextMeshProUGUI meanText;
    void Start()
    {

       // vocaImage = transform.Find("Vocabulary's Image").GetComponent<Image>();
        // vocaText = transform.Find("Vocabulary").GetComponent<TextMeshProUGUI>();
       // meanText = transform.Find("Mean").GetComponent<TextMeshProUGUI>();
    }
    public void GetRandomEasyVocabulary()
    {
        Vocabulary rdVoca = VocabularyManager.instance.GetRandomEasyVocabulary();
        vocaImage.sprite = rdVoca.image;
        vocaText.text = rdVoca.vocabulary;
        meanText.text = rdVoca.mean;
    }
    public void GetRandomMediumVocabulary()
    {
        Vocabulary rdVoca = VocabularyManager.instance.GetRandomMediumVocabulary();
        vocaImage.sprite = rdVoca.image;
        vocaText.text = rdVoca.vocabulary;
        meanText.text = rdVoca.mean;
    }
    public void GetRandomHardVocabulary()
    {
        Vocabulary rdVoca = VocabularyManager.instance.GetRandomHardVocabulary();
        vocaImage.sprite = rdVoca.image;
        vocaText.text = rdVoca.vocabulary;
        meanText.text = rdVoca.mean;
    }
}
