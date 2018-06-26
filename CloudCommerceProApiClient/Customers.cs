using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCommerceProApiClient.CloudCommercePro.Customers;

namespace CloudCommerceProApiClient
{
    /// <summary>
    /// 
    /// </summary>
  public  class Customers
    {
        /// <summary>
        /// 
        /// </summary>
        public CCPApiCustomerServiceClient CustomersClient { get { return new CCPApiCustomerServiceClient(); } }


    }
}
