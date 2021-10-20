--CRIANDO BANCO DE DADOS 
IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'OperDay'))
CREATE DATABASE OperDay
GO

--SETANDO BANCO DE DADOS
USE OperDay
GO

BEGIN TRANSACTION

--DECLARANDO TABELA CONTA
CREATE TABLE Conta (
  id_Conta INT IDENTITY(1,1) NOT NULL,
  corr_Conta VARCHAR(45) NOT NULL,
  sts_Conta VARCHAR(1) NOT NULL,
  atv_Conta VARCHAR(10) NOT NULL,
  CONSTRAINT PK_id_Conta PRIMARY KEY NONCLUSTERED (id_Conta))
  GO
  IF @@ERROR <> 0
  IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
  GO

--DECLARANDO TABELA EMPRESA
CREATE TABLE Empresa (
  id_Empresa INT IDENTITY(1,1) NOT NULL,
  nm_Empresa VARCHAR(45) NOT NULL,
  cnpj_Empresa VARCHAR(18) NOT NULL,
  CONSTRAINT PK_id_Empresa PRIMARY KEY NONCLUSTERED (id_Empresa))
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO


--DECLARANDO TABELA ACTION
CREATE TABLE Acao(
  id_Action INT IDENTITY(1,1) NOT NULL,
  nm_Action VARCHAR(7) NOT NULL,
  Empresa_id_Empresa INT NOT NULL,
  CONSTRAINT PK_id_Action PRIMARY KEY (id_Action),
  CONSTRAINT FK_id_Empresa FOREIGN KEY (Empresa_id_Empresa) REFERENCES Empresa (id_Empresa))
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--DECLARANDO TABELA OPERATION
CREATE TABLE Operation (
  id_Operation INT IDENTITY(1,1) NOT NULL,
  date_Operation DATE,
  vlr_Operation DECIMAL(10,2) NOT NULL,
  Conta_id_Conta INT NOT NULL,
  Acao_id_Acao INT NOT NULL,
  tp_Operation INT NOT NULL,
  CONSTRAINT PK_id_Operation PRIMARY KEY (id_Operation, Conta_id_Conta, Acao_id_Acao),
  CONSTRAINT FK_id_Conta FOREIGN KEY (Conta_id_Conta) REFERENCES Conta (id_Conta),
  CONSTRAINT FK_id_Acao FOREIGN KEY (Acao_id_Acao) REFERENCES Acao (id_Action)
  )
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURES TABELA OPERATION

--PROCEDURE INSERCAO
CREATE PROCEDURE sp_InserirOperacao
@dateOperation AS DATETIME,
@vlrOperation AS DECIMAL(10,2),
@idConta AS INT,
@idAcao AS INT,
@tpOperation AS INT
AS
IF @tpOperation = 1
SET @vlrOperation = (@vlrOperation * -1)
IF @tpOperation = 0
SET @vlrOperation = ABS(@vlrOperation)
INSERT INTO Operation VALUES (@dateOperation, @vlrOperation, @idConta, @idAcao, @tpOperation)
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE EXCLUSAO
CREATE PROCEDURE sp_ExcluirOperacao
@idOperation AS INT
AS
IF EXISTS (SELECT * FROM Operation WHERE id_Operation = @idOperation)
DELETE FROM Operation WHERE id_Operation = @idOperation
ELSE
THROW 50001, N'Operação não encontrada! Verifique o número de ID', 1
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE CONSULTA
CREATE PROCEDURE sp_ConsultarOperacao
@idOperation AS INT
AS
IF @idOperation = 0 
SET @idOperation = (SELECT MAX(id_Operation) FROM Operation)
IF NOT EXISTS (SELECT * FROM Operation WHERE id_Operation = @idOperation)
THROW 50001, N'Operação não encontrada!', 1
ELSE
SELECT * FROM Operation WHERE id_Operation = @idOperation
IF NOT EXISTS (SELECT * FROM Operation WHERE id_Operation = @idOperation)
THROW 50001, N'Operação não encontrada!', 1
ELSE
SELECT * FROM Operation WHERE id_Operation = @idOperation
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE EDICAO
CREATE PROCEDURE sp_EditarOperacao
@idOperation AS INT,
@dateOperation AS DATETIME,
@vlrOperation AS DECIMAL(10,2),
@idConta AS INT,
@idAcao AS INT,
@tpOperation AS INT
AS
IF @tpOperation = 1
SET @vlrOperation = (@vlrOperation * -1)
IF @tpOperation = 0
SET @vlrOperation = ABS(@vlrOperation)
UPDATE Operation SET date_Operation = @dateOperation, vlr_Operation = @vlrOperation ,Conta_id_Conta = @idConta, Acao_id_Acao = @idAcao, tp_Operation = @tpOperation WHERE id_Operation = @idOperation
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE CONSULTA GRIDVIEW
CREATE PROCEDURE sp_GridOperacao
AS
SELECT TOP 1000 O.id_Operation AS 'ID', O.date_Operation AS 'DATA', O.vlr_Operation AS 'VALOR', C.corr_Conta AS 'CORRETORA', A.nm_Action AS 'NOME AÇÃO', E.nm_Empresa AS 'EMPRESA' FROM Operation O
INNER JOIN Conta C
ON O.Conta_id_Conta = C.id_Conta
INNER JOIN Acao A
ON O.Acao_id_Acao = A.id_Action
INNER JOIN Empresa E
ON A.Empresa_id_Empresa = E.id_Empresa
ORDER BY O.id_Operation DESC
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE GERACAO RELATORIO TOTAL
CREATE PROCEDURE sp_GerarRelatorioOperacaoAll
@dateOperationInit AS DATETIME,
@dateOperationFins AS DATETIME
AS
DECLARE 
@totalOperacaoCompra AS DECIMAL(10,2),
@totalOperacaoVenda AS DECIMAL(10,2),
@totalOperacao AS DECIMAL(10,2)
SET @totalOperacaoCompra = (SELECT SUM(vlr_Operation) FROM Operation
WHERE tp_Operation = 0 AND date_Operation >= @dateOperationInit AND date_Operation <= @dateOperationFins)
SET @totalOperacaoVenda = (SELECT SUM(vlr_Operation) FROM Operation
WHERE tp_Operation = 1 AND date_Operation >= @dateOperationInit AND date_Operation <= @dateOperationFins)
IF ABS(@totalOperacaoCompra) < ABS(@totalOperacaoVenda) 
SET @totalOperacao = (-1*(@totalOperacaoCompra) - @totalOperacaoVenda)
IF ABS(@totalOperacaoCompra) > ABS(@totalOperacaoVenda)
SET @totalOperacao = (-1*(@totalOperacaoCompra + @totalOperacaoVenda))
IF ABS(@totalOperacaoCompra) = ABS(@totalOperacaoVenda)
SET @totalOperacao = 0.00
SELECT O.id_Operation AS 'ID', O.date_Operation AS 'DATA', O.vlr_Operation AS 'VALOR', C.corr_Conta AS 'CORRETORA', A.nm_Action AS 'NOME AÇÃO', E.nm_Empresa AS 'EMPRESA', E.cnpj_Empresa AS 'CNPJ', @totalOperacao AS 'TOTALIZADOR' FROM Operation O
INNER JOIN Conta C
ON O.Conta_id_Conta = C.id_Conta
INNER JOIN Acao A
ON O.Acao_id_Acao = A.id_Action
INNER JOIN Empresa E
ON E.id_Empresa = A.Empresa_id_Empresa
WHERE O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins
ORDER BY O.id_Operation DESC
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE GERACAO RELATORIO POR CONTA
CREATE PROCEDURE sp_GerarRelatorioOperacaoConta
@dateOperationInit AS DATETIME,
@dateOperationFins AS DATETIME,
@idConta AS INT
AS
DECLARE 
@totalOperacaoCompra AS DECIMAL(10,2),
@totalOperacaoVenda AS DECIMAL(10,2),
@totalOperacao AS DECIMAL(10,2)
SET @totalOperacaoCompra = (SELECT SUM(vlr_Operation) FROM Operation O
INNER JOIN Conta C
ON @idConta = O.Conta_id_Conta AND @idConta = C.id_Conta
WHERE tp_Operation = 0 AND Conta_id_Conta = @idConta AND  O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins)
SET @totalOperacaoVenda = (SELECT SUM(vlr_Operation) FROM Operation O
INNER JOIN Conta C
ON @idConta = O.Conta_id_Conta AND @idConta = C.id_Conta
WHERE tp_Operation = 1 AND Conta_id_Conta = @idConta AND O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins)
IF ABS(@totalOperacaoCompra) < ABS(@totalOperacaoVenda) 
SET @totalOperacao = (-1*(@totalOperacaoCompra) - @totalOperacaoVenda)
IF ABS(@totalOperacaoCompra) > ABS(@totalOperacaoVenda)
SET @totalOperacao = (-1*(@totalOperacaoCompra + @totalOperacaoVenda))
IF ABS(@totalOperacaoCompra) = ABS(@totalOperacaoVenda)
SET @totalOperacao = 0.00
SELECT O.id_Operation AS 'ID', O.date_Operation AS 'DATA', O.vlr_Operation AS 'VALOR', C.corr_Conta AS 'CORRETORA', A.nm_Action AS 'NOME AÇÃO', E.nm_Empresa AS 'EMPRESA' , E.cnpj_Empresa AS 'CNPJ', @totalOperacao AS 'TOTALIZADOR' FROM Operation O
INNER JOIN Conta C
ON @idConta = O.Conta_id_Conta AND @idConta = C.id_Conta
INNER JOIN Acao A
ON O.Acao_id_Acao = A.id_Action
INNER JOIN Empresa E
ON E.id_Empresa = A.Empresa_id_Empresa
WHERE O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins
ORDER BY O.id_Operation DESC
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE GERACAO RELATORIO POR ACAO
CREATE PROCEDURE sp_GerarRelatorioOperacaoAcao
@dateOperationInit AS DATETIME,
@dateOperationFins AS DATETIME,
@idAcao AS INT
AS
DECLARE 
@totalOperacaoCompra AS DECIMAL(10,2),
@totalOperacaoVenda AS DECIMAL(10,2),
@totalOperacao AS DECIMAL(10,2)
SET @totalOperacaoCompra = (SELECT SUM(vlr_Operation) FROM Operation O
INNER JOIN Acao A
ON @idAcao = O.Acao_id_Acao AND @idAcao = A.id_Action
INNER JOIN Empresa E
ON E.id_Empresa = A.Empresa_id_Empresa
WHERE tp_Operation = 0 AND Acao_id_Acao = @idAcao AND E.id_Empresa = A.Empresa_id_Empresa AND O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins)
SET @totalOperacaoVenda = (SELECT SUM(vlr_Operation) FROM Operation O
INNER JOIN Acao A
ON @idAcao = O.Acao_id_Acao AND @idAcao = A.id_Action
INNER JOIN Empresa E
ON E.id_Empresa = A.Empresa_id_Empresa
WHERE tp_Operation = 1 AND Acao_id_Acao = @idAcao AND E.id_Empresa = A.Empresa_id_Empresa AND O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins)
IF ABS(@totalOperacaoCompra) < ABS(@totalOperacaoVenda) 
SET @totalOperacao = (-1*(@totalOperacaoCompra) - @totalOperacaoVenda)
IF ABS(@totalOperacaoCompra) > ABS(@totalOperacaoVenda)
SET @totalOperacao = (-1*(@totalOperacaoCompra + @totalOperacaoVenda))
IF ABS(@totalOperacaoCompra) = ABS(@totalOperacaoVenda)
SET @totalOperacao = 0.00
SELECT O.id_Operation AS 'ID', O.date_Operation AS 'DATA', O.vlr_Operation AS 'VALOR', C.corr_Conta AS 'CORRETORA', A.nm_Action AS 'NOME AÇÃO', E.nm_Empresa AS 'EMPRESA' , E.cnpj_Empresa AS 'CNPJ', @totalOperacao AS 'TOTALIZADOR' FROM Operation O
INNER JOIN Conta C
ON O.Conta_id_Conta = C.id_Conta
INNER JOIN Acao A
ON @idAcao = O.Acao_id_Acao AND @idAcao = A.id_Action
INNER JOIN Empresa E
ON E.id_Empresa = A.Empresa_id_Empresa
WHERE O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins
ORDER BY O.id_Operation DESC
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE GERACAO RELATORIO POR CONTA E ACAO
CREATE PROCEDURE sp_GerarRelatorioOperacao
@dateOperationInit AS DATETIME,
@dateOperationFins AS DATETIME,
@idConta AS INT,
@idAcao AS INT
AS
DECLARE
@totalOperacaoCompra AS DECIMAL(10,2),
@totalOperacaoVenda AS DECIMAL(10,2),
@totalOperacao AS DECIMAL(10,2)
SET @totalOperacaoCompra = (SELECT SUM(vlr_Operation) FROM Operation O
INNER JOIN Conta C
ON @idConta = O.Conta_id_Conta AND @idConta = C.id_Conta
INNER JOIN Acao A
ON @idAcao = O.Acao_id_Acao AND @idAcao = A.id_Action
INNER JOIN Empresa E
ON E.id_Empresa = A.Empresa_id_Empresa
WHERE tp_Operation = 0 AND Acao_id_Acao = @idAcao AND Conta_id_Conta = @idConta AND E.id_Empresa = A.Empresa_id_Empresa AND O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins)
SET @totalOperacaoVenda = (SELECT SUM(vlr_Operation) FROM Operation O
INNER JOIN Conta C
ON @idConta = O.Conta_id_Conta AND @idConta = C.id_Conta
INNER JOIN Acao A
ON @idAcao = O.Acao_id_Acao AND @idAcao = A.id_Action
INNER JOIN Empresa E
ON E.id_Empresa = A.Empresa_id_Empresa
WHERE tp_Operation = 1 AND Acao_id_Acao = @idAcao AND Conta_id_Conta = @idConta AND E.id_Empresa = A.Empresa_id_Empresa AND O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins)
IF ABS(@totalOperacaoCompra) < ABS(@totalOperacaoVenda) 
SET @totalOperacao = (-1*(@totalOperacaoCompra) - @totalOperacaoVenda)
IF ABS(@totalOperacaoCompra) > ABS(@totalOperacaoVenda)
SET @totalOperacao = (-1*(@totalOperacaoCompra + @totalOperacaoVenda))
IF ABS(@totalOperacaoCompra) = ABS(@totalOperacaoVenda)
SET @totalOperacao = 0.00
SELECT O.id_Operation AS 'ID', O.date_Operation AS 'DATA', O.vlr_Operation AS 'VALOR', C.corr_Conta AS 'CORRETORA', A.nm_Action AS 'NOME AÇÃO', E.nm_Empresa AS 'EMPRESA', E.cnpj_Empresa AS 'CNPJ', @totalOperacao AS 'TOTALIZADOR' FROM Operation O
INNER JOIN Conta C
ON @idConta = O.Conta_id_Conta AND @idConta = C.id_Conta
INNER JOIN Acao A
ON @idAcao = O.Acao_id_Acao AND @idAcao = A.id_Action
INNER JOIN Empresa E
ON E.id_Empresa = A.Empresa_id_Empresa
WHERE O.date_Operation >= @dateOperationInit AND O.date_Operation <= @dateOperationFins
ORDER BY O.id_Operation DESC
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURES TABELA CONTA

--PROCEDURE INSERCAO
CREATE PROCEDURE sp_InserirConta
@corrConta AS VARCHAR(45),
@stsConta AS VARCHAR(1),
@atvConta AS VARCHAR(10)
AS
INSERT INTO Conta VALUES (@corrConta, @stsConta, @atvConta)
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE EXCLUSAO
CREATE PROCEDURE sp_ExcluirConta
@idConta AS INT
AS
IF EXISTS (SELECT * FROM Conta WHERE id_Conta = @idConta)
DELETE FROM Conta WHERE id_Conta = @idConta
ELSE
THROW 50001, N'Conta não encontrada! Verifique o número de ID', 1
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE CONSULTA
CREATE PROCEDURE sp_ConsultarConta
@idConta AS INT
AS
IF @idConta = 0 
SET @idConta = (SELECT MAX(id_Conta) FROM Conta)
IF NOT EXISTS (SELECT * FROM Conta WHERE id_Conta = @idConta)
THROW 50001, N'Conta não encontrada!', 1
ELSE
SELECT * FROM Conta WHERE id_Conta = @idConta
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE EDICAO
CREATE PROCEDURE sp_EditarConta
@idConta AS INT,
@corrConta AS VARCHAR(45),
@stsConta AS VARCHAR(1),
@atvConta AS VARCHAR(10)
AS
UPDATE Conta SET corr_Conta = @corrConta, sts_Conta = @stsConta, atv_Conta = @atvConta WHERE id_Conta = @idConta
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE CONSULTA GRIDVIEW
CREATE PROCEDURE sp_GridConta
AS
SELECT id_Conta AS 'ID', corr_Conta AS 'CORRETORA', atv_Conta AS 'ATIVO', 'STATUS' = CASE
WHEN sts_Conta = 0 THEN 'ATIVA'
WHEN sts_Conta = 1 THEN 'INATIVA'
END
FROM Conta
ORDER BY corr_Conta DESC
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURES TABELA EMPRESA

--PROCEDURE INSERCAO
CREATE PROCEDURE sp_InserirEmpresa
@nmEmpresa AS VARCHAR(45),
@cnpjEmpresa AS VARCHAR(18)
AS
INSERT INTO Empresa VALUES (@nmEmpresa, @cnpjEmpresa)
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE EXCLUSAO
CREATE PROCEDURE sp_ExcluirEmpresa
@idEmpresa AS INT
AS
IF EXISTS (SELECT * FROM Empresa WHERE id_Empresa = @idEmpresa)
DELETE FROM Empresa WHERE id_Empresa = @idEmpresa
ELSE
THROW 50001, N'Empresa não encontrada! Verifique o número de ID', 1
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE CONSULTA
CREATE PROCEDURE sp_ConsultarEmpresa
@idEmpresa AS INT
AS
IF @idEmpresa = 0 
SET @idEmpresa = (SELECT MAX(id_Empresa) FROM Empresa)
IF NOT EXISTS (SELECT * FROM Empresa WHERE id_Empresa = @idEmpresa)
THROW 50001, N'Empresa não encontrada!', 1
ELSE
SELECT * FROM Empresa WHERE id_Empresa = @idEmpresa
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE EDICAO
CREATE PROCEDURE sp_EditarEmpresa
@idEmpresa AS INT,
@nmEmpresa AS VARCHAR(45),
@cnpjEmpresa AS VARCHAR(18)
AS
UPDATE Empresa SET nm_Empresa = @nmEmpresa, cnpj_Empresa = @cnpjEmpresa WHERE id_Empresa = @idEmpresa
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE CONSULTA GRIDVIEW
CREATE PROCEDURE sp_GridEmpresa
AS
SELECT id_Empresa AS 'ID', nm_Empresa AS 'NOME', cnpj_Empresa AS 'CNPJ' FROM Empresa
ORDER BY nm_Empresa DESC
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURES TABELA ACAO

--PROCEDURE INSERCAO
CREATE PROCEDURE sp_InserirAcao
@nmAction AS VARCHAR(7),
@idEmpresa AS INT
AS
INSERT INTO Acao VALUES (@nmAction, @idEmpresa)
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE EXCLUSAO
CREATE PROCEDURE sp_ExcluirAcao
@idAction AS INT
AS
IF EXISTS (SELECT * FROM Acao WHERE id_Action = @idAction)
DELETE FROM Acao WHERE id_Action = @idAction
ELSE
THROW 50001, N'Ação não encontrada! Verifique o número de ID', 1
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE CONSULTA
CREATE PROCEDURE sp_ConsultarAcao
@idAction AS INT
AS
IF @idAction = 0 
SET @idAction = (SELECT MAX(id_Action) FROM Acao)
IF NOT EXISTS (SELECT * FROM Acao WHERE id_Action = @idAction)
THROW 50001, N'Ação não encontrada!', 1
ELSE
SELECT * FROM Acao WHERE id_Action = @idAction
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE EDICAO
CREATE PROCEDURE sp_EditarAcao
@idAction AS INT,
@nmAction AS VARCHAR(7),
@idEmpresa AS INT
AS
UPDATE Acao SET nm_Action = @nmAction, Empresa_id_Empresa = @idEmpresa WHERE id_Action = @idAction
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

--PROCEDURE CONSULTA GRIDVIEW
CREATE PROCEDURE sp_GridAcao
AS
SELECT A.id_Action AS 'ID', A.nm_Action AS 'NOME', E.nm_Empresa AS 'NOME EMPRESA', E.cnpj_Empresa AS 'CNPJ EMPRESA' FROM Acao A
INNER JOIN Empresa E
ON A.Empresa_id_Empresa = E.id_Empresa
ORDER BY nm_Action DESC
GO
IF @@ERROR <> 0
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
GO

IF @@ERROR = 0
IF @@TRANCOUNT > 0 COMMIT TRANSACTION
GO
--FIM