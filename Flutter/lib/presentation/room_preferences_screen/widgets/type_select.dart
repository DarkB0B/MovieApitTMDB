import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';

class CheckboxWidget extends StatefulWidget {
  final ValueChanged<bool> onCheckboxChanged;

  CheckboxWidget({required this.onCheckboxChanged});

  @override
  _CheckboxWidgetState createState() => _CheckboxWidgetState();
}

class _CheckboxWidgetState extends State<CheckboxWidget> {
  bool _isMovieSelected = true;
  bool _isTvSelected = false;

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Expanded(
          child: GestureDetector(
            onTap: () {
              setState(() {
                _isMovieSelected = true;
                _isTvSelected = false;
                widget.onCheckboxChanged(_isMovieSelected);
              });
            },
            child: Container(
              height: 42.0,
              decoration: BoxDecoration(
                color: _isMovieSelected ? ColorConstant.pink : Colors.grey,
                borderRadius: BorderRadius.only(
                  topLeft: Radius.circular(10.0),
                  bottomLeft: Radius.circular(10.0),
                ),
              ),
              child: Center(
                child: Text(
                  "Movie",
                  style: TextStyle(
                    fontSize: 20.0,
                    color: ColorConstant.ghostWhite,
                  ),
                ),
              ),
            ),
          ),
        ),
        Expanded(
          child: GestureDetector(
            onTap: () {
              setState(() {
                _isMovieSelected = false;
                _isTvSelected = true;
                widget.onCheckboxChanged(_isMovieSelected);
              });
            },
            child: Container(
              height: 42.0,
              decoration: BoxDecoration(
                color: _isTvSelected ? ColorConstant.cyan : Colors.grey,
                borderRadius: BorderRadius.only(
                  topRight: Radius.circular(10.0),
                  bottomRight: Radius.circular(10.0),
                ),
              ),
              child: Center(
                child: Text(
                  "TV Series",
                  style: TextStyle(
                    fontSize: 20.0,
                    color: ColorConstant.ghostWhite,
                  ),
                ),
              ),
            ),
          ),
        ),
      ],
    );
  }
}
