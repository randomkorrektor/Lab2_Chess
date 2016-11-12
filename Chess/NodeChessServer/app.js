'use strict';
const Bank = require('./DataTypes/Bank');
const Lear = require('./DataTypes/Lear');
const Rating = require('./DataTypes/Rating');
const Card = require('./DataTypes/Card');

const bank = new Bank(1);
console.log(bank);
console.log(Lear);
console.log(Rating);
const card = new Card(Lear.Diamonds, Rating.Ten);
console.log(card);
