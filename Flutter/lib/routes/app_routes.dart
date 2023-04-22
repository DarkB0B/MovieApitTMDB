import 'package:flutter/material.dart';
import 'package:movie_mixer/presentation/splash_screen/splash_screen.dart';
import 'package:movie_mixer/presentation/login_screen/login_screen.dart';
import 'package:movie_mixer/presentation/create_account_screen/create_account_screen.dart';
import 'package:movie_mixer/presentation/room_preferences_screen/room_preferences_screen.dart';
import 'package:movie_mixer/presentation/main_screen/main_screen.dart';
import 'package:movie_mixer/presentation/account_preferences_screen/account_preferences_screen.dart';
import 'package:movie_mixer/presentation/waiting_room_screen/waiting_room_screen.dart';
import 'package:movie_mixer/presentation/movie_screen/movie_screen.dart';
import 'package:movie_mixer/presentation/after_selection_screen/after_selection_screen.dart';

class AppRoutes {
  static const String splashScreen = '/splash_screen';

  static const String loginScreen = '/login_screen';

  static const String createAccountScreen = '/create_account_screen';

  static const String roomPreferencesScreen = '/room_preferences_screen';

  static const String mainScreen = '/main_screen';

  static const String accountPreferencesScreen = '/account_preferences_screen';

  static const String waitingRoomScreen = '/waiting_room_screen';

  static const String movieScreen = '/movie_screen';

  static const String afterSelectionScreen = '/after_selection_screen';

  static const String roomModal = '/room_modal';

  static Map<String, WidgetBuilder> routes = {
    splashScreen: (context) => SplashScreen(),
    loginScreen: (context) => LoginScreen(),
    createAccountScreen: (context) => CreateAccountScreen(),
    roomPreferencesScreen: (context) => RoomPreferencesScreen(),
    mainScreen: (context) => MainScreen(),
    accountPreferencesScreen: (context) => AccountPreferencesScreen(),
    waitingRoomScreen: (context) => WaitingRoomScreen(
          roomId: '',
          isHost: false,
        ),
    movieScreen: (context) => MovieScreen(
          roomId: '',
        ),
    afterSelectionScreen: (context) => AfterSelectionScreen(
          roomId: '',
        ),
  };
}
