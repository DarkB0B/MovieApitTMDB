import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/models/movie_collection_model.dart';
import 'package:movie_mixer/presentation/main_screen/widgets/slider.dart';

void main() {
  group('ImageCarousel', () {
    testWidgets('renders carousel with given collections', (tester) async {
      final collection1 = MovieCollectionModel(
        id: 1,
        imagePath: 'https://example.com/collection1.jpg',
        title: 'Collection 1',
        description: 'Collection 1 description',
      );
      final collection2 = MovieCollectionModel(
        id: 2,
        imagePath: 'https://example.com/collection2.jpg',
        title: 'Collection 2',
        description: 'Collection 2 description',
      );
      final collections = [collection1, collection2];

      await tester.pumpWidget(
        ImageCarousel(
          collectionId: collections.map((c) => c.id!).toList(),
          imagePaths: collections.map((c) => c.imagePath!).toList(),
          title: collections.map((c) => c.title!).toList(),
          description: collections.map((c) => c.description!).toList(),
          scrollDirection: Axis.horizontal,
          itemsVisible: 2,
        ),
      );

      expect(find.text('Collection 1'), findsOneWidget);
      expect(find.text('Collection 1 description'), findsOneWidget);
      expect(find.text('Collection 2'), findsOneWidget);
      expect(find.text('Collection 2 description'), findsOneWidget);
      expect(find.byType(Image), findsNWidgets(2));
    });
  });
}
