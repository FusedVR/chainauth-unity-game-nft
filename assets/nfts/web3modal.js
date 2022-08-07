"use strict";

/**
 * Example JavaScript code that interacts with the page and Web3 wallets
 */

 // Unpkg imports
const Web3Modal = window.Web3Modal.default;
const WalletConnectProvider = window.WalletConnectProvider.default;
const evmChains = window.evmChains;

const contractAddress = "0xFa3af5c659ca1e918C7AFde68C72DdeA600F0EC2"
var contractAbi = ""; 

$.get('assets/nfts/nftabi.json', function (data) {  
  contractAbi = JSON.stringify(data);
});

// Web3modal instance
let web3Modal


// Address of the selected account
let selectedAccount;


/**
 * Setup the orchestra
 */
function init() {

  console.log("Initializing example");
  console.log("WalletConnectProvider is", WalletConnectProvider);
  console.log("window.web3 is", window.web3, "window.ethereum is", window.ethereum);

  // Check that the web page is run in a secure context,
  // as otherwise MetaMask won't be available
  if(location.protocol !== 'https:') {
    // https://ethereum.stackexchange.com/a/62217/620
    document.querySelector("#btn-connect").setAttribute("disabled", "disabled")
    return;
  }

  // Tell Web3modal what providers we have available.
  // Built-in web browser provider (only one can exist as a time)
  // like MetaMask, Brave or Opera is added automatically by Web3modal
  const providerOptions = {
    walletconnect: {
      package: WalletConnectProvider,
      options: {
        // Mikko's test key - don't copy as your mileage may vary
        infuraId: "8043bb2cf99347b1bfadfb233c5325c0",
      }
    }
  };

  web3Modal = new Web3Modal({
    cacheProvider: false, // optional
    providerOptions, // required
    disableInjectedProvider: false, // optional. For MetaMask / Brave / Opera.
  });

  console.log("Web3Modal instance is", web3Modal);
}

/**
 * Mint the NFT with the given provider
 */
async function mintNFT(nftId, instance) {
  let provider = new ethers.providers.Web3Provider(instance);
  let signer = provider.getSigner();
  let contractInstance = new ethers.Contract(contractAddress, contractAbi, signer);

  let nftTxn = await contractInstance.safeMint(await signer.getAddress(), nftId)
  let reciept = await nftTxn.wait();

  if (reciept) {
      console.log("Transaction is successful!!!" + '\n' + "Transaction Hash:", reciept.hash + '\n' + "Block Number:" + reciept.blockNumber + '\n' + "Navigate to https://polygonscan.com/tx/" + reciept.hash, "to see your transaction")
  } else {
      console.log("Error submitting transaction")
  }

}

/**
 * Connect wallet button pressed.
 */
async function onMint() {
  console.log("Opening a dialog", web3Modal);
  let provider;
  try {
    provider = await web3Modal.connect();
  } catch(e) {
    console.log("Could not get a wallet connection", e);
    return;
  }

  // Subscribe to accounts change
  provider.on("accountsChanged", (accounts) => {
    //fetchAccountData();
  });

  // Subscribe to chainId change
  provider.on("chainChanged", (chainId) => {
    //fetchAccountData();
  });

  // Subscribe to networkId change
  provider.on("networkChanged", (networkId) => {
    //fetchAccountData();
  });

  await mintNFT(document.querySelector("#nft-id").value , provider);
}

/**
 * Disconnect wallet button pressed.
 */
async function onDisconnect() {

  console.log("Killing the wallet connection", provider);

  // TODO: Which providers have close method?
  if(provider.close) {
    await provider.close();

    // If the cached provider is not cleared,
    // WalletConnect will default to the existing session
    // and does not allow to re-scan the QR code with a new wallet.
    // Depending on your use case you may want or want not his behavir.
    await web3Modal.clearCachedProvider();
    provider = null;
  }

  selectedAccount = null;

  // Set the UI back to the initial state
  //document.querySelector("#prepare").style.display = "block";
  //document.querySelector("#connected").style.display = "none";
}


/**
 * Main entry point.
 */
window.addEventListener('load', async () => {
  init();
  document.querySelector("#btn-connect").addEventListener("click", onMint);
  //document.querySelector("#btn-disconnect").addEventListener("click", onDisconnect);
});
