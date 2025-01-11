using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVRead : MonoBehaviour
{
    [SerializeField] private TextAsset csvFile_video;

    public string[] filevideo { get; private set; }

    private void Awake()
    {
        ReadCsv();
    }
    private void ReadCsv()
    {
        filevideo = csvFile_video.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
    }
}
