app.controller("recipeController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    let vm = this;
    let currentRecipeId = 0;
    vm.setup = true;
    vm.generateForm = function () {
        let recipe = vm.recipe;
        $http.post("/api/Recipes",
            {
                RecipeName: recipe.RecipeName
            }
        )
            .then(result => {
                console.log(result.data.RecipeId);
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
                Recipe: ingredient.Recipe
            })
            .then(result => {
                console.log(result.data);
            })
            .catch(error => console.log(error));
            
    }

}]);