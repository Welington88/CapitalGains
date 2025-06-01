# Capital Gains

## 1. Uma explicação sobre as decisões técnicas e arquiteturais do seu desafio:

- Por ser uma aplicação simples, foi desenvolvido apenas o core da aplicação as entidades de domínio e o service que processa os dados da aplicação onde se encontra a regras de negócio, em linguagem **C#** utilizando **.net core 6**.

- Com isso foi desenvolvido testes unitário e de integração para testar as entidades e processamento da aplicação.

- Foi implementado um serviço que processa um conjunto de transações com ações e retorna o valor total de impostos devido para cada transação.

- A classe _ServiceOperation_ implementa a interface _IServiceOperation_ contrato de serviço.

- Nela contém o método: _processListStocks_ recebe uma string como _input_ no terminal _cli_ que contém uma lista de transações com ações no formato JSON. E Verificando se a _string_ está vazia ou _nula_ e lança uma exceção caso esteja. Em seguida, ele chama o método _convertJsonToObject_ para converter a string JSON em uma lista de objetos _Operation_. Ele também cria as variáveis listResultConvertJsonToStocks, listweightedAveragePrice weightedAveragePriceResult, quantityOfStocksBought, taxValueResult, financialLossStock, listTaxValueResult que serão usadas depois.

- Logo depois, o método percorre a lista de transações, para cada transação, verifica se é uma compra ou venda. 

- Se for uma compra, a transação é adicionada a uma lista de transações de compra e a quantidade total de ações compradas é atualizada. O método também calcula a média ponderada do preço de compra. 

- Se for uma venda, o método verifica se a quantidade de ações vendidas é maior do que a quantidade de ações compradas. Se for o caso, uma exceção é lançada. Caso contrário, o método chama o método _sellReprocessWeightedAverageList_ para atualizar a lista de transações de compra e remover as ações vendidas. Em seguida, ele chama o método _calculateSalesTax_ para calcular o valor do imposto devido e atualiza a variável _financialLossStock_ com o valor do ganho ou perda financeira da transação.

- O método _calculateSalesTax_ calcula o valor do imposto devido para uma transação de venda. Ele verifica se o valor total da transação é inferior a um valor mínimo que é tributável e retorna zero se for o caso. Caso contrário, ele calcula o ganho ou perda financeira da transação e verifica se é negativo. Se for negativo, ele atualiza a variável _financialLossStock_ com o valor da perda. Caso contrário, ele calcula o valor do imposto com base no ganho financeiro e em uma taxa fixa.

- O método _sellReprocessWeightedAverageList_ remove as ações vendidas da lista de transações de compra. Ele percorre a lista de transações de compra e remove as ações vendidas até que todas as ações vendidas tenham sido removidas.

- O método _convertJsonToObject_ converte a string JSON em uma lista de objetos _Operation_. Ele verifica se a _string_ JSON contém uma lista de listas ou uma lista simples e executa a conversão apropriada.

- O método _convertObjectToJson_ converte uma lista de listas de objetos _Tax_ em uma string JSON. Ele verifica se a lista contém uma lista simples ou várias listas e executa a conversão apropriada. reazlizando o _OutPut_ para terminal final.

## 2. Uma justificativa para o uso de frameworks ou bibliotecas (caso sejam usadas);

Na aplicação não utilizei nenhum framework em si, utilizei apenas a biblioteca [_Newtonsoft.Json_](https://www.nuget.org/packages/Newtonsoft.Json/).

- [x] A biblioteca Newtonsoft.Json foi utilizada para converter objetos .NET em formato JSON e vice-versa. Ele suporta a serialização e desserialização de objetos complexos, como arrays, dicionários, tipos anônimos e objetos aninhados. 
- [x] Ela foi utilizada para receber o arquivo .json converter em objeto e vice-versa.

Já no projeto de tests foi necessário utilizar o framework de tests [_xUnit_](https://www.nuget.org/packages/xunit) .

- [x] _xUnit_ é um framework de teste de unidade para _.NET_ que permite criar e executar testes automatizados em _C#_, _F#_ e outros idiomas _.NET_. Ele é inspirado no _JUnit_ e em outros frameworks de teste de unidade para outras linguagens de programação.

- [x] O _xUnit_ é um framework gratuito e de código aberto que segue uma abordagem de convenção sobre configuração. Isso significa que ele fornece um conjunto de convenções que você pode usar para escrever seus testes, em vez de exigir que você configure manualmente cada aspecto do seu ambiente de teste.

## 3. Instruções sobre como compilar e executar o projeto;

Para executar a aplicação, é necessário ter instalado o _sdk .net core 6_ para instalar caso necessário acesse o link: [clique aqui!](https://dotnet.microsoft.com/en-us/download/dotnet/6.0), para verificar se o sdk já está instalado em sua máquina digite no terminal: `dotnet --list-sdks` verifique se a versão 6 está instalada, caso não, faça a instalação. 

Na Pasta Raiz abra um terminal e digite os comandos, exemplos de terminais _Terminal Bash_ ou _Power Shell_;

Para acessar pasta da aplicação:
- `cd src/CapitalGains/`

Para executar a aplicação fazendo o _input_ dos dados:
- Input bash: `dotnet run` 
- Input Redirection: `dotnet run < ./input.txt`

## 4. Instruções sobre como executar os testes da solução;

Na Pasta Raiz abra um terminal e digite os comandos;

Para acessar pasta da aplicação de testes:
- `cd src/CapitalGains.Test/`

Para executar a aplicação fazendo o _input_ dos dados:
- Input bash: `dotnet test`

## 5. Notas adicionais que você considere importantes para a avaliação.

- Projeto proposto era uma aplicação simples, que faça os cálculos em memória e entregue o resultado. Por esse motivo construir uma aplicação dentro desse princípio.