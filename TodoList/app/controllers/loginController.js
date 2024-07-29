app.controller('LoginController', function ($scope, $http) {
    $scope.user = {};
    $scope.message = "";

    $scope.login = function () {
        // Clear any previous messages
        $scope.message = "";

        // Check if username or password is empty
        if (!$scope.user.username) {
            $scope.message = "Username is required";
            return;
        }

        if (!$scope.user.password) {
            $scope.message = "Password is required";
            return;
        }

       
    };
});
