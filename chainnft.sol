// SPDX-License-Identifier: MIT
pragma solidity ^0.8.4;

import "@openzeppelin/contracts@4.7.2/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts@4.7.2/token/ERC721/extensions/ERC721Burnable.sol";
import "@openzeppelin/contracts@4.7.2/access/Ownable.sol";

contract ChainHex is ERC721, ERC721Burnable, Ownable {
    constructor() ERC721("ChainHex", "FCH") {}

    function safeMint(address to, uint256 tokenId) public onlyOwner {
        _safeMint(to, tokenId);
    }
}

//generated from https://wizard.openzeppelin.com/#erc721