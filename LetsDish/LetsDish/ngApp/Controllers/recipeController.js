app.controller("recipeController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    let vm = this;
    let currentRecipeId = 0;
    vm.setup = true;
    vm.writeRecipe = false;
    vm.generateForm = function () {
        let recipe = vm.recipe;
        $http.post("/api/Recipes",
            {
                RecipeName: recipe.RecipeName
            }
        )
            .then(result => {
                currentRecipe = result.data;

            })
            .catch(error => console.log(error));
        vm.setup = false;
    };
    vm.addIngredient = function () {

        let ingredient = vm.ingredient;
        ingredient.Recipe = currentRecipe;
        $http.post("/api/Ingredients",
            {
                IngredientDescription: ingredient.IngredientDescription,
                OnShoppingList: false,
                RecipeId: ingredient.Recipe.RecipeId
            })
            .then(result => {
                vm.ingredient.IngredientDescription = "";
                vm.getIngredients();
            })
            .catch(error => console.log(error));
            
    }
    vm.addDirections = function () {
        vm.addIngredient();

        vm.writeRecipe = true;
    }
    vm.getIngredients = function () {
        $http.get(`/api/Ingredients/forRecipe/${currentRecipe.RecipeId}`)
            .then(function (result) {
                vm.ingredients = result.data;
            });
    };
    vm.updateRecipe = function () {
        let recipe = vm.recipe;
        $http.put(`/api/Recipes/${currentRecipe.RecipeId}`,
            JSON.stringify({
                RecipeId: currentRecipe.RecipeId,
                RecipeName: recipe.RecipeName,
                Instructions: recipe.Instructions,
                Yield: recipe.Yield,
                RecipeSource: recipe.RecipeSource,
                MinutesToMake: recipe.MinutesToMake,
                Rating: recipe.Rating,
                Picture: recipe.Picture,
                Notes: recipe.Notes,
                Events: recipe.Events
               
            })).then(result => $location.path("/"))
            .catch(error => console.log(error));

    }

}]);