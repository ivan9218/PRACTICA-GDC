
CREATE DATABASE DISETOP

USE [DISETOP]
GO
/****** Object:  Table [dbo].[ACTIVOS]    Script Date: 20/10/2023 0:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACTIVOS](
	[CODIGO_ACTIVO] [varchar](25) NOT NULL,
	[NOMBRE_DE_ACTIVO] [varchar](400) NOT NULL,
	[NUMERO_DE_SERIE] [varchar](100) NOT NULL,
	[FECHA_DE_COMPRA] [date] NULL,
	[PROVEEDOR] [varchar](100) NULL,
	[VALOR_DE_COMPRA] [decimal](18, 2) NULL,
	[VALOR_ACTUAL] [decimal](18, 2) NULL,
	[COMENTARIO] [varchar](500) NULL,
 CONSTRAINT [PK_ACTIVOS] PRIMARY KEY CLUSTERED 
(
	[CODIGO_ACTIVO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[CATEGORIA_PROYECTOS]    Script Date: 20/10/2023 0:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORIA_PROYECTOS](
	[CODIGO_CATEGORIA] [varchar](25) NOT NULL,
	[NOMBRE_CATEGORIA] [varchar](100) NOT NULL,
	[COMENTARIO] [varchar](500) NULL,
 CONSTRAINT [PK_CATEGORIA_PROYECTOS] PRIMARY KEY CLUSTERED 
(
	[CODIGO_CATEGORIA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COSTO_PROYECTOS]    Script Date: 20/10/2023 0:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COSTO_PROYECTOS](
	[CODIGO_COSTO_PROYECTO] [varchar](25) NOT NULL,
	[FK_CODIGO_PROYECTO] [varchar](25) NOT NULL,
	[FK_CODIGO_CATEGORIA] [varchar](25) NOT NULL,
	[DIA_TRABAJADOR] [decimal](18, 2) NULL,
	[COMBUSTIBLE] [decimal](18, 2) NULL,
	[VIATICOS] [decimal](18, 2) NULL,
	[HOSPEDAJE] [decimal](18, 2) NULL,
	[TIMBRES_CERTIFICACIONES] [decimal](18, 2) NULL,
	[MATERIALES] [decimal](18, 2) NULL,
	[TOTAL] [decimal](35, 2) NULL,
	[COMENTARIO] [varchar](500) NULL,
 CONSTRAINT [PK_COSTO_PROYECTOS] PRIMARY KEY CLUSTERED 
(
	[CODIGO_COSTO_PROYECTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[EMPLEADOS]    Script Date: 20/10/2023 0:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMPLEADOS](
	[CODIGO_EMPLEADO] [varchar](25) NOT NULL,
	[CEDULA] [int] NULL,
	[NOMBRE_EMPLEADO] [varchar](400) NOT NULL,
	[TELEFONO] [int] NULL,
	[CORREO] [varchar](400) NULL,
	[DIRECCION] [varchar](400) NULL,
	[COMENTARIO] [varchar](500) NULL,
 CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED 
(
	[CODIGO_EMPLEADO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EMPLEADOS]
ALTER COLUMN [TELEFONO] INT;

/****** Object:  Table [dbo].[PAGOS]    Script Date: 20/10/2023 0:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAGOS](
	[CODIGO_PAGO] [varchar](25) NOT NULL,
	[FK_CODIGO_EMPLEADO] [varchar](25) NOT NULL,
	[CUENTA_BANCARIA] [varchar](100) NULL,
	[DIAS_A_PAGAR] [varchar](100) NULL,
	[MONTO] [decimal](18, 2) NULL,
	[FECHA_DE_PAGO] [date] NULL,
	[COMENTARIO] [varchar](500) NULL,
 CONSTRAINT [PK_PAGOS] PRIMARY KEY CLUSTERED 
(
	[CODIGO_PAGO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[PROYECTOS]    Script Date: 20/10/2023 0:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROYECTOS](
	[CODIGO_PROYECTO] [varchar](25) NOT NULL,
	[NOMBRE_PROYECTO] [varchar](400) NOT NULL,
	[FK_CODIGO_CATEGORIA] [varchar](25) NOT NULL,
	[FECHA_INICIO] [date] NULL,
	[ESTADO] [varchar](100) NULL,
	[FECHA_FIN] [date] NULL,
	[PRECIO] [decimal](18, 2) NULL,
	[COMENTARIO] [varchar](500) NULL,
 CONSTRAINT [PK_PROYECTOS] PRIMARY KEY CLUSTERED 
(
	[CODIGO_PROYECTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[USUARIOS]    Script Date: 20/10/2023 0:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOS](
	[ID_USUARIO] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE_USUARIO] [varchar](100) NULL,
	[CLAVE] [varchar](500) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[COSTO_PROYECTOS]  WITH CHECK ADD  CONSTRAINT [FK_COSTO_PROYECTOS_CATEGORIA] FOREIGN KEY([FK_CODIGO_CATEGORIA])
REFERENCES [dbo].[CATEGORIA_PROYECTOS] ([CODIGO_CATEGORIA])
GO
ALTER TABLE [dbo].[COSTO_PROYECTOS] CHECK CONSTRAINT [FK_COSTO_PROYECTOS_CATEGORIA]
GO
ALTER TABLE [dbo].[COSTO_PROYECTOS]  WITH CHECK ADD  CONSTRAINT [FK_COSTO_PROYECTOS_PROYECTOS] FOREIGN KEY([FK_CODIGO_PROYECTO])
REFERENCES [dbo].[PROYECTOS] ([CODIGO_PROYECTO])
GO
ALTER TABLE [dbo].[COSTO_PROYECTOS] CHECK CONSTRAINT [FK_COSTO_PROYECTOS_PROYECTOS]
GO
ALTER TABLE [dbo].[PAGOS]  WITH CHECK ADD  CONSTRAINT [FK_PAGOS_EMPLEADOS] FOREIGN KEY([FK_CODIGO_EMPLEADO])
REFERENCES [dbo].[EMPLEADOS] ([CODIGO_EMPLEADO])
GO
ALTER TABLE [dbo].[PAGOS] CHECK CONSTRAINT [FK_PAGOS_EMPLEADOS]
GO
ALTER TABLE [dbo].[PROYECTOS]  WITH CHECK ADD  CONSTRAINT [FK_PROYECTOS_CATEGORIA] FOREIGN KEY([FK_CODIGO_CATEGORIA])
REFERENCES [dbo].[CATEGORIA_PROYECTOS] ([CODIGO_CATEGORIA])
GO
ALTER TABLE [dbo].[PROYECTOS] CHECK CONSTRAINT [FK_PROYECTOS_CATEGORIA]
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarUsuario]    Script Date: 20/10/2023 0:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarUsuario](
    @Usuario VARCHAR(100),
    @Contrasena VARCHAR(500),
    @Registrado BIT OUTPUT,
    @Mensaje VARCHAR(100) OUTPUT
)
AS
BEGIN
    IF (NOT EXISTS (SELECT * FROM USUARIOS WHERE NOMBRE_USUARIO = @Usuario))
    BEGIN
        INSERT INTO USUARIOS(NOMBRE_USUARIO, CLAVE) VALUES (@Usuario, @Contrasena);
        SET @Registrado = 1;
        SET @Mensaje = 'Usuario Ingresado';
    END
    ELSE
    BEGIN
        SET @Registrado = 0;
        SET @Mensaje = 'Nombre de Usuario ya existe';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ValidarUsuario]    Script Date: 20/10/2023 0:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ValidarUsuario](
    @Usuario VARCHAR(100),
    @Contrasena VARCHAR(500)
)
AS
BEGIN
    IF (EXISTS (SELECT * FROM USUARIOS WHERE NOMBRE_USUARIO = @Usuario AND CLAVE = @Contrasena))
    BEGIN
        SELECT ID_USUARIO FROM USUARIOS WHERE NOMBRE_USUARIO = @Usuario AND CLAVE = @Contrasena;
    END
    ELSE
    BEGIN
        SELECT '0' AS Resultado;
    END
END;
GO

--**************INICIO CRUD EMPLEADOS**************--
--INSERTAR--

CREATE PROCEDURE sp_InsertaEmpleados
@codigo_empleado varchar(25) = null,
@cedula int = null,
@nombre_empleado varchar(400) = null,
@telefono int = null,
@correo varchar(400) = null,
@direccion varchar(400) = null,
@comentario varchar(500) = null
AS
BEGIN
    -- Validar y asignar valor predeterminado a CEDULA y TELEFONO si son nulos
    IF @cedula IS NULL
    BEGIN
        SET @cedula = 0;
    END

    IF @telefono IS NULL
    BEGIN
        SET @telefono = 0;
    END

    INSERT INTO EMPLEADOS(CODIGO_EMPLEADO, CEDULA, NOMBRE_EMPLEADO, TELEFONO, CORREO, DIRECCION, COMENTARIO)
    VALUES (@codigo_empleado, @cedula, @nombre_empleado, @telefono, @correo, @direccion, @comentario)
END

exec sp_InsertaEmpleados 'pero12323',44564,'Ana Cervantes Cruz', null, 'anaprogra1@gmail.com','Alajuela, San Ramon',''

DROP PROCEDURE sp_InsertaEmpleados
select * from EMPLEADOS
--FIN INSERTAR--

--ACTUALIZAR--

create procedure [dbo].[sp_ActualizaEmpleados]
@codigo_empleado varchar(25) null,
@cedula int null,
@nombre_empleado varchar(400) null,
@telefono int null,
@correo varchar(400) null,
@direccion varchar(400) null,
@comentario varchar(500) null
AS
BEGIN
    UPDATE EMPLEADOS 
    SET
        CEDULA = ISNULL(@cedula, CEDULA),
        NOMBRE_EMPLEADO = ISNULL(@nombre_empleado, NOMBRE_EMPLEADO),
        TELEFONO = ISNULL(@telefono, TELEFONO),
        CORREO = ISNULL(@correo, CORREO),
        DIRECCION = ISNULL(@direccion, DIRECCION),
        COMENTARIO = ISNULL(@comentario, COMENTARIO)
    WHERE
        CODIGO_EMPLEADO = @codigo_empleado;
END

exec sp_ActualizaEmpleados 'pero12323',115230168,'ANA LAURA CERVANTES CRUZ ',5859856,'ana.laura01@gmail.com','Alajuela San Ramon','Habilidad en curvas de nivel y catastro'

--FIN ACTUALIZAR--

--CONSUTAR--
create procedure [dbo].[sp_RetornaEmpleados]
as
Begin
	select * from EMPLEADOS
End

exec sp_RetornaEmpleados
--FIN CONSULTAR

--CONSULTAR POR ID
Create procedure [dbo].[sp_RetornaEmpleadosPorID]
@codigo_empleado varchar(25)
as
Begin
	select * from EMPLEADOS where CODIGO_EMPLEADO = @codigo_empleado
End

exec sp_RetornaEmpleadosPorID 'di14'
--FIN CONSULTAR POR ID

--ELIMINAR EMPLEADO SENCILLO Y FORANEO--

CREATE PROCEDURE [dbo].[sp_EliminaEmpleados]
@codigo_empleado VARCHAR(25)

AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Elimina registros de la tabla PAGOS relacionados con el empleado
        DELETE FROM PAGOS WHERE FK_CODIGO_EMPLEADO = @codigo_empleado;

        -- Luego, elimina el registro del empleado
        DELETE FROM EMPLEADOS WHERE CODIGO_EMPLEADO = @codigo_empleado;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;

        -- Aquí puedes manejar el error y enviar un mensaje personalizado
END CATCH
END

exec sp_EliminaEmpleados 'pero12323'


--FIN ELIMINAR--
select * from EMPLEADOS
select * from PAGOS

--**************FIN CRUD EMPLEADOS**************--


--**************INICIO CRUD ACTIVOS**************--


--CONSUTAR--
create procedure [dbo].[sp_RetornaActivos]
as
Begin
	select * from ACTIVOS
End

exec sp_RetornaActivos
--FIN CONSULTAR

--CONSULTAR POR ID
Create procedure [dbo].[sp_RetornaActivosPorID]
@codigo_activo varchar(25)
as
Begin
	select * from ACTIVOS where CODIGO_ACTIVO = @codigo_activo
End

exec sp_RetornaActivosPorID 'di14'
--FIN CONSULTAR POR ID

--ELIMINAR ACTIVO--
Create procedure [dbo].[sp_EliminaActivos]
@codigo_activo varchar(25)
as
Begin
	delete from ACTIVOS where CODIGO_ACTIVO = @codigo_activo
End


exec sp_EliminaActivos 'activo2'
--FIN ELIMINAR--


--INSERTAR--*******************************************

CREATE PROCEDURE sp_InsertaActivos
    @codigo_activo varchar(25) = null,
    @nombre_de_activo varchar(400) = null,
    @numero_de_serie varchar(100) = null,
    @FechaDeCompra date = null,
    @Proveedor varchar(100) = null,
    @Valor_de_compra decimal(18, 2) = 0,
    @Valor_actual decimal(18, 2) = 0,
    @comentario varchar(500) = null
AS
BEGIN
    -- Validar y asignar valor predeterminado a Valor_de_compra y Valor_actual si son nulos
    IF @Valor_de_compra IS NULL
    BEGIN
        SET @Valor_de_compra = 0;
    END

    IF @Valor_actual IS NULL
    BEGIN
        SET @Valor_actual = 0;
    END

    -- Insertar los datos en la tabla ACTIVOS
    INSERT INTO ACTIVOS (CODIGO_ACTIVO, NOMBRE_DE_ACTIVO, NUMERO_DE_SERIE, FECHA_DE_COMPRA, PROVEEDOR, VALOR_DE_COMPRA, VALOR_ACTUAL, COMENTARIO)
    VALUES (@codigo_activo, @nombre_de_activo, @numero_de_serie, @FechaDeCompra, @Proveedor, @Valor_de_compra, @Valor_actual, @comentario)
END

EXEC sp_InsertaActivos 'activoNUEVO20', 'ESTACION LEICA', 'ABCD1235', '2020-11-01', 'ALTEA', NULL, NULL, 'Comentario INSERTADO'

EXEC sp_InsertaActivos 'activo21232', 'laptop', 'sere', '2023-11-02', 'Proveedor2', 56567567,567567, 56567567,567567, 'Comentario2'

EXEC sp_InsertaActivos 'activo1010', 'Impresora', 'sere', null , 'Proveedor2', NULL, NULL, 'Comentario2'

select * from ACTIVOS


--FIN INSERTAR-- 
--INICIO EDITAR--
CREATE PROCEDURE [dbo].[sp_EditarActivos]
    @CodigoActivo varchar(25) null,
    @Nombre_de_activo varchar(25) null,
    @Numero_de_serie varchar(100) null,
    @Fecha_de_compra date = null,
	@Proveedor varchar(100) null,
    @Valor_de_compra decimal(18, 2) = 0,  -- Valor predeterminado de 0
	@Valor_actual decimal(18, 2) = 0,  -- Valor predeterminado de 0
    @Comentario varchar(500) null
AS
BEGIN
    UPDATE ACTIVOS  
    SET
        CODIGO_ACTIVO = ISNULL(@CodigoActivo, CODIGO_ACTIVO),
        NOMBRE_DE_ACTIVO = ISNULL(@Nombre_de_activo, NOMBRE_DE_ACTIVO),
        NUMERO_DE_SERIE = ISNULL(@Numero_de_serie, NUMERO_DE_SERIE),
        FECHA_DE_COMPRA = @Fecha_de_compra,
        PROVEEDOR = ISNULL(@Proveedor, PROVEEDOR),
		VALOR_DE_COMPRA = ISNULL(@Valor_de_compra, 0),  -- Utiliza 0 si VALOR COMPRA es nulo
	    VALOR_ACTUAL = ISNULL(@Valor_actual, 0),  -- Utiliza 0 si VALOR ACTUAL es nulo
        COMENTARIO = ISNULL(@Comentario, COMENTARIO)
    WHERE
        CODIGO_ACTIVO = @CodigoActivo;
END

select * from ACTIVOS
exec sp_EditarActivos 'ER40','computadora editada','efr-editado',null,'extree tech',null,null,'valor de compra editado'


--FIN EDITAR--



--**************FIN CRUD ACTIVOS**************--


--**************INICIO CRUD PAGOS**************--

--INICIO INSERTAR--
CREATE PROCEDURE sp_InsertarPagoEmpleado
    @CodigoPago varchar(25),
    @FK_CodigoEmpleado varchar(25),
    @CuentaBancaria varchar(100),
    @DiasAPagar varchar(100),
    @Monto decimal(18, 2) = 0,
    @FechaDePago date,
    @Comentario varchar(500)
AS
BEGIN

-- Validar y asignar valor predeterminado a CEDULA y TELEFONO si son nulos
    IF @Monto IS NULL
    BEGIN
        SET @Monto = 0;
    END
    -- Insertar el pago en la tabla PAGOS
    INSERT INTO PAGOS (CODIGO_PAGO, FK_CODIGO_EMPLEADO, CUENTA_BANCARIA, DIAS_A_PAGAR, MONTO, FECHA_DE_PAGO, COMENTARIO)
    VALUES (@CodigoPago, @FK_CodigoEmpleado, @CuentaBancaria, @DiasAPagar, @Monto, @FechaDePago, @Comentario)
END


SELECT * FROM EMPLEADOS

EXEC sp_InsertarPagoEmpleado 'pagos9', 'pero12323', 'ds566', 'miércoles y jueves', NULL, '2023-11-15', 'Otro comentario'
EXEC sp_InsertarPagoEmpleado 'pago31', 'pero12323', 'ds567', 'viernes', 56567567.567567, '2023-11-20', 'Un comentario adicional'

exec sp_InsertarPagoEmpleado 'pago1','pero12323','ds565','lunes y martes',10.52,'10-10-2023', 'Erick lo hizo'

select * from PAGOS

exec sp_InsertarPagoEmpleado 'p8787','pero12323','ds565','lunes y martes',10.52,'19-02-2023', 'Erick lo hizo MIERCOLES'



--FIN INSERTAR--



--INICIO EDITAR--

CREATE PROCEDURE [dbo].[sp_EditarPagos]
    @CodigoPago varchar(25) null,
    @FK_CodigoEmpleado varchar(25) null,
    @CuentaBancaria varchar(100) null,
    @DiasAPagar varchar(100) null,
    @Monto decimal(18, 2) = 0,  -- Valor predeterminado de 0
    @FechaDePago date null,
    @Comentario varchar(500) null
AS
BEGIN
    UPDATE PAGOS 
    SET
        FK_CODIGO_EMPLEADO = ISNULL(@FK_CodigoEmpleado, FK_CODIGO_EMPLEADO),
        CUENTA_BANCARIA = ISNULL(@CuentaBancaria, CUENTA_BANCARIA),
        DIAS_A_PAGAR = ISNULL(@DiasAPagar, DIAS_A_PAGAR),
        MONTO = ISNULL(@Monto, 0),  -- Utiliza 0 si @Monto es nulo
        FECHA_DE_PAGO = @FechaDePago,
        COMENTARIO = ISNULL(@Comentario, COMENTARIO)
    WHERE
        CODIGO_PAGO = @CodigoPago;
END
drop procedure sp_EditarPagos
select * from empleados
select * from pagos
exec sp_EditarPagos 'pago31','pero12323','cr67','martes editado',1000,null,'pago editado'


--FIN EDITAR--


--CONSUTAR--
create procedure [dbo].[sp_RetornaPagos]
as
Begin
	select * from PAGOS
End

exec sp_RetornaPagos
--FIN CONSULTAR

--CONSULTAR POR ID
Create procedure [dbo].[sp_RetornaPagosPorID]
 @CodigoPago varchar(25)
as
Begin
	select * from PAGOS where CODIGO_PAGO = @CodigoPago
End

exec sp_RetornaPagosPorID 'pago1'
--FIN CONSULTAR POR ID

--ELIMINAR PAGO--
Create procedure [dbo].[sp_EliminaPagos]
@CodigoPago varchar(25)
as
Begin
	delete from PAGOS where CODIGO_PAGO = @CodigoPago
End


exec sp_EliminaPagos '3e4'
select * from PAGOS
select * from EMPLEADOS
--FIN ELIMINAR--


--**************FIN CRUD PAGOS**************--

--**************INICIO CRUD CATEGORIA DE PROYECTOS**************--
--INSERTAR--

CREATE PROCEDURE sp_InsertaCategoria_Proyectos
@codigo_categoria varchar(25) = null,
@nombre_categoria varchar(400) = null,
@comentario varchar(500) = null
AS
BEGIN

    INSERT INTO CATEGORIA_PROYECTOS(CODIGO_CATEGORIA, NOMBRE_CATEGORIA,COMENTARIO)
    VALUES (@codigo_categoria,@nombre_categoria, @comentario)
END

exec sp_InsertaCategoria_Proyectos 'REPLA','REPLANTEO','REPLANTEO DE PLANOS'

DROP PROCEDURE sp_InsertaEmpleados
SELECT * FROM CATEGORIA_PROYECTOS

--FIN INSERTAR--

--ACTUALIZAR--

create procedure [dbo].[sp_EditarCategoria_Proyectos]
@codigo_categoria varchar(25) = null,
@nombre_categoria varchar(400) = null,
@comentario varchar(500) = null
AS
BEGIN
    UPDATE CATEGORIA_PROYECTOS
    SET
        CODIGO_CATEGORIA = ISNULL(@codigo_categoria, CODIGO_CATEGORIA),
        NOMBRE_CATEGORIA = ISNULL(@nombre_categoria, NOMBRE_CATEGORIA),
        COMENTARIO = ISNULL(@comentario, COMENTARIO)
    WHERE
        CODIGO_CATEGORIA = @codigo_categoria;
END

exec sp_EditarCategoria_Proyectos 'REPLA','REPLANTEO','REPLANTEO DE PLANOS EDITADO'

--FIN ACTUALIZAR--

--CONSUTAR--
create procedure [dbo].[sp_RetornaCategoria_Proyectos]
as
Begin
	select * from CATEGORIA_PROYECTOS
End

exec sp_RetornaCategoria_Proyectos
--FIN CONSULTAR

--CONSULTAR POR ID
Create procedure [dbo].[sp_RetornaCategoria_ProyectosPorID]
@codigo_categoria varchar(25)
as
Begin
	select * from CATEGORIA_PROYECTOS where CODIGO_CATEGORIA = @codigo_categoria
End

exec sp_RetornaCategoria_ProyectosPorID 'REPLA'
--FIN CONSULTAR POR ID

--ELIMINAR EMPLEADO SENCILLO Y FORANEO--


CREATE PROCEDURE [dbo].[sp_EliminaCategoria_Proyectos]
    @codigo_categoria VARCHAR(25)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Elimina registros de la tabla COSTO_PROYECTOS relacionados con la CATEGORIA DE PROYECTOS
        DELETE FROM COSTO_PROYECTOS WHERE FK_CODIGO_CATEGORIA = @codigo_categoria;

        -- Elimina registros de la tabla PROYECTOS relacionados con la CATEGORIA DE PROYECTOS
        DELETE FROM PROYECTOS WHERE FK_CODIGO_CATEGORIA = @codigo_categoria;

        -- Luego, elimina el registro de la CATEGORIA DE PROYECTOS
        DELETE FROM CATEGORIA_PROYECTOS WHERE CODIGO_CATEGORIA = @codigo_categoria;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;

        -- Aquí puedes manejar el error y enviar un mensaje personalizado
    END CATCH
END;


exec sp_EliminaCategoria_Proyectos 'CATA11'

--FIN ELIMINAR--
select * from CATEGORIA_PROYECTOS
select * from PROYECTOS
select * from COSTO_PROYECTOS



--**************FIN CRUD CATEGORIA DE PROYECTOS**************--

--**************INICIO CRUD PROYECTOS**************--

--INICIO INSERTAR--
CREATE PROCEDURE sp_InsertarProyecto
    @CodigoProyecto varchar(25),
	@NombreProyecto varchar(400),
    @FK_CodigoCategoria varchar(25),
    @FechaInicio date,
    @Estado varchar(100),
	@FechaFin date,
    @Precio decimal(18, 2) = 0,
    @Comentario varchar(500)
AS
BEGIN

-- Validar y asignar valor predeterminado a Precio si son nulos
    IF @Precio IS NULL
    BEGIN
        SET @Precio = 0;
    END
    -- Insertar el Proyecto en la tabla PROYECTOS
    INSERT INTO PROYECTOS(CODIGO_PROYECTO, NOMBRE_PROYECTO, FK_CODIGO_CATEGORIA, FECHA_INICIO, ESTADO,FECHA_FIN, PRECIO, COMENTARIO)
    VALUES (@CodigoProyecto, @NombreProyecto, @FK_CodigoCategoria, @FechaInicio, @Estado, @FechaFin, @Precio, @Comentario)
END

SELECT * FROM CATEGORIA_PROYECTOS

EXEC sp_InsertarProyecto'PRe', 'EL CANAL', 'CATA1', null, 'TERMINADO', null,1000, 'Otro comentario'
EXEC sp_InsertarPagoEmpleado 'pago31', '12345', 'ds567', 'viernes', 25.75, '2023-11-20', 'Un comentario adicional'

exec sp_InsertarPagoEmpleado 'pago1','12345','ds565','lunes y martes',10.52,'10-10-2023', 'Erick lo hizo'

select * from PROYECTOS

select * from EMPLEADOS
select * from PAGOS

exec sp_InsertarPagoEmpleado 'p8787','757575','ds565','lunes y martes',10.52,'19-02-2023', 'Erick lo hizo MIERCOLES'



--FIN INSERTAR--



--INICIO EDITAR HAY QUE TERMINARLO--

CREATE PROCEDURE [dbo].[sp_EditarProyectos]
    @CodigoProyecto varchar(25) = NULL,
    @NombreProyecto varchar(400) = NULL,
    @FK_CodigoCategoria varchar(25) = NULL,
    @FechaInicio date = NULL,
    @Estado varchar(100) = NULL,
    @FechaFin date = NULL,
    @Precio decimal(18, 2) = 0,
    @Comentario varchar(500) = NULL
AS
BEGIN
    UPDATE PROYECTOS 
    SET
        CODIGO_PROYECTO = ISNULL(@CodigoProyecto, CODIGO_PROYECTO),
        NOMBRE_PROYECTO = ISNULL(@NombreProyecto, NOMBRE_PROYECTO),
        FK_CODIGO_CATEGORIA = ISNULL(@FK_CodigoCategoria, FK_CODIGO_CATEGORIA),
        FECHA_INICIO = @FechaInicio,
        ESTADO = ISNULL(@Estado, ESTADO),
        FECHA_FIN = @FechaFin,
        PRECIO = ISNULL(@Precio, 0),
        COMENTARIO = ISNULL(@Comentario, COMENTARIO)
    WHERE
        CODIGO_PROYECTO = @CodigoProyecto;
END

drop procedure sp_EditarProyectos
select * from CATEGORIA_PROYECTOS
select * from PROYECTOS
exec sp_EditarProyectos '080980','EL CANAL EDITADO CON MONTO','56t',null,'EN PROCESO TERMINADO ',null,1445.45,'editar foranea'
exec sp_EditarProyectos '1111352', 'EL CANAL EDITADO CON MONTO', 'CURVA', null, 'EN PROCESO TERMINADO ', null, 1445.45, 'PROYECTO editado CON MONTO'

 
--FIN EDITAR--



--CONSUTAR--
create procedure [dbo].[sp_RetornaProyectos]
as
Begin
	select * from PROYECTOS
End

exec sp_RetornaProyectos
--FIN CONSULTAR

--CONSULTAR POR ID
Create procedure [dbo].[sp_RetornaProyectosPorID]
@CodigoProyecto varchar(25)
as
Begin
	select * from PROYECTOS where CODIGO_PROYECTO = @CodigoProyecto
End

exec sp_RetornaProyectosPorID '1111352'
--FIN CONSULTAR POR ID

--ELIMINAR PROYECTOS--


CREATE PROCEDURE [dbo].[sp_EliminaProyectos]
@CodigoProyecto VARCHAR(25)

AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Elimina registros de la tabla PAGOS relacionados con el empleado
        DELETE FROM COSTO_PROYECTOS WHERE FK_CODIGO_PROYECTO = @CodigoProyecto;

        -- Luego, elimina el registro del empleado
        DELETE FROM PROYECTOS WHERE CODIGO_PROYECTO = @CodigoProyecto;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;

        -- Aquí puedes manejar el error y enviar un mensaje personalizado
END CATCH
END
exec sp_EliminaProyectos 'proye13'
DROP PROCEDURE [sp_EliminaProyectos]



select * from CATEGORIA_PROYECTOS
select * from PROYECTOS
select * from ACTIVOS
--FIN ELIMINAR--


--**************FIN CRUD PROYECTOS**************--

--**************INICIO CRUD COSTO PROYECTOS**************--
--INICIO INSERTAR--
CREATE PROCEDURE sp_InsertarCostoProyecto
    @CodigoCostoProyecto varchar(25),
    @FK_CodigoProyecto varchar(25),
    @FK_CodigoCategoria varchar(25),
    @DiaTrabajador decimal(18, 2) = 0,
    @Combustible decimal(18, 2) = 0,
    @Viaticos decimal(18, 2) = 0,
    @Hospedaje decimal(18, 2) = 0,
    @TimbresCertificaciones decimal(18, 2) = 0,
    @Materiales decimal(18, 2) = 0,
    @Comentario varchar(500) = NULL
AS
BEGIN
    -- Validar y asignar valores predeterminados a campos decimales si son nulos
    SET @DiaTrabajador = ISNULL(@DiaTrabajador, 0);
    SET @Combustible = ISNULL(@Combustible, 0);
    SET @Viaticos = ISNULL(@Viaticos, 0);
    SET @Hospedaje = ISNULL(@Hospedaje, 0);
    SET @TimbresCertificaciones = ISNULL(@TimbresCertificaciones, 0);
    SET @Materiales = ISNULL(@Materiales, 0);

    -- Calcular la suma total de los campos decimales
    DECLARE @Total decimal(35, 2);
    SET @Total = @DiaTrabajador + @Combustible + @Viaticos + @Hospedaje + @TimbresCertificaciones + @Materiales;

    -- Insertar en la tabla COSTO_PROYECTOS
    INSERT INTO COSTO_PROYECTOS (
        CODIGO_COSTO_PROYECTO, 
        FK_CODIGO_PROYECTO, 
        FK_CODIGO_CATEGORIA, 
        DIA_TRABAJADOR, 
        COMBUSTIBLE, 
        VIATICOS, 
        HOSPEDAJE, 
        TIMBRES_CERTIFICACIONES, 
        MATERIALES, 
        TOTAL, 
        COMENTARIO
    )
    VALUES (
        @CodigoCostoProyecto, 
        @FK_CodigoProyecto, 
        @FK_CodigoCategoria, 
        @DiaTrabajador, 
        @Combustible, 
        @Viaticos, 
        @Hospedaje, 
        @TimbresCertificaciones, 
        @Materiales, 
        @Total, 
        @Comentario
    );
END

select * from CATEGORIA_PROYECTOS 
select * from PROYECTOS         
select * from COSTO_PROYECTOS

exec sp_InsertarCostoProyecto 'costo5','080980','56t',50.3,50,null,null,null,null,'PROYECTO'
--FIN INSERTAR--
--************************************************--

--INICIO EDITAR COSTO PROYECTOS--
CREATE PROCEDURE sp_EditarCostoProyecto
    @CodigoCostoProyecto varchar(25),
    @FK_CodigoProyecto varchar(25),
    @FK_CodigoCategoria varchar(25),
    @DiaTrabajador decimal(18, 2) = 0,
    @Combustible decimal(18, 2) = 0,
    @Viaticos decimal(18, 2) = 0,
    @Hospedaje decimal(18, 2) = 0,
    @TimbresCertificaciones decimal(18, 2) = 0,
    @Materiales decimal(18, 2) = 0,
    @Comentario varchar(500) = NULL
AS
BEGIN
    -- Validar y asignar valores predeterminados a campos decimales si son nulos
    SET @DiaTrabajador = ISNULL(@DiaTrabajador, 0);
    SET @Combustible = ISNULL(@Combustible, 0);
    SET @Viaticos = ISNULL(@Viaticos, 0);
    SET @Hospedaje = ISNULL(@Hospedaje, 0);
    SET @TimbresCertificaciones = ISNULL(@TimbresCertificaciones, 0);
    SET @Materiales = ISNULL(@Materiales, 0);

    -- Calcular la suma total de los campos decimales
    DECLARE @Total decimal(35, 2);
    SET @Total = @DiaTrabajador + @Combustible + @Viaticos + @Hospedaje + @TimbresCertificaciones + @Materiales;

    -- Actualizar los datos en la tabla COSTO_PROYECTOS
    UPDATE COSTO_PROYECTOS
    SET 
        FK_CODIGO_PROYECTO = @FK_CodigoProyecto,
        FK_CODIGO_CATEGORIA = @FK_CodigoCategoria,
        DIA_TRABAJADOR = @DiaTrabajador,
        COMBUSTIBLE = @Combustible,
        VIATICOS = @Viaticos,
        HOSPEDAJE = @Hospedaje,
        TIMBRES_CERTIFICACIONES = @TimbresCertificaciones,
        MATERIALES = @Materiales,
        TOTAL = @Total,
        COMENTARIO = @Comentario
    WHERE CODIGO_COSTO_PROYECTO = @CodigoCostoProyecto;
END


select * from CATEGORIA_PROYECTOS 
select * from PROYECTOS         
select * from COSTO_PROYECTOS

exec sp_EditarCostoProyecto 'costo5','080980','56t',50500,2562.3,25.3,6785.33,8,15,'COSTO EDITADO'



--FIN EDITAR COSTO PROYECTOS--

--INICIO ELIMINAR COSTO PROYECTOS--
Create procedure [dbo].[sp_EliminaCostoProyectos]
@CodigoCostoProyecto varchar(25)
as
Begin
	delete from COSTO_PROYECTOS where CODIGO_COSTO_PROYECTO = @CodigoCostoProyecto
End

exec sp_EliminaCostoProyectos 'costo5'
--FIN ELIMINAR COSTO PROYECTOS--

--CONSUTAR--
create procedure [dbo].[sp_RetornaCostoProyectos]
as
Begin
	select * from COSTO_PROYECTOS
End

exec sp_RetornaCostoProyectos
--FIN CONSULTAR

--CONSULTAR POR ID
Create procedure [dbo].[sp_RetornaCostoProyectosPorID]
@CodigoCostoProyecto varchar(25)
as
Begin
	select * from COSTO_PROYECTOS where CODIGO_COSTO_PROYECTO = @CodigoCostoProyecto
End

exec sp_RetornaCostoProyectosPorID 'costo2'


SELECT * FROM PROYECTOS WHERE CODIGO_PROYECTO = '080980';

