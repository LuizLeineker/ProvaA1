# Sistema Folhas de pagamentos
O sistema permite cadastrar folhas de pagamentos dos funcionarios.  

# Objetivo
Trabalho foi feita para obter nota parcial na matéria de Desenvolvimento de Software Visual do professor Diogo Deconto.   
Foi usado conceitos de bancos de dados e crud.
## 🛠️ Ferramentas 
Para visualização e execução do projeto instale em sua máquina a seguinte ferramenta: 
- [VSCode](https://code.visualstudio.com/download)
- [NodeJS](https://nodejs.org/en)
- [.NET](https://dotnet.microsoft.com/en-us/download)


## ⚙️ Backend
Na pasta Luiz, execute no terminal:
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```
### Connection String
No arquivo `appsettings.json`, arrume a string de acordo com o banco.

### Banco de dados
Na pasta da api, crie as migrations:
```bash
dotnet ef migrations add InitialCreate
```
Banco de dados:
```bash
dotnet ef database update
```
### RUN
```bash
dotnet RUN
```

