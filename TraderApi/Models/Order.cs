﻿namespace TraderApi.Models
{
    public class Order
    {
        public string AgentName { get; set; }
        public string item { get; set; }
        public int BagQuantity { get; set; }
        public decimal Rate { get; set; }
        public string UsedBy { get; set; }

    }
}
