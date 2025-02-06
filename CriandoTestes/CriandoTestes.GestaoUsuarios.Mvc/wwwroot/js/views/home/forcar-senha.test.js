const { JSDOM } = require('jsdom');

describe('forca-senha.js', () => {
    let dom;
    let document;

    beforeEach(() => {
        const html = `
            <!DOCTYPE html>
            <html lang="en">
            <head>
                <meta charset="UTF-8">
                <title>Test</title>
            </head>
            <body>
                <input type="password" id="Senha">
                <div id="forca-senha"></div>
                <script src="../forca-senha.js"></script>
            </body>
            </html>
        `;
        dom = new JSDOM(html, { runScripts: 'dangerously' });
        document = dom.window.document;
        global.document = document;
        global.window = dom.window;
        require('./forca-senha');
    });

    afterEach(() => {
        // Limpar o require cache para garantir que o script seja recarregado
        jest.resetModules();
    });

    test('deve exibir "Senha fraca" para senhas com menos de 6 caracteres', () => {
        let senhaInput = document.querySelector('#Senha');
        let forcaSenhaContainer = document.querySelector('#forca-senha');

        senhaInput.value = '123';
        senhaInput.dispatchEvent(new dom.window.Event('keyup'));

        expect(forcaSenhaContainer.innerHTML).toBe('Senha fraca');
        expect(forcaSenhaContainer.style.color).toBe('red');
    });

    test('deve exibir "Senha fraca" para senhas com mais de 6 caracteres, porém com apenas 1 critério atendido', () => {
        let senhaInput = document.querySelector('#Senha');
        let forcaSenhaContainer = document.querySelector('#forca-senha');

        senhaInput.value = '12345678';
        senhaInput.dispatchEvent(new dom.window.Event('keyup'));

        expect(forcaSenhaContainer.innerHTML).toBe('Senha fraca');
        expect(forcaSenhaContainer.style.color).toBe('red');
    });

    test('deve exibir "Senha razoável" para senhas razoáveis', () => {
        let senhaInput = document.querySelector('#Senha');
        let forcaSenhaContainer = document.querySelector('#forca-senha');

        senhaInput.value = '123456*';
        senhaInput.dispatchEvent(new dom.window.Event('keyup'));

        expect(forcaSenhaContainer.innerHTML).toBe('Senha razoável');
        expect(forcaSenhaContainer.style.color).toBe('orange');
    });

    test('deve exibir "Senha forte" para senhas fortes', () => {
        let senhaInput = document.querySelector('#Senha');
        let forcaSenhaContainer = document.querySelector('#forca-senha');

        senhaInput.value = 'aA123456*!';
        senhaInput.dispatchEvent(new dom.window.Event('keyup'));

        expect(forcaSenhaContainer.innerHTML).toBe('Senha forte');
        expect(forcaSenhaContainer.style.color).toBe('green');
    });
});
