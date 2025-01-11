using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class passwordToggle : MonoBehaviour
{
    public TMP_InputField passwordInputField;         
    private bool isPasswordVisible = false;

    public void TogglePasswordVisibility()
    {
        isPasswordVisible = !isPasswordVisible;

        if (isPasswordVisible)
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Password;
        }

        passwordInputField.Select();
        passwordInputField.ActivateInputField();
    }
}
