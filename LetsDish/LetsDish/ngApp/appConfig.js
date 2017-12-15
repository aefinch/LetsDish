app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/login",
        {
            templateUrl: "/ngApp/Views/login.html",
            controller: "loginController"

        })
        .when("/",
        {
            templateUrl: "/ngApp/Views/recipeList.html",
            controller: "recipeController"
        })
        .when("/addRecipe",
        {
            templateUrl: "/ngApp/Views/addRecipe.html",
            controller: "recipeController"

        })
        .when("/myFriends",
        {
            templateUrl: "/ngApp/Views/myFriends.html",
            controller: "friendsController"

        })
        .when("/events",
        {
            templateUrl: "/ngApp/Views/myEvents.html",
            controller: "eventsController"

        });


}]);

app.run(["$rootScope", "$http", "$location", function ($rootScope, $http, $location) {

    $rootScope.isLoggedIn = function () { return !!sessionStorage.getItem("token") }

    $rootScope.$on("$routeChangeStart", function (event, currRoute) {
        var anonymousPage = false;
        var originalPath = currRoute.originalPath;

        if (originalPath) {
            anonymousPage = originalPath.indexOf("/login") !== -1;
        }

        if (!anonymousPage && !$rootScope.isLoggedIn()) {
            event.preventDefault();
            $location.path("/login");
        }
    });

    var token = sessionStorage.getItem("token");

    if (token)
        $http.defaults.headers.common["Authorization"] = `bearer ${token}`;
}])