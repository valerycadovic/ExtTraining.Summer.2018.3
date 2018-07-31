CREATE TABLE [dbo].[Trade] (
    [id]                  INT        IDENTITY (1, 1) NOT NULL,
    [sourceCurrency]      CHAR (3)   NOT NULL,
    [destinationCurrency] CHAR (3)   NOT NULL,
    [lots]                FLOAT (53) NOT NULL,
    [price]               MONEY      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

