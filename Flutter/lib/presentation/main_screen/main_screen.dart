import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/core/endpoints.dart';
import 'package:movie_mixer/models/movie_collection_model.dart';
import 'package:movie_mixer/presentation/waiting_room_screen/waiting_room_screen.dart';
import 'package:movie_mixer/widgets/app_bar/appbar_image.dart';
import 'package:movie_mixer/widgets/app_bar/custom_app_bar.dart';
import 'package:movie_mixer/widgets/custom_text_form_field.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:movie_mixer/services/providers.dart';
import 'package:signalr_netcore/hub_connection_builder.dart';

class MainScreen extends StatefulWidget {
  const MainScreen({Key? key}) : super(key: key);

  @override
  State<MainScreen> createState() => _MainScreenState();
}

class _MainScreenState extends State<MainScreen> {
  late ApiProvider provider = ApiProvider();
  final TextEditingController _textController = TextEditingController();
  final connection = HubConnectionBuilder().withUrl(Endpoints.Socket).build();

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
            resizeToAvoidBottomInset: false,
            backgroundColor: ColorConstant.gray,
            body: Container(
                height: size.height,
                width: MediaQuery.of(context).size.width,
                child: Stack(alignment: Alignment.bottomCenter, children: [
                  Align(
                      alignment: Alignment.center,
                      child: Container(
                          height: size.height,
                          width: getHorizontalSize(359),
                          child: Stack(alignment: Alignment.center, children: [
                            CustomImageView(
                                imagePath: ImageConstant.imgPagebackground,
                                height: getVerticalSize(800),
                                width: getHorizontalSize(360),
                                alignment: Alignment.center),
                            Align(
                                alignment: Alignment.center,
                                child: Padding(
                                    padding: getPadding(top: 17),
                                    child: Column(
                                        mainAxisSize: MainAxisSize.min,
                                        mainAxisAlignment:
                                            MainAxisAlignment.start,
                                        children: [
                                          CustomAppBar(
                                              height: getVerticalSize(30),
                                              title: Padding(
                                                  padding: getPadding(left: 14),
                                                  child: Text(
                                                      "Welcome Traveller!",
                                                      overflow:
                                                          TextOverflow.ellipsis,
                                                      textAlign: TextAlign.left,
                                                      style: AppStyle
                                                          .txtRobotoRomanRegular20)),
                                              actions: [
                                                AppbarImage(
                                                    onTap: () =>
                                                        onTapMenu(context),
                                                    height: getVerticalSize(30),
                                                    width:
                                                        getHorizontalSize(30),
                                                    imagePath: ImageConstant
                                                        .imgProfile,
                                                    margin:
                                                        getMargin(right: 14)),
                                              ]),
                                          CustomImageView(
                                              imagePath:
                                                  ImageConstant.imgPremiumBaner,
                                              height: getVerticalSize(110),
                                              width: getHorizontalSize(359),
                                              margin: getMargin(top: 17)),
                                          Padding(
                                              padding: getPadding(top: 34),
                                              child: Text("Become room master",
                                                  overflow:
                                                      TextOverflow.ellipsis,
                                                  textAlign: TextAlign.left,
                                                  style: AppStyle
                                                      .txtRobotoRomanRegular22)),
                                          CustomButton(
                                              height: getVerticalSize(40),
                                              width: getHorizontalSize(181),
                                              text: "Create room",
                                              margin: getMargin(top: 8),
                                              variant: ButtonVariant
                                                  .OutlineBlack9003f_2,
                                              fontStyle: ButtonFontStyle
                                                  .RobotoRomanRegular18,
                                              onTap: () =>
                                                  onRoomCreate(context)),
                                          Padding(
                                              padding: getPadding(top: 6),
                                              child: Row(
                                                  mainAxisAlignment:
                                                      MainAxisAlignment.center,
                                                  crossAxisAlignment:
                                                      CrossAxisAlignment.start,
                                                  children: [
                                                    Padding(
                                                        padding: getPadding(
                                                            top: 10,
                                                            bottom: 14),
                                                        child: SizedBox(
                                                            width:
                                                                getHorizontalSize(
                                                                    80),
                                                            child: Divider(
                                                                height:
                                                                    getVerticalSize(
                                                                        1),
                                                                thickness:
                                                                    getVerticalSize(
                                                                        1),
                                                                color: ColorConstant
                                                                    .ghostWhite))),
                                                    Padding(
                                                        padding: getPadding(
                                                            left: 15),
                                                        child: Text("or",
                                                            overflow:
                                                                TextOverflow
                                                                    .ellipsis,
                                                            textAlign:
                                                                TextAlign.left,
                                                            style: AppStyle
                                                                .txtRobotoRomanRegular22)),
                                                    Padding(
                                                        padding: getPadding(
                                                            top: 10,
                                                            bottom: 14),
                                                        child: SizedBox(
                                                            width:
                                                                getHorizontalSize(
                                                                    95),
                                                            child: Divider(
                                                                height:
                                                                    getVerticalSize(
                                                                        1),
                                                                thickness:
                                                                    getVerticalSize(
                                                                        1),
                                                                color: ColorConstant
                                                                    .ghostWhite,
                                                                indent:
                                                                    getHorizontalSize(
                                                                        15))))
                                                  ])),
                                          Padding(
                                              padding: getPadding(
                                                  top: 4, bottom: 10),
                                              child: Text("Join your friends",
                                                  overflow:
                                                      TextOverflow.ellipsis,
                                                  textAlign: TextAlign.left,
                                                  style: AppStyle
                                                      .txtRobotoRomanRegular22)),
                                          Row(
                                            mainAxisAlignment:
                                                MainAxisAlignment.center,
                                            children: [
                                              Align(
                                                alignment: Alignment.center,
                                                child: Stack(
                                                  children: [
                                                    CustomTextFormField(
                                                      focusNode: FocusNode(),
                                                      controller:
                                                          _textController,
                                                      hintText: "Room code",
                                                      width: getHorizontalSize(
                                                          180),
                                                      textInputAction:
                                                          TextInputAction.done,
                                                    ),
                                                    Positioned(
                                                      right: 0,
                                                      top: -5,
                                                      child: IconButton(
                                                          icon: const Icon(Icons
                                                              .check_circle_rounded),
                                                          color: ColorConstant
                                                              .cyan,
                                                          iconSize: 35,
                                                          onPressed: () async {
                                                            String data =
                                                                _textController
                                                                    .text;
                                                            if (data
                                                                .isNotEmpty) {
                                                              bool success =
                                                                  await provider
                                                                      .joinRoom(
                                                                          data);

                                                              if (!success) {
                                                                showDialog(
                                                                  context:
                                                                      context,
                                                                  builder:
                                                                      (BuildContext
                                                                          context) {
                                                                    return AlertDialog(
                                                                      title: Text(
                                                                          "Failed To Join Room"),
                                                                      actions: [
                                                                        TextButton(
                                                                          child:
                                                                              Text("OK"),
                                                                          onPressed:
                                                                              () {
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
                                                    ),
                                                  ],
                                                ),
                                              ),
                                            ],
                                          ),

                                          Spacer(),
                                          // RoomModal(),
                                          // Spacer(),
                                        ])))
                          ]))),
                  Align(
                      alignment: Alignment.bottomCenter,
                      child: Padding(
                          padding: getPadding(bottom: 100),
                          child: Column(
                              mainAxisSize: MainAxisSize.min,
                              crossAxisAlignment: CrossAxisAlignment.start,
                              mainAxisAlignment: MainAxisAlignment.start,
                              children: [
                                Padding(
                                    padding: getPadding(left: 10),
                                    child: Text("Check featured collections",
                                        overflow: TextOverflow.ellipsis,
                                        textAlign: TextAlign.left,
                                        style:
                                            AppStyle.txtRobotoRomanRegular20)),
                                FutureBuilder<List<MovieCollectionModel>>(
                                  future: provider.fetchCollection(),
                                  builder: (context, snapshot) {
                                    if (snapshot.hasData) {
                                      final collections = snapshot.data!;
                                      return ImageCarousel(
                                        collectionId: collections
                                            .map((c) => c.id!)
                                            .toList(),
                                        imagePaths: collections
                                            .map((c) => c.imagePath!)
                                            .toList(),
                                        title: collections
                                            .map((c) => c.title!)
                                            .toList(),
                                        description: collections
                                            .map((c) => c.description!)
                                            .toList(),
                                        scrollDirection: Axis.horizontal,
                                        itemsVisible: 3,
                                      );
                                    } else if (snapshot.hasError) {
                                      return Center(
                                        child: Text('Error loading collections',
                                            style: AppStyle
                                                .txtRobotoRomanRegular20),
                                      );
                                    } else {
                                      return CircularProgressIndicator();
                                    }
                                  },
                                )
                              ]))),
                  GestureDetector(
                    // ignore: deprecated_member_use
                    onTap: () => launch('https://raidshadowlegends.com/'),
                    child: Container(
                      height: getVerticalSize(80),
                      width: getHorizontalSize(360),
                      child: Stack(
                        alignment: Alignment.topRight,
                        children: [
                          CustomImageView(
                            imagePath: ImageConstant.imgRaidSh,
                            height: getVerticalSize(80),
                            width: getHorizontalSize(360),
                            alignment: Alignment.center,
                          ),
                          Positioned(
                            top: getVerticalSize(5),
                            right: getHorizontalSize(5),
                            child: CustomImageView(
                              svgPath: ImageConstant.imgClose,
                              height: getSize(6),
                              width: getSize(6),
                              alignment: Alignment.topRight,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ]))));
  }

  onTapMenu(BuildContext context) {
    Navigator.pushNamed(context, AppRoutes.accountPreferencesScreen);
  }

  onRoomCreate(BuildContext context) {
    Navigator.pushNamed(context, AppRoutes.roomPreferencesScreen);
  }
}
