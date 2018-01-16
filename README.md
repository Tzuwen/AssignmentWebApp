# AssignmentWebApp
AssignmentWebApp

Please download the the Microsoft Adventureworks DB from 

http://msftdbprodsamples.codeplex.com/releases/view/93587 

and add the mdf to the App_Data folder

 3 stored procedure is need for this project
 
 
 
 --Get all products
 
CREATE PROCEDURE [dbo].[GetAllProducts]
	
AS
BEGIN
	SELECT 
		p.[ProductID] 
		,p.[Name] 
		,p.[ListPrice]
		,pd.[Description] 
	FROM [Production].[Product] p WITH(NOLOCK)
    INNER JOIN [Production].[ProductModel] pm WITH(NOLOCK)
    ON p.[ProductModelID] = pm.[ProductModelID] 
    INNER JOIN [Production].[ProductModelProductDescriptionCulture] pmx WITH(NOLOCK)
    ON pm.[ProductModelID] = pmx.[ProductModelID] AND pmx.[CultureID] = 'en'
    INNER JOIN [Production].[ProductDescription] pd WITH(NOLOCK)
    ON pmx.[ProductDescriptionID] = pd.[ProductDescriptionID]
	ORDER BY p.[ProductID];
END
GO


--Get product

CREATE PROCEDURE [dbo].[GetProduct]
	@ProductID int = 0
AS
BEGIN
	SELECT 
    p.[ProductID] 
    ,p.[Name] 
	,p.[ListPrice]
    ,pd.[Description] 
	FROM [Production].[Product] p WITH(NOLOCK)
    INNER JOIN [Production].[ProductModel] pm WITH(NOLOCK)
    ON p.[ProductModelID] = pm.[ProductModelID] 
    INNER JOIN [Production].[ProductModelProductDescriptionCulture] pmx WITH(NOLOCK)
    ON pm.[ProductModelID] = pmx.[ProductModelID] AND pmx.[CultureID] = 'en'
    INNER JOIN [Production].[ProductDescription] pd WITH(NOLOCK)
    ON pmx.[ProductDescriptionID] = pd.[ProductDescriptionID]
	WHERE p.ProductID = @ProductID
	ORDER BY p.[ProductID];
END

GO


--Update product name

CREATE PROCEDURE [dbo].[UpdateProduct]
	@ProductID int = 0,
	@Name varchar(50)
		
AS
DECLARE @Result AS INT
IF EXISTS (SELECT 1 FROM [Production].[Product] WHERE ProductID != @ProductID AND [Name] = @Name) 
 SET @Result = 0
ELSE
BEGIN
  UPDATE [Production].[Product] SET [Name] = @Name WHERE ProductID = @ProductID
  SET @Result = 1
END

SELECT @Result AS Result

GO
