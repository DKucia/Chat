import * as signalR from  "@microsoft/signalr" ;


export default {
  connection: null,

  async init(token) {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:44310/chathub", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () =>
         token,
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

      await this.connection.start();
  }
};
