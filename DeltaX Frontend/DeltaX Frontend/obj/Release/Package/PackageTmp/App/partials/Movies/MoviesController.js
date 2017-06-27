

app.controller('MoviesController', function ($rootScope, $scope, $http, ConstantService) {

    //load movies in array 
    $scope.list = [];
    $scope.actorsinmovie = [];
    $scope.producerlist = [];    
    $scope.actorslist = [];
    $scope.movieID;
    $scope.genderOptions = ['Male', 'Female'];

    //Get Funcion for loading all the movies
    $scope.loadlist = function () {
        $http.get(ConstantService.apiURL + '/Movies')
    .success(function (response) {        
        $scope.list = response;        
    });
    };

    //Add new Producer and actor
    $scope.AddPerson = function (args) {        
        $http({
            method: 'POST',
            url: ConstantService.apiURL + '/persons',
            data: { Name: $scope.person.personname, DOB: $scope.person.DOB, Sex: $scope.person.gender, Bio: $scope.person.bio, Designation: args },//forms user object
            headers: { 'Content-Type': 'application/json' }
        }).then(function (res) {
            $('#ProducerModal').modal('hide');
            $('#ActorModal').modal('hide');
            $scope.fillProducer();
            $scope.fillActor();
        }, function (error) {
            console.log(error.statusText);
        })
    };


    //Fill Actors
    $scope.fillActor = function () {
        $http.get(ConstantService.apiURL + '/persons/?personType=actor').success(function (response) {            
            $scope.actorslist = response;
        }).error(function (response) {
            console.log(response);
        });
    }

    //Add new Actor to the movie
    $scope.addActorToMovie = function () {
        var result = $.grep($scope.actorsinmovie, function (e) { return e.PersonID == $scope.actor; });        
        if (result.length == 0) {
            $http({
                method: 'POST',
                url: ConstantService.apiURL + '/MoviesActors',
                data: { MovieID: $scope.movieID, PersonID: $scope.actor  },//forms user object
                headers: { 'Content-Type': 'application/json' }
            }).then(function (res) {                
                $scope.fillActors($scope.movieID);
            }, function (error) {
                console.log(error.statusText);
            })
        } else if (result.length == 1) {
            alert('This actor is already in the movie');
        }
        result = null;
    }

    //Add new Movie
    $scope.addMovie = function () {        
        $http({
            method: 'POST',
            url: ConstantService.apiURL + '/movies',
            data: { Name: $scope.movie.movieName, YEAROFRELEASE: $scope.movie.yor, PLOT: $scope.movie.plot, PLOT: $scope.movie.plot, Producer: $scope.movie.producer },//forms user object
            headers: { 'Content-Type': 'application/json' }
        }).then(function (res) {                        
            $('#myModal').modal('hide');
            $('#InfoModal').modal('show');
            $scope.loadlist();
            
        }, function (error) {
            console.log(error.statusText);
        })
    };


    //Load Actors corresponding to there movie
    $scope.fillActors = function (movieID) {
        $scope.movieID = movieID;
        $http.get(ConstantService.apiURL + '/movies/' + $scope.movieID).success(function (response) {
            $scope.MovieName = response.Name;//to check movie name
        });
        $http.get(ConstantService.apiURL + '/MoviesActors/?movieID=' + $scope.movieID).success(function (response) {
            $scope.actorsinmovie = response;            
        }).error(function (response) {
            console.log(response);
        });
    }

    //Remove Actor from the corresponding movie
    $scope.removeActor = function (id) {
        var r = confirm("Are you sure you want to remove this actor from this movie");       
        if (r == true) {
            return $http.delete(ConstantService.apiURL + '/MoviesActors/?movieId=' + $scope.movieID + '&' + 'personID=' + id).then(function (res) {                
                $scope.fillActors($scope.movieID);
            }, function (error) {
                console.log(error.statusText);
            });
        }
    }

    //Laod Producers
    $scope.fillProducer = function () {
        $http.get(ConstantService.apiURL + '/persons/?personType=producer').success(function (response) {
            $scope.producerlist = response;            
        }).error(function (response) {
            console.log(response);
        });
    }

    //Edit data
    $scope.saveData = function (data, id) {        
        $http.get(ConstantService.apiURL + '/Movies/?name=' + data.Producer.Name).success(function (response) {           
            var newData = JSON.parse(JSON.stringify(data));            
            newData.Producer = response.PersonID;
            return $http.put(ConstantService.apiURL + '/Movies/' + id, newData).then(function (res) {
            }, function (error) {
                console.log(error.statusText)
            });
        });      
    };

    //Removing Movie form DB
    $scope.removeData = function (id) {

        var r = confirm("Are you sure you want to delete this Movie");

        if (r == true) {

            return $http.delete(ConstantService.apiURL + '/movies/' + id).then(function (res) {                
                $scope.loadlist();
            }, function (error) {
                console.log(error.statusText);                
            });
        }
    };


    
});

