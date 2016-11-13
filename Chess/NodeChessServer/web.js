'use strict';
const express = require('express');
const http = require('http');
const io = require('socket.io');
const init = (ioManager) => {
    const app = express();
    const httpClient = http.Server(app);
    const ioClient = io(httpClient);

    ioClient.on('connection', function (socket) {
        socket.on('add user', function (name) {
            ioManager.AddUser(name, (player) => {
                socket.emit('refresh player', player);
            }, () => {
                socket.emit('lose');
            }, () => {
                socket.emit('win');
            }, (num) => {
                socket.emit('step', num);
            });
        });
        socket.on('rate',  (obj) => {
            ioManager.RateOfPlayer(obj.playerNumber, obj.command, obj.raise);
        });
    });

    ioManager.RefreshUserList = (users) => {
        ioClient.emit('refresh user list', users);
    };


    ioManager.TimerTick = (ticks) => {
        ioClient.emit('timer tick', ticks);
    };

    ioManager.StartGame = () => {
        ioClient.emit('start game');
    };

    ioManager.StartRound = () => {
        ioClient.emit('start round');
    };

    ioManager.RefreshTableHandler = (table) => {
        ioClient.emit('refresh table', table);
    };


    httpClient.listen(3000, function () {
        console.log('listening on *:3000');
    });
}


module.exports = init;