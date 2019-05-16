import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NbLayoutModule, NbCardModule, NbUserModule, NbSidebarModule, NbSearchModule, NbChatModule } from '@nebular/theme';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { WebAppRoutingModule } from './web-app-routing.module';
import { LoginComponent } from './login/login.component';
import { ChatsComponent } from './chats/chats.component';
import { ChatComponent } from './chat/chat.component';
import { NgxCaptchaModule } from 'ngx-captcha';


@NgModule({
  declarations: [LoginComponent, ChatsComponent, ChatComponent],
  imports: [
    CommonModule,
    WebAppRoutingModule,
    NbLayoutModule, NbCardModule, ReactiveFormsModule, FormsModule, NbUserModule, NbSidebarModule, NbSearchModule,
    NbChatModule,
    HttpClientModule,
    NgxCaptchaModule
  ]
})
export class WebAppModule { }
