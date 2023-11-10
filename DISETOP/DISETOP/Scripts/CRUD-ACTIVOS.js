$(document).ready(function () {


    //ESTE ES EL JS QUE SE USA PARA LOS CRUD, EJECUTA LOS METODOS DE LOS CONTROLADORES

    //URL DE LOS CONTROLADORES PRIMERO VA EL CONTROLADOR Y DESPUES EL METODO 
    const tabla = $('#miTabla').DataTable();

    const url_activo = '/Activos/InsertarActivo'; // Define la URL para insertar activos
    const url_actualizar_activo = '/Activos/EditarActivo'; // Define la URL para actualizar activos
    const url_eliminar_empleado = '/Empleados/EliminarEmpleado'; // Define la URL para actualizar empleados

    //BOTON GUARDAR ACTIVO
    $("#handler-project-crud").click(function () {
        // Aquí puedes agregar el código que se ejecutará cuando se haga clic en el botón
        $('#exampleModalCenter').modal('show')
    });

    //INICIO INSERTAR EMPLEADO 
    $("#btnGuardar").click(function () {
        // Guarda la info del emp
        const form = $('#form-activo')


        // Obtén un objeto FormData a partir del formulario
        var formData = new FormData(document.getElementById("form-activo"));

        // Convierte el objeto FormData a un array de pares clave-valor y luego a un objeto
        var formDataObject = Object.fromEntries(formData.entries());

        // Imprime el objeto con los valores en la consola
        console.log(formDataObject);
        $.ajax({
            url: url_activo,
            method: "POST",
            data: formDataObject,

            success: function (result) {
                if (result.success) {
                    window.location.reload()
                    alert("Activo ingresado correctamente.");
                } else {
                    // El muestra un mensaje de error
                    alert(result.message);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error al agregar el Activo: " + errorThrown);
            }
        });
    });

    //INICIO DE EDITAR ACTIVO

    //BOTON EDITAR ACTIVO
    $("#buttonEditar").click(function () {

        $('#editarModal').modal('show')
    });
    //INICIO CARGAR LOS DATOS ACTUALES EN EL MODAL FORM
    $('#miTabla').on('click', 'button.editar', function () {
        const row = $(this).closest('tr');
        const data = tabla.row(row).data();
        /*      debugger*/
        // Cargar datos en el modal de edición
        $('#codigo_activo_edit').val(data[0]);
        $('#nombre_de_activo_edit').val(data[1]); //traerse el codigo para que lo muestre
        $('#numero_de_serie_edit').val(data[2]);// EL CAMPO 2 SE USA PARA EL NOMBRE DEL EMPLEADO OSEA EL CAMPO QUE REQUIERE DESPUES DE CODIGO EMPLEADO
        const fecha = data[3]; // toma el campo fecha en el campo 5 se le asigna variable fecha 
        if (fecha) { //crea un 
            const [day, month, year] = fecha.split('/'); //split lo que hace es convertir un string en un array para poder dividir la fecha y formatearla aqui llega como yyyy/mm/dd
            $('#fechaDeCompra_edit').val(year + '-' + month + '-' + day);// en el html fechaDePago_edit osea en el picker cargue el ano el mes y el dia. 
        }
        $('#Proveedor_edit').val(data[4]);
        $('#Valor_de_compra_edit').val(data[5]);
        $('#Valor_actual_edit').val(data[6]);
        $('#comentario_edit').val(data[7]);
        $('#editarModal').modal('show');
        console.log('Editar fila con datos: ', data);
    });
     //INICIO CARGAR LOS DATOS ACTUALES EN EL MODAL FORM
     //INICIO GUARDAR LOS CAMBIOS REALIZADOS
    $("#btnEditar").click(function () {
        // Obtén los valores actualizados del formulario de edición
        const codigo_activo = $('#codigo_activo_edit').val();
        const nombre_de_activo = $('#nombre_de_activo_edit').val();
        const numero_de_serie = $('#numero_de_serie_edit').val();
        const fechaDeCompra = $('#fechaDeCompra_edit').val() || 0;
        const proveedor = $('#Proveedor_edit').val();
        const valor_de_compra = $('#Valor_de_compra_edit').val();
        const valor_actual = $('#Valor_actual_edit').val();
        const comentario = $('#comentario_edit').val();

        // Crea un objeto con los datos actualizados
        const activoData = {
            CODIGO_ACTIVO: codigo_activo,
            NOMBRE_DE_ACTIVO: nombre_de_activo,
            NUMERO_DE_SERIE: numero_de_serie,
            FECHA_DE_COMPRA: fechaDeCompra,
            PROVEEDOR: proveedor,
            VALOR_DE_COMPRA: valor_de_compra,
            VALOR_ACTUAL: valor_actual,
            COMENTARIO: comentario
        };

        // Realiza una solicitud AJAX para actualizar el activo
        $.ajax({
            url: url_actualizar_activo,
            method: "POST",
            data: activoData,
            success: function (result) {
                if (result.success) {
                    // Actualización exitosa, cierra el modal
                    $('#editarModal').modal('hide');
                    // Recarga la tabla 
                    window.location.reload();
                    alert("Cambios ingresados correctamente.");
                } else {
                    // Muestra un mensaje de error si la actualización falla
                    alert(result.message);
                }
            }
        });
    });

    //FIN GUARDAR LOS CAMBIOS REALIZADOS


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
                    alert('Empleado eliminado correctamente');
                },
                error: function () {
                    // Maneja los errores si es necesario
                    alert('No se pudo eliminar el empleado');
                }
            });
        } else {
            // Si el usuario hace clic en "Cancelar" en el cuadro de diálogo
            alert('No se eliminó el empleado');
        }
    });


    //FIN ELIMINAR EN GRID




});




