app.controller('LoginController', function ($scope, $location, UserService) {
    $scope.user = {};
    $scope.message = "";

    $scope.login = function () {
        // Clear any previous messages
        $scope.message = "";

       

        // Call the checkUserLogin method from UserService
        UserService.checkUserLogin($scope.user.username, $scope.user.password)
            .then(function (response) {
                if (response.data.success === true) {
                    // Handle login success
                    $scope.message = "Login successful";
                    $scope.success = true;
                    // Check in console
                    console.log(response.data.user);
                    UserService.setUser(response.data.user);
                    $location.path("/dashboard");
                } else {
                    // Handle login failure
                    $scope.message = response.data.message || "Invalid username or password";
                    $scope.success = false;
                    console.log(response.data);
                }
            }, function (error) {
                // Handle error in API call
                $scope.message = "An error occurred during login";
                console.log(error);
            });
    };
});
