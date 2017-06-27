var app = angular.module('MyApp', ['ngRoute', 'xeditable', 'ui.select', 'ui.bootstrap']);

app.factory('ConstantService', function () {
    return {
        apiURL: 'http://deltaxpoc.apphb.com/api'
    };
});

app.config(['$routeProvider', function ($routeProvider) {    
    $routeProvider.
        when('/',
        {
            templateUrl: 'partials/Movies/Movies.html',
            controller: 'MoviesController'
        }).        
        when('/Actor',
        {
            templateUrl: 'partials/Actor/Actor.html',
            controller: 'ActorController'
        }).when('/Producer',
        {
            templateUrl: 'partials/Producer/Producer.html',
            controller: 'ProducerController'
        }).
    otherwise({
        redirectTo: '/'
    });

}]);