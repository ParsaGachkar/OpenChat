import 'package:http/http.dart';
import 'package:flutter/foundation.dart';
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

  Future<String> getAuthHeaders() async {
    return "Bearer ${await getToken()}";
  }

  Future<UserInfoResponse> getUserInfo() async {
    var resp =
        await http.get(AppSettings.Url + "api/user", headers: getAuthHeaders());
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
