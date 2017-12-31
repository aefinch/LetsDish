app.controller("editRecipeController", ["$scope", "$http", "$location", "$routeParams", function ($scope, $http, $location, $routeParams) {
    let vm = this;
    let id = $routeParams.id
    vm.editIngredient = false;
    vm.moreIngredients = false;
    thisRecipe = function () {
        $http.get(`api/Recipes/${id}`)
            .then(function (result) {
                vm.recipe = result.data;
            })
    }
    thisRecipe();
    getIngredients = function () {
        $http.get(`/api/Ingredients/forRecipe/${id}`)
            .then(function (result) {
                vm.ingredients = result.data;
            });
    };
    getIngredients();
    vm.showEditIngredient = function () {
        vm.editIngredient = !vm.editIngredient;
    }
    vm.updateIngredient = function (ingredient) {
        $http.put(`/api/Ingredients/${ingredient.IngredientId}`,
            JSON.stringify({
                IngredientId: ingredient.IngredientId,
                IngredientDescription: ingredient.IngredientDescription,
                OnShoppingList: ingredient.OnShoppingList,
                RecipeId: id

            }))
            .then(function (result) {
                vm.showEditIngredient();
                getIngredients();
            })


    }
    vm.deleteIngredient = function (ingredientId) {
        $http.delete(`/api/Ingredients/${ingredientId}`)
            .then(function (result) {
                getIngredients();
            });

    }
    vm.addMoreIngredients = function () {
        vm.moreIngredients = !vm.moreIngredients;
    }
    vm.addIngredient = function () {

        let ingredient = vm.ingredient;
        $http.post("/api/Ingredients",
            {
                IngredientDescription: ingredient.IngredientDescription,
                OnShoppingList: false,
                RecipeId: id
            })
            .then(result => {
                vm.ingredient.IngredientDescription = "";
                getIngredients();
            })
            .catch(error => console.log(error));

    }
    vm.doneWithIngredients = function () {
        vm.addIngredient();
        vm.addMoreIngredients();

    }
   
    vm.updateRecipe = function () {
        let recipe = vm.recipe;
        $http.put(`/api/Recipes/${id}`,
            JSON.stringify({
                RecipeId: id,
                RecipeName: recipe.RecipeName,
                Instructions: recipe.Instructions,
                Yield: recipe.Yield,
                RecipeSource: recipe.RecipeSource,
                MinutesToMake: recipe.MinutesToMake,
                Rating: recipe.Rating,
                Picture: recipe.Picture,
                Notes: recipe.Notes,
                Events: recipe.Events

            })).then(function (result) {
                $location.path("/");
            })
            .catch(error => console.log(error));

    }
    

}]);