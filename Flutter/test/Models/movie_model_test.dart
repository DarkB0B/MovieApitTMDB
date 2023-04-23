import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/models/movie_model.dart';

void main() {
  group('MovieModel', () {
    test('fromJson should parse JSON correctly', () {
      final json = {
        'id': '123',
        'title': 'The Matrix',
        'overview': 'A computer hacker learns from mysterious rebels about...',
        'posterPath': '/hEpWvX6Bp79eLxY1kX5ZZJcme5U.jpg',
        'releaseDate': '1999-03-30',
        'backdropPath': '/fNG7i7RqMErkcqhohV2a6cV1Ehy.jpg',
        'originalTitle': 'The Matrix',
        'voteAvredge': '8.5',
        'voteCount': '21240',
        'popularity': '48.836'
      };

      final movie = MovieModel.fromJson(json);

      expect(movie.id, '123');
      expect(movie.title, 'The Matrix');
      expect(movie.overview,
          'A computer hacker learns from mysterious rebels about...');
      expect(movie.posterPath, '/hEpWvX6Bp79eLxY1kX5ZZJcme5U.jpg');
      expect(movie.releaseDate, '1999-03-30');
      expect(movie.backdropPath, '/fNG7i7RqMErkcqhohV2a6cV1Ehy.jpg');
      expect(movie.originalTitle, 'The Matrix');
      expect(movie.voteAvredge, '8.5');
      expect(movie.voteCount, '21240');
      expect(movie.popularity, '48.836');
    });

    test('toJson should convert the model to JSON', () {
      final movie = MovieModel(
        id: '123',
        title: 'The Matrix',
        overview: 'A computer hacker learns from mysterious rebels about...',
        posterPath: '/hEpWvX6Bp79eLxY1kX5ZZJcme5U.jpg',
        releaseDate: '1999-03-30',
        backdropPath: '/fNG7i7RqMErkcqhohV2a6cV1Ehy.jpg',
        originalTitle: 'The Matrix',
        voteAvredge: '8.5',
        voteCount: '21240',
        popularity: '48.836',
      );

      final json = movie.toJson();

      expect(json['id'], '123');
      expect(json['title'], 'The Matrix');
      expect(json['overview'],
          'A computer hacker learns from mysterious rebels about...');
      expect(json['posterPath'], '/hEpWvX6Bp79eLxY1kX5ZZJcme5U.jpg');
      expect(json['releaseDate'], '1999-03-30');
      expect(json['backdropPath'], '/fNG7i7RqMErkcqhohV2a6cV1Ehy.jpg');
      expect(json['originalTitle'], 'The Matrix');
      expect(json['voteAvredge'], '8.5');
      expect(json['voteCount'], '21240');
      expect(json['popularity'], '48.836');
    });
  });
}
