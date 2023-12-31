﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DISETOP.Models
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using System.Data.Entity.Core.Objects;
using System.Linq;


public partial class DISETOPEntities : DbContext
{
    public DISETOPEntities()
        : base("name=DISETOPEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<ACTIVO> ACTIVOS { get; set; }

    public virtual DbSet<CATEGORIA_PROYECTOS> CATEGORIA_PROYECTOS { get; set; }

    public virtual DbSet<COSTO_PROYECTOS> COSTO_PROYECTOS { get; set; }

    public virtual DbSet<EMPLEADO> EMPLEADOS { get; set; }

    public virtual DbSet<PAGO> PAGOS { get; set; }

    public virtual DbSet<PROYECTO> PROYECTOS { get; set; }

    public virtual DbSet<USUARIO> USUARIOS { get; set; }


    public virtual int sp_ActualizaEmpleados(string codigo_empleado, Nullable<int> cedula, string nombre_empleado, Nullable<int> telefono, string correo, string direccion, string comentario)
    {

        var codigo_empleadoParameter = codigo_empleado != null ?
            new ObjectParameter("codigo_empleado", codigo_empleado) :
            new ObjectParameter("codigo_empleado", typeof(string));


        var cedulaParameter = cedula.HasValue ?
            new ObjectParameter("cedula", cedula) :
            new ObjectParameter("cedula", typeof(int));


        var nombre_empleadoParameter = nombre_empleado != null ?
            new ObjectParameter("nombre_empleado", nombre_empleado) :
            new ObjectParameter("nombre_empleado", typeof(string));


        var telefonoParameter = telefono.HasValue ?
            new ObjectParameter("telefono", telefono) :
            new ObjectParameter("telefono", typeof(int));


        var correoParameter = correo != null ?
            new ObjectParameter("correo", correo) :
            new ObjectParameter("correo", typeof(string));


        var direccionParameter = direccion != null ?
            new ObjectParameter("direccion", direccion) :
            new ObjectParameter("direccion", typeof(string));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("comentario", comentario) :
            new ObjectParameter("comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ActualizaEmpleados", codigo_empleadoParameter, cedulaParameter, nombre_empleadoParameter, telefonoParameter, correoParameter, direccionParameter, comentarioParameter);
    }


    public virtual int sp_EliminaEmpleados(string codigo_empleado)
    {

        var codigo_empleadoParameter = codigo_empleado != null ?
            new ObjectParameter("codigo_empleado", codigo_empleado) :
            new ObjectParameter("codigo_empleado", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EliminaEmpleados", codigo_empleadoParameter);
    }


    public virtual int sp_InsertaEmpleados(string codigo_empleado, Nullable<int> cedula, string nombre_empleado, Nullable<int> telefono, string correo, string direccion, string comentario)
    {

        var codigo_empleadoParameter = codigo_empleado != null ?
            new ObjectParameter("codigo_empleado", codigo_empleado) :
            new ObjectParameter("codigo_empleado", typeof(string));


        var cedulaParameter = cedula.HasValue ?
            new ObjectParameter("cedula", cedula) :
            new ObjectParameter("cedula", typeof(int));


        var nombre_empleadoParameter = nombre_empleado != null ?
            new ObjectParameter("nombre_empleado", nombre_empleado) :
            new ObjectParameter("nombre_empleado", typeof(string));


        var telefonoParameter = telefono.HasValue ?
            new ObjectParameter("telefono", telefono) :
            new ObjectParameter("telefono", typeof(int));


        var correoParameter = correo != null ?
            new ObjectParameter("correo", correo) :
            new ObjectParameter("correo", typeof(string));


        var direccionParameter = direccion != null ?
            new ObjectParameter("direccion", direccion) :
            new ObjectParameter("direccion", typeof(string));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("comentario", comentario) :
            new ObjectParameter("comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertaEmpleados", codigo_empleadoParameter, cedulaParameter, nombre_empleadoParameter, telefonoParameter, correoParameter, direccionParameter, comentarioParameter);
    }


    public virtual ObjectResult<sp_RetornaEmpleados_Result> sp_RetornaEmpleados()
    {

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaEmpleados_Result>("sp_RetornaEmpleados");
    }


    public virtual ObjectResult<sp_RetornaEmpleadosPorID_Result> sp_RetornaEmpleadosPorID(string codigo_empleado)
    {

        var codigo_empleadoParameter = codigo_empleado != null ?
            new ObjectParameter("codigo_empleado", codigo_empleado) :
            new ObjectParameter("codigo_empleado", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaEmpleadosPorID_Result>("sp_RetornaEmpleadosPorID", codigo_empleadoParameter);
    }


    public virtual int sp_InsertaOactualizaEmpleado(string codigo_empleado, Nullable<int> cedula, string nombre_empleado, Nullable<int> telefono, string correo, string direccion, string comentario)
    {

        var codigo_empleadoParameter = codigo_empleado != null ?
            new ObjectParameter("codigo_empleado", codigo_empleado) :
            new ObjectParameter("codigo_empleado", typeof(string));


        var cedulaParameter = cedula.HasValue ?
            new ObjectParameter("cedula", cedula) :
            new ObjectParameter("cedula", typeof(int));


        var nombre_empleadoParameter = nombre_empleado != null ?
            new ObjectParameter("nombre_empleado", nombre_empleado) :
            new ObjectParameter("nombre_empleado", typeof(string));


        var telefonoParameter = telefono.HasValue ?
            new ObjectParameter("telefono", telefono) :
            new ObjectParameter("telefono", typeof(int));


        var correoParameter = correo != null ?
            new ObjectParameter("correo", correo) :
            new ObjectParameter("correo", typeof(string));


        var direccionParameter = direccion != null ?
            new ObjectParameter("direccion", direccion) :
            new ObjectParameter("direccion", typeof(string));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("comentario", comentario) :
            new ObjectParameter("comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertaOactualizaEmpleado", codigo_empleadoParameter, cedulaParameter, nombre_empleadoParameter, telefonoParameter, correoParameter, direccionParameter, comentarioParameter);
    }


    public virtual int sp_RegistrarUsuario(string usuario, string contrasena, ObjectParameter registrado, ObjectParameter mensaje)
    {

        var usuarioParameter = usuario != null ?
            new ObjectParameter("Usuario", usuario) :
            new ObjectParameter("Usuario", typeof(string));


        var contrasenaParameter = contrasena != null ?
            new ObjectParameter("Contrasena", contrasena) :
            new ObjectParameter("Contrasena", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_RegistrarUsuario", usuarioParameter, contrasenaParameter, registrado, mensaje);
    }


    public virtual ObjectResult<Nullable<int>> sp_ValidarUsuario(string usuario, string contrasena)
    {

        var usuarioParameter = usuario != null ?
            new ObjectParameter("Usuario", usuario) :
            new ObjectParameter("Usuario", typeof(string));


        var contrasenaParameter = contrasena != null ?
            new ObjectParameter("Contrasena", contrasena) :
            new ObjectParameter("Contrasena", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_ValidarUsuario", usuarioParameter, contrasenaParameter);
    }


    public virtual int sp_EditarPagos(string codigoPago, string fK_CodigoEmpleado, string cuentaBancaria, string diasAPagar, Nullable<decimal> monto, Nullable<System.DateTime> fechaDePago, string comentario)
    {

        var codigoPagoParameter = codigoPago != null ?
            new ObjectParameter("CodigoPago", codigoPago) :
            new ObjectParameter("CodigoPago", typeof(string));


        var fK_CodigoEmpleadoParameter = fK_CodigoEmpleado != null ?
            new ObjectParameter("FK_CodigoEmpleado", fK_CodigoEmpleado) :
            new ObjectParameter("FK_CodigoEmpleado", typeof(string));


        var cuentaBancariaParameter = cuentaBancaria != null ?
            new ObjectParameter("CuentaBancaria", cuentaBancaria) :
            new ObjectParameter("CuentaBancaria", typeof(string));


        var diasAPagarParameter = diasAPagar != null ?
            new ObjectParameter("DiasAPagar", diasAPagar) :
            new ObjectParameter("DiasAPagar", typeof(string));


        var montoParameter = monto.HasValue ?
            new ObjectParameter("Monto", monto) :
            new ObjectParameter("Monto", typeof(decimal));


        var fechaDePagoParameter = fechaDePago.HasValue ?
            new ObjectParameter("FechaDePago", fechaDePago) :
            new ObjectParameter("FechaDePago", typeof(System.DateTime));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("Comentario", comentario) :
            new ObjectParameter("Comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EditarPagos", codigoPagoParameter, fK_CodigoEmpleadoParameter, cuentaBancariaParameter, diasAPagarParameter, montoParameter, fechaDePagoParameter, comentarioParameter);
    }


    public virtual int sp_EliminaPagos(string codigoPago)
    {

        var codigoPagoParameter = codigoPago != null ?
            new ObjectParameter("CodigoPago", codigoPago) :
            new ObjectParameter("CodigoPago", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EliminaPagos", codigoPagoParameter);
    }


    public virtual int sp_InsertarPagoEmpleado(string codigoPago, string fK_CodigoEmpleado, string cuentaBancaria, string diasAPagar, Nullable<decimal> monto, Nullable<System.DateTime> fechaDePago, string comentario)
    {

        var codigoPagoParameter = codigoPago != null ?
            new ObjectParameter("CodigoPago", codigoPago) :
            new ObjectParameter("CodigoPago", typeof(string));


        var fK_CodigoEmpleadoParameter = fK_CodigoEmpleado != null ?
            new ObjectParameter("FK_CodigoEmpleado", fK_CodigoEmpleado) :
            new ObjectParameter("FK_CodigoEmpleado", typeof(string));


        var cuentaBancariaParameter = cuentaBancaria != null ?
            new ObjectParameter("CuentaBancaria", cuentaBancaria) :
            new ObjectParameter("CuentaBancaria", typeof(string));


        var diasAPagarParameter = diasAPagar != null ?
            new ObjectParameter("DiasAPagar", diasAPagar) :
            new ObjectParameter("DiasAPagar", typeof(string));


        var montoParameter = monto.HasValue ?
            new ObjectParameter("Monto", monto) :
            new ObjectParameter("Monto", typeof(decimal));


        var fechaDePagoParameter = fechaDePago.HasValue ?
            new ObjectParameter("FechaDePago", fechaDePago) :
            new ObjectParameter("FechaDePago", typeof(System.DateTime));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("Comentario", comentario) :
            new ObjectParameter("Comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertarPagoEmpleado", codigoPagoParameter, fK_CodigoEmpleadoParameter, cuentaBancariaParameter, diasAPagarParameter, montoParameter, fechaDePagoParameter, comentarioParameter);
    }


    public virtual ObjectResult<sp_RetornaPagos_Result> sp_RetornaPagos()
    {

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaPagos_Result>("sp_RetornaPagos");
    }


    public virtual ObjectResult<sp_RetornaPagosPorID_Result> sp_RetornaPagosPorID(string codigoPago)
    {

        var codigoPagoParameter = codigoPago != null ?
            new ObjectParameter("CodigoPago", codigoPago) :
            new ObjectParameter("CodigoPago", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaPagosPorID_Result>("sp_RetornaPagosPorID", codigoPagoParameter);
    }


    public virtual int sp_EditarActivos(string codigoActivo, string nombre_de_activo, string numero_de_serie, Nullable<System.DateTime> fecha_de_compra, string proveedor, Nullable<decimal> valor_de_compra, Nullable<decimal> valor_actual, string comentario)
    {

        var codigoActivoParameter = codigoActivo != null ?
            new ObjectParameter("CodigoActivo", codigoActivo) :
            new ObjectParameter("CodigoActivo", typeof(string));


        var nombre_de_activoParameter = nombre_de_activo != null ?
            new ObjectParameter("Nombre_de_activo", nombre_de_activo) :
            new ObjectParameter("Nombre_de_activo", typeof(string));


        var numero_de_serieParameter = numero_de_serie != null ?
            new ObjectParameter("Numero_de_serie", numero_de_serie) :
            new ObjectParameter("Numero_de_serie", typeof(string));


        var fecha_de_compraParameter = fecha_de_compra.HasValue ?
            new ObjectParameter("Fecha_de_compra", fecha_de_compra) :
            new ObjectParameter("Fecha_de_compra", typeof(System.DateTime));


        var proveedorParameter = proveedor != null ?
            new ObjectParameter("Proveedor", proveedor) :
            new ObjectParameter("Proveedor", typeof(string));


        var valor_de_compraParameter = valor_de_compra.HasValue ?
            new ObjectParameter("Valor_de_compra", valor_de_compra) :
            new ObjectParameter("Valor_de_compra", typeof(decimal));


        var valor_actualParameter = valor_actual.HasValue ?
            new ObjectParameter("Valor_actual", valor_actual) :
            new ObjectParameter("Valor_actual", typeof(decimal));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("Comentario", comentario) :
            new ObjectParameter("Comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EditarActivos", codigoActivoParameter, nombre_de_activoParameter, numero_de_serieParameter, fecha_de_compraParameter, proveedorParameter, valor_de_compraParameter, valor_actualParameter, comentarioParameter);
    }


    public virtual int sp_EliminaActivos(string codigo_activo)
    {

        var codigo_activoParameter = codigo_activo != null ?
            new ObjectParameter("codigo_activo", codigo_activo) :
            new ObjectParameter("codigo_activo", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EliminaActivos", codigo_activoParameter);
    }


    public virtual int sp_InsertaActivos(string codigo_activo, string nombre_de_activo, string numero_de_serie, Nullable<System.DateTime> fechaDeCompra, string proveedor, Nullable<decimal> valor_de_compra, Nullable<decimal> valor_actual, string comentario)
    {

        var codigo_activoParameter = codigo_activo != null ?
            new ObjectParameter("codigo_activo", codigo_activo) :
            new ObjectParameter("codigo_activo", typeof(string));


        var nombre_de_activoParameter = nombre_de_activo != null ?
            new ObjectParameter("nombre_de_activo", nombre_de_activo) :
            new ObjectParameter("nombre_de_activo", typeof(string));


        var numero_de_serieParameter = numero_de_serie != null ?
            new ObjectParameter("numero_de_serie", numero_de_serie) :
            new ObjectParameter("numero_de_serie", typeof(string));


        var fechaDeCompraParameter = fechaDeCompra.HasValue ?
            new ObjectParameter("FechaDeCompra", fechaDeCompra) :
            new ObjectParameter("FechaDeCompra", typeof(System.DateTime));


        var proveedorParameter = proveedor != null ?
            new ObjectParameter("Proveedor", proveedor) :
            new ObjectParameter("Proveedor", typeof(string));


        var valor_de_compraParameter = valor_de_compra.HasValue ?
            new ObjectParameter("Valor_de_compra", valor_de_compra) :
            new ObjectParameter("Valor_de_compra", typeof(decimal));


        var valor_actualParameter = valor_actual.HasValue ?
            new ObjectParameter("Valor_actual", valor_actual) :
            new ObjectParameter("Valor_actual", typeof(decimal));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("comentario", comentario) :
            new ObjectParameter("comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertaActivos", codigo_activoParameter, nombre_de_activoParameter, numero_de_serieParameter, fechaDeCompraParameter, proveedorParameter, valor_de_compraParameter, valor_actualParameter, comentarioParameter);
    }


    public virtual ObjectResult<sp_RetornaActivos_Result> sp_RetornaActivos()
    {

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaActivos_Result>("sp_RetornaActivos");
    }


    public virtual ObjectResult<sp_RetornaActivosPorID_Result> sp_RetornaActivosPorID(string codigo_activo)
    {

        var codigo_activoParameter = codigo_activo != null ?
            new ObjectParameter("codigo_activo", codigo_activo) :
            new ObjectParameter("codigo_activo", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaActivosPorID_Result>("sp_RetornaActivosPorID", codigo_activoParameter);
    }


    public virtual int sp_EditarCategoria_Proyectos(string codigo_categoria, string nombre_categoria, string comentario)
    {

        var codigo_categoriaParameter = codigo_categoria != null ?
            new ObjectParameter("codigo_categoria", codigo_categoria) :
            new ObjectParameter("codigo_categoria", typeof(string));


        var nombre_categoriaParameter = nombre_categoria != null ?
            new ObjectParameter("nombre_categoria", nombre_categoria) :
            new ObjectParameter("nombre_categoria", typeof(string));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("comentario", comentario) :
            new ObjectParameter("comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EditarCategoria_Proyectos", codigo_categoriaParameter, nombre_categoriaParameter, comentarioParameter);
    }


    public virtual int sp_EliminaCategoria_Proyectos(string codigo_categoria)
    {

        var codigo_categoriaParameter = codigo_categoria != null ?
            new ObjectParameter("codigo_categoria", codigo_categoria) :
            new ObjectParameter("codigo_categoria", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EliminaCategoria_Proyectos", codigo_categoriaParameter);
    }


    public virtual int sp_InsertaCategoria_Proyectos(string codigo_categoria, string nombre_categoria, string comentario)
    {

        var codigo_categoriaParameter = codigo_categoria != null ?
            new ObjectParameter("codigo_categoria", codigo_categoria) :
            new ObjectParameter("codigo_categoria", typeof(string));


        var nombre_categoriaParameter = nombre_categoria != null ?
            new ObjectParameter("nombre_categoria", nombre_categoria) :
            new ObjectParameter("nombre_categoria", typeof(string));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("comentario", comentario) :
            new ObjectParameter("comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertaCategoria_Proyectos", codigo_categoriaParameter, nombre_categoriaParameter, comentarioParameter);
    }


    public virtual ObjectResult<sp_RetornaCategoria_Proyectos_Result> sp_RetornaCategoria_Proyectos()
    {

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaCategoria_Proyectos_Result>("sp_RetornaCategoria_Proyectos");
    }


    public virtual ObjectResult<sp_RetornaCategoria_ProyectosPorID_Result> sp_RetornaCategoria_ProyectosPorID(string codigo_categoria)
    {

        var codigo_categoriaParameter = codigo_categoria != null ?
            new ObjectParameter("codigo_categoria", codigo_categoria) :
            new ObjectParameter("codigo_categoria", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaCategoria_ProyectosPorID_Result>("sp_RetornaCategoria_ProyectosPorID", codigo_categoriaParameter);
    }


    public virtual int sp_EditarProyectos(string codigoProyecto, string nombreProyecto, string fK_CodigoCategoria, Nullable<System.DateTime> fechaInicio, string estado, Nullable<System.DateTime> fechaFin, Nullable<decimal> precio, string comentario)
    {

        var codigoProyectoParameter = codigoProyecto != null ?
            new ObjectParameter("CodigoProyecto", codigoProyecto) :
            new ObjectParameter("CodigoProyecto", typeof(string));


        var nombreProyectoParameter = nombreProyecto != null ?
            new ObjectParameter("NombreProyecto", nombreProyecto) :
            new ObjectParameter("NombreProyecto", typeof(string));


        var fK_CodigoCategoriaParameter = fK_CodigoCategoria != null ?
            new ObjectParameter("FK_CodigoCategoria", fK_CodigoCategoria) :
            new ObjectParameter("FK_CodigoCategoria", typeof(string));


        var fechaInicioParameter = fechaInicio.HasValue ?
            new ObjectParameter("FechaInicio", fechaInicio) :
            new ObjectParameter("FechaInicio", typeof(System.DateTime));


        var estadoParameter = estado != null ?
            new ObjectParameter("Estado", estado) :
            new ObjectParameter("Estado", typeof(string));


        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("FechaFin", fechaFin) :
            new ObjectParameter("FechaFin", typeof(System.DateTime));


        var precioParameter = precio.HasValue ?
            new ObjectParameter("Precio", precio) :
            new ObjectParameter("Precio", typeof(decimal));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("Comentario", comentario) :
            new ObjectParameter("Comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EditarProyectos", codigoProyectoParameter, nombreProyectoParameter, fK_CodigoCategoriaParameter, fechaInicioParameter, estadoParameter, fechaFinParameter, precioParameter, comentarioParameter);
    }


    public virtual int sp_EliminaProyectos(string codigoProyecto)
    {

        var codigoProyectoParameter = codigoProyecto != null ?
            new ObjectParameter("CodigoProyecto", codigoProyecto) :
            new ObjectParameter("CodigoProyecto", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EliminaProyectos", codigoProyectoParameter);
    }


    public virtual int sp_InsertarProyecto(string codigoProyecto, string nombreProyecto, string fK_CodigoCategoria, Nullable<System.DateTime> fechaInicio, string estado, Nullable<System.DateTime> fechaFin, Nullable<decimal> precio, string comentario)
    {

        var codigoProyectoParameter = codigoProyecto != null ?
            new ObjectParameter("CodigoProyecto", codigoProyecto) :
            new ObjectParameter("CodigoProyecto", typeof(string));


        var nombreProyectoParameter = nombreProyecto != null ?
            new ObjectParameter("NombreProyecto", nombreProyecto) :
            new ObjectParameter("NombreProyecto", typeof(string));


        var fK_CodigoCategoriaParameter = fK_CodigoCategoria != null ?
            new ObjectParameter("FK_CodigoCategoria", fK_CodigoCategoria) :
            new ObjectParameter("FK_CodigoCategoria", typeof(string));


        var fechaInicioParameter = fechaInicio.HasValue ?
            new ObjectParameter("FechaInicio", fechaInicio) :
            new ObjectParameter("FechaInicio", typeof(System.DateTime));


        var estadoParameter = estado != null ?
            new ObjectParameter("Estado", estado) :
            new ObjectParameter("Estado", typeof(string));


        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("FechaFin", fechaFin) :
            new ObjectParameter("FechaFin", typeof(System.DateTime));


        var precioParameter = precio.HasValue ?
            new ObjectParameter("Precio", precio) :
            new ObjectParameter("Precio", typeof(decimal));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("Comentario", comentario) :
            new ObjectParameter("Comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertarProyecto", codigoProyectoParameter, nombreProyectoParameter, fK_CodigoCategoriaParameter, fechaInicioParameter, estadoParameter, fechaFinParameter, precioParameter, comentarioParameter);
    }


    public virtual ObjectResult<sp_RetornaProyectos_Result> sp_RetornaProyectos()
    {

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaProyectos_Result>("sp_RetornaProyectos");
    }


    public virtual ObjectResult<sp_RetornaProyectosPorID_Result> sp_RetornaProyectosPorID(string codigoProyecto)
    {

        var codigoProyectoParameter = codigoProyecto != null ?
            new ObjectParameter("CodigoProyecto", codigoProyecto) :
            new ObjectParameter("CodigoProyecto", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaProyectosPorID_Result>("sp_RetornaProyectosPorID", codigoProyectoParameter);
    }


    public virtual int sp_EditarCostoProyecto(string codigoCostoProyecto, string fK_CodigoProyecto, string fK_CodigoCategoria, Nullable<decimal> diaTrabajador, Nullable<decimal> combustible, Nullable<decimal> viaticos, Nullable<decimal> hospedaje, Nullable<decimal> timbresCertificaciones, Nullable<decimal> materiales, string comentario)
    {

        var codigoCostoProyectoParameter = codigoCostoProyecto != null ?
            new ObjectParameter("CodigoCostoProyecto", codigoCostoProyecto) :
            new ObjectParameter("CodigoCostoProyecto", typeof(string));


        var fK_CodigoProyectoParameter = fK_CodigoProyecto != null ?
            new ObjectParameter("FK_CodigoProyecto", fK_CodigoProyecto) :
            new ObjectParameter("FK_CodigoProyecto", typeof(string));


        var fK_CodigoCategoriaParameter = fK_CodigoCategoria != null ?
            new ObjectParameter("FK_CodigoCategoria", fK_CodigoCategoria) :
            new ObjectParameter("FK_CodigoCategoria", typeof(string));


        var diaTrabajadorParameter = diaTrabajador.HasValue ?
            new ObjectParameter("DiaTrabajador", diaTrabajador) :
            new ObjectParameter("DiaTrabajador", typeof(decimal));


        var combustibleParameter = combustible.HasValue ?
            new ObjectParameter("Combustible", combustible) :
            new ObjectParameter("Combustible", typeof(decimal));


        var viaticosParameter = viaticos.HasValue ?
            new ObjectParameter("Viaticos", viaticos) :
            new ObjectParameter("Viaticos", typeof(decimal));


        var hospedajeParameter = hospedaje.HasValue ?
            new ObjectParameter("Hospedaje", hospedaje) :
            new ObjectParameter("Hospedaje", typeof(decimal));


        var timbresCertificacionesParameter = timbresCertificaciones.HasValue ?
            new ObjectParameter("TimbresCertificaciones", timbresCertificaciones) :
            new ObjectParameter("TimbresCertificaciones", typeof(decimal));


        var materialesParameter = materiales.HasValue ?
            new ObjectParameter("Materiales", materiales) :
            new ObjectParameter("Materiales", typeof(decimal));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("Comentario", comentario) :
            new ObjectParameter("Comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EditarCostoProyecto", codigoCostoProyectoParameter, fK_CodigoProyectoParameter, fK_CodigoCategoriaParameter, diaTrabajadorParameter, combustibleParameter, viaticosParameter, hospedajeParameter, timbresCertificacionesParameter, materialesParameter, comentarioParameter);
    }


    public virtual int sp_EliminaCostoProyectos(string codigoCostoProyecto)
    {

        var codigoCostoProyectoParameter = codigoCostoProyecto != null ?
            new ObjectParameter("CodigoCostoProyecto", codigoCostoProyecto) :
            new ObjectParameter("CodigoCostoProyecto", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EliminaCostoProyectos", codigoCostoProyectoParameter);
    }


    public virtual int sp_EliminaEmpleados1(string codigo_empleado)
    {

        var codigo_empleadoParameter = codigo_empleado != null ?
            new ObjectParameter("codigo_empleado", codigo_empleado) :
            new ObjectParameter("codigo_empleado", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EliminaEmpleados1", codigo_empleadoParameter);
    }


    public virtual int sp_InsertarCostoProyecto(string codigoCostoProyecto, string fK_CodigoProyecto, string fK_CodigoCategoria, Nullable<decimal> diaTrabajador, Nullable<decimal> combustible, Nullable<decimal> viaticos, Nullable<decimal> hospedaje, Nullable<decimal> timbresCertificaciones, Nullable<decimal> materiales, string comentario)
    {

        var codigoCostoProyectoParameter = codigoCostoProyecto != null ?
            new ObjectParameter("CodigoCostoProyecto", codigoCostoProyecto) :
            new ObjectParameter("CodigoCostoProyecto", typeof(string));


        var fK_CodigoProyectoParameter = fK_CodigoProyecto != null ?
            new ObjectParameter("FK_CodigoProyecto", fK_CodigoProyecto) :
            new ObjectParameter("FK_CodigoProyecto", typeof(string));


        var fK_CodigoCategoriaParameter = fK_CodigoCategoria != null ?
            new ObjectParameter("FK_CodigoCategoria", fK_CodigoCategoria) :
            new ObjectParameter("FK_CodigoCategoria", typeof(string));


        var diaTrabajadorParameter = diaTrabajador.HasValue ?
            new ObjectParameter("DiaTrabajador", diaTrabajador) :
            new ObjectParameter("DiaTrabajador", typeof(decimal));


        var combustibleParameter = combustible.HasValue ?
            new ObjectParameter("Combustible", combustible) :
            new ObjectParameter("Combustible", typeof(decimal));


        var viaticosParameter = viaticos.HasValue ?
            new ObjectParameter("Viaticos", viaticos) :
            new ObjectParameter("Viaticos", typeof(decimal));


        var hospedajeParameter = hospedaje.HasValue ?
            new ObjectParameter("Hospedaje", hospedaje) :
            new ObjectParameter("Hospedaje", typeof(decimal));


        var timbresCertificacionesParameter = timbresCertificaciones.HasValue ?
            new ObjectParameter("TimbresCertificaciones", timbresCertificaciones) :
            new ObjectParameter("TimbresCertificaciones", typeof(decimal));


        var materialesParameter = materiales.HasValue ?
            new ObjectParameter("Materiales", materiales) :
            new ObjectParameter("Materiales", typeof(decimal));


        var comentarioParameter = comentario != null ?
            new ObjectParameter("Comentario", comentario) :
            new ObjectParameter("Comentario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertarCostoProyecto", codigoCostoProyectoParameter, fK_CodigoProyectoParameter, fK_CodigoCategoriaParameter, diaTrabajadorParameter, combustibleParameter, viaticosParameter, hospedajeParameter, timbresCertificacionesParameter, materialesParameter, comentarioParameter);
    }


    public virtual ObjectResult<sp_RetornaCostoProyectos_Result> sp_RetornaCostoProyectos()
    {

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaCostoProyectos_Result>("sp_RetornaCostoProyectos");
    }


    public virtual ObjectResult<sp_RetornaCostoProyectosPorID_Result> sp_RetornaCostoProyectosPorID(string codigoCostoProyecto)
    {

        var codigoCostoProyectoParameter = codigoCostoProyecto != null ?
            new ObjectParameter("CodigoCostoProyecto", codigoCostoProyecto) :
            new ObjectParameter("CodigoCostoProyecto", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaCostoProyectosPorID_Result>("sp_RetornaCostoProyectosPorID", codigoCostoProyectoParameter);
    }

}

}

