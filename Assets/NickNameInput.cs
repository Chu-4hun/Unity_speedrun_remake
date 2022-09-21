using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickNameInput : MonoBehaviour
{
    public string nickname;

    private void OnGUI()
    {
        nickname = GUI.TextArea(new Rect(10f, 10f, 100f, 20f), text:nickname, 20);
        GlobalVariables.Set("name", nickname);
    }
}