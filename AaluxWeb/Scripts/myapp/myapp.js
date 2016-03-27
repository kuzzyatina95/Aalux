var myApp = angular.module('myApp', ['ngMap']);

myApp.controller('orderCtrl', function (NgMap, $scope, $http) {

    $scope.vm = {};
    $scope.ClassCar = {};
    $scope.order = {
        Client: {
            Name: {},
            Surname: {},
            Email: {},
            Phone: {}
        },
        Direction: {
            AddressOrigin: 'OK',
            LatOrigin: {},
            LngOrigin: {},
            AddressDestination: {},
            LatDestination: {},
            LngDestination: {}
        },
        ClassCar: {},
        PaymentId: {},
        DateBegin: {},
        TimeBegin: {}
    };

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
        $scope.order = {
            Direction: {
                AddressOrigin: 'OK',
                LatOrigin: {},
                LngOrigin: {},
                AddressDestination: {},
                LatDestination: {},
                LngDestination: {}
            }
        };
        $scope.selected = $scope.ClassCar[0];
    });

    $scope.SetClassCar = function (classcar) {
        $scope.order.ClassCar = classcar;
    }

    //$scope.CreatePost = function () {
    //    $scope.order.Direction.AddressOrigin = $scope.vm.origin;
    //    $scope.order.Direction.AddressDestination = $scope.vm.destination;
    //    $scope.order.Direction.LatOrigin = $scope.vm.placeOrigin.geometry.location.lat();
    //    $scope.order.Direction.LngOrigin = $scope.vm.placeOrigin.geometry.location.lng();
    //    $scope.order.Direction.LatDestination = $scope.vm.placeDestination.geometry.location.lat();
    //    $scope.order.Direction.LngDestination = $scope.vm.placeDestination.geometry.location.lng();
    //    $http.post('/Home/IndexCreateOrder', $scope.order).success(function (data) {
            
    //    });
    //    $scope.order = {};
    //};

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

