import { UserService } from './user.service';
import { HttpClient } from '@angular/common/http';
import { IMessegeWriteResourceModel } from './imessege-write-resource-model';
import { IChatReadResourceModel } from './ichat-read-resource-model';
import { Injectable } from '@angular/core';
import { ChatReadResource } from './ChatReadResource';
import { MessegeReadResource } from './MessegeReadResource';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private http: HttpClient, private userService: UserService) { }
  readonly route = '/api/chat';
  GetChats(Done?: (result: Array<ChatReadResource>) => void, Error?: (e: any) => void) {
    this.http.get(this.route, this.userService.GetAuthHeader())
      .subscribe(data => {
        if (Done) { Done(data as Array<ChatReadResource>); }
      }, error => {
        if (Error) { Error(error); }
      });
  }
  GetMesseges(model: ChatReadResource, Done?: (result: Array<MessegeReadResource>) => void, Error?: (e: any) => void) {
    this.http.get(`${this.route}/messeges/${model.id}`, this.userService.GetAuthHeader())
      .subscribe(data => {
        if (Done) { Done(data as Array<MessegeReadResource>); }
      }, error => {
        if (Error) { Error(error); }
      });
  }
  SendMessege(model: IMessegeWriteResourceModel, Done?: () => void, Error?: (e: any) => void) {
    this.http.post(`${this.route}`, model, this.userService.GetAuthHeader())
      .subscribe(data => {
        if (Done) { Done(); }
      }, error => {
        if (Error) { Error(error); }
      });
  }

}
