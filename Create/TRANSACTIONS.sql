CREATE TABLE [dbo].[TRANSACTIONS](
	[TRANSACTIONCODE] [INT] NOT NULL,
	[PERSONCODE] [int] NOT NULL,
	[STORECODE] [int] NOT NULL,
	[TRANSACTION_DATE] [DATE] NOT NULL,
	[PAYMENT_METHOD] [VARCHAR](50) NOT NULL
	
	CONSTRAINT pk_TRANSACTIONS PRIMARY KEY (TRANSACTIONCODE)
	
	CONSTRAINT fk__TRANSACTIONS_PERSONCODE FOREIGN KEY (PERSONCODE)
	REFERENCES PERSON(PERSONCODE),
	CONSTRAINT fk__TRANSACTIONS_STORECODE FOREIGN KEY (STORECODE)
	REFERENCES STORE(STORECODE)
	
)