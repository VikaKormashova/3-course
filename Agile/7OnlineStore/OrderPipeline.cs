using System;

namespace OnlineStoreOrderProcessing
{
    public delegate void OrderStep(OrderContext context);
    
    public class OrderPipeline
    {
        public OrderStep? Pipeline { get; set; }

        public void Run(OrderContext context)
        {
            Pipeline!(context);
        }
    }
}