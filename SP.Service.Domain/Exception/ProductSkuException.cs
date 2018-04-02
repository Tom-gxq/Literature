using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain
{
    public class ProductSkuException: Exception 
    {
        public ProductSkuException(string ex):base(ex)
        {
            
        }
    }
}
