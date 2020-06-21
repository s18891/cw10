using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace cw10.DTOs.Requests
{

        public class EnrollStudentRequest
        {
            [Required(ErrorMessage = "Brak numeru studenta")]
            [RegularExpression("^s[0-9]+$", ErrorMessage = "Błędny numer studenta")]
            public string IndexNumber { get; set; }

            [Required(ErrorMessage = "Brak imienia")]
            [MaxLength(100)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Brak nazwiska")]
            [MaxLength(100)]
            public string LastName { get; set; }

            [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Niepoprawna data urodzenia")]
            public DateTime BirthDate { get; set; }

            [Required(ErrorMessage = "Brak nazwy studiów")]
            [MaxLength(100)]
            public string StudiesName { get; set; }
        }
    }
