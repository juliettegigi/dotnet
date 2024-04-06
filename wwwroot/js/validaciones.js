$(document).ready(function () {
    $("#miFormulario").validate({
        rules: {
            ApartirDe: {
                required: true,
                fechaNoMenorQueLaActual: true
            }
        },
        messages: {
            ApartirDe: {
                required: "Campo fecha obligatorio.",
                fechaNoMenorQueLaActual: "La fecha no puede ser menor que la actual."
            }
        }
    });
});