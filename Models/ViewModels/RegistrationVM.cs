using EntityLayer.Entities;

namespace DanismaSira.Models.ViewModels
{
    public class RegistrationVM
    {
        
        public int DepartmentId { get; set; }

        public Department Department { get; set; }  

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public User? User { get; set; }      

        public IEnumerable<Department>? Departments { get; set; }

        //public RegistrationStatus IsDone { get; set; } = RegistrationStatus.NotStarted;

        //public enum RegistrationStatus
        //{
        //    NotStarted = 0,
        //    InProgress = 1,
        //    Completed = 2
        //}

    }
}


