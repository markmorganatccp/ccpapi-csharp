using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCommerceProApiClient.CloudCommercePro.ProductRanges;

namespace CloudCommerceProApiClient
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductRanges
    {
        /// <summary>
        /// 
        /// </summary>
        public CCPProductRangesClient ProductRangesClient { get { return new CCPProductRangesClient(); } }

    }

}
