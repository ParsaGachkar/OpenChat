import { IMessegeWriteResourceModel } from './imessege-write-resource-model';
import { IChatReadResourceModel } from './ichat-read-resource-model';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor() { }

  GetChats() { }
  GetMesseges(model: IChatReadResourceModel) { }
  SendMessege(model: IMessegeWriteResourceModel) { }

}
