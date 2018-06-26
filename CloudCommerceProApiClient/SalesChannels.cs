using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCommerceProApiClient.CloudCommercePro.SalesChannels;


namespace CloudCommerceProApiClient
{
    /// <summary>
    /// Makes Service Calls to CCP API SalesChannels endpoint
    /// </summary>
    public class SalesChannels
    {
        /// <summary>
        /// Service Client
        /// </summary>
        public CCPApiSalesChannelServiceClient SalesChannelClient { get { return new CCPApiSalesChannelServiceClient(); }  }

        
    }
}
