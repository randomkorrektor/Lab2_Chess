const UserManager = require('./UserManager');
const GameManager = require('./GameManager');
const IOManager = require('./IOManager');
const init = (startCache, startSmallBlind) => {
    const ioManager = new IOManager();
    const userManager = new UserManager(ioManager, startCache);
    ioManager.userManager = userManager;
    const gameManager = new GameManager(startSmallBlind, userManager, ioManager); 
    ioManager.gameManager = gameManager;
    ioManager.TimerEnd = () => {
        gameManager.StartGame();
    };
    return {
        ioManager,
        userManager,
        gameManager
    }
};

module.exports = init;