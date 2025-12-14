using System;

namespace OnlineStoreOrderProcessing
{
    public class OrderContext
    {
        public bool HasStock { get; set; }
        public int DeliveryCost { get; set; }
        public bool IsConfirmed { get; set; }
    }
}