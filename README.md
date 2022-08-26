-TABLAS Y SP PARA LA BASE DE DATOS (SQL SERVER)-
-TABLES & SP FOR THE DATABSE (SQL SERVER)-



*
create table Usuario(
Id int primary key identity(1,1),
Nombre varchar(50),
UserName varchar(50),
Email varchar(50),
Contraseña varchar(50)
)
*



*
create table Documentos(
IdDocumento int primary key identity(1,1),
Descripcion varchar(100),
Ruta varchar(100),
EmailUsu varchar(50)
)
*


*
create proc SP_RegistrarUsuario(
@Nombre varchar(50)
,@UserName varchar(50)
,@Email varchar(50)
,@Contraseña varchar(50)
)
as
begin

	if(NOT EXISTS(SELECT * FROM Usuario WHERE Email = @Email ))
	BEGIN
		INSERT INTO Usuario(Nombre, UserName, Email, Contraseña)VALUES(@Nombre, @UserName, @Email, @Contraseña )

		DECLARE @Id int = (SELECT IDENT_CURRENT('Usuario'))
	end
  
end
*


*
create proc SP_Login(
@Email varchar(50)
,@Contraseña varchar(50)
)
AS
BEGIN

	IF
	(EXISTS(SELECT * FROM Usuario where Email = @Email and Contraseña = @Contraseña))
	select Id from Usuario where Email = @Email and Contraseña = @contraseña
	ELSE
	SELECT '0'
  
END
*


*
create proc SP_ListarUsuarios
As
Begin
Select * From Usuario
end
*

*
create proc SP_GuardarDocumentos(
@descripcion varchar(100)
,@Ruta varchar(100)
)
As
Begin
	insert into Documentos(Descripcion, Ruta) values(@descripcion, @Ruta)
	--Select
	--D.Descripcion
	--,D.Ruta
	--from Documentos as D
	--inner join Usuario as U on U.Id = D.EmailUsu
end
*
