# Cumulative3

Cumulative3 is an ASP.NET Core MVC application designed to demonstrate the use of MVC architecture, Tag Helpers, and database interactions through MySQL. This project includes features like teacher information management and error handling, along with customizable views using Razor syntax.

## Table of Contents
- [Project Description](#project-description)
- [Installation Instructions](#installation-instructions)
- [Usage](#usage)
- [Folder Structure](#folder-structure)
- [Technology Stack](#technology-stack)
- [Contributing](#contributing)
- [License](#license)

## Project Description

Cumulative3 is a web application that provides a simple interface to manage teacher information. Users can edit teacher details such as first name, last name, employee number, hire date, and salary. The application uses ASP.NET Core MVC for the backend logic and Razor views for frontend rendering.

### Key Features:
- **Teacher Management**: Ability to view, edit, and update teacher details.
- **Error Handling**: Displays an error page with details for troubleshooting.
- **Responsive Design**: Built with Bootstrap for a responsive and clean interface.

## Installation Instructions

To run the project locally, follow these steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/Cumulative3.git

2. Navigate into the project directory:
    cd Cumulative3

3. Restore the required NuGet packages:
   dotnet restore


4. Configure the MySQL database connection in appsettings.json:
   {
  "ConnectionStrings": {
    "DefaultConnection": "server=yourserver;database=yourdatabase;user=youruser;password=yourpassword;"
  }
}

5. Run the project:
   dotnet run

6. Open the browser and go to http://localhost:5000 to view the application.

   
Usage

Views:
Home Page: Displays the welcome message and links to other parts of the site.
Privacy Page: Contains the privacy policy of the application.
Error Page: Displays error details with the option to enable Development mode for debugging.

Error Handling:
The application handles errors by displaying an error page with the request ID for troubleshooting purposes. In Development mode, more detailed error information is available to aid in debugging.

Folder Structure

Cumulative3
│
├── Controllers
│   ├── HomeController.cs
│   └── TeacherPageController.cs
│
├── Models
│   ├── ErrorViewModel.cs
│   └── Teacher.cs
│
├── Views
│   ├── Home
│   │   ├── Index.cshtml
│   │   └── Privacy.cshtml
│   ├── Shared
│   │   └── _Layout.cshtml
│   └── Error.cshtml
│
├── wwwroot
│   ├── css
│   │   └── site.css
│   ├── lib
│   │   ├── bootstrap
│   │   └── jquery
│   └── js
│       └── site.js
└── appsettings.json


Technology Stack

ASP.NET Core MVC: Framework for building the web application.
MySQL: Database management system for storing teacher information.
Bootstrap: Frontend framework for responsive design.
jQuery: JavaScript library for DOM manipulation.









