'use strict';
const Bank = require('./DataTypes/Bank');
const Lear = require('./DataTypes/Lear');
const Rating = require('./DataTypes/Rating');
const Card = require('./DataTypes/Card');
const CardDeck = require('./DataTypes/CardDeck');

const bank = new Bank(1);
console.log(bank);
console.log(Lear);
console.log(Rating);
const card = new Card(Lear.Diamonds, Rating.Ten);
console.log(card);
const deck = new CardDeck();
console.log(deck);
console.log(deck.TopCard());
console.log(deck.Discard());
console.log(deck.GetHand());
console.log(deck.GetFloop());