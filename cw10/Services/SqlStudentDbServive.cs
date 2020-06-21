using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw10.DTOs.Requests;
using cw10.Models;
using Microsoft.EntityFrameworkCore;

namespace cw10.Services
{
    public class SqlStudentDbService : IStudentDbService
    {
        public s18891Context _context { get; set; }

        public SqlStudentDbService(s18891Context context)
        {
            _context = context;
        }


        public IEnumerable GetStudents()
        {
            var list = _context.Student.ToList();
            return list;
        }

        public Student ModifyStudent(ModifyStudentRequest request)
        {
            var student = _context.Student.FirstOrDefault(e => e.IndexNumber == request.IndexNumber);

            if (student == null)
                throw new Exception("Brak studenta o podanym numerze");

            student.IndexNumber = request.IndexNumber;
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.BirthDate = request.BirthDate;
            student.IdEnrollment = request.IdEnrollment;

            _context.SaveChanges();

            return student;
        }

        public Student DeleteStudent(DeleteStudentRequest request)
        {
            var student = _context.Student.FirstOrDefault(s => s.IndexNumber == request.IndexNumber);

            if (student == null)
                throw new Exception("Nie ma takiego studenta");

            _context.Remove(student);
            _context.SaveChanges();

            return student;
        }


        public Enrollment EnrollStudent(EnrollStudentRequest request)
        {
            var studies = _context.Studies.FirstOrDefault(s => s.Name == request.StudiesName);

            if (studies == null)
                throw new Exception("Brak studiów o podanej nazwie");


            var enrollment = _context.Enrollment.Where(e => e.IdStudy == studies.IdStudy && e.Semester == 1)
                .OrderByDescending(e => e.StartDate).FirstOrDefault();

            if (enrollment == null)
            {
                enrollment = new Enrollment()
                {
                    IdEnrollment = _context.Enrollment.Max(e => e.IdEnrollment) + 1,
                    Semester = 1,
                    IdStudy = studies.IdStudy,
                    StartDate = DateTime.Now
                };
                _context.Enrollment.Add(enrollment);
            }

            var czyStudentIstnieje = _context.Student.FirstOrDefault(e => e.IndexNumber == request.IndexNumber);

            if (czyStudentIstnieje != null)
                throw new Exception("Student o podanym numerze juz istnieje");


            var student = new Student()
            {
                IndexNumber = request.IndexNumber,
                BirthDate = Convert.ToDateTime(request.BirthDate),
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdEnrollment = enrollment.IdEnrollment
            };

            _context.Student.Add(student);
            _context.SaveChanges();
            return enrollment;

        }

        public Enrollment PromoteStudent(PromoteStudentRequest request)
        {
            var res = _context.Enrollment.Join(_context.Studies, enroll => enroll.IdStudy, stud => stud.IdStudy,
                (enroll, stud) => new
                {
                    stud.Name,
                    enroll.Semester
                }).FirstOrDefault(e => e.Name == request.Studies && e.Semester == request.Semester);

            

            _context.Database.ExecuteSqlRaw("EXEC PromoteStudents @Studies, @Semester", request.Studies, request.Semester);
            _context.SaveChanges();

            var res2 = _context.Enrollment.Join(_context.Studies, enroll => enroll.IdStudy, stud => stud.IdStudy,
                (enroll, stud) => new
                {
                    enroll.IdEnrollment,
                    enroll.Semester,
                    enroll.IdStudy,
                    enroll.StartDate,
                    stud.Name
                }).Where(e => e.Name == request.Studies && e.Semester == request.Semester + 1).ToList();

            var enrollment = new Enrollment()
            {
                IdEnrollment = res2[0].IdEnrollment,
                Semester = res2[0].Semester,
                IdStudy = res2[0].IdStudy,
                StartDate = res2[0].StartDate
            };
            return enrollment;
        }
 
    }
}
