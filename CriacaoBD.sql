-- ============================================================
-- SalesCore CRM
-- Modelo Entidade-Relacionamento
-- Microsoft SQL Server
-- ============================================================

CREATE DATABASE SalesCoreCRM_Manha;

USE SalesCoreCRM_Manha;

-- ============================================================
-- 1. TABELA EMPRESA
-- ============================================================

CREATE TABLE Empresa
(
    idEmpresa INT IDENTITY(1,1) NOT NULL,
    razaoSocial NVARCHAR(150) NOT NULL,
    nomeFantasia NVARCHAR(150) NULL,
    cnpj VARCHAR(14) NULL,
    email NVARCHAR(150) NULL,
    telefone VARCHAR(20) NULL,
    dataCadastro DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ativo BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_Empresa
        PRIMARY KEY (idEmpresa),

    CONSTRAINT UQ_Empresa_CNPJ
        UNIQUE (cnpj)
);


-- ============================================================
-- 2. TABELA CONTATO
-- Relacionamento: EMPRESA 1:N CONTATO
-- ============================================================

CREATE TABLE Contato
(
    idContato INT IDENTITY(1,1) NOT NULL,
    nome NVARCHAR(150) NOT NULL,
    email NVARCHAR(150) NULL,
    telefone VARCHAR(20) NULL,
    cargo NVARCHAR(100) NULL,
    ativo BIT NOT NULL DEFAULT 1,

    -- FK adicionada para representar o relacionamento
    -- 1:N entre Empresa e Contato
    idEmpresa INT NOT NULL,

    CONSTRAINT PK_Contato
        PRIMARY KEY (idContato),

    CONSTRAINT FK_Contato_Empresa
        FOREIGN KEY (idEmpresa)
        REFERENCES Empresa(idEmpresa)
);


-- ============================================================
-- 3. TABELA ETAPA
-- ============================================================

CREATE TABLE Etapa
(
    idEtapa INT IDENTITY(1,1) NOT NULL,
    nome NVARCHAR(100) NOT NULL,
    ordem INT NOT NULL,
    percentualProbabilidade DECIMAL(5,2) NOT NULL,
    ativo BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_Etapa
        PRIMARY KEY (idEtapa),

    CONSTRAINT CK_Etapa_Percentual
        CHECK (
            percentualProbabilidade >= 0
            AND percentualProbabilidade <= 100
        )
);


-- ============================================================
-- 4. TABELA USUARIO
-- ============================================================

CREATE TABLE Usuario
(
    idUsuario INT IDENTITY(1,1) NOT NULL,
    nome NVARCHAR(150) NOT NULL,
    email NVARCHAR(150) NOT NULL,
    cargo NVARCHAR(100) NULL,
    ativo BIT NOT NULL DEFAULT 1,

    CONSTRAINT PK_Usuario
        PRIMARY KEY (idUsuario),

    CONSTRAINT UQ_Usuario_Email
        UNIQUE (email)
);


-- ============================================================
-- 5. TABELA OPORTUNIDADE
--
-- Relacionamentos:
-- EMPRESA 1:N OPORTUNIDADE
-- ETAPA 1:N OPORTUNIDADE
-- USUARIO 1:N OPORTUNIDADE
-- ============================================================

CREATE TABLE Oportunidade
(
    idOportunidade INT IDENTITY(1,1) NOT NULL,
    titulo NVARCHAR(200) NOT NULL,
    descricao NVARCHAR(MAX) NULL,
    valorEstimado DECIMAL(18,2) NULL,
    dataAbertura DATE NOT NULL,
    dataPrevisaoFechamento DATE NULL,

    idEmpresa INT NOT NULL,
    idEtapa INT NOT NULL,
    idUsuarioResponsavel INT NOT NULL,

    CONSTRAINT PK_Oportunidade
        PRIMARY KEY (idOportunidade),

    CONSTRAINT FK_Oportunidade_Empresa
        FOREIGN KEY (idEmpresa)
        REFERENCES Empresa(idEmpresa),

    CONSTRAINT FK_Oportunidade_Etapa
        FOREIGN KEY (idEtapa)
        REFERENCES Etapa(idEtapa),

    CONSTRAINT FK_Oportunidade_Usuario
        FOREIGN KEY (idUsuarioResponsavel)
        REFERENCES Usuario(idUsuario),

    CONSTRAINT CK_Oportunidade_Valor
        CHECK (valorEstimado IS NULL OR valorEstimado >= 0),

    CONSTRAINT CK_Oportunidade_Datas
        CHECK (
            dataPrevisaoFechamento IS NULL
            OR dataPrevisaoFechamento >= dataAbertura
        )
);


-- ============================================================
-- 6. TABELA ATIVIDADE
--
-- Relacionamentos:
-- OPORTUNIDADE 1:N ATIVIDADE
-- USUARIO 1:N ATIVIDADE
-- ============================================================

CREATE TABLE Atividade
(
    idAtividade INT IDENTITY(1,1) NOT NULL,
    tipo NVARCHAR(50) NOT NULL,
    assunto NVARCHAR(200) NOT NULL,
    descricao NVARCHAR(MAX) NULL,
    dataAtividade DATETIME2 NOT NULL,
    situacao NVARCHAR(50) NOT NULL,

    idOportunidade INT NOT NULL,
    idUsuarioResponsavel INT NOT NULL,

    CONSTRAINT PK_Atividade
        PRIMARY KEY (idAtividade),

    CONSTRAINT FK_Atividade_Oportunidade
        FOREIGN KEY (idOportunidade)
        REFERENCES Oportunidade(idOportunidade),

    CONSTRAINT FK_Atividade_Usuario
        FOREIGN KEY (idUsuarioResponsavel)
        REFERENCES Usuario(idUsuario)
);


-- ============================================================
-- ÍNDICES PARA AS CHAVES ESTRANGEIRAS
-- ============================================================

CREATE INDEX IX_Contato_idEmpresa
    ON Contato(idEmpresa);

CREATE INDEX IX_Oportunidade_idEmpresa
    ON Oportunidade(idEmpresa);

CREATE INDEX IX_Oportunidade_idEtapa
    ON Oportunidade(idEtapa);

CREATE INDEX IX_Oportunidade_idUsuarioResponsavel
    ON Oportunidade(idUsuarioResponsavel);

CREATE INDEX IX_Atividade_idOportunidade
    ON Atividade(idOportunidade);

CREATE INDEX IX_Atividade_idUsuarioResponsavel
    ON Atividade(idUsuarioResponsavel);
