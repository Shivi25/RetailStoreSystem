CREATE PROCEDURE Q14
    @addresscode int,
	@name varchar(50),
	@family varchar(50),
	@dob varchar(20),
	@email nvarchar(50)
AS	
BEGIN
	
	SET NOCOUNT ON;
	insert into PERSON (PERSONCODE, ADDRESSCODE , FIRSTNAME ,LASTNAME ,DOB, EMAIL)
	values((Select ISNULL(Max(PERSONCODE),0) + 1 From PERSON), @addresscode, @name, @family, @DOB, @EMAIL)
	
END