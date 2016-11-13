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
    }
}