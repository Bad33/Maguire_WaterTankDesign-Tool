using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace WaterTankTool_WFA
{
    public partial class Define_Materials : Form
    {
        private WaterTankDbContext _context;
        DialogResult result;
        MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
        private WaterTank _waterTankForm;


        //public Define_Materials()
        //{
        //    InitializeComponent();
        //    _context = WaterTankDbContext.GetInstance();
        //    MaterialListView();
        //}

        public Define_Materials(WaterTank waterTankForm)
        {
            InitializeComponent();
            _context = WaterTankDbContext.GetInstance();

            _waterTankForm = waterTankForm;
            MaterialListView();

        }

        public void MaterialListView()
        {
            if (_context.MaterialProperties != null)
            {
                
                dataGridView1.DataSource = _context.MaterialProperties
                    .Select(x => new
                    {
                        MaterialNumber = x.MaterialNumber,
                        MaterialName = x.MaterialName
                    })
                    .ToList();
                int columnIndexToDisable = 0;


                dataGridView1.Columns[columnIndexToDisable].ReadOnly = true;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int materialNumber = (int)selectedRow.Cells[0].Value;

            Material_Property_Data dialog = new Material_Property_Data("Modify", materialNumber,_waterTankForm);

            dialog.ShowDialog();
            MaterialListView();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Material_Property_Data dialog = new Material_Property_Data(_waterTankForm);

            dialog.ShowDialog();
            MaterialListView();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int materialNumber = (int)selectedRow.Cells[0].Value;
                string materialName = selectedRow.Cells[1].Value.ToString();

                result = MessageBox.Show($"Do you want to delete {materialName}?", "Confirm Delete", buttons, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    var materialProperties = _context.MaterialProperties.FirstOrDefault(item => item.MaterialNumber == materialNumber);

                    if (materialProperties != null)
                    {
                        _context.MaterialProperties.Remove(materialProperties);
                        _context.SaveChanges();
                        MessageBox.Show($"Material {materialName} deleted successfully.");
                        MaterialListView();
                        _waterTankForm.OnMaterialDeleted();
                    }
                    else
                    {
                        MessageBox.Show("Selected Material not found");
                    }
                    
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_context.MaterialProperties != null)
            {
                dataGridView1.DataSource = _context.MaterialProperties
                    .Select(x => new
                    {
                        MaterialNumber = x.MaterialNumber,
                        MaterialName = x.MaterialName
                    })
                    .ToList();
                int columnIndexToDisable = 0;


                dataGridView1.Columns[columnIndexToDisable].ReadOnly = true;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                }

            }
        }
    }
}
