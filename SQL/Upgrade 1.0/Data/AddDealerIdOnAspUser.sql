USE [BSIDNET_MMKSI_DMS]
GO

if col_length('dbo.AspNetUsers', 'DealerId') is null 
begin 
	
	-- Add column Dealer Id
	alter table AspNetUsers add DealerId smallint null

	-- Add Foreign Key to Dealer Table
	alter table AspNetUsers 
	add constraint FK_User_Many_Dealer_One
	foreign key (DealerId) references Dealer(Id)

end 