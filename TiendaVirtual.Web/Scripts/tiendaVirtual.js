$(document).ready(function () {
    //$("#PaisId").change(function() {
    //    $("#CiudadId").empty();
    //});

    $("#PaisId").change(function () {
        $("#CiudadId").empty();
        $("#CiudadId").append('<option value="0">[Seleccionar Ciudad]</option>');

        $.ajax({
            type: 'POST',
            url: Url,
            dataType: 'json',
            data: {
                paisId: $("#PaisId").val()

            },
            success: function (ciudades) {
                $.each(ciudades,
                    function (i, ciudad) {
                        $("#CiudadId").append('<option value="' + ciudad.CiudadId + '">' + ciudad.NombreCiudad + '</option>');
                    });
            },
            error: function (ex) {
                alert('Error al intentar cargar las ciudades.' + ex);
            }
        });
        return false;
    });
});
