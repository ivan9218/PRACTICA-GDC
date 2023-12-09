"use strict";
$(document).ready(function () {

    //ESTE ES EL JS QUE SE USA PARA LOS CRUD, EJECUTA LOS METODOS DE LOS CONTROLADORES

    //URL DE LOS CONTROLADORES PRIMERO VA EL CONTROLADOR Y DESPUES EL METODO
    const tabla = $('#miTabla').DataTable();
    const url_proyecto = '/Proyectos/InsertarProyecto'; // Define la URL para insertar empleados
    const url_actualizar_proyecto = '/Proyectos/EditarProyecto'; // Define la URL para actualizar pagos
    const url_eliminar_proyecto = '/Proyectos/EliminarProyecto'; // Define la URL para actualizar empleados


    // BOTON GUARDAR PROYECTO
    $("#handler-project-crud").click(function () {
        // Aquí puedes agregar el código que se ejecutará cuando se haga clic en el botón
        $('#exampleModalCenter').modal('show')
    });




    // Botón "Guardar Proyecto" en tu modal
    $("#btnGuardar").click(function () {
        var codigoProyecto = $("#codigo_proyecto").val();
        // obtiene el valor seleccionado del select y el primary key 
        var codigoCategoria = $("#codigo_categoria").val();

        // Obtiene los otros
        var nombreProyecto = $("#nombre_proyecto").val();
        var fechaInicio = $("#fechaDeInicio").val();
        var estado = $("#estado").val();
        var fechaFin = $("#fechaDeFin").val();
        var precio = $("#precio").val();
        var comentario = $("#comentario").val();

        // Crea un objeto con los datos del pago
        var proyecto = {
            CODIGO_PROYECTO: codigoProyecto,
            FK_CODIGO_CATEGORIA: codigoCategoria,
            NOMBRE_PROYECTO: nombreProyecto,
            FECHA_INICIO: fechaInicio,
            ESTADO: estado,
            FECHA_FIN: fechaFin,
            PRECIO:precio,
            COMENTARIO: comentario
  
        };
    
        // Realiza una solicitud AJAX al controlador para insertar el proyecto
        $.ajax({
            url: '/Proyectos/InsertarProyecto', // Asegúrate de que esta ruta sea la correcta
            method: "POST",
            data: proyecto,
            success: function (result) {
                if (result.success) {
                    $('#exampleModalCenter').modal('hide'); // Cierra el modal
                   
                    toastr.success("Proyecto ingresado correctamente.");
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
                toastr.error("Error al agregar el proyecto: " + errorThrown);
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
        $('#codigo_proyecto_edit').val(data[0]);
        $('#codigo_categoria_edit').val(data[2]); //traerse el codigo para que lo muestre //OJO PONER ATENCION A LOS CAMPOS QUE OCUPAN 
        $('#nombre_proyecto_edit').val(data[3]);// EL CAMPO 2 SE USA PARA EL NOMBRE DE LA CATEGORIA OSEA EL CAMPO QUE REQUIERE DESPUES DE CODIGO CATEGORIA
        const fecha = data[4]; // toma el campo fecha en el campo 5 se le asigna variable fecha 
        if (fecha) { //crea un 
            const [day, month, year] = fecha.split('/'); //split lo que hace es convertir un string en un array para poder dividir la fecha y formatearla aqui llega como yyyy/mm/dd
            $('#fechaDeInicio_edit').val(year + '-' + month + '-' + day);// en el html fechaDePago_edit osea en el picker cargue el ano el mes y el dia. 
        }
        $('#estado_edit').val(data[5]);
        const fecha2 = data[6]; // toma el campo fecha en el campo 5 se le asigna variable fecha 
        if (fecha2) { //crea un 
            const [day, month, year] = fecha2.split('/'); //split lo que hace es convertir un string en un array para poder dividir la fecha y formatearla aqui llega como yyyy/mm/dd
            $('#fechaDeFin_edit').val(year + '-' + month + '-' + day);// en el html fechaDePago_edit osea en el picker cargue el ano el mes y el dia. 
        }
        $('#precio_edit').val(data[7]);
        $('#comentario_edit').val(data[8]);

        $('#editarModal').modal('show');
        console.log('Editar fila con datos: ', data);
    });


    $("#btnEditar").click(function () {

        // Obtén los valores actualizados del formulario de edición
        const codigo_proyecto = $('#codigo_proyecto_edit').val();
        const codigo_categoria_edit = $('#codigo_categoria_edit').val();
        const nombre_proyecto = $('#nombre_proyecto_edit').val();
        const fechaDeInicio = $('#fechaDeInicio_edit').val();
        const estado = $('#estado_edit').val();
        const fechaDeFin = $('#fechaDeFin_edit').val();
        const precio = $('#precio_edit').val();
        const comentario = $('#comentario_edit').val();

        // Crea un objeto con los datos actualizados
        const proyectoData = {
            CODIGO_PROYECTO: codigo_proyecto,
            FK_CODIGO_CATEGORIA: codigo_categoria_edit,
            NOMBRE_PROYECTO: nombre_proyecto,
            FECHA_INICIO: fechaDeInicio,
            ESTADO: estado,
            FECHA_FIN: fechaDeFin,
            PRECIO: precio,
            COMENTARIO: comentario
        };

        /*if(validate(pagoData)) {*/
        // Realiza una solicitud AJAX para actualizar el empleado
        $.ajax({
            url: url_actualizar_proyecto,
            method: "POST",
            data: proyectoData,
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
        const CodigoProyecto = row.find('td:first').text(); // Obtiene el código del pago

        // Muestra un cuadro de diálogo de confirmación
        if (confirm('¿Está seguro de que desea eliminar el registro del proyecto? Esto también eliminará los registros de Costos del Proyecto.')) {
            // Si el usuario hace clic en "Aceptar" en el cuadro de diálogo
            // Realiza una solicitud AJAX para eliminar el registro en la base de datos
            $.ajax({
                type: 'POST', // Método HTTP POST
                url: url_eliminar_proyecto, // URL de la acción del controlador para eliminar pago
                data: { CodigoProyecto: CodigoProyecto }, // Parámetro para enviar el código del proyecto
                success: function (data) {
                    // Elimina la fila de la tabla
                    tabla.row(row).remove().draw();
                    // Muestra un mensaje de éxito
                    toastr.success("Proyecto eliminado correctamente")
                },
                error: function () {
                
                    toastr.error("No se pudo eliminar el proyecto")
                }
            });
        } else {
            // Si el usuario hace clic en "Cancelar" en el cuadro de diálogo
            toastr.error("No se eliminó el proyecto")

        }
    });


    //FIN ELIMINAR EN GRID




});
