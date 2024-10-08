# APICrud .NET / C# / EntityFramework Core


Este projeto faz parte do meu processo de aprendizado e consiste em uma API CRUD desenvolvida com .NET, C#, Entity Framework Core e SQLite.

- - **.NET 8.0**
- **C#**
- **Entity Framework Core**
- **SQLite**

## Funcionalidades

- **Criar** um novo registro de estudante
- **Listar** todos os estudantes cadastrados
- **Atualizar** as informações de um estudante
- **Deletar** um estudante da base de dados

## Endpoints

- ``GET /api/estudantes``: Retorna a lista de todos os estudantes.
- ``GET /api/estudantes/{id}``: Retorna um estudante específico pelo ID.
- ``POST /api/estudantes``: Adiciona um novo estudante.
- ``PUT /api/estudantes/{id}``: Atualiza as informações de um estudante.
- ``DELETE /api/estudantes/{id}``: Remove um estudante pelo ID.