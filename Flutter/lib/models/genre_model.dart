class GenreModel {
  int? dbId;
  int? tmdbId;
  String? name;

  GenreModel({this.dbId, this.tmdbId, this.name});

  GenreModel.fromJson(Map<String, dynamic> json) {
    dbId = json['dbId'];
    tmdbId = json['tmdbId'];
    name = json['name'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['dbId'] = this.dbId;
    data['tmdbId'] = this.tmdbId;
    data['name'] = this.name;
    return data;
  }
}
