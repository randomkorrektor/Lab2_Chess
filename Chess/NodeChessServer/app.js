'use strict';
const managers = require('./Managers')(10000, 5);
const web = require('./web')(managers.ioManager);

