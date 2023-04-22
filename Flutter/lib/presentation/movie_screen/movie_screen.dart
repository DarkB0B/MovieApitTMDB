import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/models/movie_list_model.dart';
import 'package:movie_mixer/models/movie_model.dart';
import 'package:movie_mixer/presentation/after_selection_screen/after_selection_screen.dart';
import 'package:movie_mixer/services/providers.dart';
import 'package:movie_mixer/widgets/custom_icon_button.dart';

class MovieScreen extends StatefulWidget {
  MovieScreen({Key? key, required this.roomId}) : super(key: key);
  final String roomId;
  @override
  State<MovieScreen> createState() => _MovieScreenState();
}

class _MovieScreenState extends State<MovieScreen> {
  int _selectedIndex = 0;
  late String roomId = '';
  late ApiProvider provider = ApiProvider();
  late Future<MovieListModel?> futureMovieList;
  late List<MovieModel> movies = [];
  bool clickblock = false;
  ScrollController _scrollController = ScrollController();

  @override
  void initState() {
    super.initState();
    roomId = widget.roomId;
    futureMovieList = provider.fetchStarterMovieList(roomId);
  }

  void _scrollToBottom() {
    _scrollController.animateTo(_scrollController.position.maxScrollExtent,
        duration: Duration(milliseconds: 500), curve: Curves.ease);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      extendBodyBehindAppBar: false,
      body: FutureBuilder<MovieListModel?>(
        future: provider.fetchStarterMovieList(roomId),
        builder: (context, snapshot) {
          if (snapshot.hasData) {
            final movieList = snapshot.data!;
            return Stack(
              children: [
                Container(
                  decoration: BoxDecoration(
                    color: ColorConstant.gray,
                    image: DecorationImage(
                      opacity: 120,
                      image: NetworkImage(
                          'https://image.tmdb.org/t/p/w500${movieList.movies![_selectedIndex].posterPath}'),
                      fit: BoxFit.cover,
                    ),
                  ),
                ),
                Center(
                  child: Container(
                    height: MediaQuery.of(context).size.height * 0.9,
                    width: MediaQuery.of(context).size.width * 0.85,
                    decoration: AppDecoration.fillGray900.copyWith(
                      borderRadius: BorderRadiusStyle.roundedBorder43,
                    ),
                    child: SingleChildScrollView(
                      controller: _scrollController,
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Image.network(
                            'https://image.tmdb.org/t/p/w500${movieList.movies![_selectedIndex].posterPath}',
                            width: MediaQuery.of(context).size.width,
                          ),
                          SizedBox(height: 30),
                          Text(
                            movieList.movies![_selectedIndex].title!,
                            style: AppStyle.txtRobotoRomanRegular25,
                            textAlign: TextAlign.center,
                          ),
                          SizedBox(height: 10),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                            children: [
                              Row(
                                children: [
                                  CustomImageView(
                                    svgPath: ImageConstant.imgStar,
                                    height: 32,
                                    width: 34,
                                    margin:
                                        EdgeInsets.only(right: 6, bottom: 3),
                                  ),
                                  Text(
                                    movieList
                                        .movies![_selectedIndex].voteAvredge!,
                                    style: AppStyle.txtRobotoRomanLight20,
                                  ),
                                ],
                              ),
                              Text(
                                movieList.movies![_selectedIndex].releaseDate!,
                                overflow: TextOverflow.ellipsis,
                                style: AppStyle.txtRobotoRomanLight20,
                              ),
                              Row(
                                children: [
                                  CustomImageView(
                                    svgPath: ImageConstant.imgPopularity,
                                    height: 20,
                                    width: 35,
                                    margin:
                                        EdgeInsets.only(right: 6, bottom: 3),
                                  ),
                                  Text(
                                    movieList
                                        .movies![_selectedIndex].popularity!
                                        .split(".")[0],
                                    style: AppStyle.txtRobotoRomanLight20,
                                  ),
                                ],
                              ),
                            ],
                          ),
                          SizedBox(height: 30),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.spaceAround,
                            children: [
                              CustomIconButton(
                                height: 64,
                                width: 64,
                                variant: IconButtonVariant.RoundPink,
                                onTap: () {
                                  if (_selectedIndex + 1 >=
                                          movieList.movies!.length &&
                                      clickblock == false) {
                                    setState(() {
                                      clickblock = true;
                                    });

                                    provider.addMovieList(roomId, movies);
                                    Navigator.push(
                                        context,
                                        MaterialPageRoute(
                                            builder: (context) =>
                                                AfterSelectionScreen(
                                                    roomId: widget.roomId)));
                                  } else {
                                    setState(() {
                                      _selectedIndex++;
                                    });
                                  }
                                },
                                child: CustomImageView(
                                  imagePath: ImageConstant.imgThumbsDown,
                                ),
                              ),
                              Padding(
                                padding: EdgeInsets.only(top: 40),
                                child: CustomIconButton(
                                  height: 48,
                                  width: 48,
                                  onTap: _scrollToBottom,
                                  child: CustomImageView(
                                    imagePath: ImageConstant.imgUpArrow,
                                  ),
                                ),
                              ),
                              CustomIconButton(
                                height: 64,
                                width: 64,
                                variant: IconButtonVariant.RoundCyan,
                                onTap: () {
                                  movies.add(movieList.movies![_selectedIndex]);
                                  if (_selectedIndex + 1 >=
                                          movieList.movies!.length &&
                                      clickblock == false) {
                                    setState(() {
                                      clickblock = true;
                                    });

                                    provider.addMovieList(roomId, movies);
                                    Navigator.push(
                                        context,
                                        MaterialPageRoute(
                                            builder: (context) =>
                                                AfterSelectionScreen(
                                                    roomId: widget.roomId)));
                                  } else {
                                    setState(() {
                                      _selectedIndex++;
                                    });
                                  }
                                },
                                child: CustomImageView(
                                  imagePath: ImageConstant.imgThumbsup,
                                ),
                              ),
                            ],
                          ),
                          Padding(
                            padding: EdgeInsets.symmetric(
                                horizontal: 40, vertical: 40),
                            child: Text(
                              movieList.movies![_selectedIndex].overview!,
                              style: AppStyle.txtRobotoRomanRegular14,
                              textAlign: TextAlign.left,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
              ],
            );
          } else if (snapshot.hasError) {
            return Text("${snapshot.error}");
          }
          return Center(child: CircularProgressIndicator());
        },
      ),
    );
  }
}
