import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/models/movie_list_model.dart';
import 'package:movie_mixer/models/movie_model.dart';
import 'package:movie_mixer/models/room_model.dart';

void main() {
  group('RoomModel', () {
    test('fromJson creates a RoomModel object correctly', () {
      final json = {
        'id': '12345',
        'usersInRoom': 3,
        'isStarted': true,
        'movieLists': [
          {
            'id': '67890',
            'movies': [
              {'id': '1', 'title': 'Movie 1'},
              {'id': '2', 'title': 'Movie 2'}
            ]
          }
        ],
        'isCompleted': false
      };

      final room = RoomModel.fromJson(json);

      expect(room.id, '12345');
      expect(room.usersInRoom, 3);
      expect(room.isStarted, true);
      expect(room.isCompleted, false);

      final movieList = room.movieLists![0];
      expect(movieList.id, '67890');
      expect(movieList.movies!.length, 2);
      expect(movieList.movies![0].id, '1');
      expect(movieList.movies![0].title, 'Movie 1');
      expect(movieList.movies![1].id, '2');
      expect(movieList.movies![1].title, 'Movie 2');
    });

    test('toJson creates a JSON object correctly', () {
      final room = RoomModel(
        id: '12345',
        usersInRoom: 3,
        isStarted: true,
        movieLists: [
          MovieListModel(
            id: '67890',
            movies: [
              MovieModel(id: '1', title: 'Movie 1'),
              MovieModel(id: '2', title: 'Movie 2'),
            ],
          ),
        ],
        isCompleted: false,
      );

      final json = room.toJson();

      expect(json['id'], '12345');
      expect(json['usersInRoom'], 3);
      expect(json['isStarted'], true);
      expect(json['isCompleted'], false);

      final movieListJson = json['movieLists'][0];
      expect(movieListJson['id'], '67890');
      expect(movieListJson['movies'].length, 2);
      expect(movieListJson['movies'][0]['id'], '1');
      expect(movieListJson['movies'][0]['title'], 'Movie 1');
      expect(movieListJson['movies'][1]['id'], '2');
      expect(movieListJson['movies'][1]['title'], 'Movie 2');
    });
  });
}
