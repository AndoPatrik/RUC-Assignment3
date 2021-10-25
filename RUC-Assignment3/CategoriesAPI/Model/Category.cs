namespace CategoriesAPI
{
    public class Category
    {
        public Category(int cid, string name)
        {
            Cid = cid;
            Name = name;
        }

        public Category(){}

        public int Cid { get; set; }
        public string Name { get; set; }
    }
}
