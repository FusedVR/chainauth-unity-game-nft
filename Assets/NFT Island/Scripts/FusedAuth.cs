
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
using FusedVR.Crypto;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// <summary>
/// Singleton Controller Class that is responsible for authenticating the player with FusedVR
/// </summary>
public class FusedAuth : MonoBehaviour
{
    public static FusedAuth Instance; //singleton to be called from NFTGenerator and other classes that want

    public ChainAuthManager manager = null; //single instance for game to manage for the player

    //Async Login Function that Registers and Authenticates a player
    //Returns true / false based on if the player succesfully authenticates
    //If true, then you can use the ChainAuthManager to query other functions like getting the list of NFTs the player owns
    public async Task<bool> Login(string email) {
        if (IsEmail(email)) {
            manager = await ChainAuthManager.Register(email, "App Id");
            return await manager.AwaitLogin();  //await player login
        } else {
            Debug.LogError("Invalid Email Address");
            return false;
        }
    }

    #region Email Handling
    public const string MatchEmailPattern =
    @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
      + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


    /// <summary>
    /// Checks whether the given Email-Parameter is a valid E-Mail address.
    /// </summary>
    /// <param name="email">Parameter-string that contains an E-Mail address.</param>
    /// <returns>True, wenn Parameter-string is not null and contains a valid E-Mail address;
    /// otherwise false.</returns>
    public static bool IsEmail(string email) {
        if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
        else return false;
    }
    #endregion

    #region Singleton Events
    private void Awake()
    { //check if this is the only singleton
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    { //remove reference to singleton
        if (Instance == this)
        {
            Instance = null;
        }
    }
    #endregion
}
