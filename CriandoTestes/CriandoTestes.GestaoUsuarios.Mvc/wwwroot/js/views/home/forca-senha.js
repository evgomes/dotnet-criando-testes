"use strict"

function configurarValidacaoForcaSenha(seletorCampoSenha, seletorContainerForcaSenha) {
    const campoSenha = document.querySelector(seletorCampoSenha);
    const containerForcaSenha = document.querySelector(seletorContainerForcaSenha);
    const condicoes = {
        possuiLetraMinuscula: /[a-z]/,
        possuiLetraMaiuscula: /[A-Z]/,
        possuiNumero: /[0-9]/,
        possuiCaractereEspecial: /[!@#\$%\^&\*]/,
    };

    campoSenha.addEventListener("keyup", function () {
        const senha = campoSenha.value;
        let strength = "fraca";
        let cor = "red";

        // Senhas com menos de 6 caracteres são consideradas fracas.
        if (senha.length > 6) {
            let criteriosAtendidos = 0;
            for (var chave in condicoes) {
                if (condicoes[chave].test(senha)) {
                    criteriosAtendidos++;
                }
            }

            if (criteriosAtendidos >= 3) {
                strength = "forte";
                cor = "green";
            } else if (criteriosAtendidos >= 2) {
                strength = "razoável";
                cor = "orange";
            }
        }

        containerForcaSenha.innerHTML = "Senha " + strength;
        containerForcaSenha.style.color = cor;
    });
}

document.addEventListener('DOMContentLoaded', () => {
    configurarValidacaoForcaSenha("#Senha", "#forca-senha");
});