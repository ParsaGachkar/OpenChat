import { ReadUserResource } from './../ReadUserResource';
import { ChatReadResource } from './../ChatReadResource';
import { ChatService } from './../chat.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { NbMenuService, NbSidebarService, NbSearchService } from '@nebular/theme';
import { MessegeReadResource } from '../MessegeReadResource';
import { IMessegeWriteResourceModel } from '../imessege-write-resource-model';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.scss']
})
export class ChatsComponent implements OnInit {

  constructor(
    private userService: UserService,
    private chatService: ChatService,
    private router: Router,
    private sideBarService: NbSidebarService,
    private searchService: NbSearchService) { }
  Chats: Array<ChatReadResource> = [];
  User: ReadUserResource = null;
  SelectedChat: ChatReadResource;
  ChatData: Array<MessegeReadResource> = [];

  ngOnInit() {
    if (this.userService.Token === '') {
      this.InvalidToken();
    }
    this.UpdateChats();
    this.SetupSearch();
  }
  SetupSearch() {
    this.searchService.onSearchSubmit()
      .subscribe((data: any) => {
        this.userService.GetUserInfoSpecific(data.term as string, (user) => {
          this.SelectedChat = {
            id: user.id
          };
        });

      });
  }
  UpdateChats() {
    this.chatService.GetChats(result => {
      this.Chats = result;
      this.userService.GetUserInfo(info => this.User = info, error => this.InvalidToken());
    }, error => this.InvalidToken());

  }
  InvalidToken() {
    this.userService.ResetToken();
    this.router.navigate(['/WebApp/Login']);
  }
  toggleMenu() {
    this.sideBarService.toggle();
  }
  LogOff() {
    this.InvalidToken();
  }
  SelectChat(chat: ChatReadResource) {
    this.SelectedChat = chat;
    this.ChatData = [];
    this.chatService.GetMesseges(chat, data => {
      this.ChatData = data;
      console.log(this.ChatData);
    }, error => { })
  }

  Send(event: any){
    console.log(event);
    const messege = event.message;
    const model:IMessegeWriteResourceModel = {
      SenderId: this.User.id,
      ReciverId: this.SelectedChat.userId,
      Context:messege
    };
    console.log(model);
    this.chatService.SendMessege(model,()=>{
      this.SelectChat(this.SelectedChat);
    },error=>{})
  }
  Search() {
    this.searchService.activateSearch("modal-zoomin", "");
  }
}
