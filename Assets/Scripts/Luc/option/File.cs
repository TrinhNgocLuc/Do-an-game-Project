using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "file_image")]
public class File : ScriptableObject
{
    [SerializeField] public List<Sprite> file = new List<Sprite>();

    internal static string[] ReadAllLines(string filePath)
    {
        throw new NotImplementedException();
    }

    public List<Sprite> GetSprites()
    {
        return file;
    }
}
