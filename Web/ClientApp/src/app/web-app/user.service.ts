import { Injectable, } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserVerifyResponseResource } from './UserVerifyResponseResource';
import { ReadUserResource } from './ReadUserResource';
import { EditUserResource } from './EditUserResource';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) {
    this.LoadToken();
  }
  public Token = '';
  LoadToken() {
    this.Token = localStorage.getItem('auth-token');
  }
  public GetAuthHeader() {
    console.log(`Token is ${this.Token}`);
    return { headers: new HttpHeaders({ Authorization: `Bearer ${this.Token}` }) };
  }
  GetUserAuthStart(phone: string, Done?: () => void, Error?: (e: any) => void) {
    this.http.get(`/api/user/auth/${phone}`).subscribe(data => {
      if (Done) {
        Done();
      }
    }, error => {
      if (Error) {
        Error(error);
      }
    });
  }
  GetUserAuthVerify(phone: string, code: number, Done?: (result: UserVerifyResponseResource) => void, Error?: (e: any) => void) {
    this.http.get(`/api/user/auth/${phone}/${code}`).subscribe(data => {
      const parsedData = data as UserVerifyResponseResource;
      console.log(JSON.stringify(parsedData));
      this.Token = parsedData.token;
      console.log(this.Token);
      this.SaveToken();
      if (Done) {
        Done(parsedData);
      }
    }, error => {
      if (Error) {
        Error(error);
      }
    });
  }
  private SaveToken() {
    localStorage.setItem('auth-token', this.Token);
  }

  GetUserInfo(Done?: (result: ReadUserResource) => void, Error?: (e: any) => void) {
    this.http.get(`/api/user/`, this.GetAuthHeader()).subscribe(data => {

      const parsedData = data as ReadUserResource;
      console.log(JSON.stringify(data));
      console.log(JSON.stringify(parsedData));
      if (Done) {
        Done(parsedData);
      }
    }, error => {
      if (Error) {
        Error(error);
      }
    });
  }

  SetUserInfo(model: EditUserResource, Done?: () => void, Error?: (e: any) => void) {
    this.http.get(`/api/user/`, this.GetAuthHeader()).subscribe(data => {
      if (Done) {
        Done();
      }
    }, error => {
      if (Error) {
        Error(error);
      }
    });
  }
  GetUserInfoSpecific(phone: string, Done?: (result: ReadUserResource) => void, Error?: (e: any) => void) {
    this.http.get(`/api/user/${phone}`, this.GetAuthHeader()).subscribe(data => {
      const parsedData = data as ReadUserResource;
      if (Done) {
        Done(parsedData);
      }
    }, error => {
      if (Error) {
        Error(error);
      }
    });
  }

  ResetToken() {
    localStorage.setItem('auth-token', '');
    this.Token = '';
  }
}
