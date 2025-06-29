﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Teamspace.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalId { get; set; }

        public int Year { get; set; }

        public string Password { get; set; }

        public byte[]? Image { get; set; }


        // relationShips
        // foreign key
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<QuestionAns> QuestionAnss { get; set; }

        public ICollection<AssignmentAns> AssignmentAnss { get; set; }

        public ICollection<StudentStatus> StudentStatuses { get; set; }

    }
}
