(function () {
    'use strict';

    angular
        .module('app')
        .factory('UsuarioFactory', UsuarioFactory);

    UsuarioFactory.$inject = ['$http'];

    function UsuarioFactory($http) {

        var service = {};
        var urlBase = '/Usuario/';

        service.RegistrarUsuario = RegistrarUsuario;
        service.AutenticarUsuario = AutenticarUsuario;
        service.IrAHome = IrAHome;


        function RegistrarUsuario(model) {
            return $http.post(urlBase + 'RegistrarUsuario', model);
        }

        function AutenticarUsuario(model) {
            return $http.post(urlBase + 'AutenticarUsuario', model);
        }

        function IrAHome() {
            $http.get(urlBase + 'IrAHome')
                .then(resp => {
                    console.log(resp);
                    var resp = angular.fromJson(resp.data);

                    window.location.href = resp.url;
                })
        }


        return service;

    }

})();