# INDT
Projeto de Teste de aptidão C# Dot Net.

O projeto consiste em dois microsserviços para o contexto de seguros... um gerencia a proposta e o outro a contratação.
O microsserviço de contratação consome o microsserviço de propostas.

A execução dos microsserviços pode ser realizada diretamente no browser (HTTP) ou via conteiner docker (utilize o DockerFile de cada projeto.

Os microsserviços utiliza o Sql Server como adaptador de persistência.

Para instalar o Sql Server em um conteiner docker, é necessário ja ter o Docker Desktop instalado.
A seguir, execute este comandos abaixo:


docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SuaSenhaForte@123" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest;


winget install sqlcmd;


sqlcmd -S localhost -U sa -P "SuaSenhaForte@123";


CREATE DATABASE seguros;
GO

SELECT name FROM sys.databases;
GO


a partir daqui, basta executar os microsserviços.
