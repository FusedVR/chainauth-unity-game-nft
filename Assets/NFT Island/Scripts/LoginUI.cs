
/**
 * Copyright 2022 Vasanth Mohan. All rights and licenses reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 */

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the Login UI to get the email address of the player
/// Passes UI Data to the Singleton Controller for FusedVR
/// </summary>
public class LoginUI : MonoBehaviour
{
    public UIDocument uIDocument; //ui controls on the first scene

    // Start is called before the first frame update
    void Start() {
        Button ui = uIDocument.rootVisualElement.Q<Button>("Submit");
        ui.clicked += Ui_onClick; //assign button click to method
    }

    private async void Ui_onClick() {
        TextField ui = uIDocument.rootVisualElement.Q<TextField>("Email");
        string email = ui.text;
        if (await FusedAuth.Instance.Login(email)) {
            SceneManager.LoadScene(1); //load next scene if succesfully logged in
        }
    }

}
