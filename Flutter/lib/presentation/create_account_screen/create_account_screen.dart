import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/services/providers.dart';

class CreateAccountScreen extends StatefulWidget {
  @override
  State<CreateAccountScreen> createState() => _CreateAccountScreenState();
}

class _CreateAccountScreenState extends State<CreateAccountScreen> {
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _confirmPasswordController =
      TextEditingController();

  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();

  Future<void> _register() async {
    final String username = _usernameController.text;
    final String password = _passwordController.text;
    final String confirmPassword = _confirmPasswordController.text;

    if (password != confirmPassword) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Passwords do not match!'),
        ),
      );
      return;
    }

    if (!_formKey.currentState!.validate()) {
      return;
    }

    try {
      await ApiProvider().register(username, password);
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Account created successfully! Please login.'),
        ),
      );
      Navigator.pushNamed(context, AppRoutes.loginScreen);
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Something went wrong!'),
        ),
      );
    }
  }

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
                          Center(
                            child: Container(
                                height: 530,
                                child: Form(
                                    key: _formKey,
                                    child: Container(
                                        margin: getMargin(top: 97, bottom: 5),
                                        padding: getPadding(
                                            left: 22,
                                            top: 20,
                                            right: 22,
                                            bottom: 20),
                                        decoration: AppDecoration.purple
                                            .copyWith(
                                                borderRadius: BorderRadiusStyle
                                                    .roundedBorder43),
                                        child: Column(
                                            mainAxisSize: MainAxisSize.min,
                                            mainAxisAlignment:
                                                MainAxisAlignment.start,
                                            children: [
                                              Align(
                                                  alignment:
                                                      Alignment.centerLeft,
                                                  child: Text("Create account",
                                                      overflow:
                                                          TextOverflow.ellipsis,
                                                      textAlign: TextAlign.left,
                                                      style: AppStyle
                                                          .txtRobotoRomanBold20)),
                                              SizedBox(
                                                height: getVerticalSize(50),
                                              ),
                                              SizedBox(
                                                width: getHorizontalSize(248),
                                                child: TextFormField(
                                                  controller:
                                                      _usernameController,
                                                  decoration: InputDecoration(
                                                    isDense: true,
                                                    filled: true,
                                                    fillColor: ColorConstant
                                                        .ghostWhite,
                                                    enabledBorder:
                                                        OutlineInputBorder(
                                                      borderSide: BorderSide(
                                                          width: 1,
                                                          color: ColorConstant
                                                              .gray),
                                                      borderRadius:
                                                          BorderRadius.circular(
                                                              10.0),
                                                    ),
                                                    focusedBorder:
                                                        OutlineInputBorder(
                                                      borderSide: BorderSide(
                                                          width: 2,
                                                          color: ColorConstant
                                                              .cyan),
                                                      borderRadius:
                                                          BorderRadius.circular(
                                                              10.0),
                                                    ),
                                                    hintText: "Username",
                                                  ),
                                                  textAlign: TextAlign.center,
                                                  style: TextStyle(
                                                      color:
                                                          ColorConstant.black),
                                                  validator: (value) {
                                                    if (value!.isEmpty) {
                                                      return 'Please enter your username';
                                                    }
                                                    return null;
                                                  },
                                                ),
                                              ),
                                              SizedBox(
                                                height: getVerticalSize(10),
                                              ),
                                              SizedBox(
                                                width: getHorizontalSize(248),
                                                child: TextFormField(
                                                  controller:
                                                      _passwordController,
                                                  obscureText: true,
                                                  decoration: InputDecoration(
                                                    isDense: true,
                                                    filled: true,
                                                    fillColor: ColorConstant
                                                        .ghostWhite,
                                                    enabledBorder:
                                                        OutlineInputBorder(
                                                      borderSide: BorderSide(
                                                          width: 1,
                                                          color: ColorConstant
                                                              .gray),
                                                      borderRadius:
                                                          BorderRadius.circular(
                                                              10.0),
                                                    ),
                                                    focusedBorder:
                                                        OutlineInputBorder(
                                                      borderSide: BorderSide(
                                                          width: 2,
                                                          color: ColorConstant
                                                              .cyan),
                                                      borderRadius:
                                                          BorderRadius.circular(
                                                              10.0),
                                                    ),
                                                    hintText: "Password",
                                                  ),
                                                  textAlign: TextAlign.center,
                                                  style: TextStyle(
                                                      color:
                                                          ColorConstant.black),
                                                  validator: (value) {
                                                    if (value!.isEmpty) {
                                                      return 'Please enter your password';
                                                    }
                                                    return null;
                                                  },
                                                ),
                                              ),
                                              SizedBox(
                                                height: getVerticalSize(10),
                                              ),
                                              SizedBox(
                                                width: getHorizontalSize(248),
                                                child: TextFormField(
                                                  controller:
                                                      _confirmPasswordController,
                                                  obscureText: true,
                                                  decoration: InputDecoration(
                                                    isDense: true,
                                                    filled: true,
                                                    fillColor: ColorConstant
                                                        .ghostWhite,
                                                    enabledBorder:
                                                        OutlineInputBorder(
                                                      borderSide: BorderSide(
                                                          width: 1,
                                                          color: ColorConstant
                                                              .gray),
                                                      borderRadius:
                                                          BorderRadius.circular(
                                                              10.0),
                                                    ),
                                                    focusedBorder:
                                                        OutlineInputBorder(
                                                      borderSide: BorderSide(
                                                          width: 2,
                                                          color: ColorConstant
                                                              .cyan),
                                                      borderRadius:
                                                          BorderRadius.circular(
                                                              10.0),
                                                    ),
                                                    hintText:
                                                        "Confirm password",
                                                  ),
                                                  textAlign: TextAlign.center,
                                                  style: TextStyle(
                                                      color:
                                                          ColorConstant.black),
                                                  validator: (value) {
                                                    if (value!.isEmpty) {
                                                      return 'Please enter your password';
                                                    }
                                                    return null;
                                                  },
                                                ),
                                              ),
                                              CustomButton(
                                                  height: getVerticalSize(40),
                                                  text: "Create account",
                                                  margin: getMargin(
                                                      left: 38,
                                                      top: 30,
                                                      right: 38),
                                                  variant: ButtonVariant
                                                      .OutlineBlack9003f_1,
                                                  fontStyle: ButtonFontStyle
                                                      .RobotoRomanRegular18WhiteA700,
                                                  onTap: _register),
                                            ])))),
                          )
                        ])))));
  }
}
