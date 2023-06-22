-- =============================================
-- Author:		Matheus Hafner
-- Create date: 29/07/2021
-- Description:	Insere ao perfil ADMIN todas as novas permissões criadas na base de dados
-- =============================================
CREATE OR ALTER TRIGGER [dbo].[trgInsertPermissionsRoleClaims] 
	ON [dbo].[Permissions] 
	AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	INSERT INTO AspNetRoleClaims(RoleId, ClaimType, ClaimValue) 
		SELECT r.Id, NEW.Type, NEW.Value 
		From inserted NEW, AspNetRoles r
		WHERE r.Admin = 1 AND NOT EXISTS ( SELECT 1 FROM AspNetRoleClaims WHERE RoleId = r.Id AND lower(ClaimType) = lower(NEW.Type) AND lower(ClaimValue) = lower(NEW.Value)); 
END
