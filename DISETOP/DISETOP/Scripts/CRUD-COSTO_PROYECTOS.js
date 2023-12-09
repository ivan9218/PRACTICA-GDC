"use strict";
$(document).ready(function () {

    //ESTE ES EL JS QUE SE USA PARA LOS CRUD, EJECUTA LOS METODOS DE LOS CONTROLADORES

    //URL DE LOS CONTROLADORES PRIMERO VA EL CONTROLADOR Y DESPUES EL METODO
    const tabla = $('#miTabla').DataTable();
    const url_actualizar_costoproyecto = '/Costo_Proyectos/EditarCostoProyecto'; // Define la URL para actualizar pagos
    const url_eliminar_costoproyecto = '/Costo_Proyectos/EliminarCostoProyecto'; // Define la URL para actualizar empleados


    // BOTON GUARDAR PROYECTO
    $("#handler-project-crud").click(function () {
        // Aquí puedes agregar el código que se ejecutará cuando se haga clic en el botón
        $('#exampleModalCenter').modal('show')
    });




    // Botón "Guardar Proyecto" en tu modal
    $("#btnGuardar").click(function () {
        var codigoCostoProyecto = $("#codigoCostoProyecto").val();
        // obtiene el valor seleccionado del select y el primary key 
        var codigoProyecto = $("#codigo_proyecto").val();
        var codigoCategoria = $("#codigo_categoria").val();

        // Obtiene los otros
        var DiaTrabajador = $("#DiaTrabajador").val();
        var Combustible = $("#Combustible").val();
        var Viaticos = $("#Viaticos").val();
        var Hospedaje = $("#Hospedaje").val();
        var TimbresCertificaciones = $("#TimbresCertificaciones").val();
        var Materiales = $("#Materiales").val();
        var comentario = $("#comentario").val();

        // Crea un objeto con los datos del pago
        var CostoProyecto = {
            CODIGO_COSTO_PROYECTO: codigoCostoProyecto,
            FK_CODIGO_PROYECTO: codigoProyecto,
            FK_CODIGO_CATEGORIA: codigoCategoria,
            DIA_TRABAJADOR: DiaTrabajador,
            COMBUSTIBLE: Combustible,
            VIATICOS: Viaticos,
            HOSPEDAJE: Hospedaje,
            TIMBRES_CERTIFICACIONES: TimbresCertificaciones,
            MATERIALES: Materiales,
            COMENTARIO: comentario

        };

        // Realiza una solicitud AJAX al controlador para insertar el proyecto
        $.ajax({
            url: '/Costo_Proyectos/InsertarCostoProyecto', // Asegúrate de que esta ruta sea la correcta
            method: "POST",
            data: CostoProyecto,
            success: function (result) {
                if (result.success) {
                    $('#exampleModalCenter').modal('hide'); // Cierra el modal

                    toastr.success("Costo de Proyecto ingresado correctamente.");
                    setTimeout(function () {
                        // Tu código que se ejecutará después del retraso de 500 ms
                        window.location.reload();
                    }, 500);
                } else {
                    // Muestra un mensaje de error si hay un problema
                    toastr.error(result.message);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("Error al agregar el costo del proyecto: " + errorThrown);
            }
        });
    });



    //INICIO DE EDITAR PROYECTO

    //BOTON EDITAR PROYECTO
    $("#buttonEditar").click(function () {

        $('#editarModal').modal('show')
    });

    $('#miTabla').on('click', 'button.editar', function () {
        const row = $(this).closest('tr');
        const data = tabla.row(row).data();

        // Cargar datos en el modal de edición
        $('#codigoCostoProyecto_edit').val(data[0]);
        $('#codigo_proyecto_edit').val(data[1]); //traerse el codigo para que lo muestre //OJO PONER ATENCION A LOS CAMPOS QUE OCUPAN 
        $('#codigo_categoria_edit').val(data[3]);// EL CAMPO 2 SE USA PARA EL NOMBRE DE LA CATEGORIA OSEA EL CAMPO QUE REQUIERE DESPUES DE CODIGO CATEGORIA
        $('#DiaTrabajador_edit').val(data[5]);
        $('#Combustible_edit').val(data[6]);
        $('#Viaticos_edit').val(data[7]);
        $('#Hospedaje_edit').val(data[8]);
        $('#TimbresCertificaciones_edit').val(data[9]);
        $('#Materiales_edit').val(data[10]);
        $('#comentario_edit').val(data[12]);

        $('#editarModal').modal('show');
        console.log('Editar fila con datos: ', data);
    });


    $("#btnEditar").click(function () {

        // Obtén los valores actualizados del formulario de edición
        const codigoCostoProyecto = $('#codigoCostoProyecto_edit').val();
        const codigoProyecto = $('#codigo_proyecto_edit').val();
        const codigoCategoria = $('#codigo_categoria_edit').val();
        const DiaTrabajador = $('#DiaTrabajador_edit').val();
        const Combustible = $('#Combustible_edit').val();
        const Viaticos = $('#Viaticos_edit').val();
        const Hospedaje = $('#Hospedaje_edit').val();
        const TimbresCertificaciones = $('#TimbresCertificaciones_edit').val();
        const Materiales = $('#Materiales_edit').val();

        const comentario = $('#comentario_edit').val();

        // Crea un objeto con los datos actualizados
        const CostoProyectoData = {
            CODIGO_COSTO_PROYECTO: codigoCostoProyecto,
            FK_CODIGO_PROYECTO: codigoProyecto,
            FK_CODIGO_CATEGORIA: codigoCategoria,
            DIA_TRABAJADOR: DiaTrabajador,
            COMBUSTIBLE: Combustible,
            VIATICOS: Viaticos,
            HOSPEDAJE: Hospedaje,
            TIMBRES_CERTIFICACIONES: TimbresCertificaciones,
            MATERIALES: Materiales,
            COMENTARIO: comentario
        };

       


        /*if(validate(pagoData)) {*/
        // Realiza una solicitud AJAX para actualizar el empleado
        $.ajax({
            url: url_actualizar_costoproyecto,
            method: "POST",
            data: CostoProyectoData,
            success: function (result) {
                if (result.success) {
                    // Actualización exitosa, cierra el modal
                    $('#editarModal').modal('hide');
                    // Recarga la tabla 
                    toastr.success("Cambios ingresados correctamente.");
                    setTimeout(function () {
                        // Tu código que se ejecutará después del retraso de 500 ms
                        window.location.reload();
                    }, 500);

                } else {
                    // Muestra un mensaje de error si la actualización falla
                    toastr.error(result.message)
                }
            }
        });
        /* }*/
    });
    // FIN DE EDITAR PAGO



    //INICIO ELIMINAR EN GRID
    // Agrega un evento para mostrar un cuadro de diálogo de confirmación
    $('#miTabla').on('click', 'button#buttonEliminar', function () {
        const row = $(this).closest('tr');
        const CodigoCostoProyecto = row.find('td:first').text(); // Obtiene el código del pago

        // Muestra un cuadro de diálogo de confirmación
        if (confirm('¿Está seguro de que desea eliminar el registro del Costo de proyecto?')) {
            // Si el usuario hace clic en "Aceptar" en el cuadro de diálogo
            // Realiza una solicitud AJAX para eliminar el registro en la base de datos
            $.ajax({
                type: 'POST', // Método HTTP POST
                url: url_eliminar_costoproyecto, // URL de la acción del controlador para eliminar pago
                data: { CodigoCostoProyecto: CodigoCostoProyecto }, // Parámetro para enviar el código del costo proyecto
                success: function (data) {
                    // Elimina la fila de la tabla
                    tabla.row(row).remove().draw();
                    // Muestra un mensaje de éxito
                    toastr.success("Costo de Proyecto eliminado correctamente")
                },
                error: function () {
                    
                    toastr.error("No se pudo eliminar el costo del proyecto")
                }
            });
        } else {
            // Si el usuario hace clic en "Cancelar" en el cuadro de diálogo
            toastr.error("No se eliminó el costo proyecto")

        }
    });


    //FIN ELIMINAR EN GRID




});
