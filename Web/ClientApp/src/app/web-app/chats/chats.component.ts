import { ReadUserResource } from './../ReadUserResource';
import { ChatReadResource } from './../ChatReadResource';
import { ChatService } from './../chat.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { NbMenuService, NbSidebarService, NbSearchService } from '@nebular/theme';

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
        console.log(JSON.stringify(data));
        this.userService.GetUserInfoSpecific(data.term as string, (user) => {
          console.log(`Got User ${JSON.stringify(user)}`);
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

  Search() {
    this.searchService.activateSearch("modal-zoomin", "");
  }
}
