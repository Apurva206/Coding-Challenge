CREATE DATABASE HospitalDB;
USE HospitalDB;

CREATE TABLE Patient (
    patientId INT PRIMARY KEY,
    firstName VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    dateOfBirth DATE NOT NULL,
    gender VARCHAR(10) NOT NULL,
    contactNumber VARCHAR(15) NOT NULL,
    address VARCHAR(255) NOT NULL
);

CREATE TABLE Doctor (
    doctorId INT PRIMARY KEY,
    firstName VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    specialization VARCHAR(100) NOT NULL,
    contactNumber VARCHAR(15) NOT NULL
);

CREATE TABLE Appointment (
    appointmentId INT PRIMARY KEY,
    patientId INT NOT NULL,
    doctorId INT NOT NULL,
    appointmentDate DATE NOT NULL,
    description VARCHAR(255),
    FOREIGN KEY (patientId) REFERENCES Patient(patientId),
    FOREIGN KEY (doctorId) REFERENCES Doctor(doctorId)
);

INSERT INTO Patient (patientId, firstName, lastName, dateOfBirth, gender, contactNumber, address) VALUES
(1, 'Rahul', 'Sharma', '1985-03-25', 'Male', '9876543210', '123, MG Road, Mumbai, Maharashtra'),
(2, 'Anita', 'Verma', '1990-07-15', 'Female', '9123456780', '456, Park Avenue, Delhi'),
(3, 'Amit', 'Patel', '1982-11-20', 'Male', '9988776655', '789, Main Street, Ahmedabad, Gujarat'),
(4, 'Priya', 'Kumar', '1995-01-30', 'Female', '9345678901', '321, 1st Cross, Bangalore, Karnataka'),
(5, 'Ravi', 'Singh', '1978-05-18', 'Male', '9871234567', '654, Market Road, Jaipur, Rajasthan');

INSERT INTO Doctor (doctorId, firstName, lastName, specialization, contactNumber) VALUES
(1, 'Dr. Vikram', 'Agarwal', 'Cardiologist', '9654321098'),
(2, 'Dr. Neeta', 'Sethi', 'Dermatologist', '9321567890'),
(3, 'Dr. Arjun', 'Choudhury', 'Orthopedic', '8712345678'),
(4, 'Dr. Aarti', 'Desai', 'Pediatrician', '9998887777'),
(5, 'Dr. Sunil', 'Iyer', 'General Physician', '9988776655');

INSERT INTO Appointment (appointmentId, patientId, doctorId, appointmentDate, description) VALUES
(1, 1, 1, '2024-10-20', 'Heart Checkup'),
(2, 2, 2, '2024-10-21', 'Skin Allergy Consultation'),
(3, 3, 3, '2024-10-22', 'Knee Pain Consultation'),
(4, 4, 4, '2024-10-23', 'Child Vaccination'),
(5, 5, 5, '2024-10-24', 'General Health Checkup');

SELECT * FROM Patient
SELECT * FROM Doctor
SELECT * FROM Appointment

