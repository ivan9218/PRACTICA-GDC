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
	[VALOR_DE_COMPRA] [decimal](18, 0) NULL,
	[VALOR_ACTUAL] [decimal](18, 0) NULL,
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
	[DIA_TRABAJADOR] [decimal](10, 2) NULL,
	[COMBUSTIBLE] [decimal](10, 2) NULL,
	[VIATICOS] [decimal](10, 2) NULL,
	[HOSPEDAJE] [decimal](10, 2) NULL,
	[TIMBRES_CERTIFICACIONES] [decimal](10, 2) NULL,
	[MATERIALES] [decimal](10, 2) NULL,
	[TOTAL] [decimal](10, 2) NULL,
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
	[CEDULA] [int] NOT NULL,
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
	[MONTO] [decimal](10, 2) NULL,
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
	[PRECIO] [decimal](10, 2) NULL,
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
