namespace UPZN55_3H
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Excit-el zárult");
            }
            else
            {
                MessageBox.Show("Nem Excit-el zárult");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            if (form3.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Excit-el zárult");
            }
            else
            {
                MessageBox.Show("Nem Excit-el zárult");
            }
        }
    }
}