using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterTankTool_WFA.Designer_Notes;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Migrations;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WaterTankTool_WFA
{
    public partial class Material_Property_Data : Form
    {
        string _dialogType;
        int _materialNumber;
        private WaterTankDbContext _context;
        private WaterTank _waterTankForm;

        public Material_Property_Data(WaterTank waterTankForm)
        {

            InitializeComponent();
            _context = WaterTankDbContext.GetInstance();
            _waterTankForm = waterTankForm;
            
            richTextBox1.Text = NotesManager.Notes.MaterialNotes ?? "Pre Loaded values are Standard values";
            


        }


        public Material_Property_Data(string controlType, int materialNumber, WaterTank waterTankForm)
        {
            _materialNumber = materialNumber;
            _dialogType = controlType;
            InitializeComponent();
            _context = WaterTankDbContext.GetInstance();
            _waterTankForm = waterTankForm;
            richTextBox1.Text = NotesManager.Notes.MaterialNotes ?? "Pre Loaded values are Standard values";

            ModifyDialogBox();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void ModifyDialogBox()
        {
            if (_dialogType == "Modify")
            {
                // Modify existing segment
                var materialProperties = _context.MaterialProperties.FirstOrDefault(item => item.MaterialNumber == _materialNumber);

                if (materialProperties == null)
                {
                    MessageBox.Show("Error: Segment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                textBox1.Text = materialProperties.MaterialName;
                comboBox2.Text = materialProperties.MaterialType;
                numericUpDown1.Text = materialProperties.Density.ToString();
                numericUpDown2.Text = materialProperties.ModulusOfElasticity.ToString();
                numericUpDown3.Text = materialProperties.TensileYieldStress.ToString();
                numericUpDown4.Text = materialProperties.TensileUltimateStress.ToString();


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                 string.IsNullOrWhiteSpace(comboBox2.Text) ||
                 string.IsNullOrWhiteSpace(numericUpDown1.Text) ||
                 string.IsNullOrWhiteSpace(numericUpDown2.Text) ||
                 string.IsNullOrWhiteSpace(numericUpDown3.Text) ||
                 string.IsNullOrWhiteSpace(numericUpDown4.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (
                !int.TryParse(numericUpDown1.Text, out int _density) ||
                !int.TryParse(numericUpDown2.Text, out int _modulusOfElasticity) ||
                !int.TryParse(numericUpDown3.Text, out int _tensileYieldStress) ||
                !int.TryParse(numericUpDown4.Text, out int _tensileUltimateStress))
            {
                MessageBox.Show("Please enter valid numbers ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MaterialProperties materialProperties;

            if (_dialogType == "Modify")
            {
                // Modify existing segment
                materialProperties = _context.MaterialProperties.FirstOrDefault(item => item.MaterialNumber == _materialNumber);

                if (materialProperties == null)
                {
                    MessageBox.Show("Error: Segment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                materialProperties.MaterialName = textBox1.Text;
                materialProperties.MaterialType = comboBox2.Text;
                materialProperties.Density = _density;
                materialProperties.ModulusOfElasticity = _modulusOfElasticity;
                materialProperties.TensileYieldStress = _tensileYieldStress;
                materialProperties.TensileUltimateStress = _tensileUltimateStress;
            }
            else
            {
                // Add new Material
                materialProperties = new MaterialProperties()
                {
                    MaterialName = textBox1.Text,
                    MaterialType = comboBox2.Text,
                    Density = _density,
                    ModulusOfElasticity = _modulusOfElasticity,
                    TensileYieldStress = _tensileYieldStress,
                    TensileUltimateStress = _tensileUltimateStress,
                };

                _context.MaterialProperties.Add(materialProperties);
            }



            try
            {
                int rowsAffected = _context.SaveChanges();
                successDialog(rowsAffected);
                _waterTankForm.OnmaterialAdded();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void successDialog(int rowsAffected)
        {
            if (rowsAffected > 0)
            {
                DialogResult result = MessageBox.Show("Data saved successfully!", "Confirmation", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Data might not have been saved!", "Confirmation", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            NotesManager.Notes.MaterialNotes = richTextBox1.Text;

       
            NotesManager.SaveNotes();
        }
    }
}
