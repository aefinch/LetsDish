app.controller("loginController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    let vm = this;
    vm.registered = true;
    vm.showRegister = () => {
        vm.registered = !vm.registered;
    };
    vm.login = function () {

        console.log(vm.username, vm.password);
        vm.error = "";
        vm.inProgress = true;
        $http({
            method: 'POST',
            url: "/Token",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: { grant_type: "password", username: vm.username, password: vm.password }
        })
            .then(function (result) {
                sessionStorage.setItem('token', result.data.access_token);
                $http.defaults.headers.common['Authorization'] = `bearer ${result.data.access_token}`;
                $location.path("/");

                vm.inProgress = false;
            }, function (result) {
                vm.error = result.data.error_description;
                vm.inProgress = false;
            });
    }
    vm.warning = "";
    vm.register = function () {
        if (vm.password == vm.password2) {
            vm.password = vm.password;
        } 
        else {
            vm.warning = "Your passwords do not match. Please try again!";
        }

    }
}]);