namespace DataAccess;
// using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class Client
{
    private string _phoneNumber;
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
    // public List<Deposit>? Deposits { get; set; }

    [PhoneNumber(7)]
    [Required(ErrorMessage = "Phone number is required")]
    public required string PhoneNumber
    {
        get => _phoneNumber;
        set => _phoneNumber = InternationalFormat(value);
    }

    public Client()
    {
        _createdAt = DateTime.UtcNow.Date;
    }
    private static string InternationalFormat(string phoneNumber)
    {
        if (!phoneNumber.StartsWith("+"))
        {
            return $"+{phoneNumber}";
        }
        return phoneNumber;
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

        if (!phoneNumber.StartsWith("+"))
        {
            return new ValidationResult("Phone number must start with '+'.");
        }

        // Remove the "+" and count the digits
        var digitsOnly = Regex.Replace(phoneNumber, @"\D", "");
        if (digitsOnly.Length != _requiredLength)
        {
            return new ValidationResult($"Phone number must contain exactly {_requiredLength} digits.");
        }

        return ValidationResult.Success;
    }
}