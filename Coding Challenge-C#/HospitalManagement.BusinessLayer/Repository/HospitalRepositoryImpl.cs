using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Exception;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HospitalManagementSystem.Util;

namespace HospitalManagementSystem.DAO
{
    public class HospitalServiceImpl : IHospitalService
    {
        private object DBConnUtil;

        public bool ScheduleAppointment(Appointment appointment)
        {
            var connUtil = DBConnUtil; // Check this variable's type
            Console.WriteLine(connUtil.GetType());

            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Appointment (appointmentId, patientId, doctorId, appointmentDate, description) VALUES (@appointmentId, @patientId, @doctorId, @appointmentDate, @description)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@appointmentId", appointment.AppointmentId);
                    command.Parameters.AddWithValue("@patientId", appointment.PatientId);
                    command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                    command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                    command.Parameters.AddWithValue("@description", appointment.Description);

                    int result = command.ExecuteNonQuery(); // Execute the insert command
                    return result > 0; // Return true if at least one record was inserted
                }
            }
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Appointment WHERE appointmentId = @appointmentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@appointmentId", appointmentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Appointment
                            {
                                AppointmentId = (int)reader["appointmentId"],
                                PatientId = (int)reader["patientId"],
                                DoctorId = (int)reader["doctorId"],
                                AppointmentDate = reader["appointmentDate"].ToString(),
                                Description = reader["description"].ToString()
                            };
                        }
                        else
                        {
                            throw new PatientNumberNotFoundException("Appointment not found.");
                        }
                    }
                }
            }
        }

        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Appointment WHERE patientId = @patientId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@patientId", patientId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                AppointmentId = (int)reader["appointmentId"],
                                PatientId = (int)reader["patientId"],
                                DoctorId = (int)reader["doctorId"],
                                AppointmentDate = reader["appointmentDate"].ToString(),
                                Description = reader["description"].ToString()
                            });
                        }
                    }
                }
            }

            if (appointments.Count == 0)
            {
                throw new PatientNumberNotFoundException("No appointments found for the given patient ID.");
            }

            return appointments;
        }

        public List<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Appointment WHERE doctorId = @doctorId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@doctorId", doctorId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                AppointmentId = (int)reader["appointmentId"],
                                PatientId = (int)reader["patientId"],
                                DoctorId = (int)reader["doctorId"],
                                AppointmentDate = reader["appointmentDate"].ToString(),
                                Description = reader["description"].ToString()
                            });
                        }
                    }
                }
            }

            if (appointments.Count == 0)
            {
                throw new PatientNumberNotFoundException("No appointments found for the given doctor ID.");
            }

            return appointments;
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "UPDATE Appointment SET patientId = @patientId, doctorId = @doctorId, appointmentDate = @appointmentDate, description = @description WHERE appointmentId = @appointmentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@appointmentId", appointment.AppointmentId);
                    command.Parameters.AddWithValue("@patientId", appointment.PatientId);
                    command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                    command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                    command.Parameters.AddWithValue("@description", appointment.Description);

                    int result = command.ExecuteNonQuery(); // Execute the update command
                    return result > 0; // Return true if the update was successful
                }
            }
        }

        public bool CancelAppointment(int appointmentId)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Appointment WHERE appointmentId = @appointmentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@appointmentId", appointmentId);

                    int result = command.ExecuteNonQuery(); // Execute the delete command
                    return result > 0; // Return true if the deletion was successful
                }
            }
        }
    }
}
