const { app, BrowserWindow } = require('electron');

function start() {
    let window = new BrowserWindow({ webPreferences: { nodeIntegration: true } });
    window.setMenu(null);
    window.loadURL("https://open-chat.liara.run/WebApp");
}

app.on('ready',start);

app.on('window-all-closed',()=>{
    app.quit();
});

app.on('activate',()=>{
    if(window === null){
        start();
    }
});