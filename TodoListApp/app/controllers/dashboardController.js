app.controller('DashboardController', function ($scope, UserService, $http) {
    $scope.message = 'This is dashboard page';
    $scope.user = UserService.getUser();
    $scope.newUser = {};
    $scope.users = [];
    $scope.tasks = [];
    $scope.selectedUser = null;
    $scope.currentPage = 1;
    $scope.itemsPerPage = 5;

    UserService.getAllUsers().then(function (response) {
        $scope.users = response.data;
    }, function (error) {
        console.error('Error fetching users:', error);
    });
    $scope.totalPages = function () {
        return Math.ceil($scope.users.length / $scope.itemsPerPage);
    };

    $scope.setPage = function (page) {
        if (page >= 1 && page <= $scope.totalPages()) {
            $scope.currentPage = page;
        }
    };

    $scope.paginatedUsers = function () {
        var start = ($scope.currentPage - 1) * $scope.itemsPerPage;
        var end = start + $scope.itemsPerPage;
        console.log('Paginated users:', $scope.users.slice(start, end));
        return $scope.users.slice(start, end);
    };
    $scope.$watch('users', function () {
        $scope.paginatedUsers();
    });

    $scope.$watch('currentPage', function () {
        $scope.paginatedUsers();
    });
    $scope.filterUsers = function () {
        // Implement your filter logic here
    };

    $scope.toggleAll = function () {
        var toggleStatus = !$scope.selectAll;
        angular.forEach($scope.users, function (user) {
            user.selected = toggleStatus;
        });
    };
    
    $scope.addUser = function () {
        if (!$scope.newUser || !$scope.newUser.username || !$scope.newUser.password) {
            alert('Please fill in all required fields.');
            return;
        }
        console.log($scope.newUser)
        $http.post('https://localhost:44385/api/users', $scope.newUser)
        
            .then(function (response) {
                
                if (response.data) {
                    $scope.users.push(response.data);
                    $('#addUserModal').modal('hide');
                } else {
                    alert('Registration failed. Response was undefined.');
                }
            }, function (error) {
                console.log('Error:', error);
                alert('Registration failed: ' + (error.data.message || error.statusText));
            });
    };

    $scope.viewUser = function (user) {
        $scope.selectedUser = user;
        $scope.tasks = user.tasks;
    };

    $scope.clearSelectedUser = function () {
        $scope.selectedUser = null;
        $scope.tasks = [];
    };
});
