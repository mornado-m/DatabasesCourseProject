
CREATE TABLE [Roles]
( 
	[role_id]            integer identity(1,1)  NOT NULL ,
	[name]               nvarchar(50)  NOT NULL ,
	[permission_level]   integer  NULL ,
	CONSTRAINT [XPKRole] PRIMARY KEY  CLUSTERED ([role_id] ASC)
)
go

CREATE TABLE [Users]
( 
	[user_id]            integer identity(1,1) NOT NULL ,
	[login]              nvarchar(50)  NULL ,
	[pass]               nvarchar(50)  NOT NULL ,
	CONSTRAINT [XPKUser] PRIMARY KEY  CLUSTERED ([user_id] ASC)
)
go

CREATE TABLE [User_Roles]
( 
	[role_user_id]       integer identity(1,1) NOT NULL ,
	[role_id]            integer  NOT NULL ,
	[user_id]            integer  NOT NULL ,	
	CONSTRAINT [XPKRole_User] PRIMARY KEY  CLUSTERED ([role_user_id] ASC),
	CONSTRAINT [R_12] FOREIGN KEY ([role_id]) REFERENCES [Roles]([role_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_13] FOREIGN KEY ([user_id]) REFERENCES [Users]([user_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go

CREATE TABLE [Departments]
( 
	[department_id]      integer identity(1,1) NOT NULL ,
	[name]               nvarchar(50)  NOT NULL ,
	[location]           nvarchar(max)  NULL ,
	[description]        nvarchar(max)  NULL ,
	CONSTRAINT [XPKDepartment] PRIMARY KEY  CLUSTERED ([department_id] ASC)
)
go

CREATE TABLE [Transfers_Types]
( 
	[transfers_type_id]  integer identity(1,1) NOT NULL ,
	[name]               nvarchar(50)  NOT NULL ,
	[description]        nvarchar(max)  NULL ,
	CONSTRAINT [XPKTransfers_Type] PRIMARY KEY  CLUSTERED ([transfers_type_id] ASC)
)
go

CREATE TABLE [Devices_Statuses]
( 
	[devices_status_id]  integer identity(1,1) NOT NULL ,
	[name]               nvarchar(50)  NOT NULL ,
	[description]        nvarchar(max)  NULL ,
	CONSTRAINT [XPKDevices_Status] PRIMARY KEY  CLUSTERED ([devices_status_id] ASC)
)
go

CREATE TABLE [Device_Types]
( 
	[device_type_id]     integer identity(1,1) NOT NULL ,
	[name]               nvarchar(50)  NOT NULL ,
	CONSTRAINT [XPKDevice_Type] PRIMARY KEY  CLUSTERED ([device_type_id] ASC)
)
go

CREATE TABLE [Devices]
( 
	[device_id]          integer identity(1,1) NOT NULL ,
	[devices_status_id]  integer  NOT NULL ,
	[cost]               money  NULL ,
	[serial_num]         integer  NOT NULL ,
	[department_id]      integer  NOT NULL ,
	[Production_date]    datetime  NULL ,
	[device_type_id]     integer  NOT NULL ,
	CONSTRAINT [XPKDevice] PRIMARY KEY  CLUSTERED ([device_id] ASC),
	CONSTRAINT [R_4] FOREIGN KEY ([devices_status_id]) REFERENCES [Devices_Statuses]([devices_status_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_6] FOREIGN KEY ([department_id]) REFERENCES [Departments]([department_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_25] FOREIGN KEY ([device_type_id]) REFERENCES [Device_Types]([device_type_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go

CREATE TABLE [Transfers]
( 
	[transfer_id]        integer identity(1,1) NOT NULL ,
	[transfers_type_id]  integer  NOT NULL ,
	[device_id]          integer  NOT NULL ,
	[transfer_date]      datetime  NULL ,
	[cost]               money  NULL ,
	[description]        nvarchar(max)  NULL ,
	[user_id]            integer  NOT NULL ,
	CONSTRAINT [XPKTransfer] PRIMARY KEY  CLUSTERED ([transfer_id] ASC),
	CONSTRAINT [R_17] FOREIGN KEY ([transfers_type_id]) REFERENCES [Transfers_Types]([transfers_type_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_18] FOREIGN KEY ([device_id]) REFERENCES [Devices]([device_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_26] FOREIGN KEY ([user_id]) REFERENCES [Users]([user_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go

CREATE TABLE [Transfer_Department_Roles]
( 
	[transfer_department_role_types_id] integer identity(1,1) NOT NULL ,
	[name]               nvarchar(50)  NOT NULL ,
	CONSTRAINT [XPKTransfer_Department_Role_Types] PRIMARY KEY  CLUSTERED ([transfer_department_role_types_id] ASC)
)
go

CREATE TABLE [Transfer_Departments]
( 
	[transfer_department_id] integer identity(1,1) NOT NULL ,
	[department_id]      integer  NOT NULL ,
	[transfer_id]        integer  NOT NULL ,	
	[transfer_department_role_types_id] integer  NOT NULL ,
	CONSTRAINT [XPKDepartment_Transfer] PRIMARY KEY  CLUSTERED ([transfer_department_id] ASC),
	CONSTRAINT [R_20] FOREIGN KEY ([department_id]) REFERENCES [Departments]([department_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_21] FOREIGN KEY ([transfer_id]) REFERENCES [Transfers]([transfer_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_27] FOREIGN KEY ([transfer_department_role_types_id]) REFERENCES [Transfer_Department_Roles]([transfer_department_role_types_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go

CREATE TABLE [Employees]
( 
	[employee_id]         integer identity(1,1) NOT NULL ,
	[department_id]      integer  NOT NULL ,
	[first_name]         nvarchar(50)  NOT NULL ,
	[last_name]          nvarchar(50)  NOT NULL ,
	[address]            nvarchar(50)  NULL ,
	[tel]                nvarchar(12)  NULL ,
	[dob]                datetime  NULL ,
	[user_id]            integer  NOT NULL UNIQUE,
	CONSTRAINT [XPKEmployee] PRIMARY KEY  CLUSTERED ([employee_id] ASC),
	CONSTRAINT [R_7] FOREIGN KEY ([department_id]) REFERENCES [Departments]([department_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_10] FOREIGN KEY ([user_id]) REFERENCES [Users]([user_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go

CREATE TABLE [Attributes_Types]
( 
	[attributes_type_id] integer identity(1,1) NOT NULL ,
	[name]               nvarchar(50)  NOT NULL ,
	[units_of_measurement] nvarchar(50)  NULL ,
	CONSTRAINT [XPKAttributes_Type] PRIMARY KEY  CLUSTERED ([attributes_type_id] ASC)
)
go

CREATE TABLE [Device_Type_Attributes]
( 
	[device_type_attributes_type_id] integer identity(1,1) NOT NULL ,
	[device_type_id]     integer  NOT NULL ,
	[attributes_type_id] integer  NOT NULL ,	
	CONSTRAINT [XPKDevice_Type_Attributes_Type] PRIMARY KEY  CLUSTERED ([device_type_attributes_type_id] ASC),
	CONSTRAINT [R_23] FOREIGN KEY ([device_type_id]) REFERENCES [Device_Types]([device_type_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_24] FOREIGN KEY ([attributes_type_id]) REFERENCES [Attributes_Types]([attributes_type_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go

CREATE TABLE [Attributes]
( 
	[attribute_id]       integer identity(1,1) NOT NULL ,
	[attributes_type_id] integer  NOT NULL ,
	[device_id]          integer  NOT NULL ,	
	[val]                nvarchar(50)  NOT NULL ,
	CONSTRAINT [XPKAttributes_Types_Devices] PRIMARY KEY  CLUSTERED ([attribute_id] ASC),
	CONSTRAINT [R_2] FOREIGN KEY ([attributes_type_id]) REFERENCES [Attributes_Types]([attributes_type_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT [R_3] FOREIGN KEY ([device_id]) REFERENCES [Devices]([device_id])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)
go
