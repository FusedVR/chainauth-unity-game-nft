"use strict";

/**
 * Example JavaScript code that interacts with the page and Web3 wallets
 */

async function onGenRandom() {
  var maximum = Math.pow(10, 16) -1;
  var minimum = 0;
  var randNum = Math.floor(Math.random() * (maximum - minimum + 1)) + minimum;
  document.querySelector("#nft-id").value = randNum;
}

/**
 * Main entry point.
 */
window.addEventListener('load', async () => {
  document.querySelector("#gen-rand").addEventListener("click", onGenRandom);
});
