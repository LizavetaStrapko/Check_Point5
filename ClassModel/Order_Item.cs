namespace ClassModel
{
    public class Order_Item
    {
        public Order Order { get; set; }

        public Item Item { get; set; }

        public int Item_amount { get; set; }

        public double Total_cost { get; set; }

        public Order_Item(int item_amount, double total_cost)
        {
            Item_amount = item_amount;
            Total_cost = total_cost;
        }

        public Order_Item()
        {
        }
    }
}
