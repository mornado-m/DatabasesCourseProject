CREATE VIEW TransfersView AS 
SELECT 

	t.transfer_id as tran_id,
	dt.[name] as device_type, 
	d.serial_num, 
	tt.[name] as transfer_type, 
	tdr1.[name] as first_dep_role, 
	d1.[name] as first_dep, 
	tdr2.[name] as second_dep_role, 
	d2.[name] as second_dep, 
	t.[transfer_date], 
	t.cost, 
	t.[description] 

FROM Transfers t 
JOIN Transfers_Types tt ON t.transfers_type_id = tt.transfers_type_id 

LEFT JOIN Transfer_Departments td1 ON td1.transfer_id = t.transfer_id 
AND td1.transfer_department_role_types_id = 1
LEFT JOIN Transfer_Departments td2 
ON td2.transfer_id = t.transfer_id 
AND td2.transfer_department_role_types_id = 2

LEFT JOIN Departments d1 ON d1.department_id = td1.department_id
LEFT JOIN Departments d2 ON d2.department_id = td2.department_id

LEFT JOIN Transfer_Department_Roles tdr1 
ON tdr1.transfer_department_role_types_id = td1.transfer_department_role_types_id
LEFT JOIN Transfer_Department_Roles tdr2 
ON tdr2.transfer_department_role_types_id = td2.transfer_department_role_types_id

JOIN Devices d ON t.device_id = d.device_id
JOIN Device_Types dt ON d.device_type_id = dt.device_type_id

JOIN Users u ON u.user_id = t.user_id

GO

CREATE VIEW DevicesView AS 
SELECT 

	d.device_id as dev_id,
	dt.name as device_type, 
	ds.name as device_status, 
	dd.name as device_dep, 
	d.serial_num, 
	d.Production_date, 
	d.cost 

FROM Devices d 
JOIN Device_Types dt ON d.device_type_id = dt.device_type_id
JOIN Devices_Statuses ds ON d.devices_status_id = ds.devices_status_id
JOIN Departments dd ON d.department_id = dd.department_id
GO

CREATE VIEW AttributesView AS 
SELECT 
	
	d.device_id as dev_id,
	dt.name as device_type, 
	d.serial_num, 
	dp.name as device_dep, 
	at.name as attribute, 
	a.val, 
	at.units_of_measurement 

FROM Attributes a 
JOIN Attributes_Types at ON a.attributes_type_id = at.attributes_type_id
JOIN Devices d ON a.device_id = d.device_id
JOIN Device_Types dt ON d.device_type_id = dt.device_type_id
JOIN Departments dp ON d.department_id = dp.department_id
GO