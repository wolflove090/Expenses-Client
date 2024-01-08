namespace ExpenseDomain
{
    public class CategoryFactory
    {
        public static Category CreateCategory(string name, string iconName)
        {
            return new Category(name, iconName);
        }

        public static Category CreateCategory(string name)
        {
            return new Category(name, "");
        }
    }
}

