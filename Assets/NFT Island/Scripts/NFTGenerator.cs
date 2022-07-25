using System.Collections.Generic;
using UnityEngine;
using FusedVR.Web3;

public class NFTGenerator : MonoBehaviour
{
    public HexGrid grid;
    public HexMapGenerator generator;


    // Start is called before the first frame update
    async void Start() {
        if (Web3Manager.BEARER_TOKEN_KEY != null) {
            List<Dictionary<string, string>> x = await Web3Manager.GetNFTTokens("polygon");
            generator.seed = int.Parse(x[0]["block_number_minted"]);
        }

        grid.CreateMap(20, 15);
        generator.GenerateMap(20, 15);
    }
}
