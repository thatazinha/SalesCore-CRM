-- ============================================================
-- POPULAÇÃO DE DADOS - SalesCore CRM
-- Microsoft SQL Server
-- Gera pelo menos 500 registros em CADA tabela.
-- Executar após o script de criação das tabelas.
-- ============================================================

USE SalesCoreCRM_Manha;
GO

SET NOCOUNT ON;
GO

-- ============================================================
-- 1. EMPRESA - 500 registros
-- ============================================================
DECLARE @i INT = 1;

WHILE @i <= 500
BEGIN
    INSERT INTO Empresa
        (razaoSocial, nomeFantasia, cnpj, email, telefone, dataCadastro, ativo)
    VALUES
        (
            CONCAT(N'Empresa ', @i, N' Ltda.'),
            CONCAT(N'Empresa ', @i),
            RIGHT('00000000000000' + CAST(@i AS VARCHAR(14)), 14),
            CONCAT('contato', @i, '@empresa.com.br'),
            CONCAT('11', RIGHT('0000000000' + CAST(@i AS VARCHAR(10)), 10)),
            DATEADD(DAY, -@i, SYSDATETIME()),
            CASE WHEN @i % 10 = 0 THEN 0 ELSE 1 END
        );

    SET @i += 1;
END;
GO

-- ============================================================
-- 2. CONTATO - 500 registros
-- Cada contato é associado a uma empresa existente.
-- ============================================================
DECLARE @i INT = 1;

WHILE @i <= 500
BEGIN
    INSERT INTO Contato
        (nome, email, telefone, cargo, ativo, idEmpresa)
    VALUES
        (
            CONCAT(N'Contato ', @i),
            CONCAT('contato', @i, '@empresa.com.br'),
            CONCAT('11', RIGHT('0000000000' + CAST(@i AS VARCHAR(10)), 10)),
            CASE @i % 5
                WHEN 0 THEN N'Diretor'
                WHEN 1 THEN N'Gerente'
                WHEN 2 THEN N'Analista'
                WHEN 3 THEN N'Coordenador'
                ELSE N'Assistente'
            END,
            CASE WHEN @i % 12 = 0 THEN 0 ELSE 1 END,
            @i
        );

    SET @i += 1;
END;
GO

-- ============================================================
-- 3. ETAPA - 500 registros
-- A estrutura permite demonstrar grande volume de dados.
-- Os primeiros registros representam etapas comerciais clássicas;
-- os demais são etapas de teste.
-- ============================================================
DECLARE @i INT = 1;

WHILE @i <= 500
BEGIN
    INSERT INTO Etapa
        (nome, ordem, percentualProbabilidade, ativo)
    VALUES
        (
            CASE @i
                WHEN 1 THEN N'Prospecção'
                WHEN 2 THEN N'Qualificação'
                WHEN 3 THEN N'Contato Inicial'
                WHEN 4 THEN N'Levantamento de Necessidades'
                WHEN 5 THEN N'Proposta'
                WHEN 6 THEN N'Negociação'
                WHEN 7 THEN N'Fechamento'
                ELSE CONCAT(N'Etapa de Teste ', @i)
            END,
            @i,
            CAST(((@i - 1) % 101) AS DECIMAL(5,2)),
            CASE WHEN @i % 20 = 0 THEN 0 ELSE 1 END
        );

    SET @i += 1;
END;
GO

-- ============================================================
-- 4. USUARIO - 500 registros
-- ============================================================
DECLARE @i INT = 1;

WHILE @i <= 500
BEGIN
    INSERT INTO Usuario
        (nome, email, cargo, ativo)
    VALUES
        (
            CONCAT(N'Usuário ', @i),
            CONCAT('usuario', @i, '@salescore.com.br'),
            CASE @i % 6
                WHEN 0 THEN N'Administrador'
                WHEN 1 THEN N'Vendedor'
                WHEN 2 THEN N'Gerente de Vendas'
                WHEN 3 THEN N'Analista Comercial'
                WHEN 4 THEN N'Pré-vendas'
                ELSE N'Consultor Comercial'
            END,
            CASE WHEN @i % 15 = 0 THEN 0 ELSE 1 END
        );

    SET @i += 1;
END;
GO

-- ============================================================
-- 5. OPORTUNIDADE - 500 registros
-- Os IDs utilizados nas FKs já existem nas tabelas anteriores.
-- ============================================================
DECLARE @i INT = 1;

WHILE @i <= 500
BEGIN
    INSERT INTO Oportunidade
        (
            titulo,
            descricao,
            valorEstimado,
            dataAbertura,
            dataPrevisaoFechamento,
            idEmpresa,
            idEtapa,
            idUsuarioResponsavel
        )
    VALUES
        (
            CONCAT(N'Oportunidade ', @i),
            CONCAT(N'Oportunidade comercial gerada para demonstração de dados - registro ', @i),
            CAST((1000 + (@i * 137.50)) AS DECIMAL(18,2)),
            DATEADD(DAY, -(@i % 365), CAST(GETDATE() AS DATE)),
            DATEADD(DAY, 30 + (@i % 180),
                    DATEADD(DAY, -(@i % 365), CAST(GETDATE() AS DATE))),
            ((@i - 1) % 500) + 1,
            ((@i - 1) % 500) + 1,
            ((@i - 1) % 500) + 1
        );

    SET @i += 1;
END;
GO

-- ============================================================
-- 6. ATIVIDADE - 500 registros
-- ============================================================
DECLARE @i INT = 1;

WHILE @i <= 500
BEGIN
    INSERT INTO Atividade
        (
            tipo,
            assunto,
            descricao,
            dataAtividade,
            situacao,
            idOportunidade,
            idUsuarioResponsavel
        )
    VALUES
        (
            CASE @i % 5
                WHEN 0 THEN N'Ligação'
                WHEN 1 THEN N'E-mail'
                WHEN 2 THEN N'Reunião'
                WHEN 3 THEN N'Demonstração'
                ELSE N'Follow-up'
            END,
            CONCAT(N'Atividade comercial ', @i),
            CONCAT(N'Atividade gerada para demonstração do sistema - registro ', @i),
            DATEADD(HOUR, -(@i * 3), SYSDATETIME()),
            CASE @i % 4
                WHEN 0 THEN N'Concluída'
                WHEN 1 THEN N'Agendada'
                WHEN 2 THEN N'Em andamento'
                ELSE N'Cancelada'
            END,
            ((@i - 1) % 500) + 1,
            ((@i - 1) % 500) + 1
        );

    SET @i += 1;
END;
GO

-- ============================================================
-- CONFERÊNCIA DA QUANTIDADE DE REGISTROS
-- ============================================================
SELECT 'Empresa' AS Tabela, COUNT(*) AS Quantidade FROM Empresa
UNION ALL
SELECT 'Contato', COUNT(*) FROM Contato
UNION ALL
SELECT 'Etapa', COUNT(*) FROM Etapa
UNION ALL
SELECT 'Usuario', COUNT(*) FROM Usuario
UNION ALL
SELECT 'Oportunidade', COUNT(*) FROM Oportunidade
UNION ALL
SELECT 'Atividade', COUNT(*) FROM Atividade;
GO
