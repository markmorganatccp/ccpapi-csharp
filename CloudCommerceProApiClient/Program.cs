using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

/// <summary>
/// Sample WCF Client for Cloud Commerce Pro Api
/// </summary>
namespace CloudCommerceProApiClient
{
    class Program
    {
        
        static void Main(string[] args)
        {
            TestSalesChannelAuth();

            GetActiveSalesChannels();

            GetProducts();


        }
        
        /// <summary>
        /// Basic Authentication Test against sales channel service endpoint
        /// </summary>
        /// <remarks>Add your details to the App.Config File</remarks>
        public static void TestSalesChannelAuth()
        {
            string sBrandId = ConfigurationManager.AppSettings.Get("BrandID");
            string sRSAModulus = ConfigurationManager.AppSettings.Get("PublicKeyModulus");
            string sRSAExponent = ConfigurationManager.AppSettings.Get("PublicKeyExponent");
            string password = ConfigurationManager.AppSettings.Get("Password");
            string publicKey = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>", sRSAModulus, sRSAExponent);

            string hash = ApiSecurityStatic.Encrypt(password, publicKey);

            SalesChannels sc = new SalesChannels();
            CloudCommercePro.SalesChannels.RequestObjectOfString request = new CloudCommercePro.SalesChannels.RequestObjectOfString();
            request.BrandID = int.Parse(sBrandId);
            request.SecurityHash = hash;
            request.Content = sBrandId;

            CloudCommercePro.SalesChannels.ResponseObjectOfBoolean response = sc.SalesChannelClient.TestCredentials(request);
                  
        }


        /// <summary>
        /// Retreive a lis of active sales channels
        /// </summary>
        public static void GetActiveSalesChannels()
        {
            string sBrandId = ConfigurationManager.AppSettings.Get("BrandID");
            string sRSAModulus = ConfigurationManager.AppSettings.Get("PublicKeyModulus");
            string sRSAExponent = ConfigurationManager.AppSettings.Get("PublicKeyExponent");
            string password = ConfigurationManager.AppSettings.Get("Password");
            string publicKey = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>", sRSAModulus, sRSAExponent);

            string hash = ApiSecurityStatic.Encrypt(password, publicKey);

            SalesChannels sc = new SalesChannels();
            CloudCommercePro.SalesChannels.RequestObjectOfInt32 request = new CloudCommercePro.SalesChannels.RequestObjectOfInt32();
            request.BrandID = int.Parse(sBrandId);
            request.SecurityHash = hash;
            request.Content = request.BrandID;

            CloudCommercePro.SalesChannels.ResponseObjectOfListOfAPISalesChannel channels = sc.SalesChannelClient.getActiveSalesChannels(request);
            
        }


        /// <summary>
        /// Gets list of products
        /// </summary>
        public static void GetProducts()
        {

            string sBrandId = ConfigurationManager.AppSettings.Get("BrandID");
            string sRSAModulus = ConfigurationManager.AppSettings.Get("PublicKeyModulus");
            string sRSAExponent = ConfigurationManager.AppSettings.Get("PublicKeyExponent");
            string password = ConfigurationManager.AppSettings.Get("Password");
            string publicKey = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>", sRSAModulus, sRSAExponent);

            string hash = ApiSecurityStatic.Encrypt(password, publicKey);
            
            Products p = new Products();
            var productRequest = new CloudCommercePro.Products.RequestObjectOfAPIProductGetProductListRequest();
            productRequest.BrandID = int.Parse(sBrandId);
            productRequest.SecurityHash = hash;

            // begin at the start of the list of available products
            // get the first 10 products in the list
            productRequest.Content = new CloudCommercePro.Products.APIProductGetProductListRequest { Start = 0, End = 10, SalesChannelID = 0 }; // Sales Channel parameter is redundant                

            var products = p.ProductsClient.getProducts(productRequest);

        }


    }
}
