app.service('UserService', function ($http) {
    var user = {};

    this.checkUserLogin = function (username, password) {
        return $http({
            method: 'GET',
            url: 'https://localhost:44385/api/users',
            params: { username: username, password: password }
        });
    };

    this.getUser = function () {
        return user;
    };

    this.setUser = function (userData) {
        user = userData;
    };


    this.getAllUsers = function () {
        return $http({
            method: 'GET',
            url: 'https://localhost:44385/api/users'
        });

    };
    
});
