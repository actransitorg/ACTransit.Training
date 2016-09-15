using System;

namespace ACTransit.Training.Web.Domain.Models
{
    public class EmployeeTrainee : IEquatable<EmployeeTrainee>
    {
        public string LastName;
        public string FirstName;
        public string Department;
        public string BusinessPhone;
        public string CellPhone;
        public string Badge;
        public string Name;
        public string LoginId;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EmployeeTrainee) obj);
        }

        public bool Equals(EmployeeTrainee other)
        {
            return string.Equals(Badge, other.Badge);

        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Badge != null ? Badge.GetHashCode() : 0);           
                return hashCode;
            }
        }

    }
}
