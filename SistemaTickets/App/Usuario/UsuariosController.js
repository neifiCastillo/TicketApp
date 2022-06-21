(function () {
    'use strict';

    angular
        .module('app')
        .controller('UsuariosController', UsuariosController);

    UsuariosController.$inject = ['$scope', 'UsuarioFactory'];

    function UsuariosController($scope, UsuarioFactory) {

        // variables 

        $scope.modeloUsuario = {
            usuario: '',
            password: ''
        };


        // Metodos 

        $scope.RegistrarUsuario = RegistrarUsuario;
        $scope.AutenticarUsuario = AutenticarUsuario;

        function RegistrarUsuario() {
            UsuarioFactory.RegistrarUsuario($scope.modeloUsuario)
                .then(resp => {
                    var data = angular.fromJson(resp.data);
                    data.estatus ? toastr.success(data.mensaje) : toastr.error(data.mensaje);
                })
                .catch(error => toastr.error(error));
        }

        function AutenticarUsuario() {
            UsuarioFactory.AutenticarUsuario($scope.modeloUsuario)
                .then(resp => {
                    var data = angular.fromJson(resp.data);
                    data.Estatus ? toastr.success(data.Mensaje) : toastr.error(data.Mensaje);
                    UsuarioFactory.IrAHome();
                })
                .catch(error => toastr.error(error));
        }


        
        //// variables
        //$scope.registOlogin = true;

        //// methods 
        //$scope.ClickregistOlogin = ClickregistOlogin;


        //function ClickregistOlogin() {
        //    $scope.registOlogin = false;
        //}

        //activate();

        //function activate() {
        //   ClickregistOlogin();
        //}
    }
})();