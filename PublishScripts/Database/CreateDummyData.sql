return
USE EmployeeDW
GO

/***********************************************************************************************************/
--==========================================================================================================
-- This script is used to change the badge numbers, the Emp_Id, and create dummy data for employees, supervisors, instructors
-- Database involved: EmployeeDW, MaintenanceDW, Training.
--==========================================================================================================

-- create a newIds table for storing the current badgeid and the new badgeId and the new psbadge id 

If Object_ID('dbo.newIds', 'U') is not null
	drop table dbo.newIds 

Create table dbo.newIds 
(New_EmpId int identity(1323281, 1),
 Emp_Id int,
 Badge varchar(6),
 new_badge varchar(6),
 New_psbadge varchar(6)
 )

 Insert into dbo.newIds
 (emp_Id, Badge, new_badge, New_psbadge)
 select emp_Id,
		 badge, 
		 right(cast(cast(round(rand(badge)*10000000, 0) as int) as varchar), 6), 
		'0' + right(cast(cast(round(rand(emp_id)*10000000, 0) as int) as varchar), 5)
 from dbo.EmPloyees

 --Insert into dbo.newIds
 --(Badge, new_badge)
 --select  SupervisorId, 
	--	 right(cast(cast(round(rand(SupervisorId)*10000000, 0) as int) as varchar), 6)		
 --from dbo.EmPloyees 
 --where SupervisorId not in (select badge from dbo.newIds) 
 
 Insert into dbo.newIds
 (Badge, new_badge)
 select  badge, 
		 right(cast(cast(round(rand(badge)*10000000, 0) as int) as varchar), 6)		
 from [Training].[dbo].[Instructor]
 where badge not in (select badge from dbo.newIds) 



select * from dbo.newIds


/******************************************************************************************************/
--=============== create a supervisor table ========================================================
if Object_id('dbo.supervisor', 'U') is not null
	drop table dbo.supervisor

create table dbo.supervisor 
(Id int identity(1,1),
 Badge varchar(6),
 new_badge varchar(6),
 Name varchar(30),
 NationalId varchar(15)
 )

 insert into dbo.supervisor (badge)
 select distinct supervisorId
 from dbo.Employees

 insert into dbo.supervisor(badge)
 select distinct [SupervisorBadge]
 From [Training].[Apprentice].[Progress]
 where [SupervisorBadge] not in ( select badge from dbo.supervisor)

 insert into dbo.supervisor(badge)
 select distinct [SuperintendentBadge]
 From [Training].[Apprentice].[Progress]
 where [SuperintendentBadge] not in ( select badge from dbo.supervisor)
    
 
 --======================== get supervisor name =========================================

 if Object_id('tempdb..#supervisorName') is not null
	drop table #supervisorName

 Create table #supervisorName
 (Id int identity(1,1),
  Name varchar(30),
  NationalId varchar(15)
 )

  insert into #supervisorName
  (NationalId, Name) 
  Select distinct E2.NationalIDNumber
		,P.FirstName + ' ' + Coalesce(P.MiddleName + ' ', '') + P.LastName as Name
	FROM [AdventureWorks2014].[HumanResources].[Employee] E1
	  join [AdventureWorks2014].[HumanResources].[Employee] E2
	on e1.[OrganizationNode].GetAncestor(1) = E2.OrganizationNode
  join [AdventureWorks2014].[Person].[Person] P
	on E2.BusinessEntityID = P.BusinessEntityID

select * from dbo.supervisor

select * from #supervisorName

merge dbo.supervisor S
using #supervisorName N
on N.Id = S.Id
when matched then
	Update 
	Set S.Name = N.Name
		,S.NationalId = N.NationalId
		,S.New_badge = Replicate('0', 6 - len(right(cast(cast(round(rand(S.badge)*10000000, 0) as int) as varchar), 6))) + right(cast(cast(round(rand(S.badge)*10000000, 0) as int) as varchar), 6) 
When not matched by target then
	Insert (Name, NationalId, New_badge)
	values( N.Name, N.NationalId,
		 Replicate('0', 6 - len(right(cast(cast(round(rand(N.NationalId)*10000000, 0) as int) as varchar), 6))) + right(cast(cast(round(rand(N.NationalId)*10000000, 0) as int) as varchar), 6)  );


/*****************************************************************************************************/
-- create a table to store the needed information from adventureworks2014 database

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

if Object_id('dbo.adventureEmp','U') is not null
	drop table dbo.adventureEmp 

CREATE TABLE [dbo].[AdventureEmp](
	[Emp_Id] [int] IDENTITY(1323281, 1) NOT NULL PRIMARY KEY CLUSTERED ,
	[Name] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[MiddleName] [varchar](30) NULL,
	[Suffix] [varchar](10) NULL,
	[Pref_Name] [varchar](30) NULL,
	
	[BirthDate] [datetime] NULL,
	[Address01] [varchar](35) NULL,
	[Address02] [varchar](35) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](6) NULL,
	[ZIP] [varchar](20) NULL,
	[PreferredPhone] [varchar](24) NULL,
	[WorkPhone] [varchar](24) NULL,	
	[DeptId] [varchar](10) NOT NULL,
	[DeptName] [varchar](30) NOT NULL,
	[JobCode] [varchar](6) NOT NULL,
    [JobTitle] [varchar](30) NOT NULL,
		
	[SupervisorId] [varchar](11) NULL,
	[SupervisorName] [varchar](150) NULL,

	[Sex] [varchar](1) NOT NULL,
	[Marital] [varchar](1) NULL,
) ON [PRIMARY]

GO

/***************************************************************************************************/
-- create dummy data from the adventureworks2014 into the above table

;with emp_1 as (
  select E.JobTitle
		,E.BirthDate
		,E.MaritalStatus as Marital
		,E.Gender as Sex
		,E.BusinessEntityID as jobCode
		,E.OrganizationNode
		,P.FirstName + ' ' + Coalesce(P.MiddleName + ' ', '') + P.LastName as Name
		,P.FirstName
		,P.LastName
		,P.MiddleName
		,P.Suffix 			
		,D.DepartmentID as DeptId 
		,D.Name as DeptName
		,A.AddressLine1 as address01
		,A.AddressLine2 as address02
		,A.City
		,S.StateProvinceCode as State
		,A.postalCode as ZIP
		,phone.PhoneNumber as preferredPhone
		,phone.phonenumber as Workphone
		 ,row_number() over (partition by jobtitle order by jobtitle) as row_no
  from adventureworks2014.HumanResources.employee E
  join [AdventureWorks2014].[HumanResources].[EmployeeDepartmentHistory] H
	on E.BusinessEntityID = H.BusinessEntityID
  join [AdventureWorks2014].[HumanResources].[Department] D
    on D.DepartmentID = H.DepartmentID
  Join [AdventureWorks2014].[Person].[Person] P
    on E.BusinessEntityID = P.BusinessEntityID
  Join [AdventureWorks2014].[Person].[BusinessEntityAddress] BA
	on E.BusinessEntityID = BA.BusinessEntityID
	and BA.AddressTypeID = 2
  Join [AdventureWorks2014].[Person].[Address] A
    on BA.AddressID = A.AddressID
  Join [AdventureWorks2014].[Person].[StateProvince] S
    on A.StateProvinceID = S.StateProvinceID
  join  [AdventureWorks2014].[Person].[PersonPhone] phone
    on E.BusinessEntityID = phone.BusinessEntityID
  where organizationLevel >= 2
  and jobtitle <> 'Buyer'
  and len(jobtitle) <= 30)

  --select * from emp_1

  insert into dbo.AdventureEmp 
  (  [Name]
    ,[FirstName]
    ,[LastName]
    ,[MiddleName]
    ,[Suffix]
    ,[BirthDate]
    ,[Address01]
    ,[Address02]
    ,[City]
    ,[State]
    ,[ZIP]
    ,[PreferredPhone]
    ,[WorkPhone]
    ,[DeptId]
    ,[DeptName]      
    ,[JobCode]      
    ,[JobTitle]
	,[Sex]
    ,[Marital]
    ,[SupervisorId]
    ,[SupervisorName]
  )	
  select top 21
		 e1.[Name]
		,e1.[FirstName]
		,e1.[LastName]
		,e1.[MiddleName]
		,e1.[Suffix]
		,e1.[BirthDate]
		,e1.[Address01]
		,e1.[Address02]
		,e1.[City]
		,e1.[State]
		,e1.[ZIP]
		,e1.[PreferredPhone]
		,e1.[WorkPhone]
		,e1.[DeptId]
		,e1.[DeptName]      
		,e1.[JobCode]      
		,e1.[JobTitle]
		,e1.[Sex]
		,e1.[Marital]
		,S.new_badge
		,P.FirstName + ' ' + Coalesce(P.MiddleName + ' ', '') + P.LastName as SupervisorName
  from emp_1 e1   
  join  [AdventureWorks2014].[HumanResources].[Employee] E2
	on e1.[OrganizationNode].GetAncestor(1) = E2.OrganizationNode
  join [AdventureWorks2014].[Person].[Person] P
	on E2.BusinessEntityID = P.BusinessEntityID
  Join dbo.supervisor S
	on  E2.NationalIDNumber = S.NationalId
  where row_no < 2

  GO

  select * from AdventureEmp

  /*************************************************************************************************************/
  -- create a staging table to store the employees data and do the update, which includes the badgeid and emp_id fileds and all the other fields.
 if Object_Id('dbo.employees_Stage','U') is not null
	drop table dbo.employees_stage
	 
  CREATE TABLE [dbo].[Employees_Stage](
	[Emp_Id] [int]  NOT NULL,
	[AddUserId] [varchar](50) NULL,
	[AddDateTime] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[Badge] [varchar](6) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[MiddleName] [varchar](30) NULL,
	[Suffix] [varchar](10) NULL,
	[Pref_Name] [varchar](30) NULL,
	[NTLogin] [varchar](50) NULL,
	[Hire_Dt] [datetime] NULL,
	[HireTime] [time](7) NULL,
	[Rehire_Dt] [datetime] NULL,
	[BusDriverQualDate] [datetime] NULL,
	[BusDriverQualTime] [time](7) NULL,
	[LastWorkDate] [datetime] NULL,
	[BirthDate] [datetime] NULL,
	[Address01] [varchar](35) NULL,
	[Address02] [varchar](35) NULL,
	[City] [varchar](30) NULL,
	[State] [varchar](6) NULL,
	[ZIP] [varchar](10) NULL,
	[PreferredPhone] [varchar](24) NULL,
	[WorkPhone] [varchar](24) NULL,
	[CellPhone] [varchar](26) NULL,
	[EmailAddress] [varchar](50) NULL,
	[Empl_Status] [varchar](1) NOT NULL,
	[DeptId] [varchar](10) NOT NULL,
	[DeptName] [varchar](30) NOT NULL,
	[Location] [varchar](10) NULL,
	[Company] [varchar](3) NOT NULL,
	[JobCode] [varchar](6) NOT NULL,
	[PayGroup] [varchar](3) NOT NULL,
	[JobTitle] [varchar](30) NOT NULL,
	[BusinessTitle] [varchar](30) NULL,
	[Empl_Type] [varchar](1) NOT NULL,
	[RegTempFlag] [varchar](1) NULL,
	[FT_PT_Flag] [varchar](1) NULL,
	[Per_Org] [varchar](5) NULL,
	[SupervisorId] [varchar](11) NULL,
	[SupervisorName] [varchar](55) NULL,
	[Action] [varchar](3) NULL,
	[ActionDate] [datetime] NULL,
	[ActionReason] [varchar](3) NULL,
	[TermDate] [datetime] NULL,
	[UnionCode] [varchar](3) NULL,
	[EEO4Code] [varchar](1) NULL,
	[EEOJobGroup] [varchar](2) NULL,
	[CompFrequency] [varchar](1) NULL,
	[CompRate] [money] NULL,
	[AnnualRate] [money] NULL,
	[MonthlyRate] [money] NULL,
	[HourlyRate] [money] NULL,
	[Grade] [varchar](3) NULL,
	[Step] [varchar](2) NULL,
	[PositionNumber] [varchar](8) NULL,
	[PositionEntryDate] [datetime] NULL,
	[Sex] [varchar](1) NOT NULL,
	[EthnicGroup] [varchar](5) NULL,
	[Veteran] [varchar](1) NULL,
	[Citizen] [varchar](1) NULL,
	[Disabled] [varchar](2) NULL,
	[Marital] [varchar](1) NULL,
	[AsOfDate] [datetime] NULL,
	[EffectiveDate] [datetime] NULL,
	[VacHoursEntitlement] [decimal](6, 2) NULL,
	[VacHoursCarryover] [decimal](6, 2) NULL,
	[DriverLicenseNumber] [varchar](20) NULL,
	[DriverLicenseExpirationDate] [date] NULL,
	[MedicalExpirationDate] [date] NULL,
	[StepStartDate] [date] NULL,
	[WorkShift] [varchar](30) NULL,
	[ScheduledDaysOff] [varchar](27) NULL,
	[PSBadge] [varchar](7) NULL,
 ) ON [PRIMARY]



 insert into dbo.Employees_Stage 
 select * from dbo.Employees

 
 /**********************************************************************************************/
 --====================== process the staging table ============================================
 
If Object_id('tempdb..#badgeMatch') is not null
	drop table #badgeMatch

-- creata a temporary table for updating the supervisorids in Training database
Select E1.SupervisorId as old_badge, E2.SupervisorId as new_badge
into #badgeMatch
from dbo.employees_Stage E1
Join dbo.NewIds N
	on E1.Emp_Id = N.Emp_Id
join dbo.AdventureEmp E2
	on N.New_empId = E2.Emp_Id


-- udpate the staging table, include changing the emp_id
update E1
set  E1.Emp_Id = N.New_EmpId 
	,E1.Badge = N.New_Badge
	,E1.Name = E2.Name
	,E1.FirstName = E2.FirstName
	,E1.LastName = E2.Lastname
	,E1.Middlename = E2.MiddleName
	,E1.Suffix = E2.Suffix
	,E1.Pref_Name = NULL
	,E1.NTLogin = left(lower(E2.firstname),1) + lower(E2.lastName) + right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 2)  
	,E1.Hire_Dt = Hire_Dt -cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 2) as int)
	,E1.rehire_Dt = Rehire_Dt + cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 2) as int)
	,E1.EmailAddress = left(lower(E2.firstname),1) + lower(E2.lastName) + right(cast(cast(Round(rand(E1.Emp_Id)*1000000, 0) as int) as varchar), 2)  + '@actraining.org'
	,E1.BirthDate= E2.BirthDate
    ,E1.Address01 = E2.Address01
    ,E1.Address02 = E2.Address02
    ,E1.City = E2.City
    ,E1.State = E2.State
    ,E1.ZIP = E2.Zip
    ,E1.PreferredPhone = E2.PreferredPhone
	,E1.WorkPhone = E2.WorkPhone
	,E1.DeptId = E2.DeptId
	,E1.DeptName = E2.DeptName
	,E1.[Location] = E1.Location
    ,E1.[Company] = E1.Company
    ,E1.[JobCode] = E2.JobCode
    ,E1.[PayGroup] = E1.PayGroup
    ,E1.[JobTitle] = E2.JobTitle
    ,E1.[BusinessTitle] = E2.Jobtitle
    ,E1.[Empl_Type] = E1.Empl_Type
    ,E1.[RegTempFlag] = E1.RegTempFlag
    ,E1.[FT_PT_Flag] = E1.FT_PT_Flag
    ,E1.[Per_Org] = E1.Per_Org
    ,E1.[SupervisorId] = E2.SupervisorId
    ,E1.[SupervisorName] = E2.SupervisorName
    ,E1.[Action] = E1.Action
    ,E1.[ActionDate] = E1.ActionDate -cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 1) as int)
    ,E1.[ActionReason] = E1.ActionReason
    ,E1.[TermDate] = E1.TermDate
    ,E1.[UnionCode] = E1.UnionCode
    ,E1.[EEO4Code] = right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 1) 
    ,E1.[EEOJobGroup] = E1.EEOJobGroup
    ,E1.[CompFrequency] = E1.[CompFrequency]
    ,E1.[CompRate] = E1.CompRate + cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 1) as int)
    ,E1.[AnnualRate] = (E1.CompRate + cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 1) as int)) * 8 * 21 * 12
    ,E1.[MonthlyRate] = (E1.CompRate + cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 1) as int)) * 8 * 21 
    ,E1.[HourlyRate] = E1.CompRate + cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 1) as int)
    ,E1.[Grade] = E1.Grade 
    ,E1.[Step] = E1.Step
    ,E1.[PositionNumber] = Replicate('0', 8 - len(cast(cast(Round(rand(E1.Emp_Id)*10000, 0) as int) as varchar))) + cast(cast(Round(rand(E1.Emp_Id)*10000, 0) as int) as varchar) 
    ,E1.[PositionEntryDate] = E1.PositionEntryDate -cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 2) as int)
    ,E1.[Sex] =	E2.Sex
    ,E1.[EthnicGroup] = E1.EthnicGroup
    ,E1.[Veteran] = E1.Veteran
    ,E1.[Citizen] = E1.Citizen
    ,E1.[Disabled] = E1.Disabled
    ,E1.[Marital] = E2.Marital
    ,E1.[AsOfDate] = E1.AsOfDate
    ,E1.[EffectiveDate] = E1.EffectiveDate - cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 2) as int)
    ,E1.[VacHoursEntitlement] = E1.VacHoursEntitlement
    ,E1.[VacHoursCarryover] = E1.VacHoursCarryover
    ,E1.[DriverLicenseNumber] = 'C' + Replicate('0', 7 - len(cast(cast(Round(rand(E1.Emp_Id)*10000000, 0) as int) as varchar))) + cast(cast(Round(rand(E1.Emp_Id)*10000000, 0) as int) as varchar) 
    ,E1.[DriverLicenseExpirationDate] = Cast(cast(E1.DriverLicenseExpirationDate as datetime) - cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 3) as int) as date)
    ,E1.[MedicalExpirationDate] = Cast(cast(E1.MedicalExpirationDate as datetime) - cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 2) as int) as date)
    ,E1.[StepStartDate] = Cast(cast(E1.StepStartDate as datetime) + cast(right(cast(cast(Round(rand(E1.Emp_Id)*100000, 0) as int) as varchar), 2) as int) as date)
    ,E1.[WorkShift] = E1.WorkShift
    ,E1.[ScheduledDaysOff] = E1.ScheduledDaysOff
    ,E1.[PSBadge] = N.new_psbadge
from dbo.employees_Stage E1
Join dbo.NewIds N
	on E1.Emp_Id = N.Emp_Id
join dbo.AdventureEmp E2
	on N.New_empId = E2.Emp_Id

 /************************************************************************************************/
 -- update the badgeId in all the related tables in all the databases
 
 print 'Before updating the badgeIds'

 --====================  [Training].[Apprentice].[Progress] ============================
 Update P
  SET P.SupervisorBadge = B.new_badge
  from [Training].[Apprentice].[Progress] P
  join #badgeMatch B
	on P.supervisorBadge = B.Old_badge

  Update P
  SET P.SuperintendentBadge = B.new_badge
  from [Training].[Apprentice].[Progress] P
  join #badgeMatch B
	on P.SuperintendentBadge = B.Old_badge

  Update P
  SET P.SupervisorBadge = S.new_badge
  from [Training].[Apprentice].[Progress] P
  join EmployeeDW.dbo.supervisor S
	on P.supervisorBadge = S.badge

  Update P
  SET P.SuperintendentBadge = S.new_badge
  from [Training].[Apprentice].[Progress] P
  join EmployeeDW.dbo.supervisor S
	on P.SuperintendentBadge = S.badge

 --====================  [Training].[Apprentice].[ProgressDay] ============================
  Update P
  SET P.SupervisorBadge = B.new_badge
  from  [Training].[Apprentice].[ProgressDay] P
  join #badgeMatch B
	on P.supervisorBadge = B.Old_badge

  Update P
  SET P.CommentBadge = B.new_badge
  from  [Training].[Apprentice].[ProgressDay] P
  join #badgeMatch B
	on P.CommentBadge = B.Old_badge

  Update P
  SET P.SupervisorBadge = S.new_badge
  from  [Training].[Apprentice].[ProgressDay] P
  join EmployeeDW.dbo.supervisor S
	on P.supervisorBadge = S.badge

  Update P
  SET P.CommentBadge = S.new_badge
  from  [Training].[Apprentice].[ProgressDay] P
  join EmployeeDW.dbo.supervisor S
	on P.CommentBadge = S.badge
GO

 --=================== EmployeeDW.dbo.EmployeesLocation  ===================
 Update EL
  SET EL.Badge = N.new_badge
	  ,EL.EmployeeName = A.Name
	  ,EL.FirstName = A.FirstName
	  ,EL.MiddleName = A.MiddleName
	  ,EL.LastName = A.LastName
  FROM [EmployeeDW].[dbo].[EmployeesLocation] EL
  JOIN EmployeeDW.dbo.newIds N on N.Badge = EL.Badge
  JOIN EmployeeDW.dbo.AdventureEmp A on A.Emp_Id = N.New_EmpId

 --================== MaintenanceDW.dbo.LaborCosting     =========================
  update L
  set L.BadgeNum = Replicate('0', 10-len(N.New_Badge)) + N.New_Badge
	,L.EarnClass = Cast(Cast(L.EarnClass as int) + 2165 as varchar)
	,L.AccountCode = Coalesce(substring(L.accountCode, 1, len(L.accountCode)-4), '') + Cast(Cast(Right(L.AccountCode,4) as int) + 165 as varchar)
	,L.LaborCost = LaborCost + Round(Rand(L.laborcost)*10, 2)
	,L.WorkOrderNum = Replicate('0', 8-Len(Cast(cast(workorderNum as int) + Cast(round(rand(workorderNum)*1000, 0) as int) as varchar))) + Cast(cast(workorderNum as int) + Cast(round(rand(workorderNum)*1000, 0) as int) as varchar)  
	,L.labBatchNum = SUBSTRING(L.labBatchNUm, 1, 1) + Reverse(Substring(L.LabBatchNum, 2, len(L.labBatchNum)))
  from [MaintenanceDW].[dbo].[LaborCosting] L
  JOIN EmployeeDW.dbo.newIds N on Replicate('0', 10-len(N.Badge)) + N.Badge = L.BadgeNum

 --================== [Training].[dbo].[CourseEnrollment]  ========================
  Update C
  SET C.Badge = N.new_badge	 
  FROM [Training].[dbo].[CourseEnrollment] C
  JOIN EmployeeDW.dbo.newIds N on N.Badge = C.Badge

 --==================  [Training].[dbo].[Instructor]  =============================
  Update I
  SET I.Badge = N.new_badge	 
  FROM [Training].[dbo].[Instructor] I
  JOIN EmployeeDW.dbo.newIds N on N.Badge = I.Badge

  --================  [Training].[Apprentice].[Participant]  ======================
  Update P
  SET P.Badge = N.new_badge	 
  FROM [Training].[Apprentice].[Participant] P
  JOIN EmployeeDW.dbo.newIds N on N.Badge = P.Badge



/************************************************************************************************/
 -- Delete the EmployeeDW.dbo.Employees table
 delete from EmployeeDW.dbo.Employees

 -- reseed the Emp_Id column
 --DBCC CheckIdent ('EmployeeDW.dbo.Employees', RESEED, 1323281)

 -- Set identity insert on 
 SET IDENTITY_INSERT EmployeeDW.dbo.Employees ON

 -- insert the data back to employees table from the staging table
 insert into EmployeeDW.dbo.Employees (Emp_Id, AddUserId, AddDateTime, UpdatedBy, UpdatedOn, Badge, Name, FirstName, LastName, 
			MiddleName, Suffix, Pref_Name, NTLogin, Hire_Dt, HireTime, Rehire_Dt, BusDriverQualDate, BusDriverQualTime, LastWorkDate, BirthDate,
			Address01, Address02, City, State, ZIP, PreferredPhone, WorkPhone, CellPhone, EmailAddress, Empl_Status, DeptId, DeptName, Location, 
			Company, JobCode, PayGroup, JobTitle, BusinessTitle, Empl_Type, RegTempFlag, FT_PT_Flag, Per_Org, SupervisorId, SupervisorName, Action, 
			ActionDate, ActionReason, TermDate, UnionCode, EEO4Code, EEOJobGroup, CompFrequency, CompRate, AnnualRate, MonthlyRate, HourlyRate, Grade, 
			Step, PositionNumber, PositionEntryDate, Sex, EthnicGroup, Veteran, Citizen, Disabled, Marital, AsOfDate, EffectiveDate, VacHoursEntitlement, 
			VacHoursCarryover, DriverLicenseNumber, DriverLicenseExpirationDate, MedicalExpirationDate, StepStartDate, WorkShift, ScheduledDaysOff, PSBadge)
 select		Emp_Id, AddUserId, AddDateTime, UpdatedBy, UpdatedOn, Badge, Name, FirstName, LastName, 
			MiddleName, Suffix, Pref_Name, NTLogin, Hire_Dt, HireTime, Rehire_Dt, BusDriverQualDate, BusDriverQualTime, LastWorkDate, BirthDate, 
			Address01, Address02, City, State, ZIP, PreferredPhone, WorkPhone, CellPhone, EmailAddress, Empl_Status, DeptId, DeptName, Location, 
			Company, JobCode, PayGroup, JobTitle, BusinessTitle, Empl_Type, RegTempFlag, FT_PT_Flag, Per_Org, SupervisorId, SupervisorName, Action, 
			ActionDate, ActionReason, TermDate, UnionCode, EEO4Code, EEOJobGroup, CompFrequency, CompRate, AnnualRate, MonthlyRate, HourlyRate, Grade, 
			Step, PositionNumber, PositionEntryDate, Sex, EthnicGroup, Veteran, Citizen, Disabled, Marital, AsOfDate, EffectiveDate, VacHoursEntitlement, 
			VacHoursCarryover, DriverLicenseNumber, DriverLicenseExpirationDate, MedicalExpirationDate, StepStartDate, WorkShift, ScheduledDaysOff, PSBadge  
 from EmployeeDW.dbo.Employees_Stage
  
 -- Set identity insert on 
 SET IDentity_Insert EmployeeDW.dbo.Employees OFF

 GO

/**************************************************************************/
-- delete all the staging tables

If Object_ID('dbo.newIds', 'U') is not null
	drop table dbo.newIds 

if Object_id('dbo.supervisor', 'U') is not null
	drop table dbo.supervisor

 if Object_id('tempdb..#supervisorName') is not null
	drop table #supervisorName

if Object_id('dbo.adventureEmp','U') is not null
	drop table dbo.adventureEmp 

 if Object_Id('dbo.employees_Stage','U') is not null
	drop table dbo.employees_stage

If Object_id('tempdb..#badgeMatch') is not null
	drop table #badgeMatch
GO