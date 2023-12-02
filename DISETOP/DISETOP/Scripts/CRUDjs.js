$(document).ready(function () {
   

    //ESTE ES EL JS QUE SE USA PARA LOS CRUD, EJECUTA LOS METODOS DE LOS CONTROLADORES

    //URL DE LOS CONTROLADORES PRIMERO VA EL CONTROLADOR Y DESPUES EL METODO 
    const tabla = $('#miTabla').DataTable();

    const url_empleado = '/Empleados/InsertarEmpleado'; // Define la URL para insertar empleados
    const url_actualizar_empleado = '/Empleados/EditarEmpleado'; // Define la URL para actualizar empleados
    const url_eliminar_empleado = '/Empleados/EliminarEmpleado'; // Define la URL para actualizar empleados
    
    //BOTON GUARDAR EMPLEADO
    $("#handler-project-crud").click(function () {
        // Aquí puedes agregar el código que se ejecutará cuando se haga clic en el botón
        $('#exampleModalCenter').modal('show')
    });

   //INICIO INSERTAR EMPLEADO 



    $("#btnGuardar").click(function () {
        // Guarda la info del emp
        const form = $('#form-empleado')


        // Obtén un objeto FormData a partir del formulario
        var formData = new FormData(document.getElementById("form-empleado"));

        // Convierte el objeto FormData a un array de pares clave-valor y luego a un objeto
        var formDataObject = Object.fromEntries(formData.entries());

        // Imprime el objeto con los valores en la consola
        console.log(formDataObject);
        $.ajax({
            url: url_empleado,
            method: "POST",
            data: formDataObject,
           
            success: function (result) {
                if (result.success) {
                    $('#exampleModalCenter').modal('hide'); // Cierra el modal
                    toastr.success("Empleado ingresado correctamente.");   
                    setTimeout(function () {
                        // Tu código que se ejecutará después del retraso de 500 ms
                        window.location.reload();
                    }, 500);
                } else {
                    // El muestra un mensaje de error
                    toastr.error(result.message);                    
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("Error al agregar el empleado: " + errorThrown);
            }
        });
    }); 

       //INICIO DE EDITAR EMPLEADO

       //BOTON EDITAR EMPLEADO
    $("#buttonEditar").click(function () {
      
        $('#editarModal').modal('show')
    });

    $('#miTabla').on('click', 'button.editar', function () {
        const row = $(this).closest('tr');
        const data = tabla.row(row).data();

        // Cargar datos en el modal editarModal
        $('#codigo_empleado_edit').val(data[0]);
        $('#cedula_edit').val(data[1]);
        $('#nombre_empleado_edit').val(data[2]);
        $('#telefono_edit').val(data[3]);
        $('#correo_edit').val(data[4]);
        $('#direccion_edit').val(data[5]);
        $('#comentario_edit').val(data[6]);

        $('#editarModal').modal('show');
        console.log('Editar fila con datos: ', data);
    });

    $("#btnEditar").click(function () {
        // Obtén los valores actualizados del formulario de edición
        const codigo_empleado = $('#codigo_empleado_edit').val();
        const cedula = $('#cedula_edit').val();
        const nombre_empleado = $('#nombre_empleado_edit').val();
        const telefono = $('#telefono_edit').val() || 0;
        const correo = $('#correo_edit').val();
        const direccion = $('#direccion_edit').val();
        const comentario = $('#comentario_edit').val();

    // Crea un objeto con los datos actualizados
    const empleadoData = {
        CODIGO_EMPLEADO: codigo_empleado,
        CEDULA: cedula,
        NOMBRE_EMPLEADO: nombre_empleado,
        TELEFONO: telefono,
        CORREO: correo,
        DIRECCION: direccion,
        COMENTARIO: comentario
    };

        // Realiza una solicitud AJAX para actualizar el empleado
        $.ajax({
            url: url_actualizar_empleado, 
            method: "POST",
            data: empleadoData,
            success: function (result) {
                if (result.success) {
                    // Actualización exitosa, cierra el modal
                    $('#editarModal').modal('hide');
                    // Recarga la tabla 
                    
                    toastr.success("Cambios ingresados correctamente.");  
                } else {
                    // Muestra un mensaje de error si la actualización falla
                    toastr.error(result.message);
                }
            }
        });
    });
    // FIN DE EDITAR EMPLEADO


     //INICIO ELIMINAR EN GRID
    // Agrega un evento para mostrar un cuadro de diálogo de confirmación
    $('#miTabla').on('click', 'button#buttonEliminar', function () {
        const row = $(this).closest('tr');
        const codigoEmpleado = row.find('td:first').text(); // Obtiene el código del empleado

        // Muestra un cuadro de diálogo de confirmación
        if (confirm('¿Está seguro de que desea eliminar el empleado? Esto también eliminará los registros de pago de este empleado.')) {
            // Si el usuario hace clic en "Aceptar" en el cuadro de diálogo
            // Realiza una solicitud AJAX para eliminar el registro en la base de datos
            $.ajax({
                type: 'POST', // Método HTTP POST
                url: url_eliminar_empleado, // URL de la acción del controlador para eliminar empleado
                data: { codigoEmpleado: codigoEmpleado }, // Parámetro para enviar el código del empleado
                success: function (data) {
                    // Elimina la fila de la tabla
                    tabla.row(row).remove().draw();
                    // Muestra un mensaje de éxito
                    toastr.success('Empleado eliminado correctamente');
                },
                error: function () {
                    // Maneja los errores si es necesario
                    toastr.error('No se pudo eliminar el empleado');
                }
            });
        } else {
            // Si el usuario hace clic en "Cancelar" en el cuadro de diálogo
            toastr.error('No se eliminó el empleado');
        }
    });


    //FIN ELIMINAR EN GRID




});





