import 'package:flutter/cupertino.dart';
import 'package:movie_mixer/core/app_export.dart';

class NumberPicker extends StatelessWidget {
  final int selectedValue;
  final ValueChanged<int> onValueChanged;

  NumberPicker({
    required this.selectedValue,
    required this.onValueChanged,
  });

  @override
  Widget build(BuildContext context) {
    return CupertinoPicker(
        itemExtent: 30,
        looping: false,
        squeeze: 1,
        children: List<Widget>.generate(31, (int index) {
          final value = index + 10; // fixed here
          final isSelected = (value == selectedValue);
          final color =
              isSelected ? ColorConstant.cyan : ColorConstant.ghostWhite;
          final fontSize = isSelected ? 25.0 : 18.0;
          return Container(
            alignment: Alignment.center,
            child: Text(
              value.toString(),
              style: TextStyle(
                color: color,
                fontSize: fontSize,
              ),
            ),
          );
        }),
        onSelectedItemChanged: (int index) {
          onValueChanged(index + 10);
        },
        selectionOverlay: null);
  }
}
