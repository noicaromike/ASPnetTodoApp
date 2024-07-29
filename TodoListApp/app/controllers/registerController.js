app.controller('RegisterController', function ($scope, $location,$http) {
   

    

    $scope.user = {};

    $scope.register = function () {
        if (!$scope.user || !$scope.user.username || !$scope.user.password) {
            alert('Please fill in all required fields.');
            return;
        }

        console.log('User data:', $scope.user);

        $http.post('https://localhost:44385/api/users', $scope.user)
            .then(function (response) {
                console.log('Response:', response);
                if (response.data) {
                    $location.path("/login");
                } else {
                    alert('Registration failed. Response was undefined.');
                }
            }, function (error) {
                console.log('Error:', error);
                alert('Registration failed: ' + (error.data.message || error.statusText));
            });
    };
});

