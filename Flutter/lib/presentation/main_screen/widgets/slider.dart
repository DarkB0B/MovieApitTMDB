import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
// import 'package:movie_mixer/core/endpoints.dart';
import 'package:movie_mixer/presentation/waiting_room_screen/waiting_room_screen.dart';
import 'package:movie_mixer/services/providers.dart';
// import 'package:signalr_netcore/signalr_client.dart';

class ImageCarousel extends StatelessWidget {
  final List<String> imagePaths;
  final List<String> title;
  final List<String> description;
  final List<int> collectionId;
  final Axis scrollDirection;
  final int itemsVisible;
  final double spacing;

  const ImageCarousel({
    Key? key,
    required this.imagePaths,
    this.title = const [],
    this.collectionId = const [],
    this.description = const [],
    this.scrollDirection = Axis.horizontal,
    this.itemsVisible = 3,
    this.spacing = 10,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final itemWidth = MediaQuery.of(context).size.width / itemsVisible;
    final itemHeight = itemWidth * 1.5;
    late String roomId;
    ApiProvider provider = new ApiProvider();
    // final connection = HubConnectionBuilder().withUrl(Endpoints.Socket).build();

    return Padding(
      padding: const EdgeInsets.only(top: 10),
      child: Container(
        height: scrollDirection == Axis.horizontal ? itemHeight : null,
        width: scrollDirection == Axis.vertical ? itemWidth : null,
        child: ListView.separated(
          itemCount: imagePaths.length,
          scrollDirection: scrollDirection,
          itemBuilder: (BuildContext context, int index) {
            return GestureDetector(
              onTap: () {
                showDialog(
                  context: context,
                  builder: (BuildContext context) {
                    return Dialog(
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0),
                      ),
                      backgroundColor: ColorConstant.purple,
                      child: SizedBox(
                        height: itemHeight,
                        child: Row(
                          children: [
                            Image.network(
                              imagePaths[index],
                              width: itemWidth,
                              height: itemHeight,
                              fit: BoxFit.cover,
                            ),
                            SizedBox(
                              width: spacing,
                            ),
                            Expanded(
                              child: Padding(
                                padding: const EdgeInsets.all(10.0),
                                child: Column(
                                  mainAxisAlignment: MainAxisAlignment.center,
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  children: [
                                    Text(title[index],
                                        style: AppStyle
                                            .txtRobotoRomanRegular20WhiteA70001),
                                    SizedBox(
                                      height: 10,
                                    ),
                                    Text(description[index],
                                        style:
                                            AppStyle.txtRobotoRomanRegular12),
                                    SizedBox(
                                      height: 10,
                                    ),
                                    CustomButton(
                                        height: getVerticalSize(40),
                                        width: getHorizontalSize(248),
                                        text: "Open room",
                                        variant: ButtonVariant.Cyan,
                                        fontStyle:
                                            ButtonFontStyle.RobotoRomanMedium20,
                                        onTap: () async {
                                          int id = collectionId[index];
                                          roomId = await provider
                                              .createRoomCollection(id);
                                          Navigator.push(
                                              context,
                                              MaterialPageRoute(
                                                  builder: (context) =>
                                                      WaitingRoomScreen(
                                                        roomId: roomId,
                                                        isHost: true,
                                                      )));
                                        },
                                        alignment: Alignment.bottomCenter)
                                  ],
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    );
                  },
                );
              },
              child: Container(
                height: scrollDirection == Axis.horizontal ? itemHeight : null,
                width: scrollDirection == Axis.vertical ? itemWidth : null,
                child: Row(
                  children: [
                    ClipRRect(
                      borderRadius: BorderRadius.circular(10),
                      child: Image.network(
                        imagePaths[index],
                        width: itemWidth,
                        height: itemHeight,
                        fit: BoxFit.cover,
                      ),
                    ),
                    SizedBox(
                      width: spacing,
                    ),
                  ],
                ),
              ),
            );
          },
          separatorBuilder: (BuildContext context, int index) => SizedBox(
            width: scrollDirection == Axis.horizontal ? spacing : null,
            height: scrollDirection == Axis.vertical ? spacing : null,
          ),
        ),
      ),
    );
  }
}
