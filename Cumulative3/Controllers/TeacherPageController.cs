using Microsoft.AspNetCore.Mvc;
using CumulativeProject.Models;
using MySql.Data.MySqlClient;
using System;

namespace CumulativeProject.Controllers
{
    /// <summary>
    /// MVC Controller for rendering views and handling form submissions related to teachers.
    /// </summary>
    public class TeacherPageController : Controller
    {
        // Database context used to access and interact with the school database.
        private readonly SchoolDbContext _context = new SchoolDbContext();

        /// <summary>
        /// Loads the teacher's data into an editable form.
        /// </summary>
        /// <param name="id">ID of the teacher to be edited.</param>
        /// <returns>Returns the Edit view populated with the teacher's data, or redirects if not found.</returns>
        public IActionResult Edit(int id)
        {
            Teacher teacher = new Teacher();
            using var conn = _context.AccessDatabase();
            conn.Open();

            string query = "SELECT * FROM teachers WHERE teacherid = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                teacher.TeacherId = Convert.ToInt32(reader["teacherid"]);
                teacher.TeacherFName = reader["teacherfname"]?.ToString();
                teacher.TeacherLName = reader["teacherlname"]?.ToString();
                teacher.EmployeeNumber = reader["employeenumber"]?.ToString();
                teacher.HireDate = reader["hiredate"] as DateTime?;
                teacher.Salary = reader["salary"] as decimal?;
            }
            else
            {
                // Teacher not found
                TempData["ErrorMessage"] = "Teacher not found!";
                return RedirectToAction("Index", "Home");
            }

            return View(teacher);
        }

        /// <summary>
        /// Updates a teacher's information in the database after form submission.
        /// </summary>
        /// <param name="teacher">The updated teacher object from the form.</param>
        /// <returns>
        /// Redirects to Edit page on success,
        /// or returns the same view with validation errors if input is invalid.
        /// </returns>
        [HttpPost]
        public IActionResult Update(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                // Validate required fields
                if (string.IsNullOrEmpty(teacher.TeacherFName) || string.IsNullOrEmpty(teacher.TeacherLName))
                {
                    ModelState.AddModelError("TeacherFName", "First Name and Last Name are required.");
                    return View("Edit", teacher);
                }

                // Validate hire date
                if (teacher.HireDate > DateTime.Now)
                {
                    ModelState.AddModelError("HireDate", "Hire Date cannot be in the future.");
                    return View("Edit", teacher);
                }

                // Validate salary
                if (teacher.Salary < 0)
                {
                    ModelState.AddModelError("Salary", "Salary must be greater than or equal to 0.");
                    return View("Edit", teacher);
                }

                using var conn = _context.AccessDatabase();
                conn.Open();

                // Check if teacher exists
                string checkQuery = "SELECT COUNT(*) FROM teachers WHERE teacherid = @TeacherId";
                using var checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);
                var count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    TempData["ErrorMessage"] = "Teacher not found!";
                    return RedirectToAction("Edit", new { id = teacher.TeacherId });
                }

                // Update query
                string query = @"UPDATE teachers 
                                    SET teacherfname = @FirstName,
                                        teacherlname = @LastName,
                                        employeenumber = @EmployeeNumber,
                                        hiredate = @HireDate,
                                        salary = @Salary
                                    WHERE teacherid = @TeacherId";

                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", teacher.TeacherFName);
                cmd.Parameters.AddWithValue("@LastName", teacher.TeacherLName);
                cmd.Parameters.AddWithValue("@EmployeeNumber", teacher.EmployeeNumber);
                cmd.Parameters.AddWithValue("@HireDate", teacher.HireDate);
                cmd.Parameters.AddWithValue("@Salary", teacher.Salary);
                cmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);

                cmd.ExecuteNonQuery();

                TempData["SuccessMessage"] = "Teacher information updated successfully!";
                return RedirectToAction("Edit", new { id = teacher.TeacherId });
            }

            return View(teacher);
        }
    }
}
