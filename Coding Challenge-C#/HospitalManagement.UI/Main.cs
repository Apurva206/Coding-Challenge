using System;
using System.Collections.Generic;
using HospitalManagementSystem.DAO;
using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Exception;
using System.Data.SqlClient; 
using HospitalManagementSystem.Util; 

namespace HospitalManagementSystem.Main
{
    public class MainModule
    {
        public static object DBPropertyUtil { get; private set; }

        static void Main(string[] args)
        {
            IHospitalService service = new HospitalServiceImpl();

            // Check the database connection
            if (!CheckDatabaseConnection())
            {
                Console.WriteLine("Database connection failed. Exiting the application.");
                return; // Exit if the connection fails
            }

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n---- Hospital Management System ----");
                Console.WriteLine("1. Schedule Appointment");
                Console.WriteLine("2. View Appointment by ID");
                Console.WriteLine("3. View Appointments for a Patient");
                Console.WriteLine("4. View Appointments for a Doctor");
                Console.WriteLine("5. Update Appointment");
                Console.WriteLine("6. Cancel Appointment");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option (1-7): ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                    continue; // Skip to the next iteration of the loop
                }

                switch (choice)
                {
                    case 1:
                        ScheduleAppointment(service);
                        break;
                    case 2:
                        ViewAppointmentById(service);
                        break;
                    case 3:
                        ViewAppointmentsForPatient(service);
                        break;
                    case 4:
                        ViewAppointmentsForDoctor(service);
                        break;
                    case 5:
                        UpdateAppointment(service);
                        break;
                    case 6:
                        CancelAppointment(service);
                        break;
                    case 7:
                        exit = true;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static bool CheckDatabaseConnection()
        {
            string connectionString = DBPropertyUtil.GetConnectionString(); // Retrieve the connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Attempt to open the connection
                    Console.WriteLine("Database connection successful!");
                    return true; // Connection was successful
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    Console.WriteLine("Database connection failed: " + ex.Message);
                    return false; // Connection failed
                }
                catch (System.Exception ex)
                {
                    // Handle general exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                    return false; // Connection failed
                }
            }
        }

        static void ScheduleAppointment(IHospitalService service)
        {
            try
            {
                Console.WriteLine("Enter Appointment Details:");
                Console.Write("Appointment ID: ");
                int appointmentId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Patient ID: ");
                int patientId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Doctor ID: ");
                int doctorId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Appointment Date (YYYY-MM-DD): ");
                string date = Console.ReadLine();

                Console.Write("Description: ");
                string description = Console.ReadLine();

                Appointment appointment = new Appointment
                {
                    AppointmentId = appointmentId,
                    PatientId = patientId,
                    DoctorId = doctorId,
                    AppointmentDate = date,
                    Description = description
                };

                bool success = service.ScheduleAppointment(appointment);
                if (success)
                    Console.WriteLine("Appointment scheduled successfully.");
                else
                    Console.WriteLine("Failed to schedule appointment.");
            }
            catch (System.Exception ex)  // Fully qualify the exception
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ViewAppointmentById(IHospitalService service)
        {
            try
            {
                Console.Write("Enter Appointment ID: ");
                int appointmentId = Convert.ToInt32(Console.ReadLine());

                Appointment appointment = service.GetAppointmentById(appointmentId);
                Console.WriteLine($"Appointment found: {appointment.Description}");
            }
            catch (PatientNumberNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (System.Exception ex)  // Fully qualify the exception
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ViewAppointmentsForPatient(IHospitalService service)
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                int patientId = Convert.ToInt32(Console.ReadLine());

                List<Appointment> appointments = service.GetAppointmentsForPatient(patientId);
                Console.WriteLine($"Appointments for Patient {patientId}:");
                foreach (var appointment in appointments)
                    Console.WriteLine($"ID: {appointment.AppointmentId}, Date: {appointment.AppointmentDate}");
            }
            catch (PatientNumberNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (System.Exception ex)  // Fully qualify the exception
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ViewAppointmentsForDoctor(IHospitalService service)
        {
            try
            {
                Console.Write("Enter Doctor ID: ");
                int doctorId = Convert.ToInt32(Console.ReadLine());

                List<Appointment> appointments = service.GetAppointmentsForDoctor(doctorId);
                if (appointments.Count > 0)
                {
                    Console.WriteLine($"Appointments for Doctor {doctorId}:");
                    foreach (var appointment in appointments)
                        Console.WriteLine($"ID: {appointment.AppointmentId}, Date: {appointment.AppointmentDate}");
                }
                else
                {
                    Console.WriteLine("No appointments found for the doctor.");
                }
            }
            catch (System.Exception ex)  // Fully qualify the exception
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void UpdateAppointment(IHospitalService service)
        {
            try
            {
                Console.Write("Enter Appointment ID to update: ");
                int appointmentId = Convert.ToInt32(Console.ReadLine());

                Appointment appointment = new Appointment
                {
                    AppointmentId = appointmentId
                };

                Console.Write("New Patient ID: ");
                appointment.PatientId = Convert.ToInt32(Console.ReadLine());

                Console.Write("New Doctor ID: ");
                appointment.DoctorId = Convert.ToInt32(Console.ReadLine());

                Console.Write("New Appointment Date (YYYY-MM-DD): ");
                appointment.AppointmentDate = Console.ReadLine();

                Console.Write("New Description: ");
                appointment.Description = Console.ReadLine();

                bool success = service.UpdateAppointment(appointment);
                if (success)
                    Console.WriteLine("Appointment updated successfully.");
                else
                    Console.WriteLine("Failed to update appointment.");
            }
            catch (System.Exception ex)  // Fully qualify the exception
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void CancelAppointment(IHospitalService service)
        {
            try
            {
                Console.Write("Enter Appointment ID to cancel: ");
                int appointmentId = Convert.ToInt32(Console.ReadLine());

                bool success = service.CancelAppointment(appointmentId);
                if (success)
                    Console.WriteLine("Appointment cancelled successfully.");
                else
                    Console.WriteLine("Failed to cancel appointment.");
            }
            catch (System.Exception ex)  // Fully qualify the exception
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
