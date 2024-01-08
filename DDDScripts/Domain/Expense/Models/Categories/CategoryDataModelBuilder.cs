namespace Expense.Models.Categories
{
    public class CategoryDataModelBuilder : ICategoryNotification
    {
        string name;
        string iconName;

        public void Name(string name)
        {
            this.name = name;
        }

        public void IconName(string iconName)
        {
            this.iconName = iconName;
        }

        public CategoryDataModel Build()
        {
            return new CategoryDataModel()
            {
                name = this.name,
                iconName = this.iconName,
            };
        }
    }
}
