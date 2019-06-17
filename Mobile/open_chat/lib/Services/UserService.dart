import 'package:http/http.dart';
import 'package:flutter/foundation.dart';
import 'package:open_chat/Models/Chat.dart';
import 'package:open_chat/Models/Message.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'dart:convert';
import 'dart:io';
import 'package:http/io_client.dart';
import '../AppSettings.dart';

class UserService {
  SharedPreferences sp;
  var http;

  UserService() {
    final ioc = new HttpClient();
    ioc.badCertificateCallback =
        (X509Certificate cert, String host, int port) => true;
    this.http = new IOClient(ioc);
  }

  Future<void> initSharedPrefs() async {
    sp = await SharedPreferences.getInstance();
  }

  Future<void> setToken(String token) async {
    await initSharedPrefs();
    await sp.setString("token", token);
  }

  Future<String> getToken() async {
    await initSharedPrefs();
    String token = sp.getString("token") ?? "";
    if (token == "") setToken("");
    return token;
  }

  Future<void> submitPhone(String phoneNumber) async {
    var resp = await http.get(AppSettings.Url + "api/user/auth/$phoneNumber");
    CheckStatusCode(resp);
    print("Send Phone OK!");
  }

  Future<String> submitCode(String phoneNumber, String code) async {
    var resp =
        await http.get(AppSettings.Url + "api/user/auth/$phoneNumber/$code");
    CheckStatusCode(resp);
    VerificationResponse parsedResponse =
        VerificationResponse.fromJson(json.decode(resp.body));
    if (parsedResponse.token == "") {
      throw new Exception("Empty token retuned");
    }
    return parsedResponse.token;
  }

   Future<Map<String,String>> getAuthHeaders() async {
    var header = {HttpHeaders.authorizationHeader: "Bearer ${await getToken()}"};
    return header;
  }

  Future<UserInfoResponse> getUserInfo() async {
    var resp =
        await http.get(AppSettings.Url + "api/user", headers: await getAuthHeaders());
    CheckStatusCode(resp);
    UserInfoResponse parsedUserInfo =
        UserInfoResponse.fromJson(json.decode(resp.body));
    return parsedUserInfo;
  }

  Future<UserInfoResponse> getUserSpecificInfo(String id) async {
    var resp =
    await http.get(AppSettings.Url + "api/user/$id", headers: await getAuthHeaders());
    CheckStatusCode(resp);
    UserInfoResponse parsedUserInfo =
    UserInfoResponse.fromJson(json.decode(resp.body));
    return parsedUserInfo;
  }

  void CheckStatusCode(resp) {
    if (resp.statusCode != 200) {
      throw new Exception("Server Returned somthing other than 200");
    }
  }
  Future<Chat> GetChats() async{
    var resp =
    await http.get(AppSettings.Url + "api/user", headers: await getAuthHeaders());
    CheckStatusCode(resp);
    //Chat parsedChat =
    //Chat.fromJson(json.decode(resp.body));
    //return parsedChat;
    //Todo: implement This
  }

  Future<List<Message>> GetMessagesForChat(Chat chat) async{
    var resp =
    await http.get(AppSettings.Url + "api/user", headers: await getAuthHeaders());
    CheckStatusCode(resp);
    //Chat parsedChat =
    //Chat.fromJson(json.decode(resp.body));
    //return parsedChat;
    //Todo: implement This
  }

  Future<void> SendMessege() async{
    var resp =
        await http.get(AppSettings.Url + "api/user", headers: await getAuthHeaders());
    CheckStatusCode(resp);
    //Todo: implement This
  }


}





class VerificationResponse {
  String token;

  VerificationResponse(String token) : token = token;

  factory VerificationResponse.fromJson(Map<String, dynamic> json) {
    return VerificationResponse(json['token']);
  }
}

class UserInfoResponse {
  String userId;
  String phoneNumber;

  UserInfoResponse(String userId, String phoneNumber)
      : userId = userId,
        phoneNumber = phoneNumber;

  factory UserInfoResponse.fromJson(Map<String, dynamic> json) {
    return UserInfoResponse(json["id"], json["phoneNumber"]);
  }
}
