angular.module("umbraco").controller("CustomContentDashboardController", function ($scope, userService, logResource, entityResource, $http) {
    var vm = this;
    $http({
        method: 'GET',
        url: '/umbraco/backoffice/NamNung/PageDetail/GetAll'
    }).then(function successCallback(response) {
        vm.DataApi = response.data;
    });
});
