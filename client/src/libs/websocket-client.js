class WebSocketClient {
    constructor(params) {
        let self = this;
 
        self.wsAddress = params.wsAddress;
        self.onConnect = params.onConnect;
        self.onMessage = params.onMessage;
        self.onError = params.onError;
 
        self.ws = new WebSocket(self.wsAddress);
 
        self.ws.onopen = function (event) {
            console.log(`[WS] Connected to websocket on ${self.wsAddress}`);
            self.onConnect();
        };
 
        self.ws.onmessage = function(event) {
            console.log(`[WS] Message recieved`);
            self.onMessage(JSON.parse(event.data));
        }
 
        self.ws.onerror = function(event) {
            console.log(`[WS] Error`);
            self.onError();
        }
    }
 
    sendMessage(request, onMessage) {
        let self = this;
        self.onMessage = onMessage;
        self.ws.send(JSON.stringify(request));
    }
}

// constwsAddress = "ws://192.168.1.177:17500/websocket";
    

// self.webSocket = newWebSocketClient({
// wsAddress:wsAddress,
// onConnect:self.onWebsocketConnect,
// onError:self.onError
//       });


 
export default WebSocketClient;