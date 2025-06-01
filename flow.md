Arquivo de entrada
      ↓
NormalizeJsonList
      ↓
Desserialização (List<List<Operation>>)
      ↓
Para cada cenário:
    Para cada operação:
        Atualiza carteira, calcula preço médio, calcula imposto
      ↓
Gera lista de {"tax": valor}
      ↓
Serializa resultado
      ↓
Output final (um array por cenário, separados por linha)