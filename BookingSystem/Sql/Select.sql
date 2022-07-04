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
SELECT Requests.Id, Requests.StatusId, Requests.UserNote, Requests.OfficeId, Offices.Name FROM Requests INNER JOIN Offices ON Requests.OfficeId = Offices.Id;
SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Offices.Name as Office, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt FROM Requests INNER JOIN Statuses ON Requests.StatusId=Statuses.Id INNER JOIN Offices ON Requests.OfficeId=Offices.Id;

SELECT Authentications.Id, Authentications.Token, Roles.Name FROM Authentications JOIN Roles ON Authentications.RoleId = Roles.Id WHERE Token = 'Bearer sjdjsgdjgsjgdsd';