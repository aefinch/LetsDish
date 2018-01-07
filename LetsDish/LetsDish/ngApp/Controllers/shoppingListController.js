app.controller("shoppingListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    let vm = this;
    var myIngredients = [];
    vm.ingredients = [];
    var myRecipes = function () {
        $http.get("api/Recipes/forUser")
            .then(function (result) {
                getIngredients(result.data);
            });
    }
    myRecipes();

    var getIngredients = function (recipes) {
        vm.ingredients = [];
        myIngredients = [];
        for (var x = 0; x < recipes.length; x++)
        {
            let thisRecipe = recipes[x];
            $http.get(`/api/Ingredients/onList/forRecipe/${thisRecipe.RecipeId}`)
                .then(function (result) {
                    myIngredients = result.data;
                    for (var y = 0; y < myIngredients.length; y++) {
                        vm.ingredients.push(myIngredients[y]);
                    }
                });
        }
        
    }

    vm.removeFromList = function (ingredient) {
        $http.put(`/api/Ingredients/${ingredient.IngredientId}`,
            JSON.stringify({
                IngredientId: ingredient.IngredientId,
                IngredientDescription: ingredient.IngredientDescription,
                OnShoppingList: false,
                RecipeId: ingredient.Recipe.RecipeId

            })).then(function (result) {
                myRecipes();
            })
            .catch(error => console.log(error));
    }

}]);