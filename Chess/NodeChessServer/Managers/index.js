const UserManager = require('./UserManager');
const GameManager = require('./GameManager');
const IOManager = require('./IOManager');
const init = (startSmallBlid) => {
    const ioManager = new IOManager();
    const userManager = new UserManager(ioManager);
    const gameManager = new GameManager(startSmallBlind, userManager, ioManager);
    ioManager.userManager = userManager;
    ioManager.gameManager = gameManager;

    return {
        ioManager,
        userManager,
        gameManager
    }
};

module.exports = init;