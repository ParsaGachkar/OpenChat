import 'dart:ui';
import 'package:flutter/widgets.dart';
import 'Helpers/CustomButton.dart';
import 'package:flutter/material.dart';
import 'package:flutter/animation.dart';
import 'package:open_chat/Services/UserService.dart';
import 'MainPage.dart';

class LoginPage extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    // TODO: implement createState
    return _LoginStateEnterPhone();
  }
}

enum _State { Loading, EnterPhone, EnterCode, Error, Success }

class _LoginStateEnterPhone extends State<LoginPage>
    with SingleTickerProviderStateMixin {
  _State state = _State.Loading;
  Animation<Alignment> bgImageAlignment;
  AnimationController controller;
  UserService _userService;
  TextEditingController phoneTextController;
  TextEditingController codeTextController;
  String _phoneNumber;

  @override
  void initState() {
    // TODO: implement initState

    super.initState();
    setState(() {
      _userService = UserService();
      phoneTextController = new TextEditingController();
      codeTextController = new TextEditingController();
      state = _State.Loading;
      controller = new AnimationController(
          vsync: this, duration: Duration(milliseconds: 400));
      controller.reset();
      bgImageAlignment =
          Tween<Alignment>(begin: Alignment.center, end: Alignment.center)
              .animate(controller)
                ..addListener(() {
                  setState(() {});
                });
      controller.forward();
      ;
    });
    EnterPhone();
  }

  void EnterPhone() async {
    setState(() {
      controller.reset();
      state = _State.EnterPhone;
      bgImageAlignment =
          Tween<Alignment>(begin: Alignment.center, end: Alignment.centerLeft)
              .animate(controller)
                ..addListener(() {
                  setState(() {});
                });
      controller.forward();
    });
  }

  void SubmitPhone() async {
    _phoneNumber = phoneTextController.text;

    setState(() {
      controller.reset();
      state = _State.Loading;
      bgImageAlignment =
          Tween<Alignment>(begin: Alignment.centerLeft, end: Alignment.center)
              .animate(controller)
                ..addListener(() {
                  setState(() {});
                });
      controller.forward();
    });
    try {
      await _userService.submitPhone(_phoneNumber);
    } catch (exception) {
      setState(() {
        state = _State.Error;
      });
    }
    setState(() {
      controller.reset();
      state = _State.EnterCode;
      bgImageAlignment =
          Tween<Alignment>(begin: Alignment.center, end: Alignment.centerRight)
              .animate(controller)
                ..addListener(() {
                  setState(() {});
                });
      controller.forward();
    });
  }

  void SubmitCode() async {
    setState(() {
      controller.reset();
      state = _State.Loading;
      bgImageAlignment =
          Tween<Alignment>(begin: Alignment.centerLeft, end: Alignment.center)
              .animate(controller)
                ..addListener(() {
                  setState(() {});
                });
      controller.forward();
    });
    try {
      var token =
          await _userService.submitCode(_phoneNumber, codeTextController.text);
      _userService.setToken(token);
      setState(() {
        state = _State.Success;
      });
      await Future.delayed(Duration(seconds: 1), () {
        Navigator.push(
            context, MaterialPageRoute(builder: (context) => Scaffold(body: MainPage(),)));
      });
    } catch (exception) {
      print(exception);
      setState(() {
        state = _State.Error;
      });
    }
  }

  void Retry() {
    EnterPhone();
  }

  Widget GetEnterPhoneUI() => Column(
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          Text(
            "ورود به سیستم",
            style: TextStyle(fontSize: 44, fontWeight: FontWeight.w200),
          ),
          TextFormField(
            keyboardType: TextInputType.phone,
            controller: phoneTextController,
            decoration: InputDecoration(
              fillColor: Colors.white.withOpacity(0.5),
              filled: true,
              border: OutlineInputBorder(
                  borderRadius: BorderRadius.all(Radius.circular(31)),
                  borderSide: BorderSide(color: Colors.white.withOpacity(1))),
              hintText: "شماره تلفن",
            ),
            textAlign: TextAlign.center,
          ),
          CustomButton(
            onPressed: () => {this.SubmitPhone()},
            text: "ورود",
          )
        ],
      );

  Widget GetEnterCodeUI() => Column(
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          Text(
            "کد ارسال شده را وارد کنید",
            textDirection: TextDirection.rtl,
            textAlign: TextAlign.center,
          ),
          TextFormField(
              textAlign: TextAlign.center,
              keyboardType: TextInputType.number,
              controller: codeTextController,
              decoration: InputDecoration(
                fillColor: Colors.white.withOpacity(0.5),
                filled: true,
                border: OutlineInputBorder(
                    borderRadius: BorderRadius.all(Radius.circular(31)),
                    borderSide: BorderSide(color: Colors.white.withOpacity(1))),
                hintText: "کد ارسال شده",
              )),
          CustomButton(
            onPressed: () => {this.SubmitCode()},
            text: "ورود",
          )
        ],
      );

  Widget ErrorUI() => Column(
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          Text("خطایی رخ داده"),
          CustomButton(
            text: "دوباره امتحان کنید",
            onPressed: Retry,
          )
        ],
      );

  Widget SuccessUI() => Text("شما با موفقیت وارد سیستم شدید");

  Widget GetLoadingUI() => CircularProgressIndicator(
        valueColor: AlwaysStoppedAnimation<Color>(Colors.black),
      );

  Widget GetUI() => SingleChildScrollView(
        child: Padding(
          padding: EdgeInsets.only(left: 17, right: 17, top: 113),
          child: ClipRRect(
            borderRadius: BorderRadius.all(Radius.circular(31)),
            child: BackdropFilter(
              filter: ImageFilter.blur(sigmaX: 31, sigmaY: 31),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: <Widget>[
                  Container(
                    padding: EdgeInsets.all(41),
                    alignment: Alignment.center,
                    color: Colors.white.withOpacity(0.2),
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        Image(
                          image: AssetImage("assets/graphics/Path 1.png"),
                          alignment: Alignment.center,
                        ),
                        Padding(
                          padding: EdgeInsets.only(top: 10),
                          child: SelectUI(),
                        )
                      ],
                    ),
                  )
                ],
              ),
            ),
          ),
        ),
      );

  Widget SelectUI() {
    return state == _State.EnterPhone
        ? GetEnterPhoneUI()
        : state == _State.EnterCode
            ? GetEnterCodeUI()
            : state == _State.Loading
                ? GetLoadingUI()
                : state == _State.Error
                    ? ErrorUI()
                    : state == _State.Success ? SuccessUI() : Container();
  }

  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Stack(
      children: <Widget>[
        //Background

        AnimatedContainer(
          duration: Duration(seconds: 5),
          constraints: BoxConstraints.expand(),
          child: SizedBox.expand(
              child: Image(
            image: AssetImage("assets/graphics/Background.png"),
            fit: BoxFit.cover,
            alignment: bgImageAlignment.value,
          )),
        ),

        //Blur Overlay
        BackdropFilter(
          filter: ImageFilter.blur(sigmaY: 5, sigmaX: 5),
          child: Container(
            constraints: BoxConstraints.expand(),
            alignment: Alignment.topCenter,
            child: this.GetUI(),
          ),
        )
      ],
    );
  }
}
