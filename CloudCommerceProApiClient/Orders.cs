using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCommerceProApiClient.CloudCommercePro.Orders;

namespace CloudCommerceProApiClient
{
    /// <summary>
    /// Makes Service Calls to CCP API Orders endpoint
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// Orders Client
        /// </summary>
        public CCPApiOrderServiceClient OrdersClient { get { return new CCPApiOrderServiceClient(); } }



    }

}
