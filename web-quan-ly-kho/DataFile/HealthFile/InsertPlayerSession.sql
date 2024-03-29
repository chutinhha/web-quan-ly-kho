set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



























-- ==============================================================================
-- Author:			<Hoang Thanh Trung>
-- Create date: <25/09/2007>
-- Description:	<Insert player record>
-- ==============================================================================
ALTER PROCEDURE [dbo].[Dyn_insertPlayerSession] 
	@_mo_Id INT,    
	@_UserId NVARCHAR(20),	
	@_requestTime datetime,
	@_messageIn NVARCHAR(255),
	@_sessionId INT, 
	@_msgId     INT,     
	@_messageout  NVARCHAR(255),   
	@_isSpam    INT,
	@_playerId BIGINT OUTPUT,
	@_tabName NVARCHAR(255)
WITH ENCRYPTION
AS
BEGIN
  SET NOCOUNT ON;
  BEGIN TRANSACTION
		SET @_messageout = REPLACE(@_messageout,'''','''''')
		SET @_messageIn = REPLACE(@_messageIn,'''','''''')
    DECLARE @sql NVARCHAR(4000);
    SET @sql='INSERT INTO '+@_tabName
			+ ' ([mo_id],[userId],[requesttime],[SessionId],messageIn,msgId,messageOut,isspam)'
			+ ' VALUES('+dbo.alltrim(str(@_mo_Id))+','''+ @_UserId+''','
			+ ' convert(datetime,'''+convert(NVARCHAR(30),@_requestTime,21)+''',21),'
			+ dbo.alltrim(str(@_sessionId))+','''+@_messageIn+''','
			+ dbo.alltrim(str(@_msgId))+','''+@_messageOut+''','
			+ dbo.alltrim(str(@_isSpam))+')';
    EXEC sp_sqlexec @sql;
  IF @@error = -1 BEGIN
		ROLLBACK TRANSACTION;
    SET @_playerId=-1;        
  END
  ELSE BEGIN 
		COMMIT TRANSACTION;  
    SET @_playerId=@@IDENTITY;
		SET @_playerId=ISNULL(@_playerId,-1);
    IF (@_playerId=-1) BEGIN
      EXEC [dbo].[Dyn_getPlayerIdByMoUserReqTimeSession] @_mo_Id, @_UserId, @_requestTime, @_sessionId, @_tabName, @_playerId OUTPUT;
    END
  END
END

