import 'package:flutter_test/flutter_test.dart';
import 'package:movie_mixer/models/user_model.dart';

void main() {
  group('UserModel tests', () {
    test('fromJson creates a UserModel', () {
      final json = {
        'id': '1',
        'name': 'John',
        'password': 'password',
        'token': '12345',
        'role': true,
      };

      final user = UserModel.fromJson(json);

      expect(user.id, '1');
      expect(user.username, 'John');
      expect(user.password, 'password');
      expect(user.token, '12345');
      expect(user.premium, true);
    });

    test('toJson creates a JSON object', () {
      final user = UserModel(
        id: '1',
        username: 'John',
        password: 'password',
        token: '12345',
        premium: true,
      );

      final json = user.toJson();

      expect(json['id'], '1');
      expect(json['name'], 'John');
      expect(json['password'], 'password');
      expect(json['token'], '12345');
      expect(json['role'], true);
    });
  });
}
