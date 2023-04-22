import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';

import 'widgets/change_password.dart';

class AccountPreferencesScreen extends StatefulWidget {
  @override
  _AccountPreferencesScreenState createState() =>
      _AccountPreferencesScreenState();
}

class _AccountPreferencesScreenState extends State<AccountPreferencesScreen> {
  bool isNotified = true;
  bool isPremium = false;
  bool isLogged = true;

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
            resizeToAvoidBottomInset: false,
            backgroundColor: ColorConstant.gray,
            body: Container(
                width: size.width,
                height: size.height,
                decoration: BoxDecoration(
                    color: ColorConstant.gray,
                    image: DecorationImage(
                        image: AssetImage(ImageConstant.imgPagebackground),
                        fit: BoxFit.cover)),
                child: Container(
                    width: double.maxFinite,
                    padding:
                        getPadding(left: 13, top: 17, right: 13, bottom: 17),
                    child: Column(
                        mainAxisSize: MainAxisSize.min,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: [
                          CustomImageView(
                              svgPath: ImageConstant.imgArrowBack,
                              height: getVerticalSize(17),
                              width: getHorizontalSize(23),
                              margin: getMargin(left: 1),
                              onTap: () {
                                Navigator.of(context).pop();
                              }),
                          Container(
                              width: double.maxFinite,
                              child: Container(
                                  height: getVerticalSize(530),
                                  margin: getMargin(top: 97, bottom: 5),
                                  padding: getPadding(all: 22),
                                  decoration: AppDecoration.purple.copyWith(
                                      borderRadius:
                                          BorderRadiusStyle.roundedBorder43),
                                  child: Column(
                                      mainAxisSize: MainAxisSize.min,
                                      crossAxisAlignment:
                                          CrossAxisAlignment.start,
                                      mainAxisAlignment:
                                          MainAxisAlignment.start,
                                      children: [
                                        Text("Manage account",
                                            overflow: TextOverflow.ellipsis,
                                            textAlign: TextAlign.left,
                                            style:
                                                AppStyle.txtRobotoRomanBold20),
                                        Align(
                                            alignment: Alignment.center,
                                            child: Padding(
                                                padding: getPadding(
                                                  top: 37,
                                                ),
                                                child: Column(
                                                    mainAxisAlignment:
                                                        MainAxisAlignment
                                                            .center,
                                                    children: [
                                                      Row(
                                                        mainAxisAlignment:
                                                            MainAxisAlignment
                                                                .center,
                                                        children: [
                                                          Switch(
                                                            value: isNotified,
                                                            onChanged: (value) {
                                                              setState(() {
                                                                isNotified =
                                                                    value;
                                                              });
                                                            },
                                                            activeTrackColor:
                                                                ColorConstant
                                                                    .ghostWhite,
                                                            activeColor:
                                                                ColorConstant
                                                                    .cyan,
                                                          ),
                                                          Padding(
                                                              padding:
                                                                  getPadding(
                                                                      left: 8),
                                                              child: Text(
                                                                  "Notifications",
                                                                  overflow:
                                                                      TextOverflow
                                                                          .ellipsis,
                                                                  textAlign:
                                                                      TextAlign
                                                                          .left,
                                                                  style: AppStyle
                                                                      .txtRobotoRomanRegular20))
                                                        ],
                                                      ),
                                                      Row(
                                                        mainAxisAlignment:
                                                            MainAxisAlignment
                                                                .center,
                                                        children: [
                                                          Switch(
                                                            value: isPremium,
                                                            onChanged: (value) {
                                                              setState(() {
                                                                isPremium =
                                                                    value;
                                                              });
                                                            },
                                                            activeTrackColor:
                                                                ColorConstant
                                                                    .ghostWhite,
                                                            activeColor:
                                                                ColorConstant
                                                                    .cyan,
                                                          ),
                                                          Padding(
                                                              padding:
                                                                  getPadding(
                                                                      left: 8),
                                                              child: Text(
                                                                  "Premium user",
                                                                  overflow:
                                                                      TextOverflow
                                                                          .ellipsis,
                                                                  textAlign:
                                                                      TextAlign
                                                                          .left,
                                                                  style: AppStyle
                                                                      .txtRobotoRomanRegular20)),
                                                        ],
                                                      ),
                                                      CustomButton(
                                                          height:
                                                              getVerticalSize(
                                                                  40),
                                                          width:
                                                              getHorizontalSize(
                                                                  180),
                                                          text:
                                                              "Change Password",
                                                          variant: ButtonVariant
                                                              .Cyan,
                                                          fontStyle: ButtonFontStyle
                                                              .RobotoRomanMedium20,
                                                          alignment: Alignment
                                                              .bottomCenter,
                                                          onTap: () =>
                                                              showDialog(
                                                                context:
                                                                    context,
                                                                builder: (_) =>
                                                                    ChangePasswordDialog(),
                                                              )),
                                                      SizedBox(
                                                          height:
                                                              getVerticalSize(
                                                                  230)),
                                                      CustomButton(
                                                          height:
                                                              getVerticalSize(
                                                                  40),
                                                          width:
                                                              getHorizontalSize(
                                                                  180),
                                                          text: "Log out",
                                                          variant: ButtonVariant
                                                              .OutlineBlack9003f_1,
                                                          fontStyle: ButtonFontStyle
                                                              .RobotoRomanMedium20,
                                                          alignment: Alignment
                                                              .bottomCenter),
                                                    ])))
                                      ])))
                        ])))));
  }
}
