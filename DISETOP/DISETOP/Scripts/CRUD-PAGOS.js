"use strict";
$(document).ready(function () {
    
    //ESTE ES EL JS QUE SE USA PARA LOS CRUD, EJECUTA LOS METODOS DE LOS CONTROLADORES

    //URL DE LOS CONTROLADORES PRIMERO VA EL CONTROLADOR Y DESPUES EL METODO
    const tabla = $('#miTabla').DataTable();
    const url_pago = '/Pagos/InsertarPago'; // Define la URL para insertar empleados
    const url_actualizar_empleado = '/Empleados/EditarEmpleado'; // Define la URL para actualizar empleados
    const url_eliminar_empleado = '/Empleados/EliminarEmpleado'; // Define la URL para actualizar empleados


    // BOTON GUARDAR PAGO
    $("#handler-project-crud").click(function () {
        // Aquí puedes agregar el código que se ejecutará cuando se haga clic en el botón
        $('#exampleModalCenter').modal('show')
    });

    

    // Botón "Guardar Pago" en tu modal
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
    });

    // Inicializa el datepicker para el campo de fecha de pago
    $(".datepicker").datepicker({
        dateFormat: "yy-mm-dd", // Formato de fecha deseado (YYYY-MM-DD)
        onSelect: function (dateText, inst) {
            // Convierte la fecha al formato esperado por el servidor (yyyy-MM-dd)
            var selectedDate = $.datepicker.formatDate("yy-mm-dd", new Date(dateText));
            // Asigna la fecha formateada al campo de fecha para enviar al servidor
            $("#fechaDePago").val(selectedDate);
        }
    });


});



















