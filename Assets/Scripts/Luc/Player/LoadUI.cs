using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static Cinemachine.DocumentationSortingAttribute;

public class LoadUI : MonoBehaviour
{

    public TextMeshProUGUI userScore;
    public TextMeshProUGUI userName;
    public playerData _playerData;

    void Update()
    {
        updateUI();
    }
    public void updateUI()
    {
        userScore.text = "Điểm: " + _playerData.GetScoreSum().ToString();
        userName.text = _playerData.GetName().ToString();
    }
}
