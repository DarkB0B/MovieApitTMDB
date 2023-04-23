import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/presentation/account_preferences_screen/widgets/change_password.dart';

void main() {
  group('ChangePasswordDialog widget', () {
    testWidgets('should display dialog with title and input fields',
        (WidgetTester tester) async {
      await tester.pumpWidget(MaterialApp(home: ChangePasswordDialog()));

      expect(find.text('Change Password'), findsNWidgets(2));
      expect(find.byType(TextFormField), findsNWidgets(3));
      expect(find.byType(ElevatedButton), findsOneWidget);
    });

    testWidgets('should show loading indicator when button is pressed',
        (WidgetTester tester) async {
      await tester.pumpWidget(MaterialApp(home: ChangePasswordDialog()));

      await tester.tap(find.byType(ElevatedButton));
      await tester.pump();

      expect(find.byType(CircularProgressIndicator), findsOneWidget);
    });

    testWidgets('should show success snackbar when password is changed',
        (WidgetTester tester) async {
      await tester.pumpWidget(MaterialApp(home: ChangePasswordDialog()));

      final usernameField =
          find.widgetWithText(TextFormField, 'Enter username');
      final oldPasswordField =
          find.widgetWithText(TextFormField, 'Enter current password');
      final newPasswordField =
          find.widgetWithText(TextFormField, 'Enter new password');

      await tester.enterText(usernameField, 'testuser');
      await tester.enterText(oldPasswordField, 'oldpass');
      await tester.enterText(newPasswordField, 'newpass');

      await tester.tap(find.byType(ElevatedButton));
      await tester.pumpAndSettle();

      expect(find.text('Password changed successfully!'), findsOneWidget);
    });

    testWidgets('should show error snackbar when password change fails',
        (WidgetTester tester) async {
      await tester.pumpWidget(MaterialApp(home: ChangePasswordDialog()));

      await tester.tap(find.byType(ElevatedButton));
      await tester.pumpAndSettle();

      expect(find.text('Something went wrong!'), findsOneWidget);
    });
  });
}
