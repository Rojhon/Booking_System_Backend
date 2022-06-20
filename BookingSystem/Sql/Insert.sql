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

--INSERT INTO Requests (UserNote) VALUES ('Request1');
--INSERT INTO Requests (OfficeId, ServiceId, UserNote) VALUES (1, 2, 'Request2');
--INSERT INTO Requests (OfficeId, ServiceId, UserNote) VALUES (2, 3, 'Request3');