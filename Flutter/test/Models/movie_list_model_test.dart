import 'package:movie_mixer/models/movie_model.dart';
import 'package:movie_mixer/models/movie_list_model.dart';
import 'package:flutter_test/flutter_test.dart';

void main() {
  group('MovieListModel', () {
    final movie1 = MovieModel(
      id: '1',
      title: 'Iron Man',
      overview: 'Tony Stark builds an armored suit to fight the throes of evil',
      posterPath: '/iron_man_poster.jpg',
      releaseDate: '2008-05-01',
      backdropPath: '/iron_man_backdrop.jpg',
      originalTitle: 'Iron Man',
      voteAvredge: '7.5',
      voteCount: '12445',
      popularity: '79.433',
    );

    final movie2 = MovieModel(
      id: '2',
      title: 'The Avengers',
      overview:
          'Earth\'s mightiest heroes must come together and learn to fight as a team',
      posterPath: '/avengers_poster.jpg',
      releaseDate: '2012-04-25',
      backdropPath: '/avengers_backdrop.jpg',
      originalTitle: 'The Avengers',
      voteAvredge: '7.7',
      voteCount: '25375',
      popularity: '110.782',
    );

    final List<MovieModel> movies = [movie1, movie2];
    final movieListModel = MovieListModel(id: '1', movies: movies);

    test('MovieListModel can be created', () {
      expect(movieListModel, isNotNull);
    });

    test('MovieListModel can be created from JSON', () {
      final movieListModelJson = {
        'id': '1',
        'movies': [
          {
            'id': '1',
            'title': 'Iron Man',
            'overview':
                'Tony Stark builds an armored suit to fight the throes of evil',
            'posterPath': '/iron_man_poster.jpg',
            'releaseDate': '2008-05-01',
            'backdropPath': '/iron_man_backdrop.jpg',
            'originalTitle': 'Iron Man',
            'voteAvredge': '7.5',
            'voteCount': '12445',
            'popularity': '79.433',
          },
          {
            'id': '2',
            'title': 'The Avengers',
            'overview':
                'Earth\'s mightiest heroes must come together and learn to fight as a team',
            'posterPath': '/avengers_poster.jpg',
            'releaseDate': '2012-04-25',
            'backdropPath': '/avengers_backdrop.jpg',
            'originalTitle': 'The Avengers',
            'voteAvredge': '7.7',
            'voteCount': '25375',
            'popularity': '110.782',
          },
        ],
      };

      final movieListModelFromJson =
          MovieListModel.fromJson(movieListModelJson);

      expect(movieListModelFromJson.id, '1');
      expect(movieListModelFromJson.movies?.length, 2);
      expect(movieListModelFromJson.movies?[0].title, 'Iron Man');
      expect(movieListModelFromJson.movies?[1].title, 'The Avengers');
    });

    test('MovieListModel can be converted to JSON', () {
      final movieListModelJson = movieListModel.toJson();

      expect(movieListModelJson['id'], '1');
      expect(movieListModelJson['movies']?.length, 2);
      expect(movieListModelJson['movies'][0]['title'], 'Iron Man');
      expect(movieListModelJson['movies'][1]['title'], 'The Avengers');
    });
  });
}
