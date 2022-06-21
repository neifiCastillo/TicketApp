(function () {
    'use strict';

    angular
        .module('app')
        .factory('EmpleadoFactory', EmpleadoFactory);

    EmpleadoFactory.$inject = ['$http'];

    function EmpleadoFactory($http) {

        var service = {};
        var urlBase = '/Empleados/';

        service.CreateEmpleado = CreateEmpleado;
        service.GetAllEmpleados = GetAllEmpleados;
        service.UpdateEmpleado = UpdateEmpleado;
        service.DeleteEmpleado = DeleteEmpleado;

        function CreateEmpleado(model) {
            return $http.post(urlBase + 'CreateEmpleado', model);
        }

        function GetAllEmpleados() {
            return $http.get(urlBase + 'GetAllEmpleados');
        }

        function UpdateEmpleado(empleado) {
            return $http.post(urlBase + 'UpdateEmpleado', empleado);
        }

        function DeleteEmpleado(empleado) {
            return $http.post(urlBase + 'DeleteEmpleado', { empleado: empleado });
        }


        return service;

    }

})();