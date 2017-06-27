app.controller('ActorController', function ($scope,$http, ConstantService) {

    $scope.personlist = [];
    $scope.genderOptions = ['Male', 'Female'];
    
     $scope.opened = {};
     $scope.open = function ($event, elementOpened) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened[elementOpened] = !$scope.opened[elementOpened];
     };

    //Add New Actor
     $scope.addPerson = function () {
         $http({
             method: 'POST',
             url: ConstantService.apiURL + '/persons',
             data: { Name: $scope.person.personname, DOB: $scope.person.DOB, Sex: $scope.person.gender, Bio: $scope.person.bio, Designation: 'actor' },//forms user object
             headers: { 'Content-Type': 'application/json' }
         }).then(function (res) {
             $('#myModal').modal('hide');
             $('#InfoModal').modal('show');
             $scope.loadlist();

         }, function (error) {
             console.log(error.statusText);
         })
     };
           
    //Fill Actors
    $scope.loadlist = function () {
        $http.get(ConstantService.apiURL + '/persons/?personType=actor').success(function (response) {
            $scope.personlist = response;
        }).error(function (response) {
            console.log(response);
        });
    }

    //Edit data
    $scope.saveData = function (data, id) {        
        data.Designation = 'actor';
        return $http.put(ConstantService.apiURL + '/persons/' + id, data).then(function (res) {           
        }, function (error) {            
            console.log(error.statusText)
        });        
    };


    //Removing Actor form DB
    $scope.removeData = function (id) {

        var r = confirm("Are you sure you want to delete this category");

        if (r == true) {

            return $http.delete(ConstantService.apiURL + '/persons/' + id).then(function (res) {               
                if (res.data != "") {
                    alert(res.data);
                }
                $scope.loadlist();
            }, function (error) {
                console.log(error.statusText);
            });
        }
    };
});

