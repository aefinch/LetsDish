app.controller("eventsController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    let vm = this;
    vm.selectedRecipe = "";
    vm.addRecipe = false;
    vm.RecipeList = false;
    vm.listView = true;
    var recipesToAdd = [];
    vm.createEvent = function () {
        let event = vm.event;
        $http.post("/api/Events",
            {
                EventName: event.EventName
            }
        )
            .then(result => {
                currentEvent = result.data;
            })
            .catch(error => console.log(error));
                vm.addRecipe = true;
    };
    var getEvents = function () {
        $http.get("/api/Events/forUser")
            .then(function (result) {
                vm.events = result.data;
            })
            .catch(function (error) {
                console.log(error)
            });
    }
    getEvents();
    vm.addEvent = function () {
        $location.path("addEvent");

    };
    vm.returnToList = function () {
        $location.path("events")
    }
    vm.addRecipeToEvent = function () {
        vm.RecipeList = true;
        $http.get(`api/Recipes/forUser`)
            .then(function (result) {
                vm.recipes = result.data;
            })
    }
    vm.recipeToList = function (recipe) {
        $http.post(`/api/Events/${currentEvent.EventId}/${recipe.RecipeId}`)
            .then(function (result) {

            })
            .catch(error => console.log(error));

    }
    vm.viewEvent = function (event)
    {
        vm.listView = false;
        $http.get(`/api/Events/${event.EventId}`)
            .then(function (result) {
                vm.singleEvent = result.data;

            })
            .catch(error => console.log(error));
    }
    vm.backToList = function ()
    {
        vm.listView = true;
    }
    vm.viewRecipe = function (id)
    {
        $location.path(`recipeView/${id}`)
    }
    
}]);