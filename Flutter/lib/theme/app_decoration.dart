import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';

class AppDecoration {
  static BoxDecoration get fillGray900 => BoxDecoration(
        color: ColorConstant.gray,
      );
  static BoxDecoration get purple => BoxDecoration(
        color: ColorConstant.purple,
        border: Border.all(
          color: ColorConstant.ghostWhite70,
          width: getHorizontalSize(
            1,
          ),
        ),
      );
  static BoxDecoration get outlineBlack9003f => BoxDecoration(
        color: ColorConstant.gray,
        boxShadow: [
          BoxShadow(
            color: ColorConstant.black25,
            spreadRadius: getHorizontalSize(
              2,
            ),
            blurRadius: getHorizontalSize(
              2,
            ),
            offset: Offset(
              0,
              4,
            ),
          ),
        ],
      );
  static BoxDecoration get outlineBlack9003f1 => BoxDecoration();

  static BoxDecoration get outlineWhiteA700 => BoxDecoration(
        color: ColorConstant.ghostWhite,
        border: Border.all(
          color: ColorConstant.ghostWhite,
          width: getHorizontalSize(
            1,
          ),
        ),
      );
  static BoxDecoration get black => BoxDecoration(
        color: ColorConstant.black,
        boxShadow: [
          BoxShadow(
            color: ColorConstant.black25,
            spreadRadius: getHorizontalSize(
              2,
            ),
            blurRadius: getHorizontalSize(
              2,
            ),
            offset: Offset(
              0,
              4,
            ),
          ),
        ],
      );
  static BoxDecoration get fillWhiteA701 => BoxDecoration(
        color: ColorConstant.white,
      );
  static BoxDecoration get fillWhiteA700 => BoxDecoration(
        color: ColorConstant.ghostWhite,
      );
  static BoxDecoration get outlineWhiteA7001 => BoxDecoration(
        color: ColorConstant.ghostWhite15,
        border: Border.all(
          color: ColorConstant.ghostWhite,
          width: getHorizontalSize(
            2,
          ),
          strokeAlign: strokeAlignOutside,
        ),
      );
}

class BorderRadiusStyle {
  static BorderRadius roundedBorder15 = BorderRadius.circular(
    getHorizontalSize(
      15,
    ),
  );

  static BorderRadius roundedBorder43 = BorderRadius.circular(
    getHorizontalSize(
      43,
    ),
  );

  static BorderRadius roundedBorder10 = BorderRadius.circular(
    getHorizontalSize(
      10,
    ),
  );
}

// Comment/Uncomment the below code based on your Flutter SDK version.

// For Flutter SDK Version 3.7.2 or greater.

double get strokeAlignInside => BorderSide.strokeAlignInside;

double get strokeAlignCenter => BorderSide.strokeAlignCenter;

double get strokeAlignOutside => BorderSide.strokeAlignOutside;

// For Flutter SDK Version 3.7.1 or less.

// StrokeAlign get strokeAlignInside => StrokeAlign.inside;
//
// StrokeAlign get strokeAlignCenter => StrokeAlign.center;
//
// StrokeAlign get strokeAlignOutside => StrokeAlign.outside;
