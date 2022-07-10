--Don't forget to connect into your database before executing...

INSERT INTO Offices (Name) VALUES ('Registrar''s Office');
INSERT INTO Offices (Name) VALUES ('OSDS Office');

INSERT INTO Services(Name, Fee) VALUES ('Transcript of Records', 50);
INSERT INTO Services(Name, Fee) VALUES ('Scholastic Records', 20);
INSERT INTO Services(Name, Fee) VALUES ('Certificate of Grades', 20);
INSERT INTO Services(Name, Fee) VALUES ('Certificate of Enrolment', 20);
INSERT INTO Services(Name, Fee) VALUES ('Certificate of Graduation', 20);
INSERT INTO Services(Name, Fee) VALUES ('Certification, Authentication and Verification', 20);
INSERT INTO Services(Name, Fee) VALUES ('Transfer Credentials/Honorable Dismissal', 20);
INSERT INTO Services(Name, Fee) VALUES ('Certificate of Good Moral Character', 20);
INSERT INTO Services(Name, Fee) VALUES ('Form 137(For High School and Senior High School)', 0);
INSERT INTO Services(Name, Fee) VALUES ('Second Copy of Diploma', 0);

INSERT INTO Statuses(Id, Name) VALUES (1, 'Paid');
INSERT INTO Statuses(Id, Name) VALUES (2, 'Processing');
INSERT INTO Statuses(Id, Name) VALUES (3, 'Pending');
INSERT INTO Statuses(Id, Name) VALUES (4, 'Payment Required');
INSERT INTO Statuses(Id, Name) VALUES (5, 'Finished');

INSERT INTO Roles(Id, Name) VALUES (1, 'Admin');
INSERT INTO Roles(Id, Name) VALUES (2, 'Officer');

--INSERT INTO Authentications(Token, RoleId, UserId) VALUES ('Bearer sjdjsgdjgsjgdsd', 1, 1);
--INSERT INTO Authentications(Token, RoleId, UserId) VALUES ('Bearer hefiafizepzgenozngopzngpzegn', 1, 1);
--INSERT INTO Authentications(Token, RoleId, UserId) VALUES ('Bearer abcdefgh', 2, 1);

--INSERT INTO Requests (UserNote) VALUES ('Request1');
--INSERT INTO Requests (OfficeId, ServiceId, UserNote) VALUES (1, 2, 'Request2');
--INSERT INTO Requests (OfficeId, ServiceId, UserNote) VALUES (2, 3, 'Request3');