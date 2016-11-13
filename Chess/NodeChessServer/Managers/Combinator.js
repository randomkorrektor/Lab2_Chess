class combinator {

    IsRoyalFlush(cards) {
        let lears = [0, 0, 0, 0];
        for (const card of cards) {
            if (card.rating > 9) {
                lears[card.lear]++;
            }
        }
        if (lears.find(u => (u > 4))) {
            return {
                code: 10
            };
        }
        return null;
    }

    IsStritFlush(cards) {
        let lears = [{ rating: null, count: 0 }, { rating: null, count: 0 }, { rating: null, count: 0 }, { rating: null, count: 0 }];
        for (const card of cards) {
            if (lears[card.lear].rating == null ||
                lears[card.lear].rating - 1 == card.rating) {
                lears[card.lear].rating = card.rating;
                lears[card.lear].count++;
            }
        }
        let num = lears.find(u => (u.count > 4));
        if (num) {
            return {
                code: 9,
                topCard: lears[num].rating + 4
            };
        }
        return null;
    }


    IsSquare(cards) {
        const sets = [];
        for (const card in cards) {
            if (sets[card.rating] == null)
                sets[card.rating] = 1;
            else
                sets[card.rating]++;
        }
        let num = sets.find(u => (u > 3));
        if (num) {
            return {
                code: 8,
                topCard: num
            };
        }
        return null;
    }


    IsFullHouse(cards) {
        const sets = {};
        for (const card of cards) {
            if (sets[card.rating] == null) {
                sets[card.rating] = 1;
            } else {
                sets[card.rating]++;
            }
        }
        let haveTwo = false;
        let topCard = null;

        for (const key in sets) {
            if (topCard == null && sets[key] == 3) {
                topCard = key;
            }
            if (key != topCard && !haveTwo && sets[key] > 1) {
                haveTwo = true;
            }
            if (topCard != null && haveTwo) {
                break;
            }
        }
        if (topCard && haveTwo) {
            return {
                code: 7,
                topCard: topCard
            }
        }
        return null;
    }

    IsFlush(cards) {
        let lears = [{ count: 0 }, { count: 0 }, { count: 0 }, { count: 0 }];
        for (const card of cards) {
            lears[card.lear].count++;
            if (lears[card.lear].top == null) {
                lears[card.lear] = card.rating;
            }
        }
        let num = lears.find(u => (u > 4));
        if (num) {
            return {
                code: 6,
                topCard: lears[num]
            };
        }
        return null;
    }


    IsSet(cards) {
        const sets = [];
        for (const card in cards) {
            if (sets[card.rating] == null)
                sets[card.rating] = 1;
            else
                sets[card.rating]++;
        }
        let num = sets.find(u => (u > 2));
        if (num) {
            return {
                code: 4,
                topCard: num
            };
        }
        return null;
    }


    IsStraight(cards) {
        let counter = {
            last: 15,
            count: 0
        };
        for (const card of cards) {
            if (cards.rating + 1 == counter.last) {
                counter.count++;
                counter.last = card.rating;
            }
            else if (cards.rating != counter.last) {
                counter.count = 1;
                counter.last = card.rating;
            }
            if (counter.count > 4)
                return {
                    code: 5,
                    topCard: counter.last + 4
                };
        }
        return null;
    }

    IsTwoPairs(cards) {


        const sets = {};
        for (const card of cards) {
            if (sets[card.rating] == null) {
                sets[card.rating] = 1;
            } else {
                sets[card.rating]++;
            }
        }

        let haveTwo = false;
        let topCard = null;
        let kicker = null;
        for (const key in sets) {
            if (topCard == null && sets[key] == 2) {
                topCard = key;
            }
            if (key != topCard && !haveTwo && sets[key] > 1) {
                haveTwo = true;
            }
            if (sets[key] == 1) {
                kicker = key;
            }
            if (topCard != null && haveTwo && kicker != null) {
                break;
            }
        }

        if (topCard && haveTwo) {
            return {
                code: 3,
                topCard: topCard,
                kicker
            }
        }
        return null;
    }

    IsPair(cards) {
        const sets = [];
        for (const card in cards) {
            if (sets[card.rating] == null)
                sets[card.rating] = 1;
            else
                sets[card.rating]++;
        }
        let topCard = sets.find(u => (u > 1));
        let kicker = sets.find(u => (u == 1));
        if (num) {
            return {
                code: 2,
                topCard,
                kicker
            };
        }
        return null;
    }

    IsTopCard(cards) {
        let topCard = cards[0].rating;
        let kicker = topCard;
        if (num) {
            return {
                code: 1,
                topCard,
                kicker
            };
        }
        return null;
    }
}