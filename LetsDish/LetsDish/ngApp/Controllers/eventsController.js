app.controller("eventsController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    let vm = this;
    vm.addRecipe = false;
    vm.createEvent = function () {
        let event = vm.event;
        $http.post("/api/Events",
            {
                EventName: event.EventName
            }
        )
            .then(result => {
                currentEvent = result.data;
                vm.addRecipe = true;
                console.log(vm.addRecipe)
            })
            .catch(error => console.log(error));
    };
}]);