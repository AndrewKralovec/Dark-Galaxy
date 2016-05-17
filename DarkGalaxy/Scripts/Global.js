var app = angular.module('myApp', []);

app.controller('customersCtrl', function ($scope, $http) {

    function refresh() {
        $http.get("/Database/KralovecSelect").success(function (response) {
            $scope.names = response;
        }).error(function (error) {
            alert("Get Error: " + error);
        });

    }
    refresh(); // init refresh 

    // Version
    function getVersion() {
        $http.get("/Database/KralovecVersion").success(function (response) {
            alert("" + response);
        }).error(function (error) {
            alert("Get Error: " + error);
        });
    }


    // put response into the input boxes 
    $scope.editItem = function (id) {
        alert("Course"+id)
        $http.post('/Database/KralovecMessage', { id: id }).success(function (response) {
            console.log("Response" + JSON.stringify(response));
            $scope.contact = response.data;
            refresh();
        });
    }

    // Insert Item from DB
    $scope.addContact = function () {
        alert("Course: " + $scope.contact.Course);
        $http.post('/Database/KralovecInsert', $scope.contact).success(function (response) {
            alert("Response" + JSON.stringify(response));
            refresh();
        }).error(function (error) {
            alert("Get Error: " + error);
        });
    }

    // Delete Item from DB
    $scope.dropItem = function (id) {
        alert("Course" + id);
        $http.post('/Database/KralovecDelete', { id: id }).success(function (response) {
            alert("Response" + JSON.stringify(response));
            refresh();
        }).error(function (error) {
            alert("Get Error: " + error);
        });
    }

    //update Database 
    $scope.update = function () {
        alert("Course: " + $scope.contact.Course);
        $http.post('/Database/KralovecUpdate', $scope.contact).success(function (response) {
            alert("Response" + JSON.stringify(response));
            $scope.contact = response.data;
            refresh();
        }).error(function (error) {
            alert("Get Error: " + error);
        });
    }

    // Empty out fields 
    $scope.deselect = function () {
        $scope.contact = "";
    }
});

