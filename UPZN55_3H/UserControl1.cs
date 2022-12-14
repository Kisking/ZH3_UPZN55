using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPZN55_3H
{

    public partial class UserControl1 : UserControl
    {
        Models.SeCocktailsContext context = new Models.SeCocktailsContext();
        public UserControl1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var Név = from x in context.Cocktails
                      where x.Name.StartsWith(textBox1.Text)
                      select x;

            listBox1.DataSource = Név.ToList();
            listBox1.DisplayMember = "Name";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var i = ((Models.Cocktail)listBox1.SelectedItem).CocktailSk;
            var er = from s in context.Recipes
                     where s.CocktailFk == i
                     select new
                     {
                         Ár = s.MaterialFkNavigation.Price,
                         Típus = s.MaterialFkNavigation.TypeFkNavigation.Name,
                         Alapanyag = s.MaterialFkNavigation.Name,
                         Mennyiség = s.Quantity,
                         Mértékegység = s.MaterialFkNavigation.UnitFkNavigation.Name,


                     };
            dataGridView1.DataSource = er.ToList();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Now;
        }
    }
}
