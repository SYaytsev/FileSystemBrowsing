(function () {
    'use strict';

    angular
        .module('app')
        .controller('BrowsingController', BrowsingController);

    BrowsingController.$inject = ['BrowsingService'];

    function BrowsingController(BrowsingService) {
        var vm = this;
        vm.follow = follow;

        function follow(path) {
            BrowsingService.getFileSystemModel(path).success(function (response) {
                vm.model = response;
            });
        }

        activate();

        function activate() {
            BrowsingService.getFileSystemModel('').success(function (response) {
                vm.model = response;
            });
        }
    }
})();