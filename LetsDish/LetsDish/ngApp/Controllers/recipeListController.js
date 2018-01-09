app.controller("recipeListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    let vm = this;

    myRecipes = function () {
        $http.get("api/Recipes/forUser")
            .then(function (result) {
                vm.recipes = result.data;
            })
    }
    myRecipes();
    vm.RecipeView = function (id) {
        $location.path(`recipeView/${id}`);
    }
    vm.addArecipe = function () {
        $location.path("addRecipe")
    }
}]);