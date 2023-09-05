# Projeto Consultório: Nível 2
ConsultorioAPI é uma API REST desenvolvida em .NET 7.0 para gerenciar informações sobre médicos, pacientes, e consultas.

### Grupo `I`
- [Helena Luz](https://github.com/helenaluz)
- [Raphael C. Teodoro](https://github.com/raphael-teodoro)

## Endpoints

### Consulta
- `POST /api/Consulta`: Cria uma nova consulta.
- `GET /api/Consulta`: Retorna todas as consultas, com parâmetro opcional para filtrar por data.
- `PUT /api/Consulta/{Id}`: Atualiza todos os dados de uma consulta pelo Id.
- `DELETE /api/Consulta/{Id}`: Remove uma consulta pelo Id.

### Medico
- `POST /api/Medico`: Cria um novo médico.
- `GET /api/Medico`: Retorna todos os médicos, com parâmetro opcional para filtrar por especialidade.
- `PUT /api/Medico/{id}`: Atualiza Telefone e Endereço de um médico pelo Id.
- `PATCH /api/Medico/{id}`: Atualiza Especialidade de um médico pelo Id.
- `GET /api/Medico/{id}/consultas`: Retorna consultas realizadas pelo médico do Id informado.
- `GET /api/Medico/disponiveis`: Retorna médicos disponíveis em determinada data com determinada especialidade.
- `GET /api/Medico/crm/{crm}`: Retorna um médico pelo CRM.

### Paciente
- `POST /api/Paciente`: Cria um novo paciente.
- `GET /api/Paciente`: Retorna toodos os pacientes, com parâmetro opcional para filtrar por idade mínima.
- `GET /api/Paciente/{Id}`: Retorna um paciente pelo Id.
- `PUT /api/Paciente/{Id}`: Atualiza Telefone de um paciente pelo Id.
- `PATCH /api/Paciente/{Id}`: Atualiza Endereço de um paciente pelo Id.
- `GET /api/Paciente/{Id}/consulta`: Retorna consultas feitas pelo paciente do Id informado.
- `GET /api/Paciente/sangue/{tipo}`: Retorna pacientes pelo Tipo Sanguíneo.

## Tecnologias Utilizadas
- .NET 7.0
- Entity Framework Core
- SQL Server
