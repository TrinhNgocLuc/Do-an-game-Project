using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vocabulary_image", menuName = "ScriptableObject/VocabularyData")]
public class Vocabulary_imageSO : ScriptableObject
{
    public List<Sprite> easyVocaImages = new List<Sprite>();
    public List<Sprite> mediumVocaImages = new List<Sprite>();
    public List<Sprite> hardVocaImages = new List<Sprite>();
}
