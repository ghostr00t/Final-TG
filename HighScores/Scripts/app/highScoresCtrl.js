(function () {
    'use strict';

//create angularjs controller
var app = angular.module('app', []);//set and get the angular module
app.controller('highScoresController', ['$scope', '$http', highScoresController]);

//angularjs controller method
function highScoresController($scope, $http) {

    //declare variable for mainain ajax load and entry or edit mode
    $scope.loading = true;
    $scope.addMode = false;

    //get all scores
    $http.get('/api/HighScores/').success(function (data) {
        debugger
        $scope.highScores = data;
        $scope.loading = false;
    })
        .error(function () {
            $scope.error = "An Error has occured while loading scores!";
            $scope.loading = false;
        });

    //by pressing toggleEdit button ng-click in html, this method will be hit
    $scope.toggleEdit = function () {
        this.highScore.editMode = !this.highScore.editMode;
    };

    //by pressing toggleAdd button ng-click in html, this method will be hit
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Insert HighScore
    $scope.add = function () {
        $scope.loading = true;
        $http.post('/api/HighScores/', this.newhighscore).success(function (data) {
            alert("Added Successfully!!");
            $scope.addMode = false;
            $scope.highScores.push(data);
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An error has occured while adding score! " + data;
            $scope.loading = false;
        });
    };

    //Edit HighScore
    $scope.save = function () {
        alert("Edit");
        $scope.loading = true;
        var frien = this.highScore;
        alert(frien);
        $http.put('/api/HighScores/' + frien.Id, frien).success(function (data) {
            alert("Saved Successfully!!");
            frien.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while saving score! " + data;
            $scope.loading = false;
        });
    };

    //Delete HighScore
    $scope.deletehighscore = function () {
        $scope.loading = true;
        var Id = this.highScore.Id;
        $http.delete('/api/HighScores/' + Id).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.highScores, function (i) {
                if ($scope.highScores[i].Id === Id) {
                    $scope.highScores.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while saving score! " + data;
            $scope.loading = false;
        });
    };

}

})();