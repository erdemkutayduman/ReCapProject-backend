CREATE TABLE [dbo].[Brands] (
    [BrandId]   INT           IDENTITY (1, 1) NOT NULL,
    [BrandName] NVARCHAR (25) NULL,
    PRIMARY KEY CLUSTERED ([BrandId] ASC)
);

CREATE TABLE [dbo].[Colors] (
    [ColorId]   INT           IDENTITY (1, 1) NOT NULL,
    [ColorName] NVARCHAR (25) NULL,
    PRIMARY KEY CLUSTERED ([ColorId] ASC)
);

CREATE TABLE [dbo].[Cars] (
    [CarId]       INT           IDENTITY (1, 1) NOT NULL,
    [BrandId]     INT           NULL,
    [ColorId]     INT           NULL,
    [CarName]     NVARCHAR (25) NULL,
    [ModelYear]   NVARCHAR (25) NULL,
    [DailyPrice]  DECIMAL (18)  NULL,
    [Description] NVARCHAR (25) NULL,
    PRIMARY KEY CLUSTERED ([CarId] ASC),
    FOREIGN KEY ([ColorId]) REFERENCES [dbo].[Colors] ([ColorId]),
    FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brands] ([BrandId])
);

CREATE TABLE [dbo].[CarImages] (
    [CarImageId] INT            IDENTITY (1, 1) NOT NULL,
    [CarId]      INT            NOT NULL,
    [ImagePath]  NVARCHAR (MAX) NULL,
    [ImageDate]  DATETIME       NULL,
    CONSTRAINT [PK_CarImages] PRIMARY KEY CLUSTERED ([CarImageId] ASC),
    FOREIGN KEY ([CarId]) REFERENCES [dbo].[Cars] ([CarId])
);

CREATE TABLE [dbo].[CreditCards] (
    [CreditCardId]   INT           IDENTITY (1, 1) NOT NULL,
    [CustomerId]     INT           NOT NULL,
    [NameSurname]    VARCHAR (250) NOT NULL,
    [CardNo]         VARCHAR (250) NOT NULL,
    [ExpirationDate] VARCHAR (250) NOT NULL,
    [Cvc]            VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_CreditCards] PRIMARY KEY CLUSTERED ([CreditCardId] ASC),
    FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId])
);

CREATE TABLE [dbo].[Customers] (
    [CustomerId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserId]       INT           NOT NULL,
    [CustomerName] NVARCHAR (25) NOT NULL,
    [FindeksScore] INT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([CustomerId] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[OperationClaims] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (50)    NOT NULL,
    [LastName]     VARCHAR (50)    NOT NULL,
    [Email]        VARCHAR (50)    NOT NULL,
    [PasswordHash] VARBINARY (500) NOT NULL,
    [PasswordSalt] VARBINARY (500) NOT NULL,
    [Status]       BIT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[UserOperationClaims] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [UserId]           INT NOT NULL,
    [OperationClaimId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([Id]) REFERENCES [dbo].[OperationClaims] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Rentals] (
    [RentalId]   INT  IDENTITY (1, 1) NOT NULL,
    [CarId]      INT  NULL,
    [CustomerId] INT  NULL,
    [RentDate]   DATE NULL,
    [ReturnDate] DATE NULL,
    PRIMARY KEY CLUSTERED ([RentalId] ASC),
    FOREIGN KEY ([CarId]) REFERENCES [dbo].[Cars] ([CarId]),
    FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId])
);

CREATE TABLE [dbo].[Payments] (
    [PaymentId]      INT           IDENTITY (1, 1) NOT NULL,
    [RentalId]       INT           NOT NULL,
    [NameSurname]    VARCHAR (250) NOT NULL,
    [CardNo]         VARCHAR (250) NOT NULL,
    [ExpirationDate] VARCHAR (250) NOT NULL,
    [Cvc]            VARCHAR (250) NOT NULL,
    [PaymentDate]    DATETIME      DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([PaymentId] ASC),
    FOREIGN KEY ([RentalId]) REFERENCES [dbo].[Rentals] ([RentalId])
);


