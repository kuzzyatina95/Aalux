var myApp = angular.module('myApp', ['ngMap']);

myApp.controller('mapCtrl', function (NgMap, $scope) {
    $scope.vm = {};
    $scope.totalDistance = {};
    $scope.vm.placeChangedOrigin = function () {
        $scope.vm.placeOrigin = this.getPlace();
        console.log('location', $scope.vm.placeOrigin.geometry.location);
        $scope.vm.map.setCenter($scope.vm.placeOrigin.geometry.location);
    }
    $scope.vm.placeChangedDestination = function () {
        $scope.vm.placeDestination = this.getPlace();
        console.log('location', $scope.vm.placeDestination.geometry.location);
        $scope.vm.map.setCenter($scope.vm.placeDestination.geometry.location);
    }

    NgMap.getMap().then(function (map) {
        $scope.vm.map = map;
    });
});