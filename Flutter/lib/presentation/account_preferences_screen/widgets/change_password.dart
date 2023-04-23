import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/services/providers.dart';

class ChangePasswordDialog extends StatefulWidget {
  const ChangePasswordDialog({
    Key? key,
  }) : super(key: key);

  @override
  _ChangePasswordDialogState createState() => _ChangePasswordDialogState();
}

class _ChangePasswordDialogState extends State<ChangePasswordDialog> {
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _oldPasswordController = TextEditingController();
  final TextEditingController _newPasswordController = TextEditingController();

  bool _isLoading = false;

  Future<void> _changePassword() async {
    setState(() {
      _isLoading = true;
    });

    final String username = _usernameController.text;
    final String oldPassword = _oldPasswordController.text;
    final String newPassword = _newPasswordController.text;

    try {
      await ApiProvider().changePassword(username, oldPassword, newPassword);
      Navigator.pop(context);
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Password changed successfully!'),
        ),
      );
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Something went wrong!'),
        ),
      );
    }

    setState(() {
      _isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      shape: RoundedRectangleBorder(
        side: BorderSide(color: ColorConstant.ghostWhite, width: 1),
        borderRadius: BorderRadius.all(
          Radius.circular(43),
        ),
      ),
      backgroundColor: ColorConstant.purple,
      child: SingleChildScrollView(
        child: Padding(
          padding: EdgeInsets.all(25),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Change Password',
                style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                  fontFamily: 'Roboto',
                  color: ColorConstant.ghostWhite,
                ),
              ),
              SizedBox(height: 16),
              TextFormField(
                controller: _usernameController,
                obscureText: true,
                decoration: InputDecoration(
                  isDense: true,
                  filled: true,
                  fillColor: ColorConstant.ghostWhite,
                  enabledBorder: OutlineInputBorder(
                    borderSide: BorderSide(width: 1, color: ColorConstant.gray),
                    borderRadius: BorderRadius.circular(10.0),
                  ),
                  focusedBorder: OutlineInputBorder(
                    borderSide: BorderSide(width: 2, color: ColorConstant.cyan),
                    borderRadius: BorderRadius.circular(10.0),
                  ),
                  hintText: "Enter username",
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
              SizedBox(height: 16),
              TextFormField(
                controller: _oldPasswordController,
                obscureText: true,
                decoration: InputDecoration(
                  isDense: true,
                  filled: true,
                  fillColor: ColorConstant.ghostWhite,
                  enabledBorder: OutlineInputBorder(
                    borderSide: BorderSide(width: 1, color: ColorConstant.gray),
                    borderRadius: BorderRadius.circular(10.0),
                  ),
                  focusedBorder: OutlineInputBorder(
                    borderSide: BorderSide(width: 2, color: ColorConstant.cyan),
                    borderRadius: BorderRadius.circular(10.0),
                  ),
                  hintText: "Enter current password",
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
              SizedBox(height: 16),
              TextFormField(
                controller: _newPasswordController,
                obscureText: true,
                decoration: InputDecoration(
                  isDense: true,
                  filled: true,
                  fillColor: ColorConstant.ghostWhite,
                  enabledBorder: OutlineInputBorder(
                    borderSide: BorderSide(width: 1, color: ColorConstant.gray),
                    borderRadius: BorderRadius.circular(10.0),
                  ),
                  focusedBorder: OutlineInputBorder(
                    borderSide: BorderSide(width: 2, color: ColorConstant.cyan),
                    borderRadius: BorderRadius.circular(10.0),
                  ),
                  hintText: "Enter new password",
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
              SizedBox(height: 16),
              Center(
                child: SizedBox(
                  height: getVerticalSize(40),
                  width: getHorizontalSize(180),
                  child: ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: ColorConstant.cyan,
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(20),
                      ),
                    ),
                    onPressed: _isLoading ? null : _changePassword,
                    child: _isLoading
                        ? Container(
                            height: 20,
                            width: 20,
                            child: CircularProgressIndicator(
                              color: ColorConstant.cyan,
                              strokeWidth: 3,
                            ),
                          )
                        : Text('Change Password'),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
