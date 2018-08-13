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
        //    TestSalesChannelAuth();

        //    GetActiveSalesChannels();

         //   GetProducts();

         //   GetDispatchedOrders();

            BatchStockUpateBySKU();

        }

        /// <summary>
        /// Basic Authentication Test against sales channel service endpoint
        /// </summary>
        /// <remarks>Add your details to the App.Config File</remarks>
        public static void TestSalesChannelAuth()
        {
            string sBrandId = ConfigurationManager.AppSettings.Get("BrandID");
            SalesChannels sc = new SalesChannels();
            CloudCommercePro.SalesChannels.RequestObjectOfString request = new CloudCommercePro.SalesChannels.RequestObjectOfString();
            request.BrandID = int.Parse(sBrandId);
            request.SecurityHash = GetSecurityHash();
            request.Content = sBrandId;

            CloudCommercePro.SalesChannels.ResponseObjectOfBoolean response = sc.SalesChannelClient.TestCredentials(request);
        }


        /// <summary>
        /// Retreive a lis of active sales channels
        /// </summary>
        public static void GetActiveSalesChannels()
        {
            string sBrandId = ConfigurationManager.AppSettings.Get("BrandID");
            SalesChannels sc = new SalesChannels();
            CloudCommercePro.SalesChannels.RequestObjectOfInt32 request = new CloudCommercePro.SalesChannels.RequestObjectOfInt32();
            request.BrandID = int.Parse(sBrandId);
            request.SecurityHash = GetSecurityHash();
            request.Content = request.BrandID;

            CloudCommercePro.SalesChannels.ResponseObjectOfListOfAPISalesChannel channels = sc.SalesChannelClient.getActiveSalesChannels(request);

            if (channels.Content != null && channels.Content.Any())
            {
                foreach (var salesChannel in channels.Content)
                {
                    Console.WriteLine(string.Format("ID: {0} Name : {1} Channel Type: {1} Country : {2}", salesChannel.ID, salesChannel.Name, salesChannel.Type, salesChannel.Country));
                }

                Console.ReadKey();
            }

        }


        /// <summary>
        /// Gets list of products
        /// </summary>
        public static void GetProducts()
        {
            string sBrandId = ConfigurationManager.AppSettings.Get("BrandID");
            Products p = new Products();
            var productRequest = new CloudCommercePro.Products.RequestObjectOfAPIProductGetProductListRequest();
            productRequest.BrandID = int.Parse(sBrandId);
            productRequest.SecurityHash = GetSecurityHash();

            // begin at the start of the list of available products
            // get the first 10 products in the list
            productRequest.Content = new CloudCommercePro.Products.APIProductGetProductListRequest { Start = 0, End = 10, SalesChannelID = 0 };

            var products = p.ProductsClient.getProducts(productRequest);

            if (products.Content.products != null && products.Content.products.Any())
            {
                foreach (var product in products.Content.products)
                {
                    Console.WriteLine(string.Format("ID {0} ManufacturerSKU {1} Name {2}", product.ID, product.ManufacturerSKU, product.Name));
                }

                Console.ReadKey();

            }

        }

        public static void BatchStockUpateBySKU()
        {
            string sBrandId = ConfigurationManager.AppSettings.Get("BrandID");
            int brandId = int.Parse(sBrandId);
            int stockQuantity = 100;

            var productsClient = new Products().ProductsClient;


            var productRequest = new CloudCommercePro.Products.RequestObjectOfAPIProductGetProductListRequest();
            productRequest.BrandID = int.Parse(sBrandId);
            productRequest.SecurityHash = GetSecurityHash();

            productRequest.Content = new CloudCommercePro.Products.APIProductGetProductListRequest { Start = 0, End = 5, SalesChannelID = 0 };

            var products = productsClient.getProducts(productRequest);

            List<CloudCommercePro.Products.APIUpdateStockRequest> stockUpdates = new List<CloudCommercePro.Products.APIUpdateStockRequest>();

            if (products.Content.products != null && products.Content.products.Any())
            {
                foreach (var product in products.Content.products)
                {
                    stockUpdates.Add(new CloudCommercePro.Products.APIUpdateStockRequest
                    {
                        SKU = product.ManufacturerSKU,
                        StockQuantity = stockQuantity,
                        UpdateRealStock = true
                    });
                }
            }


            var res = productsClient.BatchStockUpdates(new CloudCommercePro.Products.RequestObjectOfAPIBulkUpdateStockRequest
            {
                BrandID = brandId,
                SecurityHash = GetSecurityHash(),
                Content = new CloudCommercePro.Products.APIBulkUpdateStockRequest
                {
                    StockUpdates = stockUpdates.ToArray()
                }
            });

            Console.ReadKey();

        }

        public static void GetDispatchedOrders()
        {
            string sBrandId = ConfigurationManager.AppSettings.Get("BrandID");
            int brandId = int.Parse(sBrandId);
            int salesChannelId = 5879;

            var ordersClient = new Orders().OrdersClient;
            var dispatchedOrders = ordersClient.GetDispatchedOrders(new CloudCommercePro.Orders.RequestObjectOfApiOrderQuery
            {
                BrandID = brandId,
                SecurityHash = GetSecurityHash(),
                Content = new CloudCommercePro.Orders.ApiOrderQuery
                {
                    BrandID = brandId,
                    CustomerOrderStatusTypes = null,
                    SalesChannelID = salesChannelId,
                    StartDate = DateTime.Now.AddDays(-3),
                    EndDate = DateTime.Now.AddDays(1),
                    Skip = 0,
                    Take = 250
                }

            });

            if (dispatchedOrders.Content.orders != null && dispatchedOrders.Content.orders.Any())
            {
                foreach (var orderDetail in dispatchedOrders.Content.orders)
                {
                    Console.WriteLine(string.Format("ID {0} CustomerID {1} Customer Trading Name {2} Sales Channel ID {3} Name {4} ", orderDetail.order.ID, orderDetail.customer.ID, orderDetail.customer.TradingName, orderDetail.order.SalesChannelID, orderDetail.order.SalesChannel.Name));
                }

                Console.ReadKey();
            }

        }




        public static string GetSecurityHash()
        {
            string sRSAModulus = ConfigurationManager.AppSettings.Get("PublicKeyModulus");
            string sRSAExponent = ConfigurationManager.AppSettings.Get("PublicKeyExponent");
            string password = ConfigurationManager.AppSettings.Get("Password");
            string publicKey = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>", sRSAModulus, sRSAExponent);

            string hash = ApiSecurityStatic.Encrypt(password, publicKey);

            return hash;
        }

    }
}
