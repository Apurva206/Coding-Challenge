using System;

namespace HospitalManagementSystem.Exception
{
    public class PatientNumberNotFoundException : System.Exception
    {
        public PatientNumberNotFoundException(string message) : base(message) { }
    }
}
