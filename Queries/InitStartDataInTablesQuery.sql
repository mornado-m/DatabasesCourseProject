INSERT INTO Devices_Statuses (name, description) VALUES 
('�������', ''),
('������� �������', ''),
('� ������', ''),
('�� ������ �������', ''),
('��������', ''),
('��������', '');

INSERT INTO Transfers_Types (name, description) VALUES 
('������', ''),
('����������', ''),
('����� � ������', ''),
('������', ''),
('��������', '');

INSERT INTO Transfer_Department_Roles (name) VALUES 
('³���-�������'),
('³��� �����������');

INSERT INTO Roles (name, permission_level) VALUES 
('SuperAdmin', 3),
('������������', 2),
('����������', 1);

INSERT INTO Users (login, pass) VALUES 
('mornado', '1224'),
('n.dykyi', '123'),
('v.andruhovych', '123');

INSERT INTO User_Roles(user_id, role_id) VALUES 
(1, 1),
(2, 2),
(3, 3);

INSERT INTO Departments (name, location) VALUES 
('��������', '�. ����, ���. ���������� 7'),
('����������', '�. ����, ���. ���������� 8'),
('�������', '�. ����, ���. ���������� 7');

INSERT INTO Managers (department_id, first_name, last_name, address, tel, dob, user_id) VALUES 
(1, '������', '�������', '�. ����, ���. ������� ������� 14', '380635478123', '1996-06-06', 1),
(2, '�����', '���������', '�. ����, ���. ��������� 23', '380671234567', '1975-07-24', 2),
(2, '����', '������', '�. ����, ���. ������������� 37', '380707010000', '1985-01-07', 3);

INSERT INTO Device_Types(name) VALUES 
('��'),
('�������'),
('����'),
('���������'),
('�������'),
('������'),
('�������'),
('�������');

INSERT INTO Attributes_Types(name, units_of_measurement) VALUES 
('RAM', '��'),
('ROM', '��'),
('CPU', '���'),
('GPU', '��'),
('���', ''),
('ĳ������� ������', '"'),
('�������� ���������', ''),
('����', ''),
('������� ���������', '��'),
('��� ������', '�'),
('��������', ''),
('������', '');

INSERT INTO Device_Type_Attributes(device_type_id, attributes_type_id) VALUES 
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 8),
(1, 11),
(1, 12),
(2, 5),
(2, 6),
(2, 7),
(2, 8),
(2, 9),
(2, 10),
(2, 11),
(2, 12),
(3, 5),
(3, 8),
(3, 11),
(3, 12),
(4, 5),
(4, 8),
(4, 11),
(4, 12),
(5, 5),
(5, 8),
(5, 11),
(5, 12),
(6, 5),
(6, 8),
(6, 11),
(6, 12),
(7, 1),
(7, 2),
(7, 3),
(7, 4),
(7, 5),
(7, 6),
(7, 7),
(7, 8),
(7, 10),
(7, 11),
(7, 12),
(8, 1),
(8, 2),
(8, 3),
(8, 4),
(8, 6),
(8, 7),
(8, 8),
(8, 9),
(8, 10),
(8, 11),
(8, 12);

INSERT INTO Devices (devices_status_id, device_type_id, cost, serial_num, department_id, Production_date) VALUES 
(1, 1, 700, 1001, 1, '2016-03-15'),
(1, 1, 700, 1002, 1, '2016-03-15'),
(1, 1, 700, 1003, 1, '2016-03-15'),
(2, 1, 700, 1004, 1, '2016-03-15'),
(1, 1, 700, 1005, 2, '2016-03-15'),
(3, 1, 700, 1006, 2, '2016-03-15'),

(1, 8, 900, 8001, 2, '2016-04-04'),

(1, 2, 300, 2001, 1, '2015-08-30'),
(1, 2, 300, 2002, 1, '2015-08-30'),
(1, 2, 300, 2003, 1, '2015-08-30'),
(4, 2, 300, 2004, 1, '2015-08-30'),
(1, 2, 300, 2005, 2, '2015-08-30'),
(1, 2, 300, 2006, 2, '2015-08-30'),
(1, 2, 350, 2007, 1, '2016-03-09'),

(1, 3, 50, 3001, 1, '2015-07-13'),
(1, 3, 50, 3002, 1, '2015-07-13'),
(1, 3, 50, 3003, 1, '2015-07-13'),
(1, 3, 50, 3004, 1, '2015-07-13'),
(1, 3, 50, 3005, 2, '2015-07-13'),
(1, 3, 50, 3006, 2, '2015-07-13'),

(1, 4, 65, 4001, 1, '2014-09-12'),
(1, 4, 65, 4002, 1, '2014-09-12'),
(1, 4, 65, 4003, 1, '2014-09-12'),
(1, 4, 65, 4004, 1, '2014-09-12'),
(1, 4, 65, 4005, 2, '2014-09-12'),
(1, 4, 65, 4006, 2, '2014-09-12'),

(1, 5, 150, 5001, 2, '2013-10-11'),
(1, 6, 100, 6001, 2, '2014-12-10'),
(1, 8, 900, 8002, 3, '2016-04-04');

INSERT INTO Attributes (device_id, attributes_type_id, val) VALUES 
(1, 1, '8'),
(1, 2, '1000'),
(1, 3, '2.4'),
(1, 4, '2'),
(1, 8, '������'),
(1, 11, 'ASUS'),

(2, 1, '8'),
(2, 2, '1000'),
(2, 3, '2.4'),
(2, 4, '2'),
(2, 8, '������'),
(2, 11, 'ASUS'),

(3, 1, '8'),
(3, 2, '1000'),
(3, 3, '2.4'),
(3, 4, '2'),
(3, 8, '������'),
(3, 11, 'ASUS'),

(4, 1, '8'),
(4, 2, '1000'),
(4, 3, '2.4'),
(4, 4, '2'),
(4, 8, '������'),
(4, 11, 'ASUS'),

(5, 1, '8'),
(5, 2, '1000'),
(5, 3, '2.4'),
(5, 4, '2'),
(5, 8, '������'),
(5, 11, 'ASUS'),

(6, 1, '8'),
(6, 2, '1000'),
(6, 3, '2.4'),
(6, 4, '2'),
(6, 8, '������'),
(6, 11, 'ASUS'),

(7, 1, '8'),
(7, 2, '1000'),
(7, 3, '2.7'),
(7, 4, '2'),
(7, 6, '17'),
(7, 7, '1920 X 1080'),
(7, 8, '������'),
(7, 9, '60'),
(7, 10, '175'),
(7, 11, 'Lenovo'),
(7, 12, 'Y600'),

(8, 6, '24'),
(8, 7, '1920 X 1080'),
(8, 8, '������'),
(8, 9, '60'),
(8, 10, '180'),
(8, 11, 'LG'),
(8, 12, '24MP88HV'),

(9, 6, '24'),
(9, 7, '1920 X 1080'),
(9, 8, '������'),
(9, 9, '60'),
(9, 10, '180'),
(9, 11, 'LG'),
(9, 12, '24MP88HV'),

(10, 6, '24'),
(10, 7, '1920 X 1080'),
(10, 8, '������'),
(10, 9, '60'),
(10, 10, '180'),
(10, 11, 'LG'),
(10, 12, '24MP88HV'),

(11, 6, '24'),
(11, 7, '1920 X 1080'),
(11, 8, '������'),
(11, 9, '60'),
(11, 10, '180'),
(11, 11, 'LG'),
(11, 12, '24MP88HV'),

(12, 6, '24'),
(12, 7, '1920 X 1080'),
(12, 8, '������'),
(12, 9, '60'),
(12, 10, '180'),
(12, 11, 'LG'),
(12, 12, '24MP88HV'),

(13, 6, '24'),
(13, 7, '1920 X 1080'),
(13, 8, '������'),
(13, 9, '60'),
(13, 10, '180'),
(13, 11, 'LG'),
(13, 12, '24MP88HV'),

(14, 6, '25'),
(14, 7, '1920 X 1080'),
(14, 8, '������'),
(14, 9, '60'),
(14, 10, '180'),
(14, 11, 'LG'),
(14, 12, '24MP90HQ'),

(15, 5, '�������'),
(15, 8, '������'),
(15, 11, 'Logitech'),
(15, 12, 'G300S'),

(16, 5, '�������'),
(16, 8, '������'),
(16, 11, 'Logitech'),
(16, 12, 'G300S'),

(17, 5, '�������'),
(17, 8, '������'),
(17, 11, 'Logitech'),
(17, 12, 'G300S'),

(18, 5, '�������'),
(18, 8, '������'),
(18, 11, 'Logitech'),
(18, 12, 'G300S'),

(19, 5, '�������'),
(19, 8, '������'),
(19, 11, 'Logitech'),
(19, 12, 'G300S'),

(20, 5, '�������'),
(20, 8, '������'),
(20, 11, 'Logitech'),
(20, 12, 'G300S'),

(21, 5, '�������'),
(21, 8, '������'),
(21, 11, 'Logitech'),
(21, 12, 'K350'),

(22, 5, '�������'),
(22, 8, '������'),
(22, 11, 'Logitech'),
(22, 12, 'K350'),

(23, 5, '�������'),
(23, 8, '������'),
(23, 11, 'Logitech'),
(23, 12, 'K350'),

(24, 5, '�������'),
(24, 8, '������'),
(24, 11, 'Logitech'),
(24, 12, 'K350'),

(25, 5, '�������'),
(25, 8, '������'),
(25, 11, 'Logitech'),
(25, 12, 'K350'),

(26, 5, '�������'),
(26, 8, '������'),
(26, 11, 'Logitech'),
(26, 12, 'K350'),

(27, 5, '���'),
(27, 8, '������'),
(27, 11, 'Canon'),
(27, 12, 'TS4050'),

(28, 5, '���'),
(28, 8, '������'),
(28, 11, 'Canon'),
(28, 12, 'TS5040'),

(29, 1, '8'),
(29, 2, '1000'),
(29, 3, '2.7'),
(29, 4, '2'),
(29, 6, '17'),
(29, 7, '1920 X 1080'),
(29, 8, '������'),
(29, 9, '60'),
(29, 10, '175'),
(29, 11, 'Lenovo'),
(29, 12, 'Y600');

INSERT INTO Transfers(device_id, transfers_type_id, cost, date, description, user_id) VALUES 
(1, 1, 650, '2016-06-10', '�������� � Terratm.', 1),
(2, 1, 650, '2016-06-10', '�������� � Terratm.', 1),
(3, 1, 650, '2016-06-10', '�������� � Terratm.', 1),
(4, 1, 650, '2016-06-10', '�������� � Terratm.', 1),
(5, 1, 650, '2016-06-10', '�������� � Terratm.', 1),
(6, 1, 650, '2016-06-10', '�������� � Terratm.', 1),
(7, 1, 880, '2016-06-10', '�������� � Terratm.', 1),

(8, 1, 280, '2016-06-10', '�������� � Terratm.', 1),
(9, 1, 280, '2016-06-10', '�������� � Terratm.', 1),
(10, 1, 280, '2016-06-10', '�������� � Terratm.', 1),
(11, 1, 280, '2016-06-10', '�������� � Terratm.', 1),
(12, 1, 280, '2016-06-10', '�������� � Terratm.', 1),
(13, 1, 280, '2016-06-10', '�������� � Terratm.', 1),

(15, 1, 50, '2016-06-10', '�������� � Terratm.', 1),
(16, 1, 50, '2016-06-10', '�������� � Terratm.', 1),
(17, 1, 50, '2016-06-10', '�������� � Terratm.', 1),
(18, 1, 50, '2016-06-10', '�������� � Terratm.', 1),
(19, 1, 50, '2016-06-10', '�������� � Terratm.', 1),
(20, 1, 50, '2016-06-10', '�������� � Terratm.', 1),

(21, 1, 65, '2016-06-10', '�������� � Terratm.', 1),
(22, 1, 65, '2016-06-10', '�������� � Terratm.', 1),
(23, 1, 65, '2016-06-10', '�������� � Terratm.', 1),
(24, 1, 65, '2016-06-10', '�������� � Terratm.', 1),
(25, 1, 65, '2016-06-10', '�������� � Terratm.', 1),
(26, 1, 65, '2016-06-10', '�������� � Terratm.', 1),

(27, 1, 150, '2016-06-10', '�������� � Terratm.', 1),
(28, 1, 100, '2016-06-10', '�������� � Terratm.', 1),
(29, 1, 880, '2016-06-10', '�������� � Terratm.', 1),

(7, 2, 0, '2016-08-01', '���������� �� ������� �������� ������������ ����� ����������.', 1),
(7, 2, 0, '2016-10-10', '����������.', 2),
(6, 3, 50, '2016-11-15', '����� �� ������ � Terratm.', 2),
(11, 5, 0, '2017-02-3', '�������.', 1),
(14, 1, 350, '2017-02-10', '�������� � Terratm.', 1);

INSERT INTO Transfer_Departments (transfer_id, department_id, transfer_department_role_types_id) VALUES 
(1, 1, 2),
(2, 1, 2),
(3, 1, 2),
(4, 1, 2),
(5, 2, 2),
(6, 2, 2),
(7, 2, 2),

(8, 1, 2),
(9, 1, 2),
(10, 1, 2),
(11, 1, 2),
(12, 2, 2),
(13, 2, 2),

(14, 1, 2),
(15, 1, 2),
(16, 1, 2),
(17, 1, 2),
(18, 2, 2),
(19, 2, 2),

(20, 1, 2),
(21, 1, 2),
(22, 1, 2),
(23, 1, 2),
(24, 2, 2),
(25, 2, 2),

(26, 2, 2),
(27, 2, 2),
(28, 3, 2),

(29, 1, 1),
(29, 2, 2),

(30, 2, 1),
(30, 1, 2),

(31, 2, 1),
(32, 1, 1),
(33, 1, 2);