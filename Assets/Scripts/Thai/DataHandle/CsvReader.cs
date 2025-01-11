using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CsvReader : MonoBehaviour
{
    [SerializeField] private TextAsset csvFile_easy;
    [SerializeField] private TextAsset csvFile_medium;
    [SerializeField] private TextAsset csvFile_hard;
    public string[] easyVocabularyData { get; private set; }
    public string[] mediumVocabularyData { get; private set; }
    public string[] hardVocabularyData { get; private set; }
    private void Awake()
    {
        ReadCsv();
    }
    private void ReadCsv()
    {
        easyVocabularyData = csvFile_easy.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        mediumVocabularyData = csvFile_medium.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        hardVocabularyData = csvFile_hard.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
    }
}
