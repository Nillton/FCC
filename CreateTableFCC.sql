CREATE TABLE Clientes (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    CPF VARCHAR(11) NOT NULL UNIQUE,
    Nome VARCHAR(100),
    RG VARCHAR(20),
    DataExpedicaoRG DATE,
    OrgaoExpedidor VARCHAR(50),
    UF CHAR(2),
    DataNascimento DATE,
    Sexo CHAR(1),
    EstadoCivil VARCHAR(20)
);


CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,   
    Password NVARCHAR(100) NOT NULL,  
    DataCriacao DATETIME DEFAULT GETDATE() 
);
