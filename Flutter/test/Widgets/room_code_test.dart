import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/core/app_export.dart';

void main() {
  testWidgets('CopyableText widget test', (WidgetTester tester) async {
    final String testText = 'Test text';
    await tester.pumpWidget(MaterialApp(
      home: Scaffold(
        body: CopyableText(text: testText),
      ),
    ));

    final Finder copyableTextFinder = find.text(testText);
    expect(copyableTextFinder, findsOneWidget);

    final GestureDetector copyableTextGestureDetector =
        tester.widget(find.byType(GestureDetector));
    expect(copyableTextGestureDetector.onTap, isNotNull);

    final ClipboardData clipboardData = ClipboardData(text: testText);
    await tester.tap(find.byType(GestureDetector));
    await tester.pumpAndSettle(Duration(seconds: 5));
    expect(
        await Clipboard.getData(Clipboard.kTextPlain), equals(clipboardData));

    final Finder snackBarFinder = find.text('Text copied to clipboard');
    expect(snackBarFinder, findsOneWidget);

    final SnackBar snackBar = tester.widget(snackBarFinder);
    expect(snackBar.duration, equals(Duration(seconds: 2)));
  });
}
