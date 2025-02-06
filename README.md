# Escrita de Testes - Visão Geral

Escrever testes automatizados é uma etapa essencial do desenvolvimento de software. Uma boa cobertura de testes garante que bugs em produção sejam drasticamente reduzidos através da identificação precoce dos problemas ao realizar manutenção, garantindo uma boa qualidade dos projetos.

Essa solução contém exemplos de como escrever testes unitários e de integração para aplicações ASP.NET Core, incluindo projetos no formato WebAPI e MVC. Além disso, no projeto MVC, há exemplos de como escrever testes para arquivos JavaScript, garantindo também cobertura de testes no front-end.

## Arquitetura da Solução

A solution é modularizada, escrita usando .NET 9 e JavaScript moderno, sendo que as regras de negócio estão dentro dos projetos contidos no diretório virtual `Gerenciamento de Usuários`. Esse módulo implementa regras de negócios de forma superficial, sem adentrar muito em regras específicas, para cadastrar usuários na aplicação seguindo princípios arquiteturais vistos com frequência em projetos .NET. 

Você encontrará projetos representando diferentes camadas da aplicação nesse módulo:

- Camada de domínio com models e interfaces de repositórios e serviços;
- Camada de acesso a dados que utiliza Entity Framework Core e PostgreSQL para acesso a dados;
- Camada com implementações de serviços e regras de negócio;
- Camada de configurações, com métodos de configuração para inicializar as dependências desse módulo;
- Projeto de testes unitários, onde há exemplos de testes unitários para todas as camadas citadas anteriormente.

No diretório virtual `API`, você encontrará um projeto WebAPI que integra o módulo de gestão de usuários e expõe rotas de API para criar os usuários e para efetuar autenticação. Não são aplicadas validações complexas de existência de usuários ou autenticação, apenas funcionalidades necessárias para demonstrar os testes.

Junto ao projeto WebAPI, há dois projetos de testes distintos, um mostrando como escrever testes unitários e outro com testes de integração.

Similar a ideia acima, o diretório virtual `MVC` possui um projeto que também integra o módulo de gestão de usuários, porém desenvolvido utilizando ASP.NET Core MVC, e com projetos de testes unitários e de integração respectivos.

Em relação ao projeto MVC, no diretório `wwwroot`, há exemplos de como escrever testes em JavaScript. Uma funcionalidade de validação de força da senha foi desenvolvida, e testes unitários usando Jest estão presentes.

## Diferenças Entre Testes Unitários e de Integração

Testes unitários testam componentes isoladamente na aplicação. Considere o exemplo do método de criação de usuário presente na aplicação. Antes de criar um usuários, algumas validações são feitas, como:

- Verificar se o email informado está sendo usado por outro usuário;
- Verificar se os dados obrigatórios de cadastro foram preenchidos internamente na model de usuários;
- Verificar se a chamada do método que insere os dados na base de dados funcionou.

Ao escrever testes unitários para esse método, não devemos testar também se a persistência de base de dados está funcionando. Podemos criar **mocks** das dependências necessárias e testar o fluxo de cadastro em si.

Já em testes de integração, a ideia é testar o fluxo completo com todas as integrações necessárias. Em aplicações ASP.NET Core, fazemos isso configurando todo o pipeline da aplicação e trocando apenas providers que se conectam a aplicações externas para usar algum mock interno. É recomendado também utilizar uma base de dados de testes na mesma tecnologia utilizada na aplicação (nesse caso, PostgreSQL), de forma a garantir que um determinado provider de testes não resulte em falsos positivos na hora de testar. Os testes são criados geralmente para testar as chamadas de endpoints de API ou os métodos de controllers, e verificam as saídas produzidas pela aplicação.

## Bibliotecas para Testes

Para as aplicações .NET, as seguintes bibliotecas são utilizadas:

- `xUnit`: Um framework de testes popular para .NET.
- `NSubstitute`: Framework de código aberto para criação de mocks. Apesar da biblioteca `Moq` ser mais popular, optamos pelo NSubstitute devido a questões de licenciamento.
- `coverlet`: permite cálculo da cobertura de código das aplicações .NET.
- `ReportGenerator`: ferramenta usada para gerar relatórios a partir de arquivos de cobertura de código.

Já para os testes em JavaScript, usamos os seguintes pacotes instalados via NPM:

- `Jest`: Um framework de testes popular para aplicação JavaScript. Suporta tanto JavaScript puro quanto frameworks como Angular, React.js, Vue.js, dentre outros.
- `babel`: permite escrever testes para código JavaScript moderno (ECMA6 e superior). Usado em conjunto com os pacotes `babel-jest` e `@babel/preset-env`.
- `jsdom`: Implementação JavaScript do DOM e padrões HTML para os testes.

É importante notar que a execução dos testes em JavaScript depende da instalação do Node.js (pode ser baixada a última versão LTS disponível) e do NPM.

##  Conceitos Importantes

É importante entender alguns conceitos para elaborar testes eficazes, que realmente validam os pontos necessários da aplicação. Recomendamos uma pesquisa dos itens abaixo para melhor entendimento de como testar as aplicações de forma eficaz:

- **Padrão AAA (Arrange / Act / Assert)**: padrão utilizado nos testes, referente aos passos de preparação para o teste, execução do teste, e verificação dos resultados obtidos.
- **Mock**: estrutura utilizada para substituir uma determinada dependência da aplicação na escrita de testes unitários. Por exemplo, ao invés de usar uma classe que dispara o envio de emails para a rede externa, podemos criar um mock que apenas receba a chamada dos métodos e retorne um resultado simulado.
- **System Under Test (SUT)**: refere-se a parte do sistema que está sendo testada. Esse conceito é especialmente importante para entender como estruturar testes de integração e como diferenciar os tipos de testes.
- **Code Coverage**: code coverage ou cobertura de testes é a métrica que indica a porcentagem do código de um software que foi testado. É importante para verificar se determinados fluxos de código não foram testados de forma satisfatória.

## Executando os Testes

### Testes .NET

Para executar os testes .NET e calcular a cobertura de código, execute os seguintes comandos no diretório raiz da solução

```
rmdir coveragereport /s /q
rmdir TestResults /s /q
dotnet test --settings coverage.runsettings --results-directory "TestResults"
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:coveragereport
```

Esses comandos removem os diretórios existentes (se houver) contendo os cálculos de cobertura de testes e criam um novo relatório de cobertura em formato HTML (arquivo `index.html`) no diretório `coveragereport`.

### Testes JavaScript

Para executar os testes JavaScript e calcular a cobertura de código, execute o seguinte comando no diretório do projeto `CriandoTestes.GestaoUsuarios.Mvc` (certifique-se de ter o Node.js e o NPM instalados corretamente na sua máquina):

```
npm run test-coverage
```

Este comando é um alias para o comando `jest --coverage`. Um diretório chamado `coverage-js` será gerado na raiz da solução, também contendo um relatório em formato HTML com os testes.

Note que é preciso instalar as dependências via NPM antes de rodar esse comando. Você pode fazer isso executando o comando `npm install` no diretório da aplicação MVC.

## FAQ -  Dúvidas Comuns

**1 - Como eu removo um determinado arquivo do meu cálculo de cobertura de testes?**

Dependendo da funcionalidade, é necessário excluir arquivos do seu cálculo de cobertura, como, por exemplo, ao usar recursos do framework para gerar código automaticamente, como migrations.

Em aplicações .NET, podemos fazer a exclusão de algumas formas:

- Incluindo o atributo `[ExcludeFromCodeCoverage]` na classe ou estrutura de código em questão;
- Modificando o arquivo `coverage.runsettings` na raiz da solution e instruindo diretórios para exclusão, atributos para exclusão, ou arquivos específicos.

Consulte a documentação do xUnit e do coverlet para referência.

Já em código JavaScript, se usarmos o Jest, podemos modificar o arquivo `jest.config.js` para excluir determinados arquivos ou diretórios do cálculo.

Consulte a documentação do Jest para referência.

**2 - Como eu faço para escrever mocks? Como eu verifico se métodos receberam um parâmetro específico ou foram chamados n vezes em um bloco de código?**

As bibliotecas NSubstitute e Jest possuem estruturas próprias para gerar os mocks e validar as chamadas. Consulte a documentação específica para referência, ou estude os exemplo criados nessa solução.

NSubstitute - Mocks: https://nsubstitute.github.io/
Jest - Mocks: https://jestjs.io/docs/mock-function-api

**3 - Quais estruturas do .NET eu preciso "mockar" ou substituir nos meus testes?**

Ao escrever testes em .NET, geralmente nos deparamos com situações onde temos que criar substitutos para algumas das dependências abaixo:

- `ControllerContext` e `ModelState`;
- `HttpContext`;
- `HttpClient`;
- `TempData`;
- Provider de base de dados para o Entity Framework Core;
- `IServiceCollection`;
- `IOptions` e `IConfiguration`.

A necessidade varia de acordo com o tipo de aplicação desenvolvida e a estrutura de código utilizada. 

A solução de exemplo possui testes que criam substitutos para algumas dessas estruturas em seus testes. 
Use como referência para implementação. Você também pode verificar os artigos abaixo para exemplos detalhados:

- ControllerContext e ModelState: https://johnnyreilly.com/unit-testing-modelstate
- HttpContext: https://medium.com/c-sharp-programming/mock-httpcontext-for-asp-net-core-unit-testing-17d802f546bd
- HttpClient: https://medium.com/younited-tech-blog/easy-httpclient-mocking-3395d0e5c4fa
- TempData: https://stackoverflow.com/questions/52181767/mocking-a-tempdata-in-asp-net-core-in-mstest
- EF Core Provider: https://learn.microsoft.com/en-us/ef/core/testing/
- IServiceCollection: https://code-maze.com/dotnet-how-to-test-iservicecollection-registrations/
- IConfiguration e IOptions: https://code-maze.com/csharp-mock-ioptions/

**4 - O que exatamente eu preciso testar no front-end?**

Em geral, é uma boa prática escrever testes que validem a interação do usuário com componentes da tela, como campos de formulários. Também é interessante testar a renderização de conteúdo na tela e modificação de estado conforme navegação do usuário.

**5 - Quais recomendações devo considerar ao escrever testes que validem valores em formato `string`?**

Para não criar testes frágeis, que se quebram com facilidade, não é uma boa ideia testar se um determinado texto ou frase está exatamente igual ao esperado. Se uma única letra é alterada, isso pode acabar quebrando os testes de forma desnecessária.

Ao invés disso, procure usar expressões regulares e validar pedaços dos textos, focando na ideia principal.

Por exemplo, suponha que você tem um método de validação que retorna a seguinte mensagem se o email é inválido: "O email digitado é inválido devido a regra xyz.".
Ao invés de verificar nos testes se a frase inteira está presente, você pode verificar se o retorno contém a palavra "email". Dessa forma, se alguém alterar o texto para "Digite um email válido.", seu teste unitário não quebrará.

**6 - Eu posso rodar um mesmo teste várias vezes com parâmetros diferentes?**

Sim. Isso é útil caso você escreva métodos que validem várias regras para um mesmo valor (exemplo: validação de CPF).

Em aplicações .NET com xUnit, você pode definir um método de testes com o atributo `[Theory]` e usar `[InlineData]` para passagem de parâmetros.

Já em JavaScript com Jest, é possível usar o método `.each()` passando vários parâmetros a um mesmo teste.

Consulte as documentações respectivas para referência:

- https://xunit.net/faq/theory-data-stability-in-vs
- https://jestjs.io/docs/api#testeachtablename-fn-timeout

**7 - Eu preciso ter uma cobertura de testes de 100% para que minha aplicação seja considerada aceitável?**

Não. A porcentagem pode variar de acordo com as definições dos líderes técnicos responsáveis pelo projeto e da necessidade de negócio. 

O mais importante é testar as funcionalidades críticas da aplicação de forma satisfatória.

Na literatura, existem recomendações como ter um mínimo de 70% de cobertura de testes, porém fica a definição do time técnico quais métricas utilizar.


**8 - Esses padrões aplicados na solução se aplicam apenas a esses modelos de aplicação .NET e JavaScript? E somente a essas bibliotecas?**

Não. Os padrões apresentados podem ser usados em outros modelos de projetos e outras tecnologias. Existem vários livros e tutoriais que cobrem em detalhes as técnicas usadas e similaridades.

Também existem várias outras bibliotecas e frameworks em .NET e JavaScript para escrita de testes e cálculo de cobertura de código.

Você pode usar outras tecnologias e seguir os conceitos aqui apresentados sem problema nenhum.

## Conclusão

Esse repositório visa apresentar técnicas para criar testes unitários e de integração para aplicações .NET e JavaScript, seguindo conceitos adotados no mercado.

Sinta-se livre para enviar pull requests melhorando o código e criando mais exemplos. Contribuições são sempre bem-vindas!