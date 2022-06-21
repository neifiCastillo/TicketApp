(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$scope'];

    function HomeController($scope) {

        $scope.title = "vista Home";

        // variables
       /* $scope.loadTemplateTickets = "/Views/Tickets/Index.cshtml";*/

        // methods 
        //$scope.GetAllTickets = GetAllTickets;


        //function GetAllTickets() {
        //    TicketFactory.GetAllTickets()
        //        .then(resp => {
        //            var data = angular.fromJson(resp.data);
        //            $scope.listadoTickets = data;
        //        })
        //        .catch(error => console.log(error));
        //}

        activate();

        function activate() {
           
        }
    }
})();
