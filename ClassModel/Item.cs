namespace ClassModel
{
    public class Item
    {        
        public int Item_Id { get; set; }

        public string Title { get; set; }

        public double Cost { get; set; }

       public int Amount { get; set; }

        public Item(int id, string title, double cost, int amount)
                : this(title, cost, amount)
        {
            Item_Id = id;
        }

        public Item(string title, double cost, int amount)
        {
            Title = title;
            Cost = cost;
            Amount = amount;
        }

        public Item(string title)
        {
            Title = title;
        }

        public Item()
        {
        }
    }
}
   