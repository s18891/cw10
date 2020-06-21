using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace cw10.DTOs.Requests
{
    public class ModifyStudentRequest
    {
        [Required]
        public string IndexNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int IdEnrollment { get; set; }
    }
}
