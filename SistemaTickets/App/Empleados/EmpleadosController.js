(function () {
    'use strict';

    angular
        .module('app')
        .controller('EmpleadosController', EmpleadosController);

    EmpleadosController.$inject = ['$scope', 'EmpleadoFactory'];

    function EmpleadosController($scope, EmpleadoFactory) {

        $scope.title = "vista Empleado";

        //// variables
        $scope.createModeEmpleado = false;
        $scope.IsEditModeEmpleado = false;
        $scope.listaEmpleados = [];
/*        $scope.CopieRecord = [];*/
        $scope.empleadoModel = {
            Cedula: '',
            NombreCompleto: '',
        };

        //// methods         
        $scope.CreateEmpleado = CreateEmpleado;
        $scope.GetAllEmpleados = GetAllEmpleados;
        $scope.PushChanges = PushChanges;
        $scope.PushDesactivar = PushDesactivar;


        function CreateEmpleado() {
            EmpleadoFactory.CreateEmpleado($scope.empleadoModel)
                .then(resp => {
                    console.log(resp);
                    GetAllEmpleados();
                    $scope.toggleCreateMode();
                })
        }

        function GetAllEmpleados() {
            EmpleadoFactory.GetAllEmpleados()
                .then(resp => {
                    var data = angular.fromJson(resp.data);
                    $scope.listaEmpleados = data;
                })
        }

        $scope.toggleCreateMode = () => {
            $scope.createModeEmpleado = $scope.createModeEmpleado ? false : true;
        }


        $scope.toggleEditMode = (record) => {
            $scope.IsEditModeEmpleado = true;
            //angular.copy(record, $scope.empleadoModel);
            $scope.empleadoModel = record;
           /* $scope.empleadoModel.push(CopieRecord);*/
        }

        function PushChanges(){
            EmpleadoFactory.UpdateEmpleado($scope.empleadoModel).then(r => {
                $scope.createModeEmpleado = false;
                $scope.IsEditModeEmpleado = false;
                $scope.toggleCreateMode();
                GetAllEmpleados();

            });
        };

        function PushDesactivar(empleado){
            EmpleadoFactory.DeleteEmpleado(empleado).then(r => {
                GetAllEmpleados();
            });
        };
        

        activate();

        function activate() {
            GetAllEmpleados();
        }
    }
})();