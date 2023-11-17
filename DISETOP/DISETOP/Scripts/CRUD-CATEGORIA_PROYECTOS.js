$(document).ready(function () {


    //ESTE ES EL JS QUE SE USA PARA LOS CRUD, EJECUTA LOS METODOS DE LOS CONTROLADORES

    //URL DE LOS CONTROLADORES PRIMERO VA EL CONTROLADOR Y DESPUES EL METODO 
    const tabla = $('#miTabla').DataTable();

    const url_categoria_proyectos = '/Categoria_Proyectos/InsertarCategoria_Proyectos'; // Define la URL para insertar categoria de proyectos
    const url_actualizar_categoriaProyecto = '/Categoria_Proyectos/EditarCategoria_Proyectos'; // Define la URL para actualizar categoria de proyectos
    const url_eliminar_categoriaProyecto = '/Categoria_Proyectos/EliminarCategoria_Proyectos'; // Define la URL para actualizar categoria de proyectos

    //BOTON GUARDAR CATEGORIA DE PROYECTOS
    $("#handler-project-crud").click(function () {
        // Aquí puedes agregar el código que se ejecutará cuando se haga clic en el botón
        $('#exampleModalCenter').modal('show')
    });

    //INICIO INSERTAR CATEGORIA DE PROYECTOS
    $("#btnGuardar").click(function () {
        // Guarda la info del emp
        const form = $('#form-categoria_proyectos')


        // Obtén un objeto FormData a partir del formulario
        var formData = new FormData(document.getElementById("form-categoria_proyectos"));

        // Convierte el objeto FormData a un array de pares clave-valor y luego a un objeto
        var formDataObject = Object.fromEntries(formData.entries());

        // Imprime el objeto con los valores en la consola
        console.log(formDataObject);
        $.ajax({
            url: url_categoria_proyectos,
            method: "POST",
            data: formDataObject,

            success: function (result) {
                if (result.success) {
                    window.location.reload()
                    alert("Categoría de Proyectos ingresada correctamente.");
                } else {
                    // El muestra un mensaje de error
                    alert(result.message);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error al agregar la Categoría de Proyectos: " + errorThrown);
            }
        });
    });

    //FIN DE INSERTAR CATEGORIA DE PROYECTOS

    //INICIO DE EDITAR CATEGORIA DE PROYECTOS

    //BOTON EDITAR CATEGORIA DE PROYECTOS
    $("#buttonEditar").click(function () {

        $('#editarModal').modal('show')
    });

    $('#miTabla').on('click', 'button.editar', function () {
        const row = $(this).closest('tr');
        const data = tabla.row(row).data();

        // Cargar datos en el modal editarModal
        $('#codigo_categoria_edit').val(data[0]);
        $('#nombre_categoria_edit').val(data[1]);
        $('#comentario_edit').val(data[2]);

        $('#editarModal').modal('show');
        console.log('Editar fila con datos: ', data);
    });

    $("#btnEditar").click(function () {
        // Obtén los valores actualizados del formulario de edición
        const codigo_categoria = $('#codigo_categoria_edit').val();
        const nombre_categoria = $('#nombre_categoria_edit').val();
        const comentario = $('#comentario_edit').val();

        // Crea un objeto con los datos actualizados
        const categoriaProyectoData = {
            CODIGO_CATEGORIA: codigo_categoria,
            NOMBRE_CATEGORIA: nombre_categoria,
            COMENTARIO: comentario
        };

        // Realiza una solicitud AJAX para actualizar el empleado
        $.ajax({
            url: url_actualizar_categoriaProyecto,
            method: "POST",
            data: categoriaProyectoData,
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
    // FIN DE EDITAR CATEGORIA DE PROYECTOS


    //INICIO ELIMINAR EN GRID
    // Agrega un evento para mostrar un cuadro de diálogo de confirmación
    $('#miTabla').on('click', 'button#buttonEliminar', function () {
        const row = $(this).closest('tr');
        const codigoCategoria = row.find('td:first').text(); // Obtiene el código de la categoria de proyectos

        // Muestra un cuadro de diálogo de confirmación
        if (confirm('¿Está seguro de que desea eliminar la categoria de proyecto? Esto también eliminará los registros de Proyecto y Costo de Proyectos de esta Categoria.')) {
            // Si el usuario hace clic en "Aceptar" en el cuadro de diálogo
            // Realiza una solicitud AJAX para eliminar el registro en la base de datos
            $.ajax({
                type: 'POST', // Método HTTP POST
                url: url_eliminar_categoriaProyecto, // URL de la acción del controlador para eliminar categoria
                data: { codigoCategoria: codigoCategoria }, // Parámetro para enviar el código de la categoria
                success: function (data) {
                    // Elimina la fila de la tabla
                    tabla.row(row).remove().draw();
                    // Muestra un mensaje de éxito
                    alert('Categoria de proyecto eliminada correctamente');
                },
                error: function () {
                    // Maneja los errores si es necesario
                    alert('No se pudo eliminar la Categoria de proyecto');
                }
            });
        } else {
            // Si el usuario hace clic en "Cancelar" en el cuadro de diálogo
            alert('No se eliminó la Categoria de proyecto');
        }
    });


    //FIN ELIMINAR EN GRID




});





