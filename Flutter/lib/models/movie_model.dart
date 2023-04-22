class MovieModel {
  String? id;
  String? title;
  String? overview;
  String? posterPath;
  String? releaseDate;
  String? backdropPath;
  String? originalTitle;
  String? voteAvredge;
  String? voteCount;
  String? popularity;

  MovieModel(
      {this.id,
      this.title,
      this.overview,
      this.posterPath,
      this.releaseDate,
      this.backdropPath,
      this.originalTitle,
      this.voteAvredge,
      this.voteCount,
      this.popularity});

  MovieModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    title = json['title'];
    overview = json['overview'];
    posterPath = json['posterPath'];
    releaseDate = json['releaseDate'];
    backdropPath = json['backdropPath'];
    originalTitle = json['originalTitle'];
    voteAvredge = json['voteAvredge'];
    voteCount = json['voteCount'];
    popularity = json['popularity'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['title'] = this.title;
    data['overview'] = this.overview;
    data['posterPath'] = this.posterPath;
    data['releaseDate'] = this.releaseDate;
    data['backdropPath'] = this.backdropPath;
    data['originalTitle'] = this.originalTitle;
    data['voteAvredge'] = this.voteAvredge;
    data['voteCount'] = this.voteCount;
    data['popularity'] = this.popularity;
    return data;
  }
}
