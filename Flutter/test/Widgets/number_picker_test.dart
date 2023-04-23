import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/presentation/room_preferences_screen/widgets/number_picker.dart';

void main() {
  testWidgets('NumberPicker should display correctly',
      (WidgetTester tester) async {
    int selectedValue = 14;

    await tester.pumpWidget(
      MaterialApp(
        home: Scaffold(
          body: NumberPicker(
            selectedValue: selectedValue,
            onValueChanged: (int value) => selectedValue = value,
          ),
        ),
      ),
    );

    // Find the picker widget
    final picker = find.byType(CupertinoPicker);

    // Expect the picker to be found on the screen
    expect(picker, findsOneWidget);

    // Try scrolling the picker down
    await tester.drag(picker, Offset(0, -100.0), touchSlopY: 0);
    await tester.pumpAndSettle();

    // Check that the selected value has changed
    expect(selectedValue, equals(13));

    // Try scrolling the picker up
    await tester.drag(picker, Offset(0, -25.0), touchSlopY: 0);
    await tester.pumpAndSettle();

    // Check that the selected value has changed back to the original value
    expect(selectedValue, equals(14));
  });
}
