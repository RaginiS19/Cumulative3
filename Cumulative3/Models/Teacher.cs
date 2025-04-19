namespace CumulativeProject.Models
{
    /// <summary>
    /// Represents a teacher in the school system.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Gets or sets the unique ID of the teacher.
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Gets or sets the teacher's first name.
        /// </summary>
        public string? TeacherFName { get; set; }

        /// <summary>
        /// Gets or sets the teacher's last name.
        /// </summary>
        public string? TeacherLName { get; set; }

        /// <summary>
        /// Gets or sets the teacher's employee number.
        /// </summary>
        public string? EmployeeNumber { get; set; }

        /// <summary>
        /// Gets or sets the date the teacher was hired.
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// Gets or sets the teacher's salary.
        /// </summary>
        public decimal? Salary { get; set; }
    }
}
