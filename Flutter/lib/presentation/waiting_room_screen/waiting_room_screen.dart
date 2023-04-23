import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/presentation/movie_screen/movie_screen.dart';
import 'package:movie_mixer/services/providers.dart';
import 'package:signalr_netcore/hub_connection_builder.dart';

import '../../core/endpoints.dart';
import '../../models/room_model.dart';

class WaitingRoomScreen extends StatefulWidget {
  WaitingRoomScreen({Key? key, required this.roomId, required this.isHost})
      : super(key: key);
  final String roomId;
  final bool isHost;

  @override
  State<WaitingRoomScreen> createState() => _WaitingRoomScreenState();
}

class _WaitingRoomScreenState extends State<WaitingRoomScreen> {
  late String? _currentUserNumber;

  final ApiProvider provider = new ApiProvider();

  final connection = HubConnectionBuilder().withUrl(Endpoints.Socket).build();
  Future<void> handleOnJoin(List<Object?>? args) async {
    final value = await provider.fetchRoom(widget.roomId);
    setState(() {
      _currentUserNumber = value.usersInRoom.toString();
    });
  }

  Future<void> handleOnStart(List<Object?>? args) async {
    Navigator.push(
        context,
        MaterialPageRoute(
            builder: (context) => MovieScreen(roomId: widget.roomId)));
  }

  @override
  void initState() {
    super.initState();
    connection.start()?.then((_) {
      connection.invoke('JoinRoom', args: [widget.roomId]);
      connection.on("onJoin", handleOnJoin);
      connection.on("onLeave", handleOnJoin);
      connection.on("onStart", handleOnStart);
    });
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
            body: Container(
                width: size.width,
                height: size.height,
                decoration: BoxDecoration(
                    color: ColorConstant.gray,
                    image: DecorationImage(
                        image: AssetImage(ImageConstant.imgPagebackground),
                        fit: BoxFit.cover)),
                child: Container(
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
                              onTap: () {
                                if (widget.isHost) {
                                  provider.deleteRoom(widget.roomId);
                                } else {
                                  provider.leaveRoom(widget.roomId);
                                }
                                Navigator.of(context).pop();
                              }),
                          Container(
                              height: getVerticalSize(556),
                              width: getHorizontalSize(334),
                              margin: getMargin(
                                top: 97,
                              ),
                              child:
                                  Stack(alignment: Alignment.center, children: [
                                Align(
                                    alignment: Alignment.topCenter,
                                    child: Container(
                                        padding: getPadding(
                                            left: 7,
                                            top: 20,
                                            right: 7,
                                            bottom: 20),
                                        decoration: AppDecoration.purple
                                            .copyWith(
                                                borderRadius: BorderRadiusStyle
                                                    .roundedBorder43),
                                        child: Column(
                                            mainAxisSize: MainAxisSize.min,
                                            crossAxisAlignment:
                                                CrossAxisAlignment.start,
                                            mainAxisAlignment:
                                                MainAxisAlignment.start,
                                            children: [
                                              Padding(
                                                  padding: getPadding(left: 10),
                                                  child: Text("Almost got it",
                                                      textAlign: TextAlign.left,
                                                      style: AppStyle
                                                          .txtLemonTuesday30)),
                                              Center(
                                                  child: CopyableText(
                                                text: '${widget.roomId}',
                                              )),
                                              Padding(
                                                  padding: getPadding(
                                                      left: 9,
                                                      top: 6,
                                                      bottom: 15),
                                                  child: Row(
                                                      crossAxisAlignment:
                                                          CrossAxisAlignment
                                                              .start,
                                                      children: [
                                                        Padding(
                                                            padding: getPadding(
                                                                bottom: 61),
                                                            child: Column(
                                                                crossAxisAlignment:
                                                                    CrossAxisAlignment
                                                                        .end,
                                                                mainAxisAlignment:
                                                                    MainAxisAlignment
                                                                        .start,
                                                                children: [
                                                                  Padding(
                                                                      padding: getPadding(
                                                                          right:
                                                                              21),
                                                                      child: Text(
                                                                          "Click to copy",
                                                                          style:
                                                                              AppStyle.txtLemonTuesday15)),
                                                                  Container(
                                                                      height:
                                                                          getVerticalSize(
                                                                              262),
                                                                      width: getHorizontalSize(
                                                                          212),
                                                                      margin: getMargin(
                                                                          top:
                                                                              24),
                                                                      child: FutureBuilder<
                                                                          RoomModel>(
                                                                        future:
                                                                            provider.fetchRoom(widget.roomId),
                                                                        builder:
                                                                            (context,
                                                                                snapshot) {
                                                                          if (snapshot
                                                                              .hasData) {
                                                                            _currentUserNumber =
                                                                                snapshot.data!.usersInRoom.toString();
                                                                            return Stack(alignment: Alignment.topCenter, children: [
                                                                              Align(
                                                                                alignment: Alignment.bottomRight,
                                                                                child: Text(
                                                                                  "$_currentUserNumber",
                                                                                  style: AppStyle.txtRobotoRomanRegular40,
                                                                                ),
                                                                              ),
                                                                              Align(
                                                                                  alignment: Alignment.topCenter,
                                                                                  child: Container(
                                                                                      height: getVerticalSize(240),
                                                                                      width: getHorizontalSize(212),
                                                                                      child: Stack(alignment: Alignment.bottomLeft, children: [
                                                                                        CustomImageView(imagePath: ImageConstant.imgArrow2, height: getVerticalSize(142), width: getHorizontalSize(65), alignment: Alignment.bottomLeft),
                                                                                        Align(alignment: Alignment.bottomLeft, child: Container(width: getHorizontalSize(85), margin: getMargin(left: 20, bottom: 55), child: Text("Wait for everybody", style: AppStyle.txtLemonTuesday20))),
                                                                                        Align(alignment: Alignment.topRight, child: InviteContainer(widget.roomId))
                                                                                      ])))
                                                                            ]);
                                                                          } else if (snapshot
                                                                              .hasError) {
                                                                            return Text('Error loading collections');
                                                                          } else {
                                                                            return CircularProgressIndicator();
                                                                          }
                                                                        },
                                                                      ))
                                                                ])),
                                                        Column(children: [
                                                          Stack(children: [
                                                            Padding(
                                                                padding:
                                                                    getPadding(
                                                                        left:
                                                                            15),
                                                                child: CustomImageView(
                                                                    imagePath:
                                                                        ImageConstant
                                                                            .imgArrow1,
                                                                    height:
                                                                        getVerticalSize(
                                                                            140),
                                                                    width:
                                                                        getHorizontalSize(
                                                                            65))),
                                                            Container(
                                                                width:
                                                                    getHorizontalSize(
                                                                        73),
                                                                margin:
                                                                    getMargin(
                                                                        top:
                                                                            29),
                                                                child: Text(
                                                                    "Send an invite",
                                                                    textAlign:
                                                                        TextAlign
                                                                            .center,
                                                                    style: AppStyle
                                                                        .txtLemonTuesday20))
                                                          ]),
                                                          CustomImageView(
                                                              imagePath:
                                                                  ImageConstant
                                                                      .imgArrow3,
                                                              height:
                                                                  getVerticalSize(
                                                                      83),
                                                              width:
                                                                  getHorizontalSize(
                                                                      40),
                                                              margin: getMargin(
                                                                  left: 10,
                                                                  top: 150))
                                                        ])
                                                      ]))
                                            ]))),
                                widget.isHost
                                    ? CustomButton(
                                        height: getVerticalSize(40),
                                        width: getHorizontalSize(248),
                                        text: "Start",
                                        variant: ButtonVariant.Cyan,
                                        fontStyle:
                                            ButtonFontStyle.RobotoRomanMedium20,
                                        onTap: () async {
                                          bool started = await provider
                                              .startRoom(widget.roomId);
                                          if (!started) {
                                            showDialog(
                                              context: context,
                                              builder: (BuildContext context) {
                                                return AlertDialog(
                                                  title:
                                                      Text("Not enough users"),
                                                  content: Text(
                                                      "Not enough users joined the room to start. Please wait for more users to join the room."),
                                                  actions: [
                                                    TextButton(
                                                      child: Text("OK"),
                                                      onPressed: () {
                                                        Navigator.of(context)
                                                            .pop();
                                                      },
                                                    ),
                                                  ],
                                                );
                                              },
                                            );
                                          } else {
                                            connection.invoke("StartRoom",
                                                args: <Object>[widget.roomId]);
                                            Navigator.push(
                                                context,
                                                MaterialPageRoute(
                                                    builder: (context) =>
                                                        MovieScreen(
                                                            roomId: widget
                                                                .roomId)));
                                          }
                                        },
                                        alignment: Alignment.bottomCenter)
                                    : SizedBox.shrink()
                              ]))
                        ])))));
  }
}
