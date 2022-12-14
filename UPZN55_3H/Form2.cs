using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPZN55_3H
{
    public partial class Form2 : Form
    {
        Models.SeCocktailsContext context = new Models.SeCocktailsContext();

        public Form2()
        {
            InitializeComponent();
        }

        private void textBoxCoctail_TextChanged(object sender, EventArgs e)
        {
            var Név = from x in context.Cocktails
                      where x.Name.StartsWith(textBoxCoctail.Text)
                      select x;

            listCoctail.DataSource = Név.ToList();
            listCoctail.DisplayMember = "Name";
        }

        private void textBoxMaterial_TextChanged(object sender, EventArgs e)
        {
            var Material = from x in context.Materials
                           where x.Name.StartsWith(textBoxMaterial.Text)
                           select x;

            listMaterial.DataSource = Material.ToList();
            listMaterial.DisplayMember = "Name";
        }

        private void listCoctail_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recept();
        }

        private void Recept()
        {
            var id = ((Models.Cocktail)listCoctail.SelectedItem).CocktailSk;
            var összetevő = from x in context.Recipes
                            where x.CocktailFk == id
                            select new DetailedRecipeItem
                            {
                                RecipeSk = x.RecipeSk,
                                MaterialName = x.MaterialFkNavigation.Name,
                                MaterialType = x.MaterialFkNavigation.TypeFkNavigation.Name,
                                Quantity = x.Quantity,
                                UnitName = x.MaterialFkNavigation.UnitFkNavigation.Name
                            };
            detailedRecipeItemBindingSource.DataSource = összetevő.ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var kiválasztottital = (Models.Cocktail)listCoctail.SelectedItem;
            var kiválasztottanyag = (Models.Material)listMaterial.SelectedItem;

            decimal x = 0;
            if (!decimal.TryParse(textBox1.Text, out x)) return;

            Models.Recipe receptsor = new Models.Recipe();

            receptsor.Quantity = x;
            receptsor.CocktailFk = kiválasztottital.CocktailSk;
            receptsor.MaterialFk = kiválasztottanyag.MaterialId;

            context.Recipes.Add(receptsor);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            Recept();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Biztos törölni akarja?", "Törlés", MessageBoxButtons.YesNo) == DialogResult.No)
            {
               // e.cancel = true;
            }

            var kiválasztottHozzávaló = (DetailedRecipeItem)detailedRecipeItemBindingSource.Current;
            var törlendő = (from x in context.Recipes
                            where x.RecipeSk == kiválasztottHozzávaló.RecipeSk
                            select x).FirstOrDefault();

            context.Recipes.Remove(törlendő);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            Recept();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            Regex r = new Regex("[0-9]");
            if (!r.IsMatch(textBox1.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox1, "Csak szám lehet");
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, "");
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Biztos beakarja zárni az abalkot?", "Alkalmazás bezárása", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

       
    }
    public class DetailedRecipeItem
    {
        public int RecipeSk { get; set; }
        public string MaterialName { get; set; }
        public string MaterialType { get; set; }
        public decimal Quantity { get; set; }
        public string UnitName { get; set; }
    }
}
