import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;
import 'package:movie_mixer/models/genre_model.dart';
import 'package:movie_mixer/models/movie_model.dart';
import '../core/endpoints.dart';
import '../models/movie_collection_model.dart';
import '../models/movie_list_model.dart';
import '../models/room_model.dart';

class ApiProvider {
  final storage = FlutterSecureStorage();

  //-----------> MOVIE COLLECTIONS <-----------//

  Future<List<MovieCollectionModel>> fetchCollection() async {
    final token = await storage.read(key: 'token');
    final response = await http.get(Uri.parse(Endpoints.movieCollections),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      final jsonList = jsonDecode(response.body) as List<dynamic>;
      return jsonList
          .map((json) => MovieCollectionModel.fromJson(json))
          .toList();
    } else {
      throw Exception('Failed to load collections');
    }
  }

  //----------------> ROOMS <-----------------//

  Future<MovieListModel?> fetchFinalMovieList(String roomId) async {
    final token = await storage.read(key: 'token');
    final response = await http.get(Uri.parse(Endpoints.rooms + '/$roomId'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseBody = json.decode(response.body);
      final RoomModel room = RoomModel.fromJson(responseBody);

      final String finalMovieListId = '${room.id}final';
      final MovieListModel? finalMovieList = room.movieLists
          ?.firstWhere((list) => list.id == finalMovieListId, orElse: () {
        return MovieListModel(id: "notready", movies: []);
      });

      return finalMovieList;
    } else {
      throw Exception('Failed to fetch room');
    }
  }

  Future<MovieListModel?> fetchStarterMovieList(String roomId) async {
    final token = await storage.read(key: 'token');

    final response = await http.get(Uri.parse(Endpoints.rooms + '/$roomId'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseBody = json.decode(response.body);
      final RoomModel room = RoomModel.fromJson(responseBody);

      final String starterMovieListId = '${room.id}starter';
      final MovieListModel? starterMovieList = room.movieLists?.firstWhere(
          (list) => list.id == starterMovieListId,
          orElse: () => throw Exception('Failed to fetch starter movie list'));

      return starterMovieList;
    } else {
      throw Exception('Failed to fetch room');
    }
  }

  Future<String> createRoomCollection(int colllectionId) async {
    final token = await storage.read(key: 'token');

    final response = await http.post(
        Uri.parse(
            Endpoints.rooms + '?option=collection&collectionId=$colllectionId'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseBody = json.decode(response.body);
      final RoomModel room = RoomModel.fromJson(responseBody);
      return room.id!;
    } else {
      throw Exception('Failed to create room');
    }
  }

  Future<String> createRoomDiscover(
      List<int> genreList, bool movie, int ammount) async {
    if (genreList.isEmpty) {
      throw Exception("Genre list can't be empty");
    }
    final token = await storage.read(key: 'token');

    final response = await http.post(
      Uri.parse(
          Endpoints.rooms + '?option=discover&movie=$movie&ammount=$ammount'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json'
      },
      body: jsonEncode(genreList),
    );

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseBody = json.decode(response.body);
      final RoomModel room = RoomModel.fromJson(responseBody);
      return room.id!;
    } else {
      throw Exception('Failed to create room');
    }
  }

  Future<bool> joinRoom(String roomId) async {
    final token = await storage.read(key: 'token');

    final response = await http.patch(
        Uri.parse(Endpoints.rooms + '/$roomId/users?option=add'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode != 200) {
      return false;
    }
    return true;
  }

  Future<RoomModel> fetchRoom(String roomId) async {
    final token = await storage.read(key: 'token');
    final response = await http.get(Uri.parse(Endpoints.rooms + '/$roomId'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseBody = json.decode(response.body);
      final RoomModel room = RoomModel.fromJson(responseBody);
      return room;
    } else {
      throw Exception('Failed to fetch room');
    }
  }

  Future<void> leaveRoom(String roomId) async {
    final token = await storage.read(key: 'token');

    final response = await http.patch(
        Uri.parse(Endpoints.rooms + '/$roomId/users?option=remove'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode != 200) {
      throw Exception('Failed to leave room');
    }
  }

  Future<void> deleteRoom(String roomId) async {
    final token = await storage.read(key: 'token');

    final response = await http.delete(Uri.parse(Endpoints.rooms + '/$roomId'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode != 200) {
      throw Exception('Failed to delete room');
    }
  }

  Future<void> addMovieList(String roomId, List<MovieModel> movieListId) async {
    final token = await storage.read(key: 'token');

    final response = await http.post(
        Uri.parse(Endpoints.rooms + '/$roomId/movielists'),
        body: jsonEncode(movieListId),
        headers: {
          'Authorization': 'Bearer $token',
          'Content-Type': 'application/json'
        });

    if (response.statusCode != 200) {
      throw Exception('Failed to add movie list');
    }
  }

  // Future<void> removeMovieList(String roomId, String movieListId) async {
  //   final response = await http.delete(
  //     Uri.parse(Endpoints.rooms + '/$roomId/movieLists/$movieListId'),
  //   );

  //   if (response.statusCode != 200) {
  //     throw Exception('Failed to remove movie list');
  //   }
  // }

  Future<bool> startRoom(String roomId) async {
    final token = await storage.read(key: 'token');

    final response = await http.patch(Uri.parse(Endpoints.rooms + '/$roomId'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      return true;
    } else if (response.statusCode == 400 &&
        response.body == "Not Enough People Joined The Room") {
      return false;
    } else {
      throw Exception('Failed to start room');
    }
  }

  Future<bool> isRoomStarted(String roomId) async {
    final token = await storage.read(key: 'token');

    final response = await http.get(Uri.parse(Endpoints.rooms + '/$roomId'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseBody = json.decode(response.body);
      final RoomModel room = RoomModel.fromJson(responseBody);
      return room.isStarted!;
    } else {
      throw Exception('Failed to fetch room');
    }
  }

  Future<bool> isRoomCompleted(String roomId) async {
    final token = await storage.read(key: 'token');

    final response = await http.get(Uri.parse(Endpoints.rooms + '/$roomId'),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseBody = json.decode(response.body);
      final RoomModel room = RoomModel.fromJson(responseBody);
      return room.isCompleted!;
    } else {
      throw Exception('Failed to fetch room');
    }
  }

  //------------------Account------------------

  Future<void> login(String username, String password) async {
    final response = await http.post(Uri.parse(Endpoints.Tokens),
        body: jsonEncode({'username': username, 'password': password}),
        headers: {'Content-Type': 'application/json'});

    if (response.statusCode == 200) {
      final token = jsonDecode(response.body)['token'];
      await storage.write(key: 'token', value: token);
    } else {
      throw Exception('Failed to login');
    }
  }

  Future<void> register(String username, String password) async {
    final response = await http.post(Uri.parse(Endpoints.Users),
        body: jsonEncode({'username': username, 'password': password}),
        headers: {'Content-Type': 'application/json'});

    if (response.statusCode != 200) {
      throw Exception('Failed to register');
    }
  }

  Future<void> changePassword(
      String username, String oldPassword, String newPassword) async {
    final token = await storage.read(key: 'token');
    final response = await http.patch(Uri.parse(Endpoints.Users),
        body: jsonEncode({
          'username': username,
          'oldPassword': oldPassword,
          'newPassword': newPassword
        }),
        headers: {
          'Authorization': 'Bearer $token',
          'Content-Type': 'application/json'
        });

    if (response.statusCode != 200) {
      throw Exception('Failed to change password');
    }
  }

  // ------------------Genres------------------
  Future<List<GenreModel>> fetchGenres() async {
    final token = await storage.read(key: 'token');
    final response = await http.get(Uri.parse(Endpoints.genres),
        headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      final Map<String, dynamic>? responseBody = json.decode(response.body);
      final List<dynamic>? genresList = responseBody?['value'];

      if (genresList != null) {
        final List<GenreModel> genres = genresList
            .map((e) => GenreModel.fromJson(e as Map<String, dynamic>))
            .toList();
        return genres;
      } else {
        throw Exception('No genres found');
      }
    } else {
      throw Exception('Failed to fetch genres');
    }
  }
}
