using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class Client
    {
        private DateTime _createdAt;

        public Guid Id { get; set; }
        public DateTime CreatedAt
        {
            get => _createdAt;
            private set => _createdAt = value;
        }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        [PhoneNumber(7)]
        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(12, MinimumLength =9)]
        public required string PhoneNumber { get; set; }

        public Client()
        {
            _createdAt = DateTime.UtcNow.Date;
        }


    }


    public class Deposit
    {
        public Guid Id { get; set; }
        public required string Type { get; set; }
        public decimal Balance { get; set; }
        public ushort Percentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public required Client Owner { get; set; }
    }



    public class PhoneNumberAttribute : ValidationAttribute
    {
        private readonly int _requiredLength;

        public PhoneNumberAttribute(int requiredLength)
        {
            _requiredLength = requiredLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new ValidationResult("Phone number is required.");
            }

            return ValidationResult.Success;
        }
    }
}