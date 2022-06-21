
use TicketDB
--Crud Colas (Agregar, Eliminar, Modificar)
--Crud Integrantes de Colar
--Crud Sistemas para asignación de incidentes
--Crud Nivel de prioridad según vencimiento con los colores (poder establecer rango de 0 – 35, de 35 a 65
--es naranja, de 65 a 100 es rojo).
go

create table Usuario(
Id int identity(1,1) primary key,
Nombre varchar(50) not null,
Password varchar(50) not null,
Rol varchar(10) not null,
)

create table Empleado (
Id int identity(1,1) primary key,
Cedula varchar(10) not null,
Nombre varchar(50) not null,
Apellido varchar(50) not null,
Puesto varchar(50) not null,
)

create table Sistema (
Id int identity(1,1) primary key,
NombreSistema varchar(50) not null,
)

create table Cola (
Id int identity(1,1) primary key,
NombreCola varchar(50) not null,
IdSistema int references Sistema(Id) not null
)

create table colaEmpleado(
IdEmpleado int references Empleado(Id) not null,
IdCola int references Cola(Id) not null
)

create table ticket(
Id int identity(1,1) primary key,
Titulo varchar(200) not null,
Descripcion varchar(max) null,
AsignadoA int references Empleado(Id) not null,
Cola int references Cola(Id) not null,
Prioridad int not null,
Estado varchar(100) not null,
FechaCreacion date not null,
FechaVencimiento date not null,
)


