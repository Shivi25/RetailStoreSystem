CREATE PROCEDURE Q15
    @personcode int,
	@name varchar(50),
	@family varchar(50),
	@dob varchar(20),
	@email nvarchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;
	update person 
	set firstname= @name, lastname = @family, dob=@dob, email=@email 
	
	where personcode =@personcode
	
END