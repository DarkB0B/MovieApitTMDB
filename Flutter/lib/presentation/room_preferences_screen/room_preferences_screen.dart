import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/models/genre_model.dart';
import '../../services/providers.dart';
import '../waiting_room_screen/waiting_room_screen.dart';

class RoomPreferencesScreen extends StatefulWidget {
  @override
  State<RoomPreferencesScreen> createState() => _RoomPreferencesScreenState();
}

class _RoomPreferencesScreenState extends State<RoomPreferencesScreen> {
  int _selectedValue = 10;
  bool _isMovie = true;
  List<GenreModel> _selectedGenres = [];
  List<int> _selectedGenresIds = [];
  ApiProvider provider = new ApiProvider();
  String roomId = "";

  void _handleGenresChanged(List<GenreModel> genres) {
    setState(() {
      _selectedGenres = genres;
      _selectedGenresIds =
          _selectedGenres.map((genre) => genre.tmdbId!).toList();
    });
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
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
                        children: [
                          CustomImageView(
                              svgPath: ImageConstant.imgArrowBack,
                              height: getVerticalSize(17),
                              width: getHorizontalSize(23),
                              margin: getMargin(left: 1),
                              onTap: () {
                                Navigator.of(context).pop();
                              }),
                          Container(
                              height: getVerticalSize(577),
                              width: getHorizontalSize(334),
                              margin: getMargin(top: 76, bottom: 5),
                              child: Stack(children: [
                                Align(
                                    alignment: Alignment.center,
                                    child: Container(
                                        height: 536,
                                        padding: getPadding(
                                            left: 43,
                                            top: 49,
                                            right: 43,
                                            bottom: 49),
                                        decoration: AppDecoration.purple
                                            .copyWith(
                                                borderRadius: BorderRadiusStyle
                                                    .roundedBorder43),
                                        child: Column(
                                            mainAxisSize: MainAxisSize.min,
                                            mainAxisAlignment:
                                                MainAxisAlignment.spaceEvenly,
                                            children: [
                                              GenrePicker(
                                                onGenresChanged:
                                                    _handleGenresChanged,
                                              ),
                                              Row(
                                                mainAxisAlignment:
                                                    MainAxisAlignment.center,
                                                children: [
                                                  Text("List size:",
                                                      overflow:
                                                          TextOverflow.ellipsis,
                                                      textAlign: TextAlign.left,
                                                      style: AppStyle
                                                          .txtRobotoRomanRegular22),
                                                  Container(
                                                      height:
                                                          getVerticalSize(80),
                                                      width:
                                                          getHorizontalSize(60),
                                                      child: NumberPicker(
                                                          selectedValue:
                                                              _selectedValue,
                                                          onValueChanged:
                                                              (int value) {
                                                            setState(() {
                                                              _selectedValue =
                                                                  value;
                                                            });
                                                          })),
                                                ],
                                              ),
                                              GestureDetector(
                                                  onTap: () {
                                                    // check if the user is a premium user
                                                    // bool isPremiumUser = checkIfUserIsPremium();

                                                    // if (!isPremiumUser) {
                                                    showDialog(
                                                      context: context,
                                                      builder: (BuildContext
                                                          context) {
                                                        return Dialog(
                                                          backgroundColor:
                                                              Colors
                                                                  .transparent,
                                                          child: Stack(
                                                            alignment: Alignment
                                                                .center,
                                                            children: [
                                                              Container(
                                                                padding:
                                                                    getPadding(
                                                                  left: 42,
                                                                  top: 20,
                                                                  right: 42,
                                                                  bottom: 20,
                                                                ),
                                                                decoration:
                                                                    AppDecoration
                                                                        .purple
                                                                        .copyWith(
                                                                  borderRadius:
                                                                      BorderRadiusStyle
                                                                          .roundedBorder43,
                                                                ),
                                                                child: Column(
                                                                  mainAxisSize:
                                                                      MainAxisSize
                                                                          .min,
                                                                  mainAxisAlignment:
                                                                      MainAxisAlignment
                                                                          .start,
                                                                  children: [
                                                                    Container(
                                                                      width:
                                                                          getHorizontalSize(
                                                                        214,
                                                                      ),
                                                                      margin:
                                                                          getMargin(
                                                                        bottom:
                                                                            68,
                                                                      ),
                                                                      child:
                                                                          Text(
                                                                        "Uh oh it seems you’re not a premium user, you can’t do that ",
                                                                        textAlign:
                                                                            TextAlign.center,
                                                                        style: AppStyle
                                                                            .txtRobotoRomanRegular15,
                                                                      ),
                                                                    ),
                                                                  ],
                                                                ),
                                                              ),
                                                              Positioned(
                                                                left: 10,
                                                                bottom: 35,
                                                                child: Row(
                                                                  children: [
                                                                    CustomImageView(
                                                                      imagePath:
                                                                          ImageConstant
                                                                              .imgDoge,
                                                                      height:
                                                                          getSize(
                                                                        100,
                                                                      ),
                                                                      width:
                                                                          getSize(
                                                                        100,
                                                                      ),
                                                                    ),
                                                                  ],
                                                                ),
                                                              ),
                                                              CustomButton(
                                                                margin:
                                                                    getMargin(
                                                                        top:
                                                                            135),
                                                                width: MediaQuery.of(
                                                                            context)
                                                                        .size
                                                                        .width *
                                                                    0.4,
                                                                text:
                                                                    "Get Premium Now!",
                                                                variant:
                                                                    ButtonVariant
                                                                        .OutlineWhiteA700_1,
                                                                padding:
                                                                    ButtonPadding
                                                                        .PaddingAll12,
                                                                fontStyle:
                                                                    ButtonFontStyle
                                                                        .RobotoRomanBold15WhiteA700,
                                                              ),
                                                            ],
                                                          ),
                                                        );
                                                      },
                                                    );

                                                    // }
                                                  },
                                                  child: Padding(
                                                      padding: getPadding(
                                                          left: 21,
                                                          top: 28,
                                                          right: 21),
                                                      child: Row(
                                                          mainAxisAlignment:
                                                              MainAxisAlignment
                                                                  .center,
                                                          children: [
                                                            CustomImageView(
                                                                svgPath:
                                                                    ImageConstant
                                                                        .imgLockLocked,
                                                                height:
                                                                    getVerticalSize(
                                                                        35),
                                                                width:
                                                                    getHorizontalSize(
                                                                        26),
                                                                alignment:
                                                                    Alignment
                                                                        .center),
                                                            Padding(
                                                                padding:
                                                                    getPadding(
                                                                        left:
                                                                            17,
                                                                        top: 7,
                                                                        bottom:
                                                                            3),
                                                                child: Text(
                                                                    "Advanced options",
                                                                    overflow:
                                                                        TextOverflow
                                                                            .ellipsis,
                                                                    textAlign:
                                                                        TextAlign
                                                                            .left,
                                                                    style: AppStyle
                                                                        .txtRobotoRomanBold20)),
                                                          ])))
                                            ]))),
                                Center(
                                  child: Padding(
                                    padding:
                                        EdgeInsets.symmetric(horizontal: 43),
                                    child: Column(
                                      mainAxisSize: MainAxisSize.min,
                                      children: [
                                        CheckboxWidget(
                                          onCheckboxChanged: (value) {
                                            _isMovie = value;
                                          },
                                        ),
                                        Spacer(),
                                        CustomButton(
                                          height: getVerticalSize(40),
                                          text: "Open Room",
                                          variant: ButtonVariant.Cyan,
                                          fontStyle: ButtonFontStyle
                                              .RobotoRomanMedium20,
                                          onTap: () async {
                                            roomId = await provider
                                                .createRoomDiscover(
                                                    _selectedGenresIds,
                                                    _isMovie,
                                                    _selectedValue);
                                            Navigator.push(
                                                context,
                                                MaterialPageRoute(
                                                    builder: (context) =>
                                                        WaitingRoomScreen(
                                                          roomId: roomId,
                                                          isHost: true,
                                                        )));
                                          },
                                        ),
                                      ],
                                    ),
                                  ),
                                )
                              ])),
                        ])))));
  }
}
