class UserModel {
  String? id;
  String? username;
  String? password;
  String? token;
  bool? premium;

  UserModel({
    this.id,
    this.username,
    this.password,
    this.token,
    this.premium,
  });

  UserModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    username = json['name'];
    password = json['password'];
    token = json['token'];
    premium = json['role'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['name'] = this.username;
    data['password'] = this.password;
    data['token'] = this.token;
    data['role'] = this.premium;
    return data;
  }
}
