import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/services/providers.dart';

class LoginScreen extends StatefulWidget {
  @override
  _LoginScreenState createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  bool _isLoading = false;

  Future<void> _login() async {
    if (_formKey.currentState!.validate()) {
      setState(() {
        _isLoading = true;
      });

      try {
        await ApiProvider()
            .login(_usernameController.text, _passwordController.text);
        // await ApiProvider().setToken(token);
        Navigator.pushReplacementNamed(context, AppRoutes.mainScreen);
      } catch (e) {
        print(e);
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Login failed'),
          ),
        );
      } finally {
        setState(() {
          _isLoading = false;
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        extendBody: true,
        extendBodyBehindAppBar: true,
        backgroundColor: ColorConstant.gray,
        resizeToAvoidBottomInset: false,
        body: Container(
          width: size.width,
          height: size.height,
          decoration: BoxDecoration(
            color: ColorConstant.gray,
            boxShadow: [
              BoxShadow(
                color: ColorConstant.black25,
                spreadRadius: getHorizontalSize(2),
                blurRadius: getHorizontalSize(2),
                offset: Offset(0, 4),
              ),
            ],
            image: DecorationImage(
              image: AssetImage(ImageConstant.imgPagebackground),
              fit: BoxFit.cover,
            ),
          ),
          child: Form(
            key: _formKey,
            child: Container(
              width: double.maxFinite,
              padding: getPadding(left: 13, right: 13),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Container(
                    width: double.maxFinite,
                    child: Container(
                      padding:
                          getPadding(left: 10, top: 34, right: 10, bottom: 34),
                      decoration: AppDecoration.purple.copyWith(
                        borderRadius: BorderRadiusStyle.roundedBorder43,
                      ),
                      child: Column(
                        mainAxisSize: MainAxisSize.min,
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: [
                          Container(
                            height: getVerticalSize(142),
                            width: getHorizontalSize(314),
                            child: Stack(
                              alignment: Alignment.center,
                              children: [
                                CustomImageView(
                                  imagePath: ImageConstant.imgNeonMovie,
                                  height: getSize(58),
                                  width: getSize(127),
                                  alignment: Alignment.topLeft,
                                ),
                                CustomImageView(
                                  imagePath: ImageConstant.imgNeonMixer,
                                  height: getSize(58),
                                  width: getSize(127),
                                  alignment: Alignment.bottomRight,
                                ),
                                CustomImageView(
                                  imagePath: ImageConstant.imgLogo,
                                  height: getSize(130),
                                  width: getSize(130),
                                  alignment: Alignment.center,
                                ),
                              ],
                            ),
                          ),
                          SizedBox(
                            height: getVerticalSize(30),
                          ),
                          SizedBox(
                            width: getHorizontalSize(248),
                            child: TextFormField(
                              controller: _usernameController,
                              decoration: InputDecoration(
                                isDense: true,
                                filled: true,
                                fillColor: ColorConstant.ghostWhite,
                                enabledBorder: OutlineInputBorder(
                                  borderSide: BorderSide(
                                      width: 1, color: ColorConstant.gray),
                                  borderRadius: BorderRadius.circular(10.0),
                                ),
                                focusedBorder: OutlineInputBorder(
                                  borderSide: BorderSide(
                                      width: 2, color: ColorConstant.cyan),
                                  borderRadius: BorderRadius.circular(10.0),
                                ),
                                hintText: "Username",
                              ),
                              textAlign: TextAlign.center,
                              style: TextStyle(color: ColorConstant.black),
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
                              controller: _passwordController,
                              obscureText: true,
                              decoration: InputDecoration(
                                isDense: true,
                                filled: true,
                                fillColor: ColorConstant.ghostWhite,
                                enabledBorder: OutlineInputBorder(
                                  borderSide: BorderSide(
                                      width: 1, color: ColorConstant.gray),
                                  borderRadius: BorderRadius.circular(10.0),
                                ),
                                focusedBorder: OutlineInputBorder(
                                  borderSide: BorderSide(
                                      width: 2, color: ColorConstant.cyan),
                                  borderRadius: BorderRadius.circular(10.0),
                                ),
                                hintText: "Password",
                              ),
                              textAlign: TextAlign.center,
                              style: TextStyle(color: ColorConstant.black),
                              validator: (value) {
                                if (value!.isEmpty) {
                                  return 'Please enter your password';
                                }
                                return null;
                              },
                            ),
                          ),
                          Container(
                              margin: getMargin(
                                  left: 33, top: 33, right: 33, bottom: 17),
                              decoration: AppDecoration.outlineBlack9003f1
                                  .copyWith(
                                      borderRadius:
                                          BorderRadiusStyle.roundedBorder15),
                              child: Column(
                                  mainAxisSize: MainAxisSize.min,
                                  mainAxisAlignment: MainAxisAlignment.start,
                                  children: [
                                    SizedBox(
                                      height: getVerticalSize(40),
                                      width: getHorizontalSize(260),
                                      child: ElevatedButton(
                                        style: ElevatedButton.styleFrom(
                                          backgroundColor:
                                              ColorConstant.ghostWhite,
                                          shape: RoundedRectangleBorder(
                                            borderRadius:
                                                BorderRadius.circular(20),
                                          ),
                                        ),
                                        onPressed: _isLoading ? null : _login,
                                        child: _isLoading
                                            ? Container(
                                                height: 20,
                                                width: 20,
                                                child:
                                                    CircularProgressIndicator(
                                                  color: ColorConstant.cyan,
                                                  strokeWidth: 3,
                                                ),
                                              )
                                            : Text(
                                                'Sign in',
                                                style: TextStyle(
                                                    fontFamily: 'Roboto',
                                                    fontSize: 18,
                                                    color: ColorConstant.black),
                                              ),
                                      ),
                                    ),
                                    SizedBox(
                                      height: getVerticalSize(10),
                                    ),
                                    CustomButton(
                                        height: getVerticalSize(40),
                                        text: "Sign up",
                                        variant:
                                            ButtonVariant.OutlineBlack9003f_1,
                                        fontStyle: ButtonFontStyle
                                            .RobotoRomanRegular18WhiteA700,
                                        onTap: () => onTapSignup(context)),
                                  ])),
                        ],
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

onTapSignup(BuildContext context) {
  Navigator.pushNamed(context, AppRoutes.createAccountScreen);
}
