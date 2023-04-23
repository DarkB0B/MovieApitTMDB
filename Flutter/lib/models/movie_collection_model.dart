import 'movie_model.dart';

class MovieCollectionModel {
  int? id;
  String? title;
  String? description;
  String? imagePath;
  List<MovieModel>? movies;

  MovieCollectionModel(
      {this.id, this.title, this.description, this.imagePath, this.movies});

  MovieCollectionModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    title = json['title'];
    description = json['description'];
    imagePath = json['imagePath'];
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
    data['title'] = this.title;
    data['description'] = this.description;
    data['imagePath'] = this.imagePath;
    if (this.movies != null) {
      data['movies'] = this.movies!.map((v) => v.toJson()).toList();
    }
    return data;
  }
}
