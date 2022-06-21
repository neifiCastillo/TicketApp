(function () {
    'use strict';

    angular
        .module('app')
        .factory('TicketFactory', TicketFactory);

    TicketFactory.$inject = ['$http'];

    function TicketFactory($http) {

        var service = {};
        var urlBase = '/Tickets/';

        service.GetAllTickets = GetAllTickets;
        service.SaveTicket = SaveTicket;
        service.GetAllEmpleados = GetAllEmpleados;

        // methods 

        function GetAllTickets() {
            return $http.get(urlBase + 'GetAllTickets');
        }

        function SaveTicket(model) {
            return $http.post(urlBase + 'SaveTicket', model);
        }

        function GetAllEmpleados() {
            return $http.get(urlBase + 'GetAllEmpleados');
        }

        return service;
        
    }      
    
})();