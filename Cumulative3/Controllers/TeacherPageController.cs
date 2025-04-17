using Microsoft.AspNetCore.Mvc;
using CumulativeProject.Models;
using MySql.Data.MySqlClient;
using System;

namespace CumulativeProject.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();

        // GET: /TeacherPage/Edit/5
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
                // Teacher not found in the database
                TempData["ErrorMessage"] = "Teacher not found!";
                return RedirectToAction("Index", "Home");  // Redirect to a home
            }

            return View(teacher); // Returns Views/Teacher/Edit.cshtml
        }

        // POST: /TeacherPage/Update
        [HttpPost]
        public IActionResult Update(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                //Check if Teacher's First Name or Last Name is empty.
                if (string.IsNullOrEmpty(teacher.TeacherFName) || string.IsNullOrEmpty(teacher.TeacherLName))
                {
                    ModelState.AddModelError("TeacherFName", "First Name and Last Name are required.");
                    return View("Edit", teacher);
                }

                //Check if Teacher's Hire Date is not in the future.
                if (teacher.HireDate > DateTime.Now)
                {
                    ModelState.AddModelError("HireDate", "Hire Date cannot be in the future.");
                    return View("Edit", teacher);
                }

                //Check if Teacher's Salary is not less than 0.
                if (teacher.Salary < 0)
                {
                    ModelState.AddModelError("Salary", "Salary must be greater than or equal to 0.");
                    return View("Edit", teacher);
                }

                using var conn = _context.AccessDatabase();
                conn.Open();

                //Check if the teacher exists in the database.
                string checkQuery = "SELECT COUNT(*) FROM teachers WHERE teacherid = @TeacherId";
                using var checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);
                var count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    // Teacher not found in the database
                    TempData["ErrorMessage"] = "Teacher not found!";
                    return RedirectToAction("Edit", new { id = teacher.TeacherId });
                }

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

                // Success message
                TempData["SuccessMessage"] = "Teacher information updated successfully!";
                return RedirectToAction("Edit", new { id = teacher.TeacherId });
            }

            return View(teacher);
        }
    }
}
