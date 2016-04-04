var myApp = angular.module('myApp', ['ngMap']);

myApp.controller('orderCtrl', function (NgMap, $scope, $http) {

    $scope.vm = {};
    $scope.ClassCar = {};
    $scope.order = {};


    $scope.reRednerMap = function () {
        google.maps.event.trigger(this.map, 'resize');
    };

    $('#myModal').on('shown.bs.modal', function () {
        $scope.reRednerMap();
    });
    $('#payForm').validate({
        ignore: "#payForm *"
    });

    $http.get('/Home/IndexCreateOrder').success(function (data) {
        $scope.ClassCar = data;
        $scope.order = {};
        $scope.selected = $scope.ClassCar[0];
    });

    $scope.SetClassCar = function (classcar) {
        $scope.order.ClassCar = classcar;
    };

    $scope.vm.placeChangedOrigin = function () {
        $scope.vm.placeOrigin = this.getPlace();
        console.log('location', $scope.vm.placeOrigin.geometry.location);
        $scope.vm.map.setCenter($scope.vm.placeOrigin.geometry.location);
    };
    $scope.vm.placeChangedDestination = function () {
        $scope.vm.placeDestination = this.getPlace();
        console.log('location', $scope.vm.placeDestination.geometry.location);
        $scope.vm.map.setCenter($scope.vm.placeDestination.geometry.location);
    };
    NgMap.getMap().then(function (map) {
        $scope.vm.map = map;
    });


});

