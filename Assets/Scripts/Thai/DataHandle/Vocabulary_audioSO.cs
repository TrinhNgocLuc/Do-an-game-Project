using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Vocabulary_audio", menuName = "ScriptableObject/Vocabulary_audio")]
public class Vocabulary_audioSO : ScriptableObject
{
    public List<AudioClip> easyAudios;
    public List<AudioClip> mediumAudios;
    public List<AudioClip> hardAudios;
}
