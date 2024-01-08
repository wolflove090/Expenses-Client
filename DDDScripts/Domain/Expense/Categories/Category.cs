using System;

namespace ExpenseDomain
{
    public class Category : IEquatable<Category>
    {
        readonly string name;
        readonly string iconName;

        public Category(string name, string iconName)
        {
            this.name = name;
            this.iconName = iconName;
        }

        public void Notify(ICategoryNotification note)
        {
            note.Name(this.name);
            note.IconName(this.iconName);
        }

        public bool Equals(Category other)
        {
            return this.name == other.name;
        }
    }
}
