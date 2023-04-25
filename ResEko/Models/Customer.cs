using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace ResEko.Models
{
    public enum ResOrRed
    {
        Restaurang, 
        Redovisning
    }
    public class Customer
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Var god ange ett telefonnummer")]
        [Phone(ErrorMessage = "Var god ange ett giltigt telefonnummer")]
        public string Phonenumber { get; set; }
        public string ? Comment { get; set; }
        public ResOrRed ResOrRed { get; set; }

    }
}
