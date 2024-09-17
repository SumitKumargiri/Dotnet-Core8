import { Component, OnInit } from '@angular/core';
import { ChatService } from './service/chat.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public username: string = '';
  public message: string = '';
  public messages: { username: string, message: string, connectionid: string }[] = [];

  constructor(private chatService: ChatService) {}

  ngOnInit(): void {
    this.chatService.startConnection();

    this.chatService.addMessageListener((username: string, message: string, connectionid: string) => {
      this.messages.push({ username, message, connectionid });
    });
  }

  public sendMessage(): void {
    if (this.username && this.message) {
      this.chatService.sendMessage(this.username, this.message);
      this.message = '';  
    } else {
      console.error("Username or message is empty");
    }
  }
}
