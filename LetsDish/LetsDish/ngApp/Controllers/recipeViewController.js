app.controller("recipeViewController", ["$scope", "$http", "$location", "$routeParams", function ($scope, $http, $location, $routeParams) {
    let vm = this;
    let id = $routeParams.id;
    $scope.photos = [];
    vm.uploadPic = false;
    vm.overwriteRecipe = false;
    thisRecipe = function () {
        $http.get(`api/Recipes/${id}`)
            .then(function (result) {
                vm.recipe = result.data;
            });
    };
    thisRecipe();
    getIngredients = function () {
        $http.get(`/api/Ingredients/forRecipe/${id}`)
            .then(function (result) {
                vm.ingredients = result.data;
            });
    };
    getIngredients();

    vm.addToList = function (ingredient) {

        $http.put(`/api/Ingredients/${ingredient.IngredientId}`,
            JSON.stringify({
                IngredientId: ingredient.IngredientId,
                IngredientDescription: ingredient.IngredientDescription,
                OnShoppingList: true,
                RecipeId: vm.recipe.RecipeId

            })).then(function (result) {

            })
            .catch(error => console.log(error));

    };
    vm.showButton = function () {
        vm.uploadPic = true;
    };
    vm.addPicture = function () {
        // vm.image.PostedFile.SaveAs(Server.MapPath("~/images")+vm.image.FileName)
        var files = {};
        angular.forEach($scope.photos, function (photo) {
            //console.log(vm.recipe.RecipeId);
            //name = vm.recipe.RecipeId + photo.name;
            //photo.name = name;
            //console.log(photo.name);
            photo.name = vm.recipe.RecipeId + photo.name
            files[photo.name] = photo;
            //files[photo.name].name = name;
            //console.log(files[photo.name].name);
            //console.log(files[photo.name]);
        });
        $http.post("api/photo", files,
            {
                headers: { "Content-Type": undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    angular.forEach(data, function (value, key) {
                        debugger
                        value.name = vm.recipe.RecipeId + value.name;
                        debugger
                        formData.append(key, value);
                    });
                    return formData;
                }
            });
        let recipe = vm.recipe;
        recipe.Picture = "./images/" + $scope.photos[0].name;
        $http.put(`/api/Recipes/${recipe.RecipeId}`,
            JSON.stringify({
                RecipeId: recipe.RecipeId,
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


    };
    vm.editRecipe = function () {
        vm.overwriteRecipe = true;
    };
    vm.editCurrentRecipe = function () {
        $location.path(`editRecipe/${id}`);
    };
    vm.duplicateRecipe = function () {
        let recipe = vm.recipe;
        $http.post("/api/Recipes",
            {
                RecipeName: recipe.RecipeName,
                Instructions: recipe.Instructions,
                Yield: recipe.Yield,
                RecipeSource: recipe.RecipeSource,
                MinutesToMake: recipe.MinutesToMake,
                Rating: recipe.Rating,
                Picture: recipe.Picture,
                Notes: recipe.Notes,
                Events: recipe.Events
            }
        )
            .then(result => {
                currentRecipe = result.data;
                console.log(currentRecipe);

                vm.ingredients.forEach(function (ingredient) {

                    ingredient.Recipe = currentRecipe;
                    $http.post("/api/Ingredients",
                        {
                            IngredientDescription: ingredient.IngredientDescription,
                            OnShoppingList: false,
                            RecipeId: ingredient.Recipe.RecipeId
                        })
                        .then(result => {
                            $location.path(`editRecipe/${currentRecipe.RecipeId}`);
                        })
                        .catch(error => console.log(error));
                });
            })
            .catch(error => console.log(error));
    };
    vm.emailRecipe = function () {
        console.log("emailing");
    }
}]);