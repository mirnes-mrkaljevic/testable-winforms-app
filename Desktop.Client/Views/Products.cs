using Desktop.Client.DBAccess;
using Desktop.Client.Models;
using Desktop.Client.Presenters;
using Desktop.Client.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.Client.View
{
    public partial class Products : Form, IProductView
    {
      
        OpenFileDialog fileDialog = new OpenFileDialog();

        private ProductPresenter productPresenter;

        public event EventHandler ViewLoad;
        public event EventHandler<ProductViewModel> AddNewProduct;
        public event EventHandler<ProductViewModel> ModifyProduct;
        public event EventHandler<int> DeleteProduct;
        public event EventHandler<int> ProductSelected;

        public Products()
        {
            InitializeComponent();
            productPresenter = new ProductPresenter(this, new ProductDataAccess());
            this.Load += ViewLoad;
            this.btnAddNew.Click += BtnAddNew_Click;
            this.btnSave.Click += BtnSave_Click;
            this.dgvProducts.CellClick += DgvProducts_CellClick;
        }

        public void PopulateDataGridView(IList<Product> products)
        {
            dgvProducts.Rows.Clear();
            dgvProducts.Columns.Clear();

            dgvProducts.DataSource = new BindingList<Product>(products);
            for (int i = 0; i < dgvProducts.Columns.Count; i++)
            {
                if (dgvProducts.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)dgvProducts.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                    break;
                }
            }

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            dgvProducts.Columns.Add(btnEdit);
            btnEdit.Text = "Modify";
            btnEdit.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            dgvProducts.Columns.Add(btnDelete);
            btnDelete.Text = "Delete";
            btnDelete.UseColumnTextForButtonValue = true;
        }

        public void ClearInputControls()
        {
            txtName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtPhotoPath.Text = string.Empty;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnChoosePhoto_Click(object sender, EventArgs e)
        {
            DialogResult result = fileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                txtPhotoPath.Text = fileDialog.FileName;
            }
        }

        private void DgvProducts_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 5)
            {
                ProductSelected?.Invoke(this, int.Parse(dgvProducts[0, e.RowIndex].Value.ToString()));
                PopulateInputControlsFromGrid(e.RowIndex);
               
            }
            else if(e.ColumnIndex == 6)
            {
                DeleteProduct?.Invoke(this, int.Parse(dgvProducts[0, e.RowIndex].Value.ToString()));
            }
        }

        private void PopulateInputControlsFromGrid(int rowIndex)
        {
            txtName.Text = dgvProducts[1, rowIndex].Value.ToString();
            txtPrice.Text = dgvProducts[3, rowIndex].Value.ToString();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            ProductViewModel viewModel = new ProductViewModel()
            {
                NameText = txtName.Text,
                PriceText = txtPrice.Text,
                PhotoPathText = txtPhotoPath.Text
            };
            AddNewProduct?.Invoke(this, viewModel);
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            ProductViewModel viewModel = new ProductViewModel()
            {
                NameText = txtName.Text,
                PriceText = txtPrice.Text,
                PhotoPathText = txtPhotoPath.Text
            };
            ModifyProduct?.Invoke(this, viewModel);
        }


    }
}
