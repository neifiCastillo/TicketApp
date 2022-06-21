(function () {
    'use strict';

    angular
        .module('app')
        .factory('SistemaFactory', SistemaFactory);

    SistemaFactory.$inject = ['$http'];

    function SistemaFactory($http) {

        var service = {};
        var urlBase = '/Sistemas/';

        service.GetAllSistemas = GetAllSistemas;
        service.CreateSistema = CreateSistema;
        service.UpdateSistema = UpdateSistema;
        service.DeleteSistema = DeleteSistema;

        function CreateSistema(model) {
            return $http.post(urlBase + 'CreateSistema', model);
        }

        function GetAllSistemas() {
            return $http.get(urlBase + 'GetAllSistemas');
        }

        function UpdateSistema(sistema) {
            return $http.post(urlBase + 'UpdateSistema', sistema);
        }

        function DeleteSistema(sistema) {
            return $http.post(urlBase + 'DeleteSistema', { sistema: sistema });
        }

        return service;

    }

})();