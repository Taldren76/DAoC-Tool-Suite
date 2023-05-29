using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAoCToolSuite.CharacterTool.Items.Metadata
{
    public class Categories
    {
        public List<Category> categories { get; private set; } = new List<Category>();
        private void AddCategory(int _id, string _category)
        {
            categories.Add(new Category() { id = _id, category = _category });
        }
        public Categories()
        {
            AddCategory(1, "Weapon");
            AddCategory(2, "Armor");
            AddCategory(3, "Shield");
            AddCategory(5, "Instrucment");
            AddCategory(6, "Mount");
            AddCategory(7, "Consumable");
            AddCategory(8, "Other");
        }
    }

    public class Category
    {
        public int id { get; set; } = -1;
        public string? category { get; set; }
    }
}
