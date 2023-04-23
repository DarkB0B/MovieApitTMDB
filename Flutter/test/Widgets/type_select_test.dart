import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/presentation/room_preferences_screen/widgets/type_select.dart';

void main() {
  group('CheckboxWidget', () {
    testWidgets('should toggle the checkbox and call callback', (tester) async {
      bool isMovieSelected = true;
      bool isTvSelected = false;
      await tester.pumpWidget(
        MaterialApp(
          home: Scaffold(
            body: CheckboxWidget(
              onCheckboxChanged: (isSelected) {
                isMovieSelected = isSelected;
                isTvSelected = !isSelected;
              },
            ),
          ),
        ),
      );

      expect(isMovieSelected, true);
      expect(isTvSelected, false);

      // tap the "TV Series" container to select it
      await tester.tap(find.text('TV Series'));
      await tester.pumpAndSettle();

      expect(isMovieSelected, false);
      expect(isTvSelected, true);

      // tap the "Movie" container to select it again
      await tester.tap(find.text('Movie'));
      await tester.pumpAndSettle();

      expect(isMovieSelected, true);
      expect(isTvSelected, false);
    });
  });
}
