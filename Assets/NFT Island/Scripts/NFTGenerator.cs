
/**
 * Copyright 2022 Vasanth Mohan. All rights and licenses reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 */

using System.Collections.Generic;
using UnityEngine;
using FusedVR.Crypto;
using TMPro;

/// <summary>
/// Gets the list of NFTs that are owned by the player for the specific smart contract
/// Populates a dropdown with the IDs of the NFTs that are owned by the player on the Mumbai Test Net
/// Renders the map based on the NFT ID as a Random Seed for the Hex Map
/// </summary>
public class NFTGenerator : MonoBehaviour
{
    #region Inspector Variables
    public HexGrid grid; // the object responsible for rendering the grid 
    public HexMapGenerator generator; //generates the hex map tiles based on the settings

    public TMP_Dropdown dropdown; //UI Dropdown List for NFT IDs
    #endregion

    public const string MUMBAI_NFT_ADDRESS = "0xfa3af5c659ca1e918c7afde68c72ddea600f0ec2"; //smart contract address

    List<Dictionary<string, string>> nfts = new List<Dictionary<string, string>>(); //class storage of nfts owned by player

    // Start is called before the first frame update
    async void Start() {
        if (FusedAuth.Instance.manager != null) { //make sure the user has logged in
            List<Dictionary<string, string>> x = await FusedAuth.Instance.manager.GetNFTTokens(ChainAuthManager.CHAIN.mumbai);
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i]["token_address"] == MUMBAI_NFT_ADDRESS) { //if part of our collection, save the NFT id
                    nfts.Add(x[i]);
                    options.Add(new TMP_Dropdown.OptionData(x[i]["token_id"]));
                }
            }

            dropdown.ClearOptions(); //clear any cached data
            dropdown.AddOptions(options); //show NFT options
            dropdown.value = -1; //do not select an option for the player
        }
    }

    //this method is assigned to the NFT Swap Dropdown in Editor
    public void GenMap(int listId)
    {
        Debug.Log(nfts[listId]["token_id"]);
        generator.seed = (int) long.Parse(nfts[listId]["token_id"]); //note that the ids cannot be longer than 4294967296
        grid.CreateMap(20, 15); //create the map sizing data
        generator.GenerateMap(20, 15); //generate map
    }
}
