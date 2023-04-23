import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/core/endpoints.dart';
import 'package:movie_mixer/models/movie_list_model.dart';
import 'package:signalr_netcore/signalr_client.dart';

import '../../services/providers.dart';

// ignore: must_be_immutable
class AfterSelectionScreen extends StatefulWidget {
  AfterSelectionScreen({Key? key, required this.roomId}) : super(key: key);

  @override
  State<AfterSelectionScreen> createState() => _AfterSelectionScreenState();
  final String roomId;
  late bool isFinished = false;
}

class _AfterSelectionScreenState extends State<AfterSelectionScreen> {
  final connection = HubConnectionBuilder().withUrl(Endpoints.Socket).build();
  late ApiProvider provider = ApiProvider();

  @override
  void initState() {
    super.initState();
    connection.start()?.then((_) {
      connection.invoke('JoinRoom', args: [widget.roomId]);
      provider.fetchFinalMovieList(widget.roomId).then((value) {
        if (value?.id != "notready") {
          connection.invoke("ListReady", args: [widget.roomId]);
          setState(() {
            widget.isFinished = true;
          });
        } else {
          connection.on("onListReady", (data) {
            setState(() {
              widget.isFinished = true;
            });
          });
        }
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        extendBody: true,
        extendBodyBehindAppBar: true,
        backgroundColor: ColorConstant.gray,
        body: Container(
          width: size.width,
          height: size.height,
          decoration: BoxDecoration(
            color: ColorConstant.gray,
            image: DecorationImage(
              image: AssetImage(ImageConstant.imgPagebackground),
              fit: BoxFit.cover,
            ),
          ),
          child: Stack(
            children: [
              Container(
                width: double.maxFinite,
                padding: getPadding(left: 13, right: 13),
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Container(
                      height: getVerticalSize(550),
                      width: getHorizontalSize(330),
                      child: Stack(
                        alignment: Alignment.bottomCenter,
                        children: [
                          Align(
                            alignment: Alignment.topCenter,
                            child: Container(
                              padding: getPadding(all: 22),
                              decoration: AppDecoration.purple.copyWith(
                                borderRadius: BorderRadiusStyle.roundedBorder43,
                              ),
                              child: Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                mainAxisAlignment: MainAxisAlignment.start,
                                children: [
                                  if (widget.isFinished == false) ...[
                                    Center(
                                      child: Column(
                                        children: [
                                          Text(
                                            "Waiting for others...",
                                            textAlign: TextAlign.center,
                                            style: AppStyle
                                                .txtRobotoRomanRegular25,
                                          ),
                                          Image.asset(
                                            ImageConstant.imgWaiting,
                                            height: getVerticalSize(200),
                                            width: getHorizontalSize(200),
                                          ),
                                        ],
                                      ),
                                    ),
                                  ] else if (widget.isFinished == true) ...[
                                    Text(
                                      "People have spoken",
                                      overflow: TextOverflow.ellipsis,
                                      textAlign: TextAlign.left,
                                      style: AppStyle.txtRobotoRomanBold20,
                                    ),
                                    SizedBox(height: 10),
                                    FutureBuilder<MovieListModel?>(
                                      future: provider
                                          .fetchFinalMovieList(widget.roomId),
                                      builder: (context, snapshot) {
                                        if (snapshot.hasData) {
                                          final movies = snapshot.data!.movies;
                                          return Expanded(
                                            child: ListView.builder(
                                              itemCount: movies!.length,
                                              itemBuilder:
                                                  (BuildContext context,
                                                      int index) {
                                                return MovieListItemWidget(
                                                  movie: movies[index],
                                                );
                                              },
                                            ),
                                          );
                                        } else if (snapshot.hasError) {
                                          return Text(
                                              'Error loading collections');
                                        } else {
                                          return Center(
                                            child: CircularProgressIndicator(),
                                          );
                                        }
                                      },
                                    ),
                                  ],
                                ],
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
              if (widget.isFinished == true) ...[
                Align(
                  alignment: Alignment.bottomCenter,
                  child: Expanded(
                    child: CustomButton(
                      height: getVerticalSize(40),
                      width: getHorizontalSize(160),
                      margin: getMargin(bottom: 100),
                      text: "Awesome!",
                      variant: ButtonVariant.OutlineWhiteA700_2,
                      padding: ButtonPadding.PaddingAll12,
                      fontStyle: ButtonFontStyle.RobotoRomanBold15WhiteA700,
                      onTap: () {
                        connection.invoke("LeaveRoom", args: [widget.roomId]);
                        connection.stop();
                        Navigator.pushNamed(context, AppRoutes.mainScreen);
                      },
                    ),
                  ),
                ),
              ]
            ],
          ),
        ),
      ),
    );
  }
}
