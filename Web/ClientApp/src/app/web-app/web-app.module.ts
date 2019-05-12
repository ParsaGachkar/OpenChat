import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WebAppRoutingModule } from './web-app-routing.module';
import { LoginComponent } from './login/login.component';
import { ChatsComponent } from './chats/chats.component';
import { ChatComponent } from './chat/chat.component';

@NgModule({
  declarations: [LoginComponent, ChatsComponent, ChatComponent],
  imports: [
    CommonModule,
    WebAppRoutingModule
  ]
})
export class WebAppModule { }
