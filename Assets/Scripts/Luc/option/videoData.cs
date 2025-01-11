using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "videoData")]
public class videoData : ScriptableObject
{
    public List<VideoClip> video = new List<VideoClip>();


    internal static string[] ReadAllLines(string filePath)
    {
        throw new NotImplementedException();
    }

    public List<VideoClip> GetData()
    {
        return video;
    }
}
