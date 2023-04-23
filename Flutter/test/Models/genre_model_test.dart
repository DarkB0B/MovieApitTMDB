import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/models/genre_model.dart';

void main() {
  group('GenreModel', () {
    test('should create a GenreModel instance with the given parameters', () {
      // arrange
      final int dbId = 1;
      final int tmdbId = 2;
      final String name = 'Action';

      // act
      final GenreModel genre =
          GenreModel(dbId: dbId, tmdbId: tmdbId, name: name);

      // assert
      expect(genre.dbId, dbId);
      expect(genre.tmdbId, tmdbId);
      expect(genre.name, name);
    });

    test('should create a GenreModel instance from a JSON object', () {
      // arrange
      final Map<String, dynamic> json = {
        'dbId': 1,
        'tmdbId': 2,
        'name': 'Action',
      };

      // act
      final GenreModel genre = GenreModel.fromJson(json);

      // assert
      expect(genre.dbId, json['dbId']);
      expect(genre.tmdbId, json['tmdbId']);
      expect(genre.name, json['name']);
    });

    test('should convert a GenreModel instance to a JSON object', () {
      // arrange
      final GenreModel genre = GenreModel(dbId: 1, tmdbId: 2, name: 'Action');

      // act
      final Map<String, dynamic> json = genre.toJson();

      // assert
      expect(json['dbId'], genre.dbId);
      expect(json['tmdbId'], genre.tmdbId);
      expect(json['name'], genre.name);
    });
  });
}
