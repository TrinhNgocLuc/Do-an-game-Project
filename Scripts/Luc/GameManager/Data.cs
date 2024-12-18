﻿using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Google.MiniJSON;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class Data : MonoBehaviour
{
    public TMP_InputField Username_sighup;
    public TMP_InputField Pass_sighup;
    public TMP_InputField Pass_sighupAgain;
    public TMP_InputField Username_login;
    public TMP_InputField Pass_login;
    public startMenu start_Menu;
    public playerData _playerData;
    private DatabaseReference Database;
  


    void Start()
    {
        Database = FirebaseDatabase.GetInstance(FirebaseApp.DefaultInstance,
            "https://game-3d-english-study-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
    }
    public async void CreateUser()
    {
        bool userExists = false; 
        DataSnapshot snapshot = await Database.Child("user").GetValueAsync();

        foreach (DataSnapshot userSnapshot in snapshot.Children)
        {
            string userName = userSnapshot.Child("Username").Value.ToString();
            if (userName == Username_sighup.text)
            {
                userExists = true;
                break; 
            }
        }

        if (userExists)
        {
            Debug.Log("Tên đăng nhập đã tồn tại.");
            start_Menu.statusSighupNoti = false;
            start_Menu.ShowSighupNoti();
        }
        else
        {
            if (Pass_sighup.text == Pass_sighupAgain.text)
            {
                string UseID = System.Guid.NewGuid().ToString();
                User newUser = new User(Username_sighup.text, int.Parse(Pass_sighup.text), 0, 10, 1);
                string json = JsonUtility.ToJson(newUser);
                await Database.Child("user").Child(UseID).SetRawJsonValueAsync(json);

                start_Menu.statusSighupNoti = true;
            }
            else
            {
                Debug.Log("Mật khẩu không khớp.");
                start_Menu.statusSighupNoti = false;
            }
        }
        start_Menu.ShowSighupNoti();
    }

    public async void GetUser()
    {
            DataSnapshot snapshot = await Database.Child("user").GetValueAsync();

            foreach (DataSnapshot userSnapshot in snapshot.Children)
            {
                string userName = userSnapshot.Child("Username").Value.ToString();
                string password = userSnapshot.Child("Password").Value.ToString();

                if (Username_login.text == userName && Pass_login.text == password)
                {
                    Debug.Log("Username: " + userName);
                    Debug.Log("Password: " + password);
                    PlayerPrefs.SetString("currentUserId", userSnapshot.Key) ;
                    _playerData.expValue = int.Parse(userSnapshot.Child("Exp").Value.ToString());
                    _playerData.levelValue = int.Parse(userSnapshot.Child("Level").Value.ToString());
                    _playerData.expMax = int.Parse(userSnapshot.Child("MaxExp").Value.ToString());
                    start_Menu.Main_menu.SetActive(true);
                    start_Menu.Login.SetActive(false);
                    return;
                }
            }
            Debug.Log("Tên đăng nhập hoặc mật khẩu không chính xác.");
            start_Menu.ShowLoginNoti();
    }
}