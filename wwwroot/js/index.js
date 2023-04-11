/* --------------------------------------------------
 Aplicação criada por Bruno Araujo Rodriguez - 2022
-------------------------------------------------- */

// Write your JavaScript code.
function verificaSenha(campoSenha,campoMostrar) {
    senha = document.getElementById(campoSenha).value;
    forca = 0;
    mostra = document.getElementById(campoMostrar);
    if ((senha.length >= 4) && (senha.length <= 7)) {
        forca += 10;
    } else if (senha.length > 7) {
        forca += 25;
    }
    if (senha.match(/[a-z]+/)) {
        forca += 10;
    }
    if (senha.match(/[A-Z]+/)) {
        forca += 20;
    }
    if (senha.match(/d+/)) {
        forca += 20;
    }
    if (senha.match(/W+/)) {
        forca += 25;
    }
    return mostra_res();
}
function mostra_res() {
    if (forca < 30) {
        mostra.innerHTML = '<tr><td class="barraValidacao" bgcolor="red" width="' + forca + '"></td></tr><tr><center>Fraca</center></tr>';
    } else if ((forca >= 30) && (forca < 60)) {
        mostra.innerHTML = '<tr><td class="barraValidacao" bgcolor="yellow" width="' + forca + '"></td></tr><tr><center>Simples</center></tr>';;
    } else if ((forca >= 60) && (forca < 85)) {
        mostra.innerHTML = '<tr><td class="barraValidacao" bgcolor="blue" width="' + forca + '"></td> </tr><tr><center>Forte</center></tr>';
    } else {
        mostra.innerHTML = '<tr><td class="barraValidacao" bgcolor="green" width="' + forca + '"></td></tr><tr><center>Excelente</center></tr>';
    }
}


/* --------------------------------------------------
 Aplicação criada por Bruno Araujo Rodriguez - 2022
-------------------------------------------------- */