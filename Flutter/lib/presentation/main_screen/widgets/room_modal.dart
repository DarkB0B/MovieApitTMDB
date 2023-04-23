import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/presentation/waiting_room_screen/waiting_room_screen.dart';
import 'package:movie_mixer/services/providers.dart';
import 'package:movie_mixer/widgets/custom_text_form_field.dart';
import 'package:signalr_netcore/hub_connection_builder.dart';

import '../../../core/endpoints.dart';
import 'hero_dialog_route.dart';

class RoomModal extends StatelessWidget {
  const RoomModal({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(32.0),
      child: GestureDetector(
        onTap: () {
          Navigator.of(context).push(HeroDialogRoute(builder: (context) {
            return _AddTodoPopupCard();
          }));
        },
        child: Hero(
          tag: _heroAddTodo,
          child: CustomButton(
              height: getVerticalSize(40),
              width: getHorizontalSize(181),
              text: "Discover it!",
              variant: ButtonVariant.Purple,
              fontStyle: ButtonFontStyle.RobotoRomanMedium20),
        ),
      ),
    );
  }
}

/// Tag-value used for the add todo popup button.
const String _heroAddTodo = 'add-todo-hero';

class _AddTodoPopupCard extends StatelessWidget {
  _AddTodoPopupCard({Key? key}) : super(key: key);
  final TextEditingController _textController = TextEditingController();
  final ApiProvider provider = new ApiProvider();
  final connection = HubConnectionBuilder().withUrl(Endpoints.Socket).build();
  @override
  Widget build(BuildContext context) {
    return Center(
      child: Padding(
        padding: const EdgeInsets.all(32.0),
        child: Hero(
          tag: _heroAddTodo,
          child: Material(
            color: ColorConstant.purple,
            elevation: 2,
            shape:
                RoundedRectangleBorder(borderRadius: BorderRadius.circular(32)),
            child: SingleChildScrollView(
              child: Padding(
                padding: const EdgeInsets.all(12.0),
                child: Column(
                  children: [
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        CustomTextFormField(
                            focusNode: FocusNode(),
                            controller: _textController,
                            hintText: "Room code",
                            width: getHorizontalSize(180),
                            margin: getMargin(right: 15),
                            textInputAction: TextInputAction.done),
                        IconButton(
                            icon: const Icon(Icons.check_circle_rounded),
                            color: ColorConstant.cyan,
                            iconSize: 40,
                            onPressed: () async {
                              String data = _textController.text;
                              if (data.isNotEmpty) {
                                bool success = await provider.joinRoom(data);

                                if (!success) {
                                  showDialog(
                                    context: context,
                                    builder: (BuildContext context) {
                                      return AlertDialog(
                                        title: Text("Failed To Join Room"),
                                        actions: [
                                          TextButton(
                                            child: Text("OK"),
                                            onPressed: () {
                                              Navigator.of(context).pop();
                                            },
                                          ),
                                        ],
                                      );
                                    },
                                  );
                                } else {
                                  Navigator.push(
                                      context,
                                      MaterialPageRoute(
                                          builder: (context) =>
                                              WaitingRoomScreen(
                                                roomId: data,
                                                isHost: false,
                                              )));
                                }
                              }
                            }),
                      ],
                    ),
                    CustomButton(
                        height: getVerticalSize(40),
                        width: getHorizontalSize(248),
                        text: "Create new room",
                        variant: ButtonVariant.Cyan,
                        fontStyle: ButtonFontStyle.RobotoRomanMedium20,
                        onTap: () => onRoomCreate(context),
                        alignment: Alignment.bottomCenter)
                  ],
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}

onRoomCreate(BuildContext context) {
  Navigator.pushNamed(context, AppRoutes.roomPreferencesScreen);
}
