using System.ComponentModel.DataAnnotations;

namespace DanismaSira.Models.ViewModels
{
    public class UserVM
    {
        
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int TcOrItuNumber { get; set; }

        public bool IsItuMember { get; set; }
    }
}
