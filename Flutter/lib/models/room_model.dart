import 'movie_list_model.dart';

class RoomModel {
  String? id;
  int? usersInRoom;
  bool? isStarted;
  List<MovieListModel>? movieLists;
  bool? isCompleted;

  RoomModel(
      {this.id,
      this.usersInRoom,
      this.isStarted,
      this.movieLists,
      this.isCompleted});

  RoomModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    usersInRoom = json['usersInRoom'];
    isStarted = json['isStarted'];
    if (json['movieLists'] != null) {
      movieLists = <MovieListModel>[];
      json['movieLists'].forEach((v) {
        movieLists!.add(new MovieListModel.fromJson(v));
      });
    }
    isCompleted = json['isCompleted'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['usersInRoom'] = this.usersInRoom;
    data['isStarted'] = this.isStarted;
    if (this.movieLists != null) {
      data['movieLists'] = this.movieLists!.map((v) => v.toJson()).toList();
    }
    data['isCompleted'] = this.isCompleted;
    return data;
  }
}
