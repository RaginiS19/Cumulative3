using Microsoft.AspNetCore.Mvc;
using CumulativeProject.Models;
using MySql.Data.MySqlClient;

namespace CumulativeProject.Controllers
{
    /// <summary>
    /// API Controller for managing teacher data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        // Database context used to access the school database.
        private readonly SchoolDbContext _context = new SchoolDbContext();

        /// <summary>
        /// Updates an existing teacher's details in the database.
        /// </summary>
        /// <param name="id">The ID of the teacher to update.</param>
        /// <param name="updatedTeacher">The updated teacher data sent in the request body.</param>
        /// <returns>
        /// HTTP 200 OK if update is successful,
        /// or HTTP 404 Not Found if no teacher is found with the given ID.
        /// </returns>
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
                             WHERE teacher
