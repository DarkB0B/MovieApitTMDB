import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:movie_mixer/core/app_export.dart';

class CopyableText extends StatelessWidget {
  final String text;

  CopyableText({required this.text});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () async {
        await Clipboard.setData(ClipboardData(text: text));
        ScaffoldMessenger.of(context).showSnackBar(SnackBar(
          content: Text('Text copied to clipboard'),
          duration: Duration(seconds: 2),
        ));
      },
      child: Container(
        height: getVerticalSize(44),
        width: getHorizontalSize(248),
        margin: getMargin(top: 12),
        decoration: AppDecoration.outlineBlack9003f1,
        child: Stack(
          alignment: Alignment.centerRight,
          children: [
            Align(
              alignment: Alignment.centerLeft,
              child: Container(
                height: getVerticalSize(44),
                width: getHorizontalSize(124),
                decoration: BoxDecoration(
                  color: ColorConstant.pink,
                  borderRadius: BorderRadius.only(
                    topLeft: Radius.circular(getHorizontalSize(15)),
                    bottomLeft: Radius.circular(getHorizontalSize(15)),
                  ),
                ),
              ),
            ),
            Align(
              alignment: Alignment.centerRight,
              child: Container(
                height: getVerticalSize(44),
                width: getHorizontalSize(124),
                decoration: BoxDecoration(
                  color: ColorConstant.cyan,
                  borderRadius: BorderRadius.only(
                    topRight: Radius.circular(getHorizontalSize(15)),
                    bottomRight: Radius.circular(getHorizontalSize(15)),
                  ),
                ),
              ),
            ),
            Align(
              alignment: Alignment.center,
              child: Container(
                width: getHorizontalSize(248),
                decoration: AppDecoration.outlineWhiteA7001.copyWith(
                  borderRadius: BorderRadiusStyle.roundedBorder15,
                ),
                child: ClipRRect(
                  borderRadius: BorderRadius.circular(15),
                  child: BackdropFilter(
                    filter: ImageFilter.blur(sigmaX: 5, sigmaY: 5),
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Padding(
                          padding: getPadding(bottom: 1),
                          child: Text(
                            text,
                            textAlign: TextAlign.center,
                            style: AppStyle.txtRobotoRomanRegular35.copyWith(
                              color: Colors.white,
                            ),
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
