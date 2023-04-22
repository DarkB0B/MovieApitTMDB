import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/core/app_export.dart';
import 'package:share/share.dart';

class MockShare extends Share {
  Future<void> share(String text, {String? subject, Uri? uri}) {
    return Future.value();
  }
}

class MockShareImpl extends Share {
  Future<void> share(String text, {String? subject, Uri? uri}) {
    return Future.value();
  }
}

void main() {
  testWidgets('InviteContainer widget test', (WidgetTester tester) async {
    await tester.pumpWidget(MaterialApp(
      home: Scaffold(
        body: InviteContainer('1234'),
      ),
    ));

    final Finder inviteButtonFinder = find.text('INVITE');
    expect(inviteButtonFinder, findsOneWidget);

    final mockShare = MockShare();

    await tester.tap(inviteButtonFinder);
    await tester.pumpAndSettle();

    expect(mockShare.share('', subject: '', uri: null), isA<Future<void>>());
  });
}
