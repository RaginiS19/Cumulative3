using Microsoft.AspNetCore.Mvc;
using CumulativeProject.Models;
using MySql.Data.MySqlClient;

namespace CumulativeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();

        // PUT: api/TeacherAPI/5
        [HttpPut("{id}")]
        public IActionResult UpdateTeacher(int id, [FromBody] Teacher updatedTeacher)
        {
            using var conn = _context.AccessDatabase();
            conn.Open();

            string query = @"UPDATE teachers 
                             SET teacherfname = @FirstName,
                                 teacherlname = @LastName,
                                 employeenumber = @EmployeeNumber,
                                 hiredate = @HireDate,
                                 salary = @Salary
                             WHERE teacherid = @TeacherId";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FirstName", updatedTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@LastName", updatedTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", updatedTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", updatedTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", updatedTeacher.Salary);
            cmd.Parameters.AddWithValue("@TeacherId", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                return Ok(new { message = "Teacher updated successfully." });
            else
                return NotFound(new { message = "Teacher not found." });
        }
    }
}
