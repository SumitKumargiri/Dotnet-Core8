import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { ChatService } from './service/chat.service';
import { isPlatformBrowser } from '@angular/common';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent {
  senderName: string = ''; 
  senderNameEntered: boolean = false;
  chatList: any[] = [];
  allMessages: any[] = [];
  selectedReceiver: string | null = null;
  selectedReceiverImage: string = '';
  selectedChat: any[] = [];
  messageText: string = '';
  defaultProfileImage: string = 'assets/profile.png';

  constructor(private chatService: ChatService) {}

  // Load Chat List After Sender Enters Name
  loadChatList(): void {
    if (this.senderName.trim()) {
      this.senderNameEntered = true;
      this.fetchChatList();
    } else {
      alert("Please enter a valid name!");
    }
  }

  // Fetch Unique Chat List
  fetchChatList(): void {
    this.chatService.getMessagesByUser(this.senderName).subscribe(
      response => {
        this.allMessages = response;
        let uniqueReceivers = new Map<string, any>();

        response.forEach(chat => {
          const receiver = chat.receiverName === this.senderName ? chat.senderName : chat.receiverName;
          if (!uniqueReceivers.has(receiver)) {
            uniqueReceivers.set(receiver, {
              receiverName: receiver,
              profileImage: chat.profileImage || this.defaultProfileImage
            });
          }
        });

        this.chatList = Array.from(uniqueReceivers.values());
      },
      error => {
        console.error('Error fetching messages:', error);
      }
    );
  }

  // Show Chat Messages for Selected Receiver
  showChat(receiverName: string): void {
    this.selectedReceiver = receiverName;

    this.chatService.getMessagesBetweenUsers(this.senderName, receiverName).subscribe(
      response => {
        this.selectedChat = response;
      },
      error => {
        console.error('Error fetching chat:', error);
      }
    );
  }

  // Send Message
  sendMessage(): void {
    if (this.messageText.trim() && this.selectedReceiver) {
      const newMessage = {
        id: uuidv4(),
        senderName: this.senderName,
        receiverName: this.selectedReceiver,
        message: this.messageText,
        createdDate: new Date().toISOString(),
        isActive: true
      };

      this.chatService.sendMessage(newMessage).subscribe(
        response => {
          this.selectedChat.push(newMessage);
          this.messageText = ''; 
        },
        error => {
          console.error('Error sending message:', error);
        }
      );
    } else {
      alert("Message cannot be empty!");
    }
  }
}