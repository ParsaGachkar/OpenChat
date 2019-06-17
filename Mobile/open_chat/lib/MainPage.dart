import 'package:flutter/material.dart';
import 'package:open_chat/Services/UserService.dart';

class MainPage extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    // TODO: implement createState
    return MainPageState();
  }
}

class MainPageState extends State<MainPage> {
  UserService _userService;
  UserInfoResponse _userInfoResponse;

  @override
  void initState() {
    // TODO: implement initState
    _userService = UserService();
    _userService.getUserInfo().then((info) {
      setState(() {
        _userInfoResponse = info;
      });
    });
    super.initState();
  }

  List<Widget> GetChats(){

  }

  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return SafeArea(
      child: Padding(
        padding:
            const EdgeInsets.only(top: 12, left: 12, right: 12, bottom: 12),
        child: Column(
          children: <Widget>[
            Row(
              children: <Widget>[
                Expanded(
                  child: Padding(
                    padding: const EdgeInsets.all(8.0),
                    child: Text(
                      (_userInfoResponse?.userId) ?? "Loading",
                      textAlign: TextAlign.right,
                      style: TextStyle(fontWeight: FontWeight.w800),
                    ),
                  ),
                ),
                SizedBox(
                  child: ClipOval(
                      child: Stack(
                    children: <Widget>[
                      Container(
                        decoration: BoxDecoration(color: Colors.black),
                      ),
                      Center(
                        child: SizedBox(
                          width: 64,
                          height: 64,
                          child: CircularProgressIndicator(
                            valueColor:
                                AlwaysStoppedAnimation<Color>(Colors.white),
                          ),
                        ),
                      ),
                      _userInfoResponse == null?Container():Image(
                        image: NetworkImage(
                            "https://api.adorable.io/avatars/285/${_userInfoResponse?.userId}"),
                      ),
                    ],
                  )),
                  width: 128,
                  height: 128,
                ),
              ],
            ),
            Expanded(
              child: Padding(
                padding: const EdgeInsets.all(12.0),
                child: SingleChildScrollView(
                  child: Column(
                    children: <Widget>[
                      RawMaterialButton(
                        child: Row(
                          children: <Widget>[
                            Expanded(
                                child: Padding(
                              padding: const EdgeInsets.all(16.0),
                              child: Text(
                                "جستجو برای چت جدید",
                                textAlign: TextAlign.center,
                                style: TextStyle(
                                    fontWeight: FontWeight.w800, fontSize: 20),
                              ),
                            ))
                          ],
                        ),
                        onPressed: () {},
                      ),
                      RawMaterialButton(
                        child: Row(
                          children: <Widget>[
                            Expanded(
                              child: Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: Text(
                                  "GUID",
                                  textAlign: TextAlign.right,
                                ),
                              ),
                            ),
                            SizedBox(
                              child: ClipOval(
                                child: Placeholder(
                                  color: Colors.black,
                                ),
                              ),
                              width: 64,
                              height: 64,
                            ),
                          ],
                        ),
                        onPressed: () {},
                      ),
                    ],
                  ),
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}
