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
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string ? Comment { get; set; }
        [Display(Name = "")]
        public ResOrRed ResOrRed { get; set; }

    }
}
