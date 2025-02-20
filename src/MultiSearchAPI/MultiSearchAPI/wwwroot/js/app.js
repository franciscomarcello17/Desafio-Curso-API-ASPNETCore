(function () {
        var app = angular.module("MultiSearchApp", []);
        app.controller("MultiSearchController", function ($scope, $http) {
            $scope.searchQuery = "";
        $scope.searchResults = [];
        $scope.groupedResults = { };

        $scope.search = function () {
                if (!$scope.searchQuery.trim()) return;

        $http.get("http://localhost:5099/api/search", {params: {query: $scope.searchQuery } })
        .then(function (response) {
            $scope.searchResults = response.data;
        $scope.groupResults();
                    })
        .catch(function (error) {
            console.error("Erro ao buscar", error);
                    });
            };

        $scope.groupResults = function () {
            $scope.groupedResults = $scope.searchResults.reduce((acc, item) => {
                if (!acc[item.category]) {
                    acc[item.category] = [];
                }
                acc[item.category].push(item);
                return acc;
            }, {});
            };
        });
})();
