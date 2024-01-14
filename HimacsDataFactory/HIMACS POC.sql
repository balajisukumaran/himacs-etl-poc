create table jobs (
	Job_id int identity(1,1),
	name varchar(150)
)


insert into jobs (name)
select 'IMAP Monthly Fees Extract'

insert into jobs (name)
select 'IMAP Fee Solution'

create table IMAPFeeSolution(
id int identity(1,1),
feegroupid int primary key,
imbasefee decimal,
premiumfee decimal,
custodyfee decimal)

declare @i integer
set @i = 1000

while(@i<10000)
begin
	insert into IMAPFeeSolution (feegroupid, imbasefee, premiumfee, custodyfee)
	select @i,1000.0, 2000.0, 3000.0
	set @i=@i+1
end

create table jobhistory (
id int identity(1,1),
job_id int,
status varchar(300),
onDateTime datetime)

create procedure insertjobhistory
@job_id int,
@status varchar(300)
as
begin
	insert into jobhistory(job_id, status, onDateTime)
	select @job_id, @status, GETDATE()
end 


alter procedure checkjobexists  
@id int
as 
begin
	if exists (select 1 from jobs where job_id=@id)
	begin
		select 1 as flag
	end
	else 
	begin
		select 0 as flag
	end
end 


create procedure GetIMAPFeeSolution
as
begin
	select * from IMAPFeeSolution
end


create table select * from IMAPFS(
id int identity(1,1),
feegroupid int primary key,
imbasefee decimal,
premiumfee decimal,
custodyfee decimal,
LoadedOn datetime)

alter procedure InsertFeeSolutionXML
@xml XML
AS
BEGIN
      SET NOCOUNT ON;
	  INSERT INTO dbo.IMAPFS (
		feegroupid 
		,imbasefee  
		,premiumfee 
		,custodyfee
		,LoadedOn
		)
		SELECT
		    feegroupid = XTbl.value('(feegroupid)[1]', 'int'),
		    imbasefee  = XTbl.value('(imbasefee)[1]', 'int'),
		    premiumfee = XTbl.value('(premiumfee)[1]', 'int'),
		    custodyfee = XTbl.value('(custodyfee)[1]', 'int'),
			getdate()
		FROM 
		    @xml.nodes('/NewDataSet/Table1') AS XD(XTbl)
END

select * from dbo.IMAPFS 


select *  from dbo.jobhistory

delete from dbo.IMAPFS 


delete from dbo.jobhistory



select * from jobs

create table jobspecificconfig (
job_id integer,
[key] varchar(100),
[value] varchar(100)
)

select * from jobspecificconfig

insert into jobspecificconfig values
(1,'HIMACS.IMAPFeeSolution.TenantID','76a2ae5a-9f00-4f6b-95ed-5d33d77c4d61'),
(1,'HIMACS.IMAPFeeSolution.ApplicationId','935f6223-014e-425b-885e-34f0f742b8f4'),
(1,'HIMACS.IMAPFeeSolution.AuthenticationKey','Ij~nZNwjDgieEZp8BX3nXo-_vngx6aSYDH'),
(1,'HIMACS.IMAPFeeSolution.SubscriptionId','0e8d2934-7afe-475a-8462-6428ccba5694'),
(1,'HIMACS.IMAPFeeSolution.ResourceGroup','myresourcegroup'),
(1,'HIMACS.IMAPFeeSolution.dataFactoryName','aftestdatafactorykrati'),
(1,'HIMACS.IMAPFeeSolution.PipelineName ','IMAP Monthly Fees Extract')


CREATE PROCEDURE GetJobSpecificConfig '1'
@JobId integer
as
BEGIN
	select * from JobSpecificConfig where job_id=@JobId
END
