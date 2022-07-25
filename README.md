# Chain Auth Unity Sample

In this guide, we will showcase an end to end open source project that demonstrates how players can mint NFT tickets for your game or application and how that can be authenticated within Unity using the FusedVR Chain Auth Platform and SDK. This use case serves as a way to not only sell tickets to your game, but also give players control of owning your game, just like a physical game cartridge. 

The Unity project and front-end minting HTML code are both avaliable on [Github in the master and gh-pages branches](https://github.com/FusedVR/chainauth-unity), respectively. 

## Components

### Mint your own NFT HTML Page

To demo the HTML Minting page, first head to [https://fusedvr.github.io/chainauth-unity](https://fusedvr.github.io/chainauth-unity), which is where the HTML client for minting a NFT is hosted. Currently, the demo is only working on the Polygon Mumbai Testnet network, but the Javascript code that controls this is located in gh-pages branch under [assets/nfts/web3modal.js](https://github.com/FusedVR/chainauth-unity/blob/gh-pages/assets/nfts/web3modal.js), where you can change the network.

It leverages the [Web3 Modal](https://github.com/WalletConnect/web3modal) open source project to allow wallets to connect via native integrations like Metamask or Wallet Connect.  

The HTML page is responsible for allowing you to mint your own custom NFT with its own unique ID. 

### NFT Smart Contract

The smart contract is an open sourced ERC-721 contract and avaliable [on Github](https://github.com/FusedVR/chainauth-unity/blob/gh-pages/chainnft.sol). It was built using the [Open Zepplin Wizard](https://docs.openzeppelin.com/contracts/4.x/wizard), which enabled minting & burning of NFTs. You can use the same tool to create your own custom contract and then deploy it using a tool like [Remix](https://remix-project.org/). 

### Unity Project

The Unity Project is located on the [master branch](https://github.com/FusedVR/chainauth-unity/tree/master) and the main map generation is based on the project from [Catlike Coding](https://catlikecoding.com/unity/tutorials/hex-map/). The project utilizes the FusedVR Chain Auth solution in order to authenticate a player's wallet and confirm ownership of their assets. By logging into the service with their e-mail address, users are able to sign a confirmation message from the wallet of their choice, whether it is desktop or mobile. Then using the SDK, the game can see the list of NFTs owned by the player. 

With the list of NFTs owned by the player, we can generate the maps using the IDs as a random seed for generation. This ensures the maps that are generated are unique to the player's token id and can only be generated if a player owns the token. 

## Putting it all Together

Coming soon!
 