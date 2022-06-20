--Don't forget to connect into your database before executing...

--Get all table values
SELECT * FROM Requests;
SELECT * FROM Offices;
SELECT * FROM Services;
SELECT * FROM Users;

-- Get with order by
SELECT * FROM Requests ORDER BY TrackingId;
SELECT * FROM Requests ORDER BY UserNote DESC;
SELECT * FROM Requests ORDER BY TrackingId, CreatedAt;
SELECT * FROM Requests ORDER BY TrackingId ASC, CreatedAt DESC;

-- Get Join
SELECT * FROM Requests LEFT JOIN Offices ON Requests.OfficeId = Offices.Id;
SELECT Requests.Id, Requests.Status, Requests.UserNote, Requests.OfficeId, Offices.Name FROM Requests INNER JOIN Offices ON Requests.OfficeId = Offices.Id;