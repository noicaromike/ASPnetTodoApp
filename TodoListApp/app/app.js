var app = angular.module('todoApp', ['ui.router']);

app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
    $locationProvider.html5Mode(enabled=true);

    $stateProvider
        .state('login', {
            url: '/login',
            templateUrl: 'app/views/login.html',
            controller: 'LoginController'
        })
        .state('register', {
            url: '/register',
            templateUrl: 'app/views/register.html',
            controller: 'RegisterController'
        })
        .state('dashboard', {
            url: '/dashboard',
            templateUrl: 'app/views/dashboard.html',
            controller: 'DashboardController'
        });
    $urlRouterProvider.otherwise('/login');
});