import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection!: signalR.HubConnection;

  constructor() { }

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7221/ChatHub')  
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        const connectionId = this.hubConnection.connectionId;
        console.log(`ConnectionId: ${connectionId}`);
      })
      .catch(err => console.error('Error while starting connection: ', err));
  }

  public sendMessage(username: string, message: string): void {
    this.hubConnection.invoke('SendMessage', username, message)
      .catch(err => console.error('Error while sending message: ', err));
  }

  public addMessageListener(callback: (username: string, message: string, connectionid: string) => void): void {
    this.hubConnection.on('ReceiveMessage', (username, message, connectionid) => {
      console.log(`Received message from ${username} (ConnectionId: ${connectionid}): ${message}`);
      callback(username, message, connectionid);
    });
  }
}
