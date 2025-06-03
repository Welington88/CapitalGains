# Capital Gains

## 1. Pré-requisitos
- Instale o [.NET SDK 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) em sua máquina.
- Para melhor experiência, utilize o **Visual Studio Code**, **Visual Studio** ou outro editor compatível com .NET.

## 2. Compilando o Projeto
Abra um terminal na raiz do projeto e execute:
```sh
dotnet restore ./CapitalGains.sln      # Baixa as dependências
dotnet build   ./CapitalGains.sln      # Compila a aplicação
```

## 3. Executando a Aplicação
A aplicação principal é um CLI que lê o input via stdin (entrada padrão).

### Execução interativa (linha a linha)
Execute:
```sh
dotnet run --project ./src/1-Presentation/CapitalGains.Console/
```
Digite cada linha de input e pressione `Enter`. Para finalizar a entrada, envie uma linha vazia (apenas pressione `Enter`).

### Execução com redirecionamento de arquivo
```sh
dotnet run --project ./src/1-Presentation/CapitalGains.Console/ < ./tests/5-Tests/CapitalGains.EndToEnd/Inputs/input.case#1.json
```
Você pode substituir o arquivo de entrada pelo caminho desejado.

## 4. Executando a Aplicação Builds conteinerizadas

Execute na raiz do projeto:

- **Suba os containers em background (build automático):**
   ```sh
   docker-compose -f docker-compose.yml up -d
   ```

- **Se container não abrir o terminal, abra um terminal interativo dentro do container da aplicação:**
   ```sh
   docker-compose run --entrypoint /bin/bash capitalgains.console
   ```

- **Dentro do terminal do container, execute a aplicação manualmente:**
   ```sh
   dotnet CapitalGains.Console.dll
   ```

_Execução interativa (linha a linha) você pode digitar o input diretamente no terminal do container, ou redirecionar arquivos conforme desejar._

## 5. Executando os Testes
Para rodar todos os testes (unitários, integração e end-to-end), execute na raiz do projeto:

- Testes unitários (Domain):
  ```sh
  dotnet test ./tests/5-Tests/CapitalGains.UnitTest
  ```
- Testes de integração (Application -> Domain):
  ```sh
  dotnet test ./tests/5-Tests/CapitalGains.IntegrationTests
  ```
- Testes End-to-End (Presentation -> Application -> Domain):
  ```sh
  dotnet test ./tests/5-Tests/CapitalGains.EndToEnd
  ```
- Teste Coverage

   
<img width="440" alt="Image" src="https://github.com/user-attachments/assets/0716023a-1c50-4b06-8655-b0a01ea31571" />

## 6. Observações
- O input deve ser um JSON válido, conforme exemplos na pasta `Inputs`.
```sh
[{ "operation": "buy",  "unit-cost": 10.00, "quantity": 100 },{ "operation": "sell", "unit-cost": 15.00, "quantity": 50 }]
```
- O output será impresso no terminal, um array JSON por linha de cenário processado.
- O programa aceita tanto entrada interativa (linha a linha) quanto redirecionamento de arquivos.
