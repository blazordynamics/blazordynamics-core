using System.Globalization;

namespace BlazorDynamics.Licensing.Core.Models;

public class LicenseData
{

    public string LicenseNote { get; set; }

    public List<string> Products { get; set; }

    public string SerialNumber { get; set; }

    public string SubscriptionExpiry { get; set; }

    public string LicenseVersion { get; set; }

    public string LicenseInstruction { get; set; }

    public string OrderId { get; set; }

    public string UserId { get; set; }

    public string LicensedTo { get; set; }

    public string EmailTo { get; set; }

    public string LicenseType { get; set; }

    public bool IsValid()
    {
        var currentDate = DateTime.UtcNow.Date;
        var expiryDate = GetExpiryDate();

        if (!expiryDate.HasValue)
        {
            // Handle the case where SubscriptionExpiry is missing or in an incorrect format
            // Depending on your needs, you might want to return false or throw an exception.
            return false;
        }

        return currentDate <= expiryDate.Value;
    }

    private DateTime? GetExpiryDate()
    {
        if (DateTime.TryParseExact(SubscriptionExpiry, "yyyyMMdd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate))
        {
            return expiryDate;
        }
        return null;
    }
}
