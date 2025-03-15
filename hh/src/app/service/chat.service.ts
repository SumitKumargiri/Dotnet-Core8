import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  apiUrl = 'https://localhost:57932/chat';

  constructor(private http: HttpClient) {}

  sendMessage(chatData: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/send`, chatData);
  }

  getMessagesByUser(senderName: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/getbyusername?sendername=${senderName}`);
  }

  getMessagesBetweenUsers(senderName: string, receiverName: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/getbyreceivername?sendername=${senderName}&receivername=${receiverName}`);
  }
}
