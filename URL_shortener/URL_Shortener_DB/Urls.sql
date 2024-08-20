CREATE TABLE [dbo].[Urls]
(
  [UrlId] [uniqueidentifier] NOT NULL,
  [UserId] [nvarchar] NOT NULL,
  [OriginalUrl] [nvarchar] NOT NULL,
  [ShortenedUrl] [nvarchar] NOT NULL,
  PRIMARY KEY CLUSTERED ([UrlId] ASC)
)
