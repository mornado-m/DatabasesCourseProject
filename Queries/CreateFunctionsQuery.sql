--Example
--SELECT * FROM GetDeviceAttribute(5)

CREATE FUNCTION GetDeviceAttribute (@dev_id int)
RETURNS @res TABLE
(
	attribute_name nvarchar(50),
	val nvarchar(50),
	units_of_measurement nvarchar(50)
)
AS 
BEGIN	
	INSERT INTO @res (attribute_name, val, units_of_measurement)
	SELECT attribute, val, units_of_measurement  FROM AttributesView WHERE dev_id = @dev_id

	RETURN
END
GO

--SELECT * FROM GetAllDevicesAttributes(1)

CREATE FUNCTION GetAllDevicesAttributes (@user_id int)
RETURNS @res TABLE
(
	device_type nvarchar(50), 
	serial_num nvarchar(50), 
	device_dep nvarchar(50),
	attribute_name nvarchar(50),
	val nvarchar(50),
	units_of_measurement nvarchar(50)
)
AS 
BEGIN	
	
	DECLARE @perm_lvl int
	SELECT @perm_lvl = r.permission_level FROM User_Roles ur JOIN Roles r ON ur.role_id = r.role_id AND ur.user_id = @user_id

	INSERT INTO @res (device_type, serial_num, device_dep, attribute_name, val, units_of_measurement)
	SELECT device_type, serial_num, device_dep, attribute, val, units_of_measurement  FROM AttributesView WHERE (dev_id IN 
	(SELECT device_id FROM Devices WHERE department_id IN 
	(SELECT department_id FROM Employees WHERE user_id = @user_id)) AND @perm_lvl = 2) OR @perm_lvl = 3

	RETURN
END
GO

--SELECT * FROM GetAllDevices(1)
CREATE FUNCTION GetAllDevices (@user_id int)
RETURNS @res TABLE
(
	dev_id int,
	device_type nvarchar(50), 
	device_status nvarchar(50), 
	device_dep nvarchar(50), 
	serial_num nvarchar(50), 
	Production_date datetime, 
	cost money
)
AS 
BEGIN	
	
	DECLARE @perm_lvl int
	SELECT @perm_lvl = r.permission_level FROM User_Roles ur JOIN Roles r ON ur.role_id = r.role_id AND ur.user_id = @user_id

	INSERT INTO @res (dev_id, device_type, device_status, device_dep, serial_num, Production_date, cost)
	SELECT dev_id, device_type, device_status, device_dep, serial_num, Production_date, cost FROM DevicesView WHERE (dev_id IN 
	(SELECT device_id FROM Devices WHERE department_id IN 
	(SELECT department_id FROM Employees WHERE user_id = @user_id)) AND @perm_lvl IN (1, 2)) OR @perm_lvl = 3

	RETURN
END
GO

--SELECT * FROM GetAllTransfers(1)
CREATE FUNCTION GetAllTransfers (@user_id int)
RETURNS @res TABLE
( 
	tran_id int,
	device_type nvarchar(50), 
	serial_num nvarchar(50), 
	transfer_type nvarchar(50), 
	first_dep_role nvarchar(50), 
	first_dep nvarchar(50), 
	second_dep_role nvarchar(50), 
	second_dep nvarchar(50), 
	[transfer_date] datetime, 
	cost money, 
	[description] nvarchar(max)
)
AS 
BEGIN	
	
	DECLARE @perm_lvl int
	SELECT @perm_lvl = r.permission_level FROM User_Roles ur JOIN Roles r ON ur.role_id = r.role_id AND ur.user_id = @user_id

	INSERT INTO @res 
	(tran_id, device_type, serial_num, transfer_type, first_dep_role, first_dep, second_dep_role, second_dep, [transfer_date], cost, [description])
	SELECT tran_id, device_type, serial_num, transfer_type, first_dep_role, first_dep, second_dep_role, second_dep, [transfer_date], cost, [description] FROM TransfersView WHERE tran_id IN 
	(SELECT transfer_id FROM Transfers WHERE device_id IN
	(SELECT device_id FROM Devices WHERE department_id IN 
	(SELECT department_id FROM Employees WHERE user_id = @user_id)) AND @perm_lvl = 2) OR @perm_lvl = 3

	RETURN
END
GO