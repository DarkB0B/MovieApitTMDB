import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/models/movie_collection_model.dart';
import 'package:movie_mixer/models/movie_model.dart';

void main() {
  group('MovieCollectionModel', () {
    test(
        'should create a MovieCollectionModel instance with the given parameters',
        () {
      // arrange
      final int id = 1;
      final String title = 'Marvel Cinematic Universe';
      final String description = 'A collection of movies from the MCU.';
      final String imagePath = '/mcu.jpg';
      final List<MovieModel> movies = [
        MovieModel(id: '1', title: 'Iron Man'),
        MovieModel(id: '2', title: 'The Avengers'),
        MovieModel(id: '3', title: 'Black Panther'),
      ];

      // act
      final MovieCollectionModel collection = MovieCollectionModel(
        id: id,
        title: title,
        description: description,
        imagePath: imagePath,
        movies: movies,
      );

      // assert
      expect(collection.id, id);
      expect(collection.title, title);
      expect(collection.description, description);
      expect(collection.imagePath, imagePath);
      expect(collection.movies, movies);
    });

    test('should create a MovieCollectionModel instance from a JSON object',
        () {
      // arrange
      final Map<String, dynamic> json = {
        'id': 1,
        'title': 'Marvel Cinematic Universe',
        'description': 'A collection of movies from the MCU.',
        'imagePath': '/mcu.jpg',
        'movies': [
          {'id': '1', 'title': 'Iron Man'},
          {'id': '2', 'title': 'The Avengers'},
          {'id': '3', 'title': 'Black Panther'},
        ],
      };

      // act
      final MovieCollectionModel collection =
          MovieCollectionModel.fromJson(json);

      // assert
      expect(collection.id, json['id']);
      expect(collection.title, json['title']);
      expect(collection.description, json['description']);
      expect(collection.imagePath, json['imagePath']);
      expect(collection.movies!.length, json['movies'].length);
      expect(collection.movies!.first.id, json['movies'][0]['id']);
      expect(collection.movies!.first.title, json['movies'][0]['title']);
    });

    test('should convert a MovieCollectionModel instance to a JSON object', () {
      // arrange
      final MovieCollectionModel collection = MovieCollectionModel(
        id: 1,
        title: 'Marvel Cinematic Universe',
        description: 'A collection of movies from the MCU.',
        imagePath: '/mcu.jpg',
        movies: [
          MovieModel(id: '1', title: 'Iron Man'),
          MovieModel(id: '2', title: 'The Avengers'),
          MovieModel(id: '3', title: 'Black Panther'),
        ],
      );

      // act
      final Map<String, dynamic> json = collection.toJson();

      // assert
      expect(json['id'], collection.id);
      expect(json['title'], collection.title);
      expect(json['description'], collection.description);
      expect(json['imagePath'], collection.imagePath);
      expect(json['movies'].length, collection.movies!.length);
      expect(json['movies'][0]['id'], collection.movies!.first.id);
      expect(json['movies'][0]['title'], collection.movies!.first.title);
    });
  });
}
