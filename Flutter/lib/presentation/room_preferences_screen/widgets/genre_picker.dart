import 'package:flutter/material.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:movie_mixer/models/genre_model.dart';
import 'package:movie_mixer/services/providers.dart';

class GenrePicker extends StatefulWidget {
  final Function(List<GenreModel>) onGenresChanged;

  GenrePicker({required this.onGenresChanged});

  @override
  _GenrePickerState createState() => _GenrePickerState();
}

class _GenrePickerState extends State<GenrePicker> {
  List<GenreModel> _selectedGenres = [];

  Future<List<GenreModel>> _fetchGenres() async {
    return ApiProvider().fetchGenres();
  }

  Future<List<GenreModel>> _showGenreModal() async {
    final Set<GenreModel> selectedGenres = Set();

    return await showDialog(
      context: context,
      builder: (BuildContext context) {
        return StatefulBuilder(
          builder: (context, setState) {
            return FutureBuilder<List<GenreModel>>(
              future: _fetchGenres(),
              builder: (context, snapshot) {
                if (snapshot.hasData) {
                  final allGenres = snapshot.data!;

                  return AlertDialog(
                    backgroundColor: ColorConstant.purple,
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.all(Radius.circular(43)),
                      side: BorderSide(
                        width: 1,
                        color: ColorConstant.ghostWhite,
                      ),
                    ),
                    title: Text(
                      'Select Genres',
                      style: TextStyle(
                        color: ColorConstant.ghostWhite,
                        fontFamily: 'Roboto',
                        fontSize: 25,
                      ),
                    ),
                    content: Container(
                      height: 350,
                      child: SingleChildScrollView(
                        child: Column(
                          children: allGenres
                              .map(
                                (genre) => CheckboxListTile(
                                  title: Text(
                                    genre.name!,
                                    style: TextStyle(
                                      color: ColorConstant.ghostWhite,
                                      fontFamily: 'Roboto',
                                      fontSize: 25,
                                    ),
                                  ),
                                  value: selectedGenres.contains(genre),
                                  onChanged: (bool? value) {
                                    if (value != null && value) {
                                      selectedGenres.add(genre);
                                    } else {
                                      selectedGenres.remove(genre);
                                    }
                                  },
                                ),
                              )
                              .toList(),
                        ),
                      ),
                    ),
                    actions: [
                      TextButton(
                        onPressed: () {
                          Navigator.pop(context, selectedGenres.toList());
                        },
                        child: Text(
                          'OK',
                          style: TextStyle(
                            color: ColorConstant.ghostWhite,
                            fontFamily: 'Roboto',
                            fontSize: 25,
                          ),
                        ),
                      ),
                    ],
                  );
                } else if (snapshot.hasError) {
                  return Center(
                    child: Text(
                      'Error: ${snapshot.error}',
                      style: TextStyle(
                        color: ColorConstant.ghostWhite,
                        fontFamily: 'Roboto',
                        fontSize: 25,
                      ),
                    ),
                  );
                } else {
                  return Center(
                    child: CircularProgressIndicator(),
                  );
                }
              },
            );
          },
        );
      },
    );
  }

  void _handleGenresChanged(List<GenreModel> genres) {
    setState(() {
      _selectedGenres = genres;
    });

    widget
        .onGenresChanged(genres); // Pass selected genres list to parent widget
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        Container(
          padding: EdgeInsets.fromLTRB(19, 11, 19, 11),
          decoration: BoxDecoration(
            color: ColorConstant.ghostWhite,
            borderRadius: BorderRadius.circular(10),
          ),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text("Preferred genres",
                  textAlign: TextAlign.left,
                  style: AppStyle.txtRobotoRegular20),
              IconButton(
                  icon: const Icon(Icons.add_circle_outline_rounded),
                  color: ColorConstant.cyan,
                  iconSize: 40,
                  onPressed: () async {
                    final selectedGenres = await _showGenreModal();
                    _handleGenresChanged(selectedGenres);
                  }),
            ],
          ),
        ),
        _buildSelectedGenres(_selectedGenres)
      ],
    );
  }

  Widget _buildSelectedGenres(List<GenreModel> selectedGenres) {
    return selectedGenres.isNotEmpty
        ? Padding(
            padding: EdgeInsets.only(top: 8),
            child: Wrap(
              spacing: 8,
              runSpacing: 8,
              children: selectedGenres
                  .map((genre) => GestureDetector(
                        onTap: () {
                          _handleGenresChanged(selectedGenres
                              .where((g) => g.dbId != genre.dbId)
                              .toList());
                        },
                        child: Container(
                          padding:
                              EdgeInsets.symmetric(vertical: 8, horizontal: 16),
                          decoration: BoxDecoration(
                            color: ColorConstant.ghostWhite,
                            borderRadius: BorderRadius.circular(10),
                          ),
                          child: Text(
                            genre.name!,
                            style: AppStyle.txtRobotoRomanRegular12Black900,
                          ),
                        ),
                      ))
                  .toList(),
            ),
          )
        : Container();
  }
}
