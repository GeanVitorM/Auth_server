# Documentação da API de Autenticação para Fisioterapia

Esta API é usada para gerenciar usuários, pacientes, fisioterapeutas, coordenadores e administradores.

## Endpoints Disponíveis

### /Usuario

#### GET
Lista todos os usuários.

#### POST
Cria um novo usuário.

#### PUT
Atualiza um usuário existente.

#### DELETE
Deleta um usuário pelo ID.

### /Paciente

#### GET
Lista todos os pacientes.

#### POST
Cria um novo paciente.

#### PUT
Atualiza um paciente existente.

#### DELETE
Deleta um paciente pelo ID.

### PATH
Atualiza o campo primeira consulta

### /Fisioterapeuta

#### GET
Lista todos os fisioterapeutas.

#### POST
Cria um novo fisioterapeuta.

#### PUT
Atualiza um fisioterapeuta existente.

#### DELETE
Deleta um fisioterapeuta pelo ID.

### /Coordenador

#### GET
Lista todos os coordenadores.

#### POST
Cria um novo coordenador.

#### PUT
Atualiza um coordenador existente.

#### DELETE
Deleta um coordenador pelo ID.

### /Admin

#### GET
Lista todos os administradores.

#### POST
Cria um novo administrador.

#### PUT
Atualiza um administrador existente.

#### DELETE
Deleta um administrador pelo ID.

### /Auth/login

#### POST
Realiza o login de um usuário.

#### Formato do JSON de Login
```json
{
  "login": "user@example.com",
  "password": "senha123"
}
```

#### Resposta esperada

## Endpoint: Alteração de Senha

### Método HTTP: POST
URL: /change-password

#### Descrição
Este endpoint permite que um usuário autenticado altere sua senha. O usuário deve fornecer a senha atual e a nova senha. O endpoint verifica a validade da senha atual e, se for válida, substitui-a pela nova senha.

#### Cabeçalhos HTTP
- Content-Type: application/json
- Authorization: Bearer token do usuário autenticado (quando aplicável)

#### Parâmetros

##### Query Parameter
- `userId` (int): O ID do usuário cuja senha está sendo alterada. Este parâmetro deve ser enviado no corpo da requisição.

##### Corpo da Requisição (application/json)
```json
{
  "oldPassword": "SenhaAtual123",
  "newPassword": "NovaSenha456"
}
```

## Controlador: Usuario

### Endpoint: Obter Todos os Usuários

#### Método HTTP: GET
URL: /usuario

#### Descrição
Obtém uma lista de todos os usuários registrados no sistema. Apenas administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "AdminPolicy"

#### Respostas
- `200 OK`: Um array de objetos UsuarioDto.
- `404 Not Found`: "Usuarios not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Obter Usuário por ID

#### Método HTTP: GET
URL: /usuario/{id}

#### Descrição
Obtém os detalhes de um usuário específico com base no ID fornecido. Apenas administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "AdminPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do usuário a ser recuperado.

#### Respostas
- `200 OK`: Um objeto UsuarioDto com os detalhes do usuário.
- `404 Not Found`: "Usuario not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Criar Novo Usuário

#### Método HTTP: POST
URL: /usuario

#### Descrição
Cria um novo usuário no sistema. Este endpoint está disponível para todos, incluindo usuários anônimos.

#### Corpo da Requisição (application/json)
```json
{
  "idUser": 0,
  "username": "user@example.com",
  "password": "string",
  "tipoUsuario": 1
}
```

#### Resposta esperada

#### Respostas
- `201 Created`: O objeto UsuarioDto criado.
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Atualizar Usuário

#### Método HTTP: PUT
URL: /usuario

#### Descrição
Atualiza os detalhes de um usuário existente. Apenas administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Content-Type: application/json
- Authorization: Bearer token com política "AdminPolicy"

#### Corpo da Requisição (application/json)
```json
{
  "idUser": 10,
  "username": "exemplo@example.com",
  "password": "string",
  "tipoUsuario": 1
}
```

#### Resposta esperada

### Endpoint: Excluir Usuário

#### Método HTTP: DELETE
URL: /usuario/{id}

#### Descrição
Exclui um usuário existente do sistema com base no ID fornecido. Apenas administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "AdminPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do usuário a ser excluído.

#### Respostas
- `200 OK`: O objeto UsuarioDto excluído.
- `404 Not Found`: "Usuario not found"
- `500 Internal Server Error`: "Internal server error"

## Controlador: Paciente

### Endpoint: Obter Todos os Pacientes

#### Método HTTP: GET
URL: /paciente

#### Descrição
Obtém uma lista de todos os pacientes registrados no sistema. Apenas fisioterapeutas podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "FisioterapeutaPolicy"

#### Respostas
- `200 OK`: Um array de objetos PacienteDto com detalhes dos pacientes.
- `404 Not Found`: "Pacientes not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Obter Paciente por ID

#### Método HTTP: GET
URL: /paciente/{id}

#### Descrição
Obtém os detalhes de um paciente específico com base no ID fornecido. Apenas fisioterapeutas podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "FisioterapeutaPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do paciente a ser recuperado.

#### Respostas
- `200 OK`: Um objeto PacienteDto com os detalhes do paciente.
- `404 Not Found`: "Pacientes not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Criar Novo Paciente

#### Método HTTP: POST
URL: /paciente

#### Descrição
Cria um novo paciente no sistema. Este endpoint está disponível para todos, incluindo usuários anônimos. Além disso, cria um novo usuário vinculado ao paciente.

#### Cabeçalhos HTTP
- Content-Type: application/json

#### Corpo da Requisição (application/json)
```json
{
  "idPaciente": 0,
  "nomePaciente": "string",
  "cpf": "string",
  "uf": "string",
  "endereco": "string",
  "numeroCasa": "string",
  "dataDeNascimento": "2024-06-12T19:02:34.906Z",
  "sexo": "m",
  "proficao": "string",
  "diagnosticoClinico": "string",
  "diagnosticoFisio": "string",
  "emailPaciente": "TestePaciente@example.com",
  "password" : "passwordTeste",
  "tipoUsuario": 4
}
```

### Endpoint: Atualizar Paciente

#### Método HTTP: PUT
URL: /paciente

#### Descrição
Atualiza os detalhes de um paciente existente. Apenas pacientes autenticados podem acessar este endpoint.

#### Cabeçalhos HTTP
- Content-Type: application/json
- Authorization: Bearer token com política "PacientePolicy"

#### Corpo da Requisição (application/json)
```json
{
  "idPaciente": 10,
  "nomePaciente": "exemplo@example.com",
  "cpf": "12345678900",
  "uf": "SP",
  "endereco": "Rua A, 123",
  "numeroCasa": "1",
  "dataDeNascimento": "1990-01-01T00:00:00Z",
  "sexo": "m",
  "proficao": "Profissão",
  "diagnosticoClinico": "Diagnóstico Clínico",
  "password" : "passwordTeste",
  "diagnosticoFisio": "Diagnóstico Fisioterapêutico",
  "emailPaciente": "exemplo@example.com",
  "tipoUsuario": 4
}
```

#### Respostas
- `200 OK`: O objeto PacienteDto atualizado.
- `404 Not Found`: "Paciente not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Excluir Paciente

#### Método HTTP: DELETE
URL: /paciente/{id}

#### Descrição
Exclui um paciente existente do sistema com base no ID fornecido. Apenas administradores e fisioterapeutas podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "FisioterapeutaPolicy" ou "AdminPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do paciente a ser excluído.

#### Respostas
- `200 OK`: O objeto PacienteDto excluído.
- `404 Not Found`: "Paciente not found"
- `500 Internal Server Error`: "Internal server error"

## Controlador: Fisioterapeuta

### Endpoint: Obter Todos os Fisioterapeutas

#### Método HTTP: GET
URL: /fisioterapeuta

#### Descrição
Obtém uma lista de todos os fisioterapeutas registrados no sistema. Apenas coordenadores e administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "CoordenadorPolicy" ou "AdminPolicy"

#### Respostas
- `200 OK`: Um array de objetos FisioterapeutaDto com detalhes dos fisioterapeutas.
- `404 Not Found`: "Fisioterapeutas not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Obter Fisioterapeuta por ID

#### Método HTTP: GET
URL: /fisioterapeuta/{id}

#### Descrição
Obtém os detalhes de um fisioterapeuta específico com base no ID fornecido. Apenas coordenadores e administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "CoordenadorPolicy" ou "AdminPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do fisioterapeuta a ser recuperado.

#### Respostas
- `200 OK`: Um objeto FisioterapeutaDto com os detalhes do fisioterapeuta.
- `404 Not Found`: "Fisioterapeuta not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Criar Novo Fisioterapeuta

#### Método HTTP: POST
URL: /fisioterapeuta

#### Descrição
Cria um novo fisioterapeuta no sistema. Este endpoint está disponível para todos, incluindo usuários anônimos. Além disso, cria um novo usuário vinculado ao fisioterapeuta.

#### Cabeçalhos HTTP
- Content-Type: application/json

#### Corpo da Requisição (application/json)
```json
{
  "idFisioterapeuta": 0,
  "nomeFisioterapeuta": "string",
  "Semestre": "string",
  "matricula": "string",
  "emailFisioterapeuta": "TesteFisio@example.com",
  "password" : "passwordTeste",
  "tipoUsuario": 2
}
```



### Endpoint: Atualizar Fisioterapeuta

#### Método HTTP: PUT
URL: /fisioterapeuta

#### Descrição
Atualiza os detalhes de um fisioterapeuta existente. Apenas fisioterapeutas autenticados podem acessar este endpoint.

#### Cabeçalhos HTTP
- Content-Type: application/json
- Authorization: Bearer token com política "FisioterapeutaPolicy"

#### Corpo da Requisição (application/json)
```json
{
  "idFisio": 0,
  "nomeFisio": "string",
  "emailFisio": "user@example.com",
  "matricula": 0,
  "semestreFisio": "string",
  "password": "string",
  "tipoUsuario": 3
}
```

#### Respostas
- `200 OK`: O objeto FisioterapeutaDto atualizado.
- `404 Not Found`: "Fisioterapeuta not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Excluir Fisioterapeuta

#### Método HTTP: DELETE
URL: /fisioterapeuta/{id}

#### Descrição
Exclui um fisioterapeuta existente do sistema com base no ID fornecido. Apenas coordenadores e administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "CoordenadorPolicy" ou "AdminPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do fisioterapeuta a ser excluído.

#### Respostas
- `200 OK`: O objeto FisioterapeutaDto excluído.
- `404 Not Found`: "Fisioterapeuta not found"
- `500 Internal Server Error`: "Internal server error"

## Controlador: Coordenador

### Endpoint: Obter Todos os Coordenadores

#### Método HTTP: GET
URL: /coordenador

#### Descrição
Obtém uma lista de todos os coordenadores registrados no sistema. Apenas administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "AdminPolicy"

#### Respostas
- `200 OK`: Um array de objetos CoordenadorDto com detalhes dos coordenadores.
- `404 Not Found`: "Coordenadores not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Obter Coordenador por ID

#### Método HTTP: GET
URL: /coordenador/{id}

#### Descrição
Obtém os detalhes de um coordenador específico com base no ID fornecido. Apenas administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "AdminPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do coordenador a ser recuperado.

#### Respostas
- `200 OK`: Um objeto CoordenadorDto com os detalhes do coordenador.
- `404 Not Found`: "Coordenador not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Criar Novo Coordenador

#### Método HTTP: POST
URL: /coordenador

#### Descrição
Cria um novo coordenador no sistema. Este endpoint está disponível para todos, incluindo usuários anônimos. Além disso, cria um novo usuário vinculado ao coordenador.

#### Cabeçalhos HTTP
- Content-Type: application/json

#### Corpo da Requisição (application/json)
```json
{
  "idCoordenador": 0,
  "nomeCoordenador": "string",
  "emailCoordenador": "user@example.com",
  "password": "string",
  "tipoUsuario": 2
}
```

### Endpoint: Atualizar Coordenador

#### Método HTTP: PUT
URL: /coordenador

#### Descrição
Atualiza os detalhes de um coordenador existente. Apenas coordenadores autenticados podem acessar este endpoint.

#### Cabeçalhos HTTP
- Content-Type: application/json
- Authorization: Bearer token com política "CoordenadorPolicy"

#### Corpo da Requisição (application/json)
```json
{
  "idCoordenador": 0,
  "nomeCoordenador": "string",
  "emailCoordenador": "testeCoord@example.com",
  "password": "string",
  "tipoUsuario": 2
}
```

#### Respostas
- `200 OK`: O objeto CoordenadorDto atualizado.
- `404 Not Found`: "Coordenador not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Excluir Coordenador

#### Método HTTP: DELETE
URL: /coordenador/{id}

#### Descrição
Exclui um coordenador existente do sistema com base no ID fornecido. Apenas administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token

 com política "AdminPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do coordenador a ser excluído.

#### Respostas
- `200 OK`: O objeto CoordenadorDto excluído.
- `404 Not Found`: "Coordenador not found"
- `500 Internal Server Error`: "Internal server error"

## Controlador: Administrador

### Endpoint: Obter Todos os Administradores

#### Método HTTP: GET
URL: /administrador

#### Descrição
Obtém uma lista de todos os administradores registrados no sistema. Apenas outros administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "AdminPolicy"

#### Respostas
- `200 OK`: Um array de objetos AdministradorDto com detalhes dos administradores.
- `404 Not Found`: "Administradores not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Obter Administrador por ID

#### Método HTTP: GET
URL: /administrador/{id}

#### Descrição
Obtém os detalhes de um administrador específico com base no ID fornecido. Apenas outros administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "AdminPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do administrador a ser recuperado.

#### Respostas
- `200 OK`: Um objeto AdministradorDto com os detalhes do administrador.
- `404 Not Found`: "Administrador not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Criar Novo Administrador

#### Método HTTP: POST
URL: /administrador

#### Descrição
Cria um novo administrador no sistema. Este endpoint está disponível para todos, incluindo usuários anônimos. Além disso, cria um novo usuário vinculado ao administrador.

#### Cabeçalhos HTTP
- Content-Type: application/json

#### Corpo da Requisição (application/json)
```json
{
  "idAdmin": 0,
  "nomeAdmin": "string",
  "emailAdmin": "user@example.com",
  "password": "string",
  "tipoUsuario": 1
}
```


### Endpoint: Atualizar Administrador

#### Método HTTP: PUT
URL: /administrador

#### Descrição
Atualiza os detalhes de um administrador existente. Apenas administradores autenticados podem acessar este endpoint.

#### Cabeçalhos HTTP
- Content-Type: application/json
- Authorization: Bearer token com política "AdminPolicy"

#### Corpo da Requisição (application/json)
```json
{
  "idAdmin": 0,
  "nomeAdmin": "string",
  "emailAdmin": "user@example.com",
  "password": "string",
  "tipoUsuario": 1
}
```

#### Respostas
- `200 OK`: O objeto AdministradorDto atualizado.
- `404 Not Found`: "Administrador not found"
- `500 Internal Server Error`: "Internal server error"

### Endpoint: Excluir Administrador

#### Método HTTP: DELETE
URL: /administrador/{id}

#### Descrição
Exclui um administrador existente do sistema com base no ID fornecido. Apenas outros administradores podem acessar este endpoint.

#### Cabeçalhos HTTP
- Authorization: Bearer token com política "AdminPolicy"

#### Parâmetros

##### Path Parameter
- `id` (int): O ID do administrador a ser excluído.

#### Respostas
- `200 OK`: O objeto AdministradorDto excluído.
- `404 Not Found`: "Administrador not found"
- `500 Internal Server Error`: "Internal server error"

## Políticas de Autorização

- `AdminPolicy`: Permite acesso a usuários do tipo administrador.
- `CoordenadorPolicy`: Permite acesso a usuários do tipo coordenador.
- `FisioterapeutaPolicy`: Permite acesso a usuários do tipo fisioterapeuta.

# Models

## PacienteDto
Representa os dados de um paciente no sistema.
```json
{
  "idPaciente": 0,
  "nomePaciente": "string",
  "cpf": "string",
  "uf": "string",
  "endereco": "string",
  "numeroCasa": "string",
  "dataDeNascimento": "2024-06-19T14:44:27.902Z",
  "sexo": "m",
  "proficao": "string",
  "diagnosticoClinico": "string",
  "diagnosticoFisio": "string",
  "primeiraConsulta": true,
  "emailPaciente": "user@example.com",
  "password": "string",
  "tipoUsuario": 1
}
```

## FisioterapeutaDto
Representa os dados de um fisioterapeuta no sistema.
```json
{
  "idFisioterapeuta": 0,
  "nomeFisioterapeuta": "string",
  "Semestre": "string",
  "matricula": "string",
  "emailFisioterapeuta": "TesteFisio@example.com",
  "password" : "passwordTeste",
  "tipoUsuario": 2
}
```

## CoordenadorDto
Representa os dados de um coordenador no sistema.
```json
{
  "idCoordenador": 0,
  "nomeCoordenador": "string",
  "emailCoordenador": "TesteCoord@example.com",
  "password" : "passwordTeste",
  "tipoUsuario": 3
}
```

## AdministradorDto
Representa os dados de um administrador no sistema.
```json
{
  "idAdministrador": 0,
  "nomeAdministrador": "string",
  "emailAdministrador": "TesteAdmin@example.com",
  "password" : "passwordTeste",
  "tipoUsuario": 1
}
```

## UsuarioDto
Representa os dados de um Usuario no sistema.
```json
{
  "idUser": 0,
  "username": "admin@example.com",
  "password": "string",
  "tipoUsuario": 1
}
```
