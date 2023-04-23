import 'dart:ui';
import 'package:flutter/material.dart';

class ColorConstant {
  static Color ghostWhite70 = fromHex('#b2fffaff');

  static Color gray = fromHex('#1a1a1d');

  static Color black25 = fromHex('#3f000000');

  static Color black = fromHex('#000000');

  static Color ghostWhite15 = fromHex('#26fffaff');

  static Color purple = fromHex('#2e294e');

  static Color pink = fromHex('#d8315b');

  static Color cyan = fromHex('#17bebb');

  static Color white = fromHex('#ffffff');

  static Color ghostWhite = fromHex('#fffaff');

  static Color transparent = fromHex('#00000000');

  static Color fromHex(String hexString) {
    final buffer = StringBuffer();
    if (hexString.length == 6 || hexString.length == 7) buffer.write('ff');
    buffer.write(hexString.replaceFirst('#', ''));
    return Color(int.parse(buffer.toString(), radix: 16));
  }
}
