using BlazorDynamics.Licensing.Contracts;
using BlazorDynamics.Licensing.Core.Models;
using System.Text;
using System.Xml.Linq;

namespace BlazorDynamics.Licensing.Implementations;

public class SignedLicenseManager : ISignedLicenseManager
{
    public string CreateEncodedLicense(LicenseData licenseData, string signature)
    {
        var xmlLicense = ConvertJsonToXml(licenseData, signature);
        var xmlString = xmlLicense.ToString();
        var xmlBytes = Encoding.UTF8.GetBytes(xmlString);
        return Convert.ToBase64String(xmlBytes);
    }

    public License GetLicense(string encodedLicense)
    {
        var bytes = Convert.FromBase64String(encodedLicense);
        var xmlContent = Encoding.UTF8.GetString(bytes);
        var xmlDoc = XDocument.Parse(xmlContent);

        License license = new License
        {
            Data = new LicenseData
            {
                LicensedTo = (string)xmlDoc.Root.Element("Data").Element("LicensedTo"),
                EmailTo = (string)xmlDoc.Root.Element("Data").Element("EmailTo"),
                LicenseType = (string)xmlDoc.Root.Element("Data").Element("LicenseType"),
                LicenseNote = (string)xmlDoc.Root.Element("Data").Element("LicenseNote"),
                OrderId = (string)xmlDoc.Root.Element("Data").Element("OrderId"),
                UserId = (string)xmlDoc.Root.Element("Data").Element("UserId"),
                Products = xmlDoc.Root.Element("Data").Element("Products").Elements("Product").Select(e => e.Value).ToList(),
                SerialNumber = (string)xmlDoc.Root.Element("Data").Element("SerialNumber"),
                SubscriptionExpiry = (string)xmlDoc.Root.Element("Data").Element("SubscriptionExpiry"),
                LicenseVersion = (string)xmlDoc.Root.Element("Data").Element("LicenseVersion"),
                LicenseInstruction = (string)xmlDoc.Root.Element("Data").Element("LicenseInstruction")
            },
            Signature = (string)xmlDoc.Root.Element("Signature")
        };

        return license;

    }

    private XElement ConvertJsonToXml(LicenseData licenseData, string signature)
    {
        XElement xml = new XElement("License",
            new XElement("Data",
                new XElement("LicensedTo", licenseData.LicensedTo),
                new XElement("EmailTo", licenseData.EmailTo),
                new XElement("LicenseType", licenseData.LicenseType),
                new XElement("LicenseNote", licenseData.LicenseNote),
                new XElement("OrderId", licenseData.OrderId),
                new XElement("UserId", licenseData.UserId),
                new XElement("Products", licenseData.Products.Select(p => new XElement("Product", p))),
                new XElement("SerialNumber", licenseData.SerialNumber),
                new XElement("SubscriptionExpiry", licenseData.SubscriptionExpiry),
                new XElement("LicenseVersion", licenseData.LicenseVersion),
                new XElement("LicenseInstruction", licenseData.LicenseInstruction)
            ),
            new XElement("Signature", signature)
        );
        return xml;
    }
}