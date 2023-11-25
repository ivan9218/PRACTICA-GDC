"use strict";
$(document).ready(function () {
    
    //ESTE ES EL JS QUE SE USA PARA LOS CRUD, EJECUTA LOS METODOS DE LOS CONTROLADORES

    //URL DE LOS CONTROLADORES PRIMERO VA EL CONTROLADOR Y DESPUES EL METODO
    const tabla = $('#miTabla').DataTable();
    const url_pago = '/Pagos/InsertarPago'; // Define la URL para insertar empleados
    const url_actualizar_pago = '/Pagos/EditarPago'; // Define la URL para actualizar pagos
    const url_eliminar_pago = '/Pagos/EliminarPago'; // Define la URL para actualizar empleados


    // BOTON GUARDAR PAGO
    $("#handler-project-crud").click(function () {
        // Aquí puedes agregar el código que se ejecutará cuando se haga clic en el botón
        $('#exampleModalCenter').modal('show')
    });

    

    
    // Botón "Guardar Pago" en tu modal
<<<<<<< HEAD

=======
>>>>>>> cc5db32696f0857021b9e5e5f9e6118a93561365
    $("#btnGuardar").click(function () {
        var codigoPago = $("#codigo_pago").val();
        // obtiene el valor seleccionado del select y el primary key 
        var codigoEmpleado = $("#codigo_empleado").val();

        // Obtiene los otros
        var cuentaBancaria = $("#cuenta_bancaria").val();
        var diasApagar = $("#diasApagar").val();
        var monto = $("#monto").val();
        var fechaDePago = $("#fechaDePago").val();
        var comentario = $("#comentario").val();

        // Crea un objeto con los datos del pago
        var pago = {
            CODIGO_PAGO: codigoPago,
            FK_CODIGO_EMPLEADO: codigoEmpleado,
            CUENTA_BANCARIA: cuentaBancaria,
            DIAS_A_PAGAR: diasApagar,
            MONTO: monto,
            FECHA_DE_PAGO: fechaDePago,
            COMENTARIO: comentario
            // Agrega otros campos del pago aquí, si es necesario.
        };
        debugger
        // Realiza una solicitud AJAX al controlador para insertar el pago
<<<<<<< HEAD
        if (validate(pago)) {
            $.ajax({
                url: '/Pagos/InsertarPago', // Asegúrate de que esta ruta sea la correcta
                method: "POST",
                data: pago,
                success: function (result) {
                    if (result.success) {
                        $('#exampleModalCenter').modal('hide'); // Cierra el modal
                        toastr.success("Pago ingresado correctamente.")
                        setTimeout(function () {
                            // Tu código que se ejecutará después del retraso de 500 ms
                            window.location.reload();
                        }, 500);
                        // Puedes agregar aquí código para actualizar la tabla de pagos si es necesario.
                    } else {
                        // Muestra un mensaje de error si hay un problema
                        toastr.error(result.message)
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    toastr.error("Error al agregar el pago: " + errorThrown)
                }
            });
        }
=======
        $.ajax({
            url: '/Pagos/InsertarPago', // Asegúrate de que esta ruta sea la correcta
            method: "POST",
            data: pago,
            success: function (result) {
                if (result.success) {
                    $('#exampleModalCenter').modal('hide'); // Cierra el modal
                    window.location.reload()
                    alert("Pago ingresado correctamente.");
                    // Puedes agregar aquí código para actualizar la tabla de pagos si es necesario.
                } else {
                    // Muestra un mensaje de error si hay un problema
                    alert(result.message);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error al agregar el pago: " + errorThrown);
            }
        });
>>>>>>> cc5db32696f0857021b9e5e5f9e6118a93561365
    });

   

    //INICIO DE EDITAR PAGO

    //BOTON EDITAR PAGO
    $("#buttonEditar").click(function () {

        $('#editarModal').modal('show')
    });

    $('#miTabla').on('click', 'button.editar', function () {
        const row = $(this).closest('tr');
        const data = tabla.row(row).data();
        /*      debugger*/
        // Cargar datos en el modal de edición
        $('#codigo_pago_edit').val(data[0]);
        $('#codigo_empleado_edit').val(data[1]); //traerse el codigo para que lo muestre
        $('#cuenta_bancaria_edit').val(data[3]);// EL CAMPO 2 SE USA PARA EL NOMBRE DEL EMPLEADO OSEA EL CAMPO QUE REQUIERE DESPUES DE CODIGO EMPLEADO
        $('#diasApagar_edit').val(data[4]);
        $('#monto_edit').val(data[5]);
        const fecha = data[6]; // toma el campo fecha en el campo 5 se le asigna variable fecha 
        if (fecha) { //crea un 
           const [day, month, year] = fecha.split('/'); //split lo que hace es convertir un string en un array para poder dividir la fecha y formatearla aqui llega como yyyy/mm/dd
            $('#fechaDePago_edit').val(year + '-' + month + '-' + day);// en el html fechaDePago_edit osea en el picker cargue el ano el mes y el dia. 
        }
         /*  $('#fechaDePago_edit').val(data[5]); // esta es la forma normal */
        $('#comentario_edit').val(data[7]);

        $('#editarModal').modal('show');
        console.log('Editar fila con datos: ', data);
    });

<<<<<<< HEAD
    
    function validate(data) {
        debugger
        if (data.MONTO.length >= 22 || data.MONTO == null || data.MONTO=='') {
            toastr.error("El campo 'Monto' está fuera del rango permitido.")
            return false
        }

        if (data.CODIGO_PAGO.trim().length == 0) {
            toastr.error("codigo de pago no es valido")
            return false
        }

       

        return true;
    }

    $("#btnEditar").click(function () {
        debugger
=======

    $("#btnEditar").click(function () {
>>>>>>> cc5db32696f0857021b9e5e5f9e6118a93561365
        // Obtén los valores actualizados del formulario de edición
        const codigo_pago = $('#codigo_pago_edit').val();
        const codigo_empleado_edit = $('#codigo_empleado_edit').val();
        const cuenta_bancaria = $('#cuenta_bancaria_edit').val();
        const diasApagar = $('#diasApagar_edit').val();
<<<<<<< HEAD
        const monto = $('#monto_edit').val();
=======
        const monto = $('#monto_edit').val() || 0;
>>>>>>> cc5db32696f0857021b9e5e5f9e6118a93561365
        const fechaDePago = $('#fechaDePago_edit').val();
        const comentario = $('#comentario_edit').val();

        // Crea un objeto con los datos actualizados
        const pagoData = {
            CODIGO_PAGO: codigo_pago,
            FK_CODIGO_EMPLEADO: codigo_empleado_edit,
            CUENTA_BANCARIA: cuenta_bancaria,
            DIAS_A_PAGAR: diasApagar,
            MONTO: monto,
            FECHA_DE_PAGO: fechaDePago,
            COMENTARIO: comentario
        };

<<<<<<< HEAD
        if(validate(pagoData)) {
            // Realiza una solicitud AJAX para actualizar el empleado
            $.ajax({
                url: url_actualizar_pago,
                method: "POST",
                data: pagoData,
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
        }
=======
        // Realiza una solicitud AJAX para actualizar el empleado
        $.ajax({
            url: url_actualizar_pago,
            method: "POST",
            data: pagoData,
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
>>>>>>> cc5db32696f0857021b9e5e5f9e6118a93561365
    });
    // FIN DE EDITAR PAGO



    //INICIO ELIMINAR EN GRID
    // Agrega un evento para mostrar un cuadro de diálogo de confirmación
    $('#miTabla').on('click', 'button#buttonEliminar', function () {
        const row = $(this).closest('tr');
        const CodigoPago = row.find('td:first').text(); // Obtiene el código del pago

        // Muestra un cuadro de diálogo de confirmación
        if (confirm('¿Está seguro de que desea eliminar el registro del pago?')) {
            // Si el usuario hace clic en "Aceptar" en el cuadro de diálogo
            // Realiza una solicitud AJAX para eliminar el registro en la base de datos
            $.ajax({
                type: 'POST', // Método HTTP POST
                url: url_eliminar_pago, // URL de la acción del controlador para eliminar pago
                data: { CodigoPago: CodigoPago }, // Parámetro para enviar el código del pago
                success: function (data) {
                    // Elimina la fila de la tabla
                    tabla.row(row).remove().draw();
                    // Muestra un mensaje de éxito
<<<<<<< HEAD
                    toastr.success("Pago eliminado correctamente")    
                },
                error: function () {
                    // Maneja los errores si es necesario
                    toastr.error("No se pudo eliminar el pago")                        
=======
                    alert('Pago eliminado correctamente');
                },
                error: function () {
                    // Maneja los errores si es necesario
                    alert('No se pudo eliminar el pago');
>>>>>>> cc5db32696f0857021b9e5e5f9e6118a93561365
                }
            });
        } else {
            // Si el usuario hace clic en "Cancelar" en el cuadro de diálogo
<<<<<<< HEAD
            toastr.error("No se eliminó el pago")                        
            
=======
            alert('No se eliminó el pago');
>>>>>>> cc5db32696f0857021b9e5e5f9e6118a93561365
        }
    });


    //FIN ELIMINAR EN GRID








});



















