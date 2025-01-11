using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionInfor;        
    public List<string> options;   
    public string correctAns;          
    public QuestionType questionType;  
    public Sprite qustionImage;           
    public AudioClip qustionClip;          
    public UnityEngine.Video.VideoClip qustionVideo; 
}

[System.Serializable]
public enum QuestionType
{
    TEXT,   
    IMAGE,  
    AUDIO,   
    VIDEO     
}

