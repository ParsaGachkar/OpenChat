import 'package:flutter/material.dart';
import 'dart:ui';

class CustomButton extends StatelessWidget {
  CustomButton({@required this.text, @required this.onPressed});

  String text;
  GestureTapCallback onPressed;
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Row(
      children: <Widget>[
        Expanded(
          child: Padding(
            padding: EdgeInsets.all(4),
            child: ClipRRect(
              borderRadius: BorderRadius.all(Radius.circular(27)),
              child: BackdropFilter(
                filter: ImageFilter.blur(sigmaX: 30, sigmaY: 30),
                child: GestureDetector(
                  onTap: onPressed,
                  child: Container(
                    height: 53,
                    decoration: BoxDecoration(color: Color(0x661900FF)),
                    child: Center(
                      child: Text(
                        text,
                        style: TextStyle(
                            fontSize: 20,
                            fontWeight: FontWeight.w200,
                            color: Colors.white),
                      ),
                    ),
                  ),
                ),
              ),
            ),
          ),
        )
      ],
    );
  }
}
