import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:share/share.dart';
import 'package:flutter_svg_provider/flutter_svg_provider.dart' as fs;

class InviteContainer extends StatelessWidget {
  final String roomCode;

  InviteContainer(this.roomCode);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: getPadding(left: 36, top: 31, right: 36, bottom: 31),
      decoration: BoxDecoration(
        image: DecorationImage(
            image: fs.Svg(ImageConstant.imgCinematicket), fit: BoxFit.cover),
      ),
      child: GestureDetector(
        onTap: () {
          Share.share(
              'I need your help, quickly! Take this code and join me in the movie mixer: $roomCode');
        },
        child: Column(
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.end,
          children: [
            Padding(
              padding: getPadding(top: 51),
              child: Text(
                "INVITE",
                style: AppStyle.txtRobotoRomanExtraBold20,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
