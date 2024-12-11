// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

@section Scripts {
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const forms = document.querySelectorAll("form"); // Selecciona todos los formularios
            forms.forEach(form => {
            form.addEventListener("submit", function (event) {
                const confirmed = confirm("¿Estás seguro de que deseas realizar esta acción?");
                if (!confirmed) {
                    event.preventDefault(); // Cancela la acción si el usuario selecciona "Cancelar"
                }
            });
            });

        const deleteButtons = document.querySelectorAll(".btn-delete"); // Para botones de eliminación
            deleteButtons.forEach(button => {
            button.addEventListener("click", function (event) {
                const confirmed = confirm("¿Estás seguro de que deseas eliminar este registro?");
                if (!confirmed) {
                    event.preventDefault(); // Cancela la acción si el usuario selecciona "Cancelar"
                }
            });
            });
        });
    </script>
}