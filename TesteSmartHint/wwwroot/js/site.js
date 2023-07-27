document.addEventListener('DOMContentLoaded', (event) => {
    function toggleFiltro() {
        var filtro = document.getElementById("filtro");
        if (filtro !== null) {
            filtro.style.display = filtro.style.display === "none" ? "block" : "none";
        }
    }

    // JavaScript para controlar a exibição dos campos de acordo com o Tipo de Pessoa selecionado
    const tipoPessoaSelect = document.getElementById("tipoPessoa");
    const generoDiv = document.getElementById("generoDiv");
    const dataNascimentoDiv = document.getElementById("dataNascimentoDiv");

    tipoPessoaSelect.addEventListener("change", () => {
        const tipoPessoa = tipoPessoaSelect.value;
        generoDiv.style.display = tipoPessoa === "Física" ? "block" : "none";
        dataNascimentoDiv.style.display = tipoPessoa === "Física" ? "block" : "none";
    });

    // Função para bloquear a Inscrição Estadual quando "Isento" é selecionado
    const isentoCheckbox = document.getElementById("isento");
    const inscricaoEstadualInput = document.getElementById("inscricaoEstadual");

    isentoCheckbox.addEventListener("change", () => {
        inscricaoEstadualInput.disabled = isentoCheckbox.checked;
        if (isentoCheckbox.checked) {
            inscricaoEstadualInput.value = "";
        }
    });
    $(document).ready(function () {
        var masks = ['000.000.000-009', '00.000.000/0000-00'];
        var maskBehavior = function (val, e, field, options) {
            return val.replace(/\D/g, '').length > 11 ? masks[1] : masks[0];
        };

        $('#cpfCnpj').mask(maskBehavior, {
            onKeyPress: function (val, e, field, options) {
                field.mask(maskBehavior(val, e, field, options), options);
            },
        });
    });


    $(document).ready(function () {
        var cellPhoneMask = "(00) 00000-0000";
        $('#telefone').mask(cellPhoneMask);
    });

    $('#senha, #confirmarSenha').on('keyup', function () {
        if ($('#senha').val() === $('#confirmarSenha').val()) {
            $('#senhaNaoConfere').hide();
        } else {
            $('#senhaNaoConfere').show();
        }
    });

    $('form').on('submit', function (event) {
        if ($('#senha').val() !== $('#confirmarSenha').val()) {
            event.preventDefault();
            alert('As senhas não conferem.');
        }
    });

    $('#inscricaoEstadual').mask('000.000.000-000');

    $('#tipoPessoa').on('change', function () {
        if (this.value === 'Jurídica') {
            $('#inscricaoEstadual').attr('required', true);
        } else {
            $('#inscricaoEstadual').removeAttr('required');
        }
    });

    $('#tipoPessoa').change(function () {
        if ($(this).val() == "Física") {
            $("#dataNascimentoDiv").show();
            $("#dataNascimento").attr('required', '');
        } else {
            $("#dataNascimentoDiv").hide();
            $("#dataNascimento").removeAttr('required');
        }
    });
   
});

function toggleFiltro() {
    var filtro = document.getElementById("filtro");
    filtro.style.display = filtro.style.display === "none" ? "block" : "none";
}

function limparFiltros() {
    document.getElementById("filtroNome").value = "";
    document.getElementById("filtroEmail").value = "";
    document.getElementById("filtroTelefone").value = "";
    document.getElementById("filtroDataCadastro").value = "";
    document.getElementById("filtroBloqueado").value = "";

    // Submete o formulário após limpar os filtros
    document.querySelector("form").submit();
}

var chkTodos = document.getElementById("chkTodos");
var chkCompradores = document.querySelectorAll("input[type=checkbox]:not(#chkTodos)");

chkTodos.addEventListener("change", function () {
    chkCompradores.forEach(function (chk) {
        chk.checked = chkTodos.checked;
    });
});