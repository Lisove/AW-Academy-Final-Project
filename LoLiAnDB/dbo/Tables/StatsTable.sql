CREATE TABLE [dbo].[StatsTable] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [Mood]             INT             NOT NULL,
    [PhysicalActivity] INT             NOT NULL,
    [Stress]           INT             NOT NULL,
    [Sleep]            INT             NOT NULL,
    [MentalHealth]     INT             NOT NULL,
    [PhysicalHealth]   INT             NOT NULL,
    [Date]             DATE            NOT NULL,
    [Notes]            NVARCHAR (2000) NULL,
    [ImagePath]        NVARCHAR (200)  NULL,
    [HasImage]         BIT             NULL,
    [UserId]           NVARCHAR (450)  NOT NULL FOREIGN KEY REFERENCES AspNetUsers(Id), 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

