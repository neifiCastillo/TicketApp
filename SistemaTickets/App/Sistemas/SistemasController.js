(function () {
    'use strict';

    angular
        .module('app')
        .controller('SistemasController', SistemasController);

    SistemasController.$inject = ['$scope', 'SistemaFactory'];

    function SistemasController($scope, SistemaFactory) {

        $scope.title = "vista Sistemas";


        //// variables
        $scope.createModeSistema = false;
        $scope.IsEditModesSistema = false;
        $scope.listadoSistemas = [];
        $scope.sistemaModel = {
            Nombre: ''
        };

        // methods
        $scope.GetAllSistemas = GetAllSistemas;
        $scope.CreateSistema = CreateSistema;
        $scope.PushChanges = PushChanges;
        $scope.PushDesactivar = PushDesactivar;

        function CreateSistema() {
            SistemaFactory.CreateSistema($scope.sistemaModel)
                .then(resp => {
                    console.log(resp);
                    $scope.toggleCreateMode();
                    GetAllSistemas();
                })
        }

        function GetAllSistemas() {
            SistemaFactory.GetAllSistemas()
                .then(resp => {
                    var data = angular.fromJson(resp.data);
                    $scope.listadoSistemas = data;
                })
        }

        $scope.toggleCreateMode = () => {
            $scope.createModeSistema = $scope.createModeSistema ? false : true;
        }
        $scope.toggleEditMode = (record) => {
            $scope.IsEditModesSistema = true;
            $scope.sistemaModel = record;
        }


        function PushChanges() {
            SistemaFactory.UpdateSistema($scope.sistemaModel).then(r => {
                $scope.createModeSistema = false;
                $scope.IsEditModesSistema = false;
                $scope.toggleCreateMode();
                GetAllSistemas();

            });
        };

        function PushDesactivar(sistema) {
            SistemaFactory.DeleteSistema(sistema).then(r => {
                GetAllSistemas();
            });
        };

        activate();

        function activate() {
            GetAllSistemas();
        }
    }
})();