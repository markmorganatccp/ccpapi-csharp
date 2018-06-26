using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCommerceProApiClient.CloudCommercePro.Products;

namespace CloudCommerceProApiClient
{

    /// <summary>
    /// Makes Service Calls to CCP API Products endpoint
    /// </summary>
    public class Products
    {

        /// <summary>
        /// Service Client
        /// </summary>
        public CCPAPIProductsServiceClient ProductsClient { get { return new CCPAPIProductsServiceClient(); } }

    }
}
