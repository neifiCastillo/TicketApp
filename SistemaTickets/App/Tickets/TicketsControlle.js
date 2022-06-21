(function () {
    'use strict';

    angular
        .module('app')
        .controller('TicketsControlle', TicketsControlle);

    TicketsControlle.$inject = ['$scope', 'TicketFactory'];

    function TicketsControlle($scope, TicketFactory) {

        $scope.title = "vista tickets";

        // variables
        $scope.listadoTickets = [];
        $scope.listaEmpleados = [];
        $scope.createMode = false;
        $scope.Newitem = {
            NombreEmpleado : '',
            Titulo : '',
            Descripcion: '',
            FechaVencimiento: '',
            Estado : '',
            Prioridad: '',
            Estatus: true,
            FechaCreacion: '',
            FechaModificacion : '',
        }
        // methods 
        $scope.GetAllTickets = GetAllTickets;
        $scope.SaveTicket = SaveTicket;
        $scope.fecha = convertToJavaScriptDate;
        $scope.GetAllEmpleados = GetAllEmpleados;


        $scope.toggleCreateMode = () => {
            $scope.createMode = $scope.createMode ? false : true;
        }

        function GetAllTickets() {
            TicketFactory.GetAllTickets()
                .then(resp => {
                    var data = angular.fromJson(resp.data);
                    $scope.listadoTickets = data;
                })
                .catch(Error => console.log(Error));
        }

        function SaveTicket(model) {
            TicketFactory.SaveTicket(model)
                .then(resp => {
                    $scope.toggleCreateMode();
                    $scope.Newitem = {};
                })
                .catch(Error => console.log(Error))
        }

        function convertToJavaScriptDate(value) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
        }

        function GetAllEmpleados() {
            TicketFactory.GetAllEmpleados()
                .then(resp => {
                    var data = angular.fromJson(resp.data);
                    $scope.listaEmpleados = data;
                })
        }

        activate();

        function activate() {
            GetAllTickets();
        }
    }
})();
