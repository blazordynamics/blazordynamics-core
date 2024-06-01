namespace BlazorDynamics.Licensing.Models;

public class LicenseValidationResult
{
    public bool IsValid { get; }
    public string Message { get; } = default!;

    public LicenseValidationResult(bool isValid, string message = default)
    {
        IsValid = isValid;
        Message = message;
    }
}
