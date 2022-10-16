# Chain Auth Unity Sample

In this guide, we will showcase an end to end open source project that demonstrates how your users can mint NFT tickets for your game or application and how that can be authenticated within Unity using the FusedVR Chain Auth Platform and SDK. This use case serves as a way to not only sell tickets to your game, but also give players control of owning your game, just like a physical game cartridge. 

The Unity project and front-end minting HTML code are both avaliable on [Github in the master and gh-pages branches](https://github.com/FusedVR/chainauth-unity-game-nft), respectively. 

## Components

### Mint your own NFT HTML Page

To try out the HTML page, first head over to [https://fusedvr.github.io/chainauth-unity-game-nft/](https://fusedvr.github.io/chainauth-unity-game-nft/), which is where the HTML client for minting a NFT is hosted. Currently, the demo is only working on the Polygon Mumbai Testnet network, but the Javascript code that controls this is located in gh-pages branch under [assets/nfts/web3modal.js](https://github.com/FusedVR/chainauth-unity-game-nft/blob/gh-pages/assets/nfts/web3modal.js), where you can change the network.

To do a test mint, you can create a Wallet and get some Mumbai Testnet Tokens on the [faucet](https://faucet.polygon.technology/) to test the minting page. 

It leverages the [Web3 Modal](https://github.com/WalletConnect/web3modal) open source project to allow wallets to connect via native integrations like Metamask or Wallet Connect.  

The HTML page is responsible for allowing you to mint your own custom NFT with its own unique ID. 

### NFT Smart Contract

The smart contract is an open sourced ERC-721 contract and avaliable [on Github](https://github.com/FusedVR/chainauth-unity/blob/gh-pages/chainnft.sol). It was built using the [Open Zepplin Wizard](https://docs.openzeppelin.com/contracts/4.x/wizard), which enabled minting & burning of NFTs. You can use the same tool to create your own custom contract and then deploy it using a tool like [Remix](https://remix-project.org/). 

### Unity Project

The Unity Project is located on the [master branch](https://github.com/FusedVR/chainauth-unity-game-nft/tree/master) and the main map generation is based on the project from [Catlike Coding](https://catlikecoding.com/unity/tutorials/hex-map/). The project utilizes the FusedVR Chain Auth solution in order to authenticate a player's wallet and confirm ownership of their assets. By logging into the service with their e-mail address, users are able to sign a confirmation message from the wallet of their choice, whether it is desktop or mobile. Then using the SDK, the game can see the list of NFTs owned by the player. 

With the list of NFTs owned by the player, we can generate the maps using the IDs as a random seed for generation. This ensures the maps that are generated are unique to the player's token id and can only be generated if a player owns the token. 

## Putting it all Together

Once you have minted a NFT on the demo site, you can use your wallet to authenticate against the Unity project and prove ownership of the NFT. To run the Unity Project, you will require an Application ID, which you can get by following the [Getting Started Guide](https://crypto.fusedvr.com/docs/get-started/).

You will then need to input the Application ID into the [FusedAuth.cs script](https://github.com/FusedVR/chainauth-unity-game-nft/blob/master/Assets/NFT%20Island/Scripts/FusedAuth.cs) in the Login Function. The FusedAuth script is a [Singleton](https://gamedevbeginner.com/singletons-in-unity-the-right-way/) class that manages the ChainAuthManager for the game between scenes.  

There are two scenes in the project: A **Login Scene** (called Start under /NFT Island/Scenes) and the **Map Scene** (called NFT Biomes under /NFT Island/Scenes)

![Unity Project Scenes](https://crypto.fusedvr.com/docs/resources/samples/scenes.png)

You can then run the project from the Start Scene, which will prompt you for an email address. Input your email, which will register your client with the Chain Auth backend to begin the validation. You should receive an email that contains a link that will re-direct to [https://link.fusedvr.com](https://link.fusedvr.com).

![Login Scene Unity](https://crypto.fusedvr.com/docs/resources/samples/loginscene.png)

As a player, you would then receive a prompt to choose the wallet you would like to authenticate with.

![Unity Project Scenes](https://crypto.fusedvr.com/docs/resources/samples/web3modal.png)

Once you choose the wallet, you will then receive a prompt to sign a random message, which by doing so will prove your ownership of the wallet and send that proof back to the game.

![Unity Project Scenes](https://crypto.fusedvr.com/docs/resources/samples/signature.png)

Once successfully signed, you can head back to the game, which will now transition to the Map scene. Now that the player has fully authenticated, you can start querying the SDK for assets that are owned by the player. This occurs in the [NFTGenerator.cs](https://github.com/FusedVR/chainauth-unity-game-nft/blob/master/Assets/NFT%20Island/Scripts/NFTGenerator.cs) script, which checks for the list of NFT ids that the player owns. 

Each ID is used as a random seed to generate the map, thus providing a unique experience for players based on the assets they own. This same principle could be applied to checking whether or not a player owns a token that represents your game.

Players can then select any of the IDs that they own to view the generated map.

![NFT 777](https://crypto.fusedvr.com/docs/resources/samples/nft1.png)

Using the dropdown, players can select any of the other NFTs that they own to view those assets as well.

![NFT 2](https://crypto.fusedvr.com/docs/resources/samples/nft2.png)