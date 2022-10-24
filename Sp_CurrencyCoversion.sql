USE [bdoDB]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CurrencyCoversion]    Script Date: 24-10-2022 07:03:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CurrencyCoversion]
(@BaseCur varchar(10), 
@ExchangeCur varchar(10)  ,
@Amount float, 
@Date varchar(10) ,
@ConvertedAmt float output)
AS
begin

  DECLARE @CurrencyRate float; 



SELECT top 1 @ConvertedAmt  = (CurrencyRate * @Amount)
FROM dbo.LatestCurrencyRate
WHERE BaseCur  = isnull(@BaseCur,'NOK')
AND CurrencyCode =  isnull(@ExchangeCur,'NOK') and  CONVERT(VARCHAR(10), Apidate, 105) = @Date;
/*END TRY*/

/*BEGIN CATCH
 SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_SEVERITY() AS ErrorSeverity,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH;*/


end;
GO

