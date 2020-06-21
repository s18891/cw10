using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using cw10.DTOs.Requests;
using cw10.Models;

namespace cw10.Services
{
    public interface IStudentDbService
    {
        public IEnumerable GetStudents();
        public Student ModifyStudent(ModifyStudentRequest request);
        public Student DeleteStudent(DeleteStudentRequest request);

        Enrollment EnrollStudent(EnrollStudentRequest request);
        Enrollment PromoteStudent(PromoteStudentRequest request);
    }
}
