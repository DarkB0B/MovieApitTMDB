import 'package:movie_mixer/models/movie_model.dart';

class MovieListModel {
  String? id;
  List<MovieModel>? movies;

  MovieListModel({this.id, this.movies});

  MovieListModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    if (json['movies'] != null) {
      movies = <MovieModel>[];
      json['movies'].forEach((v) {
        movies!.add(new MovieModel.fromJson(v));
      });
    }
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    if (this.movies != null) {
      data['movies'] = this.movies!.map((v) => v.toJson()).toList();
    }
    return data;
  }
}
