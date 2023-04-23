import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';

// ignore: must_be_immutable
class CustomButton extends StatelessWidget {
  CustomButton(
      {this.shape,
      this.padding,
      this.variant,
      this.fontStyle,
      this.alignment,
      this.margin,
      this.onTap,
      this.width,
      this.height,
      this.text,
      this.prefixWidget,
      this.suffixWidget});

  ButtonShape? shape;

  ButtonPadding? padding;

  ButtonVariant? variant;

  ButtonFontStyle? fontStyle;

  Alignment? alignment;

  EdgeInsetsGeometry? margin;

  VoidCallback? onTap;

  double? width;

  double? height;

  String? text;

  Widget? prefixWidget;

  Widget? suffixWidget;

  @override
  Widget build(BuildContext context) {
    return alignment != null
        ? Align(
            alignment: alignment!,
            child: _buildButtonWidget(),
          )
        : _buildButtonWidget();
  }

  _buildButtonWidget() {
    return Padding(
      padding: margin ?? EdgeInsets.zero,
      child: TextButton(
        onPressed: onTap,
        style: _buildTextButtonStyle(),
        child: _buildButtonWithOrWithoutIcon(),
      ),
    );
  }

  _buildButtonWithOrWithoutIcon() {
    if (prefixWidget != null || suffixWidget != null) {
      return Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          prefixWidget ?? SizedBox(),
          Text(
            text ?? "",
            textAlign: TextAlign.center,
            style: _setFontStyle(),
          ),
          suffixWidget ?? SizedBox(),
        ],
      );
    } else {
      return Text(
        text ?? "",
        textAlign: TextAlign.center,
        style: _setFontStyle(),
      );
    }
  }

  _buildTextButtonStyle() {
    return TextButton.styleFrom(
      fixedSize: Size(
        width ?? double.maxFinite,
        height ?? getVerticalSize(40),
      ),
      padding: _setPadding(),
      backgroundColor: _setColor(),
      side: _setTextButtonBorder(),
      shadowColor: _setTextButtonShadowColor(),
      shape: RoundedRectangleBorder(
        borderRadius: _setBorderRadius(),
      ),
    );
  }

  _setPadding() {
    switch (padding) {
      case ButtonPadding.PaddingAll12:
        return getPadding(
          all: 11,
        );
      default:
        return getPadding(
          all: 7,
        );
    }
  }

  _setColor() {
    switch (variant) {
      case ButtonVariant.OutlineBlack9003f_2:
        return ColorConstant.ghostWhite;
      case ButtonVariant.OutlineWhiteA700_3:
        return ColorConstant.ghostWhite;
      case ButtonVariant.OutlineBlack9003f_1:
        return ColorConstant.pink;
      case ButtonVariant.OutlineWhiteA700:
        return ColorConstant.pink;
      case ButtonVariant.CyanWithBorder:
        return ColorConstant.cyan;
      case ButtonVariant.Cyan:
        return ColorConstant.cyan;
      case ButtonVariant.OutlineWhiteA700_1:
        return ColorConstant.pink;
      case ButtonVariant.OutlineWhiteA700_2:
        return ColorConstant.cyan;
      case ButtonVariant.Purple:
        return ColorConstant.purple;
      default:
        return ColorConstant.ghostWhite;
    }
  }

  _setTextButtonBorder() {
    switch (variant) {
      case ButtonVariant.OutlineWhiteA700_3:
        return BorderSide(
          color: ColorConstant.ghostWhite,
          width: getHorizontalSize(
            1.00,
          ),
        );
      case ButtonVariant.OutlineWhiteA700:
        return BorderSide(
          color: ColorConstant.ghostWhite,
          width: getHorizontalSize(
            1.00,
          ),
        );
      case ButtonVariant.CyanWithBorder:
        return BorderSide(
          color: ColorConstant.ghostWhite,
          width: getHorizontalSize(
            1.00,
          ),
        );
      case ButtonVariant.OutlineWhiteA700_1:
        return BorderSide(
          color: ColorConstant.ghostWhite,
          width: getHorizontalSize(
            1.00,
          ),
        );
      case ButtonVariant.OutlineWhiteA700_2:
        return BorderSide(
          color: ColorConstant.ghostWhite,
          width: getHorizontalSize(
            1.00,
          ),
        );
      default:
        return null;
    }
  }

  _setTextButtonShadowColor() {
    switch (variant) {
      case ButtonVariant.OutlineBlack9003f_2:
        return ColorConstant.black25;
      case ButtonVariant.OutlineBlack9003f_1:
        return ColorConstant.black25;
      case ButtonVariant.Cyan:
        return ColorConstant.black25;
      case ButtonVariant.OutlineWhiteA700_1:
        return ColorConstant.black25;
      case ButtonVariant.OutlineWhiteA700_2:
        return ColorConstant.black25;
      default:
        return null;
    }
  }

  _setBorderRadius() {
    switch (shape) {
      case ButtonShape.RoundedBorder10:
        return BorderRadius.circular(
          getHorizontalSize(
            10.00,
          ),
        );
      case ButtonShape.CustomBorderTL10:
        return BorderRadius.only(
          topLeft: Radius.circular(
            getHorizontalSize(
              10.00,
            ),
          ),
          topRight: Radius.circular(
            getHorizontalSize(
              0.00,
            ),
          ),
          bottomLeft: Radius.circular(
            getHorizontalSize(
              10.00,
            ),
          ),
          bottomRight: Radius.circular(
            getHorizontalSize(
              0.00,
            ),
          ),
        );
      case ButtonShape.CustomBorderLR10:
        return BorderRadius.only(
          topLeft: Radius.circular(
            getHorizontalSize(
              0.00,
            ),
          ),
          topRight: Radius.circular(
            getHorizontalSize(
              10.00,
            ),
          ),
          bottomLeft: Radius.circular(
            getHorizontalSize(
              0.00,
            ),
          ),
          bottomRight: Radius.circular(
            getHorizontalSize(
              10.00,
            ),
          ),
        );
      case ButtonShape.Square:
        return BorderRadius.circular(0);
      default:
        return BorderRadius.circular(
          getHorizontalSize(
            15.00,
          ),
        );
    }
  }

  _setFontStyle() {
    switch (fontStyle) {
      case ButtonFontStyle.RobotoRomanRegular15:
        return TextStyle(
          color: ColorConstant.black,
          fontSize: getFontSize(
            15,
          ),
          fontFamily: 'Roboto',
          fontWeight: FontWeight.w400,
          height: getVerticalSize(
            1.22,
          ),
        );
      case ButtonFontStyle.RobotoRomanRegular18:
        return TextStyle(
          color: ColorConstant.black,
          fontSize: getFontSize(
            18,
          ),
          fontFamily: 'Roboto',
          fontWeight: FontWeight.w400,
          height: getVerticalSize(
            1.22,
          ),
        );
      case ButtonFontStyle.RobotoRomanRegular18WhiteA700:
        return TextStyle(
          color: ColorConstant.ghostWhite,
          fontSize: getFontSize(
            18,
          ),
          fontFamily: 'Roboto',
          fontWeight: FontWeight.w400,
          height: getVerticalSize(
            1.22,
          ),
        );
      case ButtonFontStyle.RobotoRomanBold20WhiteA700:
        return TextStyle(
          color: ColorConstant.ghostWhite,
          fontSize: getFontSize(
            20,
          ),
          fontFamily: 'Roboto',
          fontWeight: FontWeight.w700,
          height: getVerticalSize(
            1.20,
          ),
        );
      case ButtonFontStyle.RobotoRomanMedium20:
        return TextStyle(
          color: ColorConstant.ghostWhite,
          fontSize: getFontSize(
            20,
          ),
          fontFamily: 'Roboto',
          fontWeight: FontWeight.w500,
          height: getVerticalSize(
            1.20,
          ),
        );
      case ButtonFontStyle.RobotoRomanBold15WhiteA700:
        return TextStyle(
          color: ColorConstant.ghostWhite,
          fontSize: getFontSize(
            15,
          ),
          fontFamily: 'Roboto',
          fontWeight: FontWeight.w700,
          height: getVerticalSize(
            1.20,
          ),
        );
      default:
        return TextStyle(
          color: ColorConstant.black,
          fontSize: getFontSize(
            12,
          ),
          fontFamily: 'Roboto',
          fontWeight: FontWeight.w400,
          height: getVerticalSize(
            1.25,
          ),
        );
    }
  }
}

enum ButtonShape {
  Square,
  RoundedBorder10,
  RoundedBorder15,
  CustomBorderTL10,
  CustomBorderLR10,
}

enum ButtonPadding {
  PaddingAll12,
  PaddingAll8,
}

enum ButtonVariant {
  FillWhiteA700,
  OutlineBlack9003f_2,
  OutlineWhiteA700_3,
  OutlineBlack9003f_1,
  OutlineWhiteA700,
  CyanWithBorder,
  Cyan,
  OutlineWhiteA700_1,
  OutlineWhiteA700_2,
  Purple,
}

enum ButtonFontStyle {
  RobotoRomanRegular12,
  RobotoRomanRegular15,
  RobotoRomanRegular18,
  RobotoRomanRegular18WhiteA700,
  RobotoRomanBold20WhiteA700,
  RobotoRomanMedium20,
  RobotoRomanBold15WhiteA700,
}
