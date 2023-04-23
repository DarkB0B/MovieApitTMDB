import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/models/movie_model.dart';

class MovieListItemWidget extends StatelessWidget {
  final MovieModel movie;

  const MovieListItemWidget({required this.movie});

  @override
  Widget build(BuildContext context) {
    final double itemWidth = getHorizontalSize(275);
    final double itemHeight = getVerticalSize(110);

    return Column(
      children: [
        DecoratedBox(
          decoration: BoxDecoration(
            color: ColorConstant.gray,
            borderRadius: BorderRadius.circular(getHorizontalSize(15)),
          ),
          child: SizedBox(
            height: itemHeight,
            width: itemWidth,
            child: Row(
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Padding(
                  padding: EdgeInsets.only(left: getHorizontalSize(10)),
                  child: SizedBox(
                    height: getVerticalSize(95),
                    width: getHorizontalSize(73),
                    child: ClipRRect(
                      borderRadius: BorderRadius.circular(10),
                      child: Image(
                        image: NetworkImage(
                            'https://image.tmdb.org/t/p/w500${movie.posterPath}'),
                        fit: BoxFit.cover,
                      ),
                    ),
                  ),
                ),
                SizedBox(width: getHorizontalSize(10)),
                Expanded(
                  child: Container(
                    height:
                        itemHeight, // set height of container to match the parent SizedBox
                    child: Align(
                      alignment: Alignment.centerLeft,
                      child: Text(
                        movie.title!,
                        textAlign: TextAlign.left,
                        style: AppStyle.txtRobotoRomanRegular20,
                      ),
                    ),
                  ),
                ),
                SizedBox(width: getHorizontalSize(10)),
              ],
            ),
          ),
        ),
        SizedBox(
            height: 10), // add a gap of 10 between MovieListItemWidget widgets
      ],
    );
  }
}
