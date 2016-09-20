# The AC Transit Training and Education Department (TED) application

This application supports the District's training operations for transportation and maintenance employees, 
primarily in the positions of Bus Operators and Heavy Duty Coach Mechanics (Apprentice and Journey), although the system supports new courses and apprenticeship programs.  This repository contains a complete application and supporting databases required to run.

Features include:

 * Course management (schedules, enrollments, class size, attendance, grading, topics).

 * Course instructor management, both employee (Active Directory) and non-employee.

 * Apprenticeship management (work order import, daily review, weekly rating system, supervisor signoff, worksheet printout).

 * Feature access control uses AD groups, NT usernames and/or job titles.  

 * log4net and email logging for errors and website tracing.

 * Uses ASP.NET MVC 5.2, Entity Framework 6.1.3, Bootstrap 3.3, jQuery/jQuery UI and Knockout.


## Requirements: 

 * Visual Studio 2015+

 * Windows 7+ or Server 2008 R2+ 
 
 * IIS 7.5+

 * SQL Server 2014+ Express 
 
 * [PowerShell 5.0](https://www.microsoft.com/en-us/download/details.aspx?id=50395)


## Installation

 * Open the project using Microsoft Visual Studio 2015.

 * Make sure the ACTransit.Training.Web project is your startup project.

 * Build and run solution.

By default, the application will run in LocalDB mode -- for demo purposes, this is sufficient.  For production, we suggest at least SQL Server 2014 Express, attaching the databases to your SQL Server instance, found within the [Database](https://github.com/actransitorg/Training/PublishScripts/Database) folder.

Since the databases reference each other, we included a custom SQLExecute tool, which rewrites database references for your particular environment.  When you compile, [PublishScripts/publish.bat](https://github.com/actransitorg/Training/PublishScripts/publish.bat) is executed and creates customized databases under the App_Data directory for the web application (see [PublishScripts](https://github.com/actransitorg/Training/PublishScripts) for more details).


## Reporting

The included Training reports require at least SQL Server Reporting Services (SSRS) 2008, located within the [SSRS](https://github.com/actransitorg/Training/SSRS) folder. These include:


### Courses and Classes

 * Attendance List

 * Course Roster

 * Training by Coach

 * Training by Day

 * Training by Route

 * Training by Topic


### Apprenticeship

 * Work History

 * Work Summary

 * Work Order Tasks

 * Work Completed (Daily Work Completed, Weekly Work Completed)


## Contact Us

We welcome and encourage your feedback regarding features, found issues, pull requests or any new business processes you have developed.
If you want to reach out to our team, please open an issue at https://github.com/actransitorg/ACTransit.Training/issues or visit http://actransit.org/open-source. 


## License

Code released under the [MIT](https://github.com/actransitorg/Training/LICENSE.md) license.  Docs released under [Creative Commons](https://github.com/actransitorg/Training/docs/LICENSE_CC.md).