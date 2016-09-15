return
USE EmployeeDW
GO

/***********************************************************************************************************/
--==========================================================================================================
-- This script is used to change the badge numbers, the Emp_Id, and create dummy data for employees, supervisors, instructors
-- Database involved: EmployeeDW, MaintenanceDW, Training.
--==========================================================================================================

/********************************************************************************************/
-- added on 5/23/2016 by Jeff Li
/******************************************************************************************/

--=========== delete some records from MaintenanceDW.EquipmentRegister =======================
-- make sure don't delete those records that are also in EquipmentGroupVehicle 

;with equips as 
( SELECT r.[EquipmentNum]
	  ,row_number() over (partition by r.equipmentgroupnum order by r.serialnum) as rowno 
  FROM [MaintenanceDW].[dbo].[EquipmentRegister] r
  join  [MaintenanceDW].[dbo].[EquipmentGroupVehicle] g
  on r.EquipmentGroupNum = g.EquipmentGroupNum)

delete E1 
from [MaintenanceDW].[dbo].[EquipmentRegister] E1 
where  not exists (select 1 from equips s where E1.equipmentNum = s.equipmentNum and s.rowno = 1) 


--======== Delete some records from [MaintenanceDW].[dbo].[LaborCosting] =====================

 delete t1
 from (  select *, row_number() over (partition by badgenum order by labordate desc) rowno
		 from [MaintenanceDW].[dbo].[LaborCosting]) as  t1 
 where rowno > 5
