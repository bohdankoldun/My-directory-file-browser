(function () {
    'use strict';

    angular
        .module('myFileBrowserApp')
        .controller('FilesController', files);

    function files($scope, $http) {

        //get all hard disk drives
        $http({
            method: 'GET',
            url: '/api/FileBrowser'
        })
         .success(function (data) {
             $scope.drives = data;
         }).error(function (error, status) {
             $scope.error = { message: error, status: status };
             console.log($scope.error);
         });

        //function for get list of directories and files of any folder
        $scope.getDirectory = function ($event) {
            var Path = $event.currentTarget.id;
            $http({
                method: "GET",
                url: '/api/FileBrowser',
                params: { path: Path }
            }).success(function (data) {
                $scope.folder = data;
                $scope.exception = "";
            }).error(function (error, status) {
                $scope.exception = "No access to the folder";
                console.log({ message: error, status: status });
            });
        };

       
    }

  
})();


