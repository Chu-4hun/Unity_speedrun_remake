using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickNameInput : MonoBehaviour
{
    public string nickname;

    private void OnGUI()
    {
        nickname = GUI.TextArea(new Rect(10f, 10f, 200f, 40f), text:nickname, 20);
        GlobalVariables.Set("name", nickname);
    }
}