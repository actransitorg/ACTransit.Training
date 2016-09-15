Use MaintenanceDW
Go
DELETE FROM [dbo].[OperatingStatistic] where cast(statdate as date)<'2015-01-01'
Delete from LaborCosting where BadgeNum not in (Select '0000'+ Badge from Training.Apprentice.Participant)

Use Training
Go
DELETE FROM CourseEnrollment Where CourseScheduleId not in (1,2)
DELETE FROM CourseScheduleInstructor Where CourseScheduleId not in (1,2)
DELETE FROM CourseSchedule Where CourseScheduleId not in (1,2)
DELETE FROM  EnrollmentInstructor where EnrollmentId in (select EnrollmentId from Enrollment where CourseEnrollmentId not in (select CourseEnrollmentId from CourseEnrollment))
DELETE FROM  EnrollmentVehicleRoute where EnrollmentVehicleId in (select EnrollmentVehicleId from EnrollmentVehicle where EnrollmentId in (select EnrollmentId from Enrollment where CourseEnrollmentId not in (select CourseEnrollmentId from CourseEnrollment)))
DELETE FROM  EnrollmentVehicle where EnrollmentId in (select EnrollmentId from Enrollment where CourseEnrollmentId not in (select CourseEnrollmentId from CourseEnrollment))
DELETE FROM  EnrollmentTopic where EnrollmentId in (select EnrollmentId from Enrollment where CourseEnrollmentId not in (select CourseEnrollmentId from CourseEnrollment))
DELETE FROM Enrollment where CourseEnrollmentId not in (select CourseEnrollmentId from CourseEnrollment)
DELETE FROM Instructor where InstructorId not in (select InstructorId from CourseScheduleInstructor) and InstructorId not in (select InstructorId from EnrollmentInstructor)


Update Apprentice.RatingCategory SET AddUserId='System'
Update Apprentice.RatingCategory SET UpdUserId='System'
Update Apprentice.RatingArea SET AddUserId='System'
Update Apprentice.RatingArea SET UpdUserId='System'
Update Apprentice.RatingItem SET AddUserId='System'
Update Apprentice.RatingItem SET UpdUserId='System'
Update dbo.TopicType SET AddUserId='System'
Update dbo.TopicType SET UpdUserId='System'
Update Apprentice.Program SET AddUserId='System'
Update Apprentice.Program SET UpdUserId='System'
Update Apprentice.ParticipantStatus SET AddUserId='System'
Update Apprentice.ParticipantStatus SET UpdUserId='System'
Update dbo.NonEmployee SET AddUserId='System'
Update dbo.NonEmployee SET UpdUserId='System'
Update dbo.Grade SET AddUserId='System'
Update dbo.Grade SET UpdUserId='System'
Update Apprentice.DailyPerformance SET AddUserId='System'
Update Apprentice.DailyPerformance SET UpdUserId='System'
Update dbo.CourseType SET AddUserId='System'
Update dbo.CourseType SET UpdUserId='System'
Update dbo.Course SET AddUserId='System'
Update dbo.Course SET UpdUserId='System'
Update dbo.Instructor SET AddUserId='System'
Update dbo.Instructor SET UpdUserId='System'
Update dbo.Topic SET AddUserId='System'
Update dbo.Topic SET UpdUserId='System'
Update Apprentice.ProgramLevelGroup SET AddUserId='System'
Update Apprentice.ProgramLevelGroup SET UpdUserId='System'
Update Apprentice.WorkCategory SET AddUserId='System'
Update Apprentice.WorkCategory SET UpdUserId='System'
Update Apprentice.DailyPerformanceProgramLevelGroup SET AddUserId='System'
Update Apprentice.DailyPerformanceProgramLevelGroup SET UpdUserId='System'
Update Apprentice.ProgramLevel SET AddUserId='System'
Update Apprentice.ProgramLevel SET UpdUserId='System'
Update Apprentice.RatingCell SET AddUserId='System'
Update Apprentice.RatingCell SET UpdUserId='System'
Update dbo.CourseTopic SET AddUserId='System'
Update dbo.CourseTopic SET UpdUserId='System'
Update dbo.CourseSchedule SET AddUserId='System'
Update dbo.CourseSchedule SET UpdUserId='System'
Update dbo.CourseEnrollment SET AddUserId='System'
Update dbo.CourseEnrollment SET UpdUserId='System'
Update Apprentice.Participant SET AddUserId='System'
Update Apprentice.Participant SET UpdUserId='System'
Update Apprentice.RatingCellScore SET AddUserId='System'
Update Apprentice.RatingCellScore SET UpdUserId='System'
Update Apprentice.ParticipantWork SET AddUserId='System'
Update Apprentice.ParticipantWork SET UpdUserId='System'
Update Apprentice.ParticipantProgramLevelGroup SET AddUserId='System'
Update Apprentice.ParticipantProgramLevelGroup SET UpdUserId='System'
Update Apprentice.ParticipantWorkSeed SET AddUserId='System'
Update Apprentice.ParticipantWorkSeed SET UpdUserId='System'
Update dbo.Enrollment SET AddUserId='System'
Update dbo.Enrollment SET UpdUserId='System'
Update dbo.EnrollmentInstructor SET AddUserId='System'
Update dbo.EnrollmentInstructor SET UpdUserId='System'
Update dbo.EnrollmentVehicle SET AddUserId='System'
Update dbo.EnrollmentVehicle SET UpdUserId='System'
Update dbo.EnrollmentTopic SET AddUserId='System'
Update dbo.EnrollmentTopic SET UpdUserId='System'
Update Apprentice.Progress SET AddUserId='System'
Update Apprentice.Progress SET UpdUserId='System'
Update Apprentice.ProgressRatingCellScore SET AddUserId='System'
Update Apprentice.ProgressRatingCellScore SET UpdUserId='System'
Update Apprentice.ProgressDay SET AddUserId='System'
Update Apprentice.ProgressDay SET UpdUserId='System'
Update dbo.EnrollmentVehicleRoute SET AddUserId='System'
Update dbo.EnrollmentVehicleRoute SET UpdUserId='System'


USE EmployeeDW
Go
DELETE from EmployeesLocation where badge not in (select badge from Training.Apprentice.Participant) AND badge not in (select badge from Training.dbo.CourseEnrollment)
DELETE FROM Employees where badge not in (select badge from Training.Apprentice.Participant) AND badge not in (select badge from Training.dbo.CourseEnrollment)

update Employees set address01='1600 Franklin Street', address02=NULL, BirthDate='1980-01-01', city='Oakland', zip = '94601', 
				PreferredPhone=' 5108914706', EmailAddress=NULL, EthnicGroup = NULL, Marital = 1, VacHoursEntitlement= 100, DriverLicenseNumber = 'C1111111',
				DriverLicenseExpirationDate='2025-01-01', WorkPhone = NULL, CellPhone = NULL

Go
