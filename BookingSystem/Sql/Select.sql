--Don't forget to connect into your database before executing...

--Get all table values
SELECT * FROM Requests;
SELECT * FROM Offices;
SELECT * FROM Services;
SELECT * FROM Users;
SELECT * FROM Statuses;
SELECT * FROM Roles;
SELECT * FROM Authentications;

-- Get with order by
SELECT * FROM Requests ORDER BY TrackingId;
SELECT * FROM Requests ORDER BY UserNote DESC;
SELECT * FROM Requests ORDER BY TrackingId, CreatedAt;
SELECT * FROM Requests ORDER BY TrackingId ASC, CreatedAt DESC;

-- Get Join
SELECT * FROM Requests LEFT JOIN Offices ON Requests.OfficeId = Offices.Id;
SELECT * FROM Users LEFT JOIN Roles ON Users.RoleId = Roles.Id;
SELECT Requests.Id, Requests.StatusId, Requests.UserNote, Requests.OfficeId, Offices.Name FROM Requests INNER JOIN Offices ON Requests.OfficeId = Offices.Id;
SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id LEFT JOIN Offices ON Requests.OfficeId=Offices.Id LEFT JOIN  Services ON Requests.ServiceId=Services.Id ORDER BY Requests.CreatedAt;

SELECT Users.Id,Users.FirstName, Users.LastName, Users.Email, Roles.Name as Role, Users.CreatedAt, Users.UpdatedAt FROM Users LEFT JOIN Roles ON Users.RoleId = Roles.Id ORDER BY Users.CreatedAt;
SELECT Users.Id as UserId, Users.FirstName, Users.LastName, Users.Email,Users.Password, Roles.Id as RoleId, Roles.Name as Role FROM Users LEFT JOIN Roles ON Users.RoleId = Roles.Id WHERE Users.Email = 'rojhon@gmail.com';
SELECT Authentications.Id, Authentications.Token, Roles.Name FROM Authentications JOIN Roles ON Authentications.RoleId = Roles.Id WHERE Token = 'Bearer sjdjsgdjgsjgdsd';