using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace cw10.DTOs.Requests
{
    public class PromoteStudentRequest
    {
        [Required]
        public string Studies { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}
