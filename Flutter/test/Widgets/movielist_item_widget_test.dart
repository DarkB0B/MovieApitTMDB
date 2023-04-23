import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/models/movie_model.dart';
import 'package:movie_mixer/presentation/after_selection_screen/widgets/movielist_item_widget.dart';

import 'package:http/http.dart' as http;
import 'package:mockito/mockito.dart';

class MockClient extends Mock implements http.Client {}

void main() {
  group('MovieListItemWidget', () {
    late http.Client client;

    setUp(() {
      client = MockClient();
    });

    tearDown(() {
      client.close();
    });

    testWidgets('should display movie title and poster', (tester) async {
      final movie =
          MovieModel(title: 'Test Movie', posterPath: '/test_poster.jpg');
      final imageBytes = Uint8List.fromList([0, 1, 2, 3]); // mock image bytes

      when(client.get(
              Uri.parse('https://image.tmdb.org/t/p/w500${movie.posterPath}')))
          .thenAnswer((_) async => http.Response.bytes(imageBytes, 200));

      await tester.pumpWidget(
        MaterialApp(
          home: Scaffold(
            body: MovieListItemWidget(movie: movie),
          ),
        ),
      );

      expect(find.text('Test Movie'), findsOneWidget);
      expect(find.byType(Image), findsOneWidget);
    });
  });
}
