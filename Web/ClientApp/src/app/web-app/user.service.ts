import { Injectable, } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserVerifyResponseResource } from './UserVerifyResponseResource';
import { ReadUserResource } from './ReadUserResource';
import { EditUserResource } from './EditUserResource';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }
  public static Token = '';
  public static GetAuthHeader() {
    return { headers: new HttpHeaders({ Authorization: `Bearer ${UserService.Token}` }) };
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
      UserService.Token = parsedData.Token;
      if (Done) {
        Done(parsedData);
      }
    }, error => {
      if (Error) {
        Error(error);
      }
    });
  }
  GetUserInfo(Done?: (result: ReadUserResource) => void, Error?: (e: any) => void) {
    this.http.get(`/api/user/`, UserService.GetAuthHeader()).subscribe(data => {
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

  SetUserInfo(model: EditUserResource, Done?: () => void, Error?: (e: any) => void) {
    this.http.get(`/api/user/`, UserService.GetAuthHeader()).subscribe(data => {
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
    this.http.get(`/api/user/${phone}`, UserService.GetAuthHeader()).subscribe(data => {
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
}
