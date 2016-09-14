(function () {
    'use strict';

    angular
        .module('app')
        .service('BrowsingService', BrowsingService);

    BrowsingService.$inject = ['$http'];

    function BrowsingService($http) {
        this.getFileSystemModel = getFileSystemModel;

        function getFileSystemModel(path) {
            return $http({
                method: 'GET',
                url: '/api/browsing',
                params: { path: path }
            });
        }
    }
})();