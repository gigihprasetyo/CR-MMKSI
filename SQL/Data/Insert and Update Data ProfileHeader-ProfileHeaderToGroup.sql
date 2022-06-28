/***** INSERT PROFILEHEADER *****/
INSERT INTO Profileheader
           (Code
           ,Description
           ,DataType
		   ,DataLength
		   ,ControlType
		   ,SelectionMode
		   ,Mandatory
		   ,Status
		   ,RowStatus
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
          ('CBU_CARROSSERIE'
           ,'KAROSERI'
           ,1 ,0 ,2 ,0 ,2 ,1 ,0
		   ,'000002nana'
		   ,'2018-08-06 10:35:38.790'
		   ,'000002nana'
		   ,'2018-08-06 10:36:06.990')

INSERT INTO Profileheader
           (Code
           ,Description
           ,DataType
		   ,DataLength
		   ,ControlType
		   ,SelectionMode
		   ,Mandatory
		   ,Status
		   ,RowStatus
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
          ('CBU_LEASING'
           ,'LEASING'
           ,1 ,0 ,2 ,0 ,2 ,1 ,0
		   ,'000002nana'
		   ,'2018-08-06 10:35:38.790'
		   ,'000002nana'
		   ,'2018-08-06 10:36:06.990')


--- Group PC Insert ---
Insert Into ProfileHeaderToGroup
			(ProfileGroupID, ProfileHeaderID, Priority, RowStatus, [CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
		VALUES
          ( (select id from ProfileGroup where code = 'cust_prf_pc')
           , (select id from Profileheader where code = 'CBU_LEASING')
		   , 2
           ,0
		   ,'000002nana'
		   ,'2018-08-06 10:35:38.790'
		   ,'000002nana'
		   ,'2018-08-06 10:36:06.990')

Insert Into ProfileHeaderToGroup
			(ProfileGroupID, ProfileHeaderID, Priority, RowStatus, [CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
		VALUES
          ( (select id from ProfileGroup where code = 'cust_prf_pc')
           , (select id from Profileheader where code = 'CBU_CARROSSERIE')
		   , 11
           ,0
		   ,'000002nana'
		   ,'2018-08-06 10:35:38.790'
		   ,'000002nana'
		   ,'2018-08-06 10:36:06.990')


--- Group PC Update ---
update ProfileHeaderToGroup 
set Priority = 3
where profilegroupid = (select id from profilegroup where code = 'cust_prf_pc')
and profileheaderid = (select id from ProfileHeader where code ='CBU_OWNERSHIP1')

update ProfileHeaderToGroup 
set Priority = 4
where profilegroupid = (select id from profilegroup where code = 'cust_prf_pc')
and profileheaderid = (select id from ProfileHeader where code ='CBU_PURCSTAT')

update ProfileHeaderToGroup 
set Priority = 5
where profilegroupid = (select id from profilegroup where code = 'cust_prf_pc')
and profileheaderid = (select id from ProfileHeader where code ='CBU_USERAGE1')

update ProfileHeaderToGroup 
set Priority = 7
where profilegroupid = (select id from profilegroup where code = 'cust_prf_pc')
and profileheaderid = (select id from ProfileHeader where code ='CBU_PURPOSE1')

update ProfileHeaderToGroup 
set Priority = 8
where profilegroupid = (select id from profilegroup where code = 'cust_prf_pc')
and profileheaderid = (select id from ProfileHeader where code = 'CBU_JENISKEND')

update ProfileHeaderToGroup 
set Priority = 9
where profilegroupid = (select id from profilegroup where code = 'cust_prf_pc')
and profileheaderid = (select id from ProfileHeader where code = 'CBU_MODELKEND')

update ProfileHeaderToGroup 
set Priority = 10
where profilegroupid = (select id from profilegroup where code = 'cust_prf_pc')
and profileheaderid = (select id from ProfileHeader where code = 'CBU_PURCSTAT2')



--- group LCV Insert---
Insert Into ProfileHeaderToGroup
			(ProfileGroupID, ProfileHeaderID, Priority, RowStatus, [CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
		VALUES
          ((select id from ProfileGroup where code = 'cust_prf_lcv')
           , (select id from Profileheader where code = 'CBU_LEASING')
		   , 2
           ,0
		   ,'000002nana'
		   ,'2018-08-06 10:35:38.790'
		   ,'000002nana'
		   ,'2018-08-06 10:36:06.990')

Insert Into ProfileHeaderToGroup
			(ProfileGroupID, ProfileHeaderID, Priority, RowStatus, [CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
		VALUES
          ( (select id from ProfileGroup where code = 'cust_prf_lcv')
           , (select id from Profileheader where code = 'CBU_CARROSSERIE')
		   , 11
           ,0
		   ,'000002nana'
		   ,'2018-08-06 10:35:38.790'
		   ,'000002nana'
		   ,'2018-08-06 10:36:06.990')


--- group LCV Update---
update ProfileHeaderToGroup 
set Priority = 3
where profilegroupid = (select id from profilegroup where code = 'cust_prf_lcv')
and profileheaderid = (select id from ProfileHeader where code ='CBU_OWNERSHIP1')

update ProfileHeaderToGroup 
set Priority = 4
where profilegroupid = (select id from profilegroup where code = 'cust_prf_lcv')
and profileheaderid = (select id from ProfileHeader where code ='CBU_PURCSTAT')

update ProfileHeaderToGroup 
set Priority = 5
where profilegroupid = (select id from profilegroup where code = 'cust_prf_lcv')
and profileheaderid = (select id from ProfileHeader where code ='CBU_BODYTYPELCV1')

update ProfileHeaderToGroup 
set Priority = 6
where profilegroupid = (select id from profilegroup where code = 'cust_prf_lcv')
and profileheaderid = (select id from ProfileHeader where code ='CBU_LOADPROFILE1')

update ProfileHeaderToGroup 
set Priority = 7
where profilegroupid = (select id from profilegroup where code = 'cust_prf_lcv')
and profileheaderid = (select id from ProfileHeader where code ='CBU_MEDANOPERASI1')

update ProfileHeaderToGroup 
set Priority = 8
where profilegroupid = (select id from profilegroup where code = 'cust_prf_lcv')
and profileheaderid = (select id from ProfileHeader where code = 'CBU_JENISKEND')

update ProfileHeaderToGroup 
set Priority = 9
where profilegroupid = (select id from profilegroup where code = 'cust_prf_lcv')
and profileheaderid = (select id from ProfileHeader where code = 'CBU_MODELKEND')

update ProfileHeaderToGroup 
set Priority = 10
where profilegroupid = (select id from profilegroup where code = 'cust_prf_lcv')
and profileheaderid = (select id from ProfileHeader where code = 'CBU_PURCSTAT2')
