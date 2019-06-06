const { app, BrowserWindow } = require('electron');

function start() {
    var window = new BrowserWindow({ webPreferences: { nodeIntegration: true } });
    window.loadURL("https://open-chat.liara.run");
}

app.on('ready',start);