using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Models;

namespace ACTransit.Training.Web.Business.Training
{
    public class EnrollmentService:TrainingServiceBase<Enrollment>
    {
        public EnrollmentService(string currentUserName) : base(currentUserName) { }

        public Enrollment GetEnrollment(long enrollmentId, params Expression<Func<Enrollment, object>>[] paths)
        {
            return Get(m => m.EnrollmentId == enrollmentId, paths).FirstOrDefault();
        }

        public Course GetCourse(long enrollmentId)
        {
            var enrollment= Get(m => m.EnrollmentId == enrollmentId,null).Include("CourseEnrollment.CourseSchedule.Course").FirstOrDefault();            
            return enrollment==null?null: enrollment.CourseEnrollment.CourseSchedule.Course;
        }

        public IQueryable<EnrollmentList> GetEnrollmentsByCourse(long courseId)
        {
            return UnitOfWork.Get<EnrollmentList>(null).Where(m => m.CourseId == courseId);
        }

        public IQueryable<Enrollment> GetEnrollmentsByCourseEnrollment(long courseEnrollmentId, params Expression<Func<Enrollment, object>>[] paths)
        {
            return Get(m=>m.CourseEnrollmentId==courseEnrollmentId, paths);
        }

        public long Add(Enrollment entity,CourseEnrollment courseEnrollment, List<EnrollmentVehicle> enrollmentVehicles, List<EnrollmentTopic> enrollmentTopics, List<EnrollmentInstructor> enrollmentInstructors)
        {
            long entityId;            
            courseEnrollment = PrepareCourseEnrollment(entity, courseEnrollment);
            entity.CourseEnrollmentId = courseEnrollment.CourseEnrollmentId;
            entity.EnrollmentInstructors.Clear();
            entity.EnrollmentVehicles.Clear();
            entity.EnrollmentTopics.Clear();
            var alreadyExistS =
                UnitOfWork.Get<Enrollment>(null).Count(m => m.CourseEnrollmentId == entity.CourseEnrollmentId && m.SessionDate == entity.SessionDate);
            if (alreadyExistS > 0)
                throw new BusinessException("Attendance for the selected session date already exists!");
            using (var transaction = new TransactionScope())
            {
                entityId= (long)AddInternal(entity);
                foreach (var ev in enrollmentVehicles)
                {                    
                    ev.EnrollmentId = entityId;
                    UnitOfWork.Create(ev);
                }
                foreach (var et in enrollmentTopics)
                {
                    et.EnrollmentId = entityId;
                    UnitOfWork.Create(et);
                }
                foreach (var ei in enrollmentInstructors)
                {
                    ei.EnrollmentId = entityId;
                    UnitOfWork.Create(ei);
                }

                UnitOfWork.SaveChanges();
                transaction.Complete();
            }

            return entityId;
        }

        public long Update(Enrollment entity, List<EnrollmentVehicle> enrollmentVehicles, List<EnrollmentTopic> enrollmentTopics, List<EnrollmentInstructor> enrollmentInstructors, bool isTransportation)
        {
            long entityId;
            
            var courseEnrollment = PrepareCourseEnrollment(entity, null);
            var cs = UnitOfWork.Get<CourseSchedule>(m => m.Course).FirstOrDefault(m => m.CourseScheduleId == courseEnrollment.CourseScheduleId);
            if (cs == null)
                throw new Exception("Course Schedule " +  courseEnrollment.CourseScheduleId + " not found.");
            bool hasWheelTime = cs.Course.HasWheelTime;

            entity.CourseEnrollmentId = courseEnrollment.CourseEnrollmentId;            
            entity.EnrollmentInstructors.Clear();
            entity.EnrollmentVehicles.Clear();
            entity.EnrollmentTopics.Clear();
            
            using (var transaction = new TransactionScope())
            {                
                if (isTransportation)
                    entityId = (long)UpdateInternal(entity,m=>m.LectureTime);
                else
                    entityId = (long)UpdateInternal(entity);
                UnitOfWork.DeleteEnrollmentVehiclesByEnrollmentId(entityId);
                if (hasWheelTime)
                {
                    foreach (var ev in enrollmentVehicles)
                    {
                        ev.EnrollmentId = entityId;
                        UnitOfWork.Create(ev);
                    }                    
                }
                UnitOfWork.DeleteEnrollmentTopicsByEnrollmentId(entityId);
                foreach (var et in enrollmentTopics)
                {
                    et.EnrollmentId = entityId;
                    UnitOfWork.Create(et);
                }
                UnitOfWork.DeleteEnrollmentInstructorsByEnrollmentId(entityId);
                foreach (var ei in enrollmentInstructors)
                {
                    ei.EnrollmentId = entityId;
                    UnitOfWork.Create(ei);
                }

                UnitOfWork.SaveChanges();
                transaction.Complete();
            }

            return entityId;
        }


        public void Save(TransactionEnrollmentsSaveViewModel enrollments)
        {
            if (enrollments == null || enrollments.Count == 0)
                return;            

            var eIds = enrollments.Select(m => m.Enrollment.EnrollmentId);
            var availableEnrollments = Get(m => m.CourseEnrollmentId == enrollments.CourseEnrollmentId, null).Select(m=>m.EnrollmentId).ToList();
            var enrollmentIdsTobeDeleted = availableEnrollments.Where(m => !eIds.Contains(m)).ToList();  //we need to delete the enrollments that have not ben passed to this function.
            var csId = enrollments.CourseScheduleId;
            var ctId=UnitOfWork.Get<CourseSchedule>(m=>m.Course).Where(m => m.CourseScheduleId == csId).Select(m => m.Course.CourseTypeId).FirstOrDefault();
            var isTransportation = ctId == 1;
            var dbEnrollmentVehicles = UnitOfWork.Get<EnrollmentVehicle>(null).Where(m => eIds.Contains(m.EnrollmentId)).Select(m => new {m.EnrollmentVehicleId,m.VehicleGroup, m.VehicleId }).ToList();
            foreach (var e in enrollments)
            {
                if (e.Enrollment.EnrollmentId != 0)
                {
                    foreach (var ev in e.EnrollmentVehicles)
                    {
                        var evId = ev.EnrollmentVehicleId; 
                        var evVehicleId=ev.VehicleId;
                        
                        if (evId != 0)
                        {
                            var dbEnrollmentVehicle = dbEnrollmentVehicles.FirstOrDefault(m => m.EnrollmentVehicleId == evId && m.VehicleId ==evVehicleId);
                            if (dbEnrollmentVehicle != null)
                                ev.VehicleGroup = dbEnrollmentVehicle.VehicleGroup;
                        }
                    }                    
                }
            }

            var courseEnrollment = new CourseEnrollment
            {
                CourseScheduleId = csId,
                Badge = enrollments.Badge,
                NonEmployeeId = enrollments.NonEmployeeId
            };
            using (var transaction = new TransactionScope())
            {
                foreach(var id in enrollmentIdsTobeDeleted) Delete(id);
                foreach (var e in enrollments)
                {
                    if (e.Enrollment.EnrollmentId == 0)
                        Add(e.Enrollment, courseEnrollment, e.EnrollmentVehicles, e.EnrollmentTopics, e.EnrollmentInstructors);
                    else
                        Update(e.Enrollment, e.EnrollmentVehicles, e.EnrollmentTopics, e.EnrollmentInstructors, isTransportation);
                }
                transaction.Complete();
            }
            
        }

        public void DeleteByCourseEnrollment(long courseEnrollmentId)
        {
            var ids = UnitOfWork.Get<Enrollment>(null).Where(m => m.CourseEnrollmentId == courseEnrollmentId).Select(m=>m.EnrollmentId).ToList();
            if (ids.Count > 0)
            {
                using (var transaction = new TransactionScope())
                {
                    foreach(var id in ids)
                        Delete(id);
                    transaction.Complete();
                }                
            }
        }

        public override void Delete<TId>(TId entityId)
        {
            using (var transaction = new TransactionScope())
            {
                UnitOfWork.DeleteEnrollmentVehiclesByEnrollmentId((long)((object)entityId));
                UnitOfWork.DeleteEnrollmentTopicsByEnrollmentId((long)((object)entityId));
                UnitOfWork.DeleteEnrollmentInstructorsByEnrollmentId((long)((object)entityId));
                base.Delete(entityId);   
                transaction.Complete();
            }            
        }

        public CourseEnrollment GetCourseEnrollment(long? courseScheduleId, long? nonEmployeeId, string badge)
        {
            if (courseScheduleId == 0)
                return null;
            List<CourseEnrollment> ceS;
            if (string.IsNullOrWhiteSpace(badge))
                ceS = UnitOfWork.Get<CourseEnrollment>().Where(m => m.CourseScheduleId == courseScheduleId && m.NonEmployeeId == nonEmployeeId).ToList();
            else
                ceS = UnitOfWork.Get<CourseEnrollment>().Where(m => m.CourseScheduleId == courseScheduleId && m.Badge == badge).ToList();
            if (ceS.Any())
                return ceS[0];
            return null;
        }

        private CourseEnrollment PrepareCourseEnrollment(Enrollment entity,CourseEnrollment courseEnrollment)
        {
            if (entity.CourseEnrollmentId == 0 && (courseEnrollment == null || (courseEnrollment.CourseEnrollmentId == 0 && courseEnrollment.CourseScheduleId == 0)))
                throw new Exception("Can't determine the CourseEnrollment.");

            if (entity.CourseEnrollmentId != 0)
                courseEnrollment = UnitOfWork.GetById<CourseEnrollment, long>(entity.CourseEnrollmentId);
            else if (courseEnrollment.CourseEnrollmentId != 0)
                courseEnrollment = UnitOfWork.GetById<CourseEnrollment, long>(courseEnrollment.CourseEnrollmentId);
            else
            {                
                
                var ce = GetCourseEnrollment(courseEnrollment.CourseScheduleId, courseEnrollment.NonEmployeeId, courseEnrollment.Badge);
                if (ce == null)
                    throw new BusinessException(string.Format("No course enrollment found for badge:'{0}' and courseSchedule:'{1}'", courseEnrollment.Badge, courseEnrollment.CourseScheduleId));
                courseEnrollment = ce;

            }
            return courseEnrollment;
        }



        public override void RefreshCache()
        {

        }
    }
}
