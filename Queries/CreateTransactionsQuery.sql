--Example
--DECLARE @attrs DeviceAttributesList;
--INSERT INTO @attrs (attribute_type_id, val) VALUES 
--(1, '8'),
--(2, '1000'),
--(3, '2.4'),
--(4, '2'),
--(8, 'Чорний'),
--(11, 'ASUS');
--EXEC AddNewDevice 1, 1, 750, 1010, '2015-10-10', '', 700, 1, @attrs
CREATE TYPE dbo.DeviceAttributesList
AS TABLE
(
  attribute_type_id INT,
  val nvarchar(50)
);
GO

CREATE PROCEDURE AddNewDevice (@type_id int, @dept_id int, @dev_cost money, @serial_num int, @prod_date datetime, 
@description nvarchar(max), @tran_cost money, @tran_date datetime, @user_id int, @attributes as dbo.DeviceAttributesList READONLY)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	BEGIN TRY
		BEGIN TRAN

			--Add device
			INSERT INTO Devices 
			(devices_status_id, device_type_id, department_id, cost, serial_num, Production_date)
			VALUES(1, @type_id, @dept_id, @dev_cost, @serial_num, @prod_date);

			--Add device attributes
			DECLARE @new_dev_id int;
			SET @new_dev_id = SCOPE_IDENTITY();			

			INSERT INTO Attributes (device_id, attributes_type_id, val) 
			SELECT @new_dev_id, attribute_type_id, val FROM @attributes;

			--Add transfer record
			INSERT INTO Transfers (transfers_type_id, device_id, transfer_date, cost, description, user_id)
			VALUES (1, @new_dev_id, @tran_date, @tran_cost, @description, @user_id)

			--Add transfer dep
			DECLARE @new_tran_id int;
			SET @new_tran_id = SCOPE_IDENTITY();	

			INSERT INTO Transfer_Departments (transfer_id, department_id, transfer_department_role_types_id)
			VALUES (@new_tran_id, @dept_id, 2);

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END
GO

CREATE PROCEDURE MoveDevice (@dev_id int, @new_dept_id int, @date datetime, @description nvarchar(max), @user_id int )
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	BEGIN TRY
		BEGIN TRAN

			--Save old department id
			DECLARE @old_dept_id int;
			SELECT @old_dept_id = department_id FROM Devices WHERE device_id = @dev_id;

			--Move device
			UPDATE Devices SET department_id = @new_dept_id WHERE device_id = @dev_id;

			--Add transfer record
			INSERT INTO Transfers (transfers_type_id, device_id, transfer_date, cost, description, user_id)
			VALUES (2, @dev_id, @date, 0, @description, @user_id)

			--Add transfer departments
			DECLARE @new_tran_id int;
			SET @new_tran_id = SCOPE_IDENTITY();
			
			--from
			INSERT INTO Transfer_Departments (transfer_id, department_id, transfer_department_role_types_id)
			VALUES (@new_tran_id, @old_dept_id, 1);

			--where
			INSERT INTO Transfer_Departments (transfer_id, department_id, transfer_department_role_types_id)
			VALUES (@new_tran_id, @new_dept_id, 2);

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END
GO

CREATE PROCEDURE MoveToRestoreDevice (@dev_id int, @cost money, @date datetime, @description nvarchar(max), @user_id int )
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	BEGIN TRY
		BEGIN TRAN
				
			--Save old department id
			DECLARE @old_dept_id int;
			SELECT @old_dept_id = department_id FROM Devices WHERE device_id = @dev_id;

			--Change device status
			UPDATE Devices SET devices_status_id = 3 WHERE device_id = @dev_id;

			--Add transfer record
			INSERT INTO Transfers (transfers_type_id, device_id, transfer_date, cost, description, user_id)
			VALUES (3, @dev_id, @date, @cost, @description, @user_id)

			--Add transfer departments
			DECLARE @new_tran_id int;
			SET @new_tran_id = SCOPE_IDENTITY();
			
			--from
			INSERT INTO Transfer_Departments (transfer_id, department_id, transfer_department_role_types_id)
			VALUES (@new_tran_id, @old_dept_id, 1);
			
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END
GO

CREATE PROCEDURE MoveFromRestoreDevice (@dev_id int, @cost money, @description nvarchar(max), @user_id int )
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	BEGIN TRY
		BEGIN TRAN
				
			--Save old department id
			DECLARE @old_dept_id int;
			SELECT @old_dept_id = department_id FROM Devices WHERE device_id = @dev_id;

			--Change device status
			UPDATE Devices SET devices_status_id = 1 WHERE device_id = @dev_id;

			--Add transfer record
			INSERT INTO Transfers (transfers_type_id, device_id, transfer_date, cost, description, user_id)
			VALUES (4, @dev_id, GETDATE(), @cost, @description, @user_id)
						
			--Add transfer departments
			DECLARE @new_tran_id int;
			SET @new_tran_id = SCOPE_IDENTITY();
			
			--where
			INSERT INTO Transfer_Departments (transfer_id, department_id, transfer_department_role_types_id)
			VALUES (@new_tran_id, @old_dept_id, 2);

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END
GO

CREATE PROCEDURE SaleDevice (@dev_id int, @cost money, @date datetime, @description nvarchar(max), @user_id int )
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	BEGIN TRY
		BEGIN TRAN
				
			--Save old department id
			DECLARE @old_dept_id int;
			SELECT @old_dept_id = department_id FROM Devices WHERE device_id = @dev_id;

			--Change device status
			UPDATE Devices SET devices_status_id = 5 WHERE device_id = @dev_id;

			--Add transfer record
			INSERT INTO Transfers (transfers_type_id, device_id, transfer_date, cost, description, user_id)
			VALUES (5, @dev_id, @date, @cost, @description, @user_id)
			
			
			--Add transfer departments
			DECLARE @new_tran_id int;
			SET @new_tran_id = SCOPE_IDENTITY();
			
			--from
			INSERT INTO Transfer_Departments (transfer_id, department_id, transfer_department_role_types_id)
			VALUES (@new_tran_id, @old_dept_id, 1);

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END
GO

CREATE PROCEDURE RemoveDevice (@dev_id int, @date datetime, @description nvarchar(max), @user_id int )
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	BEGIN TRY
		BEGIN TRAN
			
			--Save old department id
			DECLARE @old_dept_id int;
			SELECT @old_dept_id = department_id FROM Devices WHERE device_id = @dev_id;

			--Change device status
			UPDATE Devices SET devices_status_id = 6 WHERE device_id = @dev_id;

			--Add transfer record
			INSERT INTO Transfers (transfers_type_id, device_id, transfer_date, cost, description, user_id)
			VALUES (6, @dev_id, @date, 0, @description, @user_id)
			
			
			--Add transfer departments
			DECLARE @new_tran_id int;
			SET @new_tran_id = SCOPE_IDENTITY();
			
			--from
			INSERT INTO Transfer_Departments (transfer_id, department_id, transfer_department_role_types_id)
			VALUES (@new_tran_id, @old_dept_id, 1);

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END
GO

CREATE PROCEDURE SetDeviceIsBroken (@dev_id int)
AS
BEGIN
	--Change device status
	UPDATE Devices SET devices_status_id = 2 WHERE device_id = @dev_id;
END
GO

CREATE PROCEDURE SetDeviceCannotRestore (@dev_id int)
AS
BEGIN
	--Change device status
	UPDATE Devices SET devices_status_id = 4 WHERE device_id = @dev_id;
END
GO

CREATE PROCEDURE SetDeviceNewAttributeVal (@dev_id int, @attr_type_id int, @val nvarchar(50))
AS
BEGIN				
	--Add new device attribute
	INSERT INTO Attributes (device_id, attributes_type_id, val)
	VALUES (@dev_id, @attr_type_id, @val);			
END
GO

CREATE PROCEDURE SetDeviceAttributeVal (@dev_id int, @attr_type_id int, @new_val nvarchar(50), @cost money, @date datetime, @description nvarchar(max), @user_id int)
AS
BEGIN		

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	BEGIN TRY
		BEGIN TRAN
			--Set new value
			UPDATE Attributes SET val = @new_val 
			WHERE device_id = @dev_id AND attributes_type_id = @attr_type_id;

			--Add transfer record
			INSERT INTO Transfers (transfers_type_id, device_id, transfer_date, cost, description, user_id)
			VALUES (7, @dev_id, @date, @cost, @description, @user_id)
			
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
				
END
GO