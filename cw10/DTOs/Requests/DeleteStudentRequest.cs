using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace cw10.DTOs.Requests
{
    public class DeleteStudentRequest
    {
        [Required]
        public string IndexNumber { get; set; }
    }
}