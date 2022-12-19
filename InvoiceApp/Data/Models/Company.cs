using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Data.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Input the name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Input the name")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var company = obj as Company;

            if (company is null)
                return false;

            return Id == company.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
