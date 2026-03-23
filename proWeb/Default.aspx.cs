using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;
using library;

namespace proWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                lblMessage.Text = "";
            }
        }

        /// <summary>Rellena el DropDownList con las categorías de la BD.</summary>
        private void LoadCategories()
        {
            ENCategory enCat = new ENCategory();
            List<ENCategory> cats = enCat.ReadAll();

            ddlCategory.Items.Clear();
            foreach (ENCategory cat in cats)
            {
                // Value = id numérico (1..4) que se almacena en la BD
                ddlCategory.Items.Add(new ListItem(cat.Name, cat.Id.ToString()));
            }
        }

        /// <summary>Lee los datos del formulario y devuelve un ENProduct.</summary>
        private ENProduct GetProductFromForm()
        {
            ENProduct en = new ENProduct();

            en.Code = txtCode.Text.Trim();
            en.Name = txtName.Text.Trim();

            int amount = 0;
            int.TryParse(txtAmount.Text.Trim(), out amount);
            en.Amount = amount;

            // Precio: aceptamos coma o punto como separador decimal
            float price = 0f;
            float.TryParse(
                txtPrice.Text.Trim().Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out price);
            en.Price = price;

            en.Category = int.Parse(ddlCategory.SelectedValue); // 1..4

            DateTime dt = DateTime.Now;
            DateTime.TryParseExact(
                txtCreationDate.Text.Trim(),
                "dd/MM/yyyy HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dt);
            en.CreationDate = dt;

            return en;
        }

        /// <summary>Rellena el formulario con los datos de un ENProduct.</summary>
        private void FillForm(ENProduct en)
        {
            txtCode.Text = en.Code;
            txtName.Text = en.Name;
            txtAmount.Text = en.Amount.ToString();
            txtPrice.Text = en.Price.ToString("F2", CultureInfo.InvariantCulture);
            txtCreationDate.Text = en.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

            // Selecciona la categoría correspondiente en el desplegable
            ListItem item = ddlCategory.Items.FindByValue(en.Category.ToString());
            if (item != null)
                ddlCategory.SelectedValue = en.Category.ToString();
        }

        /// <summary>Valida todos los campos del formulario.
        /// Devuelve true si todo es correcto, false y rellena lblMessage si hay error.</summary>
        private bool ValidateForm()
        {
            // Code: 1–16 caracteres
            string code = txtCode.Text.Trim();
            if (code.Length < 1 || code.Length > 16)
            {
                ShowError("Code must be between 1 and 16 characters.");
                return false;
            }

            // Name: máx. 32 caracteres
            if (txtName.Text.Trim().Length > 32)
            {
                ShowError("Name must be at most 32 characters.");
                return false;
            }

            // Amount: entero 0–9999
            int amount;
            if (!int.TryParse(txtAmount.Text.Trim(), out amount)
                || amount < 0 || amount > 9999)
            {
                ShowError("Amount must be an integer between 0 and 9999.");
                return false;
            }

            // Price: real 0–9999.99
            float price;
            if (!float.TryParse(
                    txtPrice.Text.Trim().Replace(',', '.'),
                    NumberStyles.Any, CultureInfo.InvariantCulture,
                    out price)
                || price < 0f || price > 9999.99f)
            {
                ShowError("Price must be a value between 0 and 9999.99.");
                return false;
            }

            // Creation Date: formato dd/MM/yyyy HH:mm:ss
            DateTime dt;
            if (!DateTime.TryParseExact(
                    txtCreationDate.Text.Trim(),
                    "dd/MM/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out dt))
            {
                ShowError("Creation Date must follow the format dd/mm/aaaa hh:mm:ss.");
                return false;
            }

            return true;
        }

        private void ShowError(string msg)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Error: " + msg;
        }

        private void ShowSuccess(string msg)
        {
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = msg;
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            // Comprobar que NO exista ya un producto con ese Code
            ENProduct check = new ENProduct();
            check.Code = txtCode.Text.Trim();
            if (check.Read())
            {
                ShowError("A product with this Code already exists.");
                return;
            }

            ENProduct en = GetProductFromForm();
            if (en.Create())
                ShowSuccess("Product created successfully.");
            else
                ShowError("Could not create the product.");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            // Comprobar que SÍ exista un producto con ese Code
            ENProduct check = new ENProduct();
            check.Code = txtCode.Text.Trim();
            if (!check.Read())
            {
                ShowError("No product found with this Code.");
                return;
            }

            ENProduct en = GetProductFromForm();
            if (en.Update())
                ShowSuccess("Product updated successfully.");
            else
                ShowError("Could not update the product.");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text.Trim();
            if (code.Length == 0)
            {
                ShowError("Please enter a Code to delete.");
                return;
            }

            ENProduct en = new ENProduct();
            en.Code = code;
            if (en.Delete())
            {
                ShowSuccess("Product deleted successfully.");
                // Limpiar el formulario tras borrar
                txtCode.Text = txtName.Text = txtAmount.Text =
                    txtPrice.Text = txtCreationDate.Text = "";
            }
            else
            {
                ShowError("Could not delete the product.");
            }
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            ENProduct en = new ENProduct();
            en.Code = txtCode.Text.Trim();
            if (en.Read())
            {
                FillForm(en);
                ShowSuccess("Product read successfully.");
            }
            else
            {
                ShowError("No product found with this Code.");
            }
        }

        protected void btnReadFirst_Click(object sender, EventArgs e)
        {
            ENProduct en = new ENProduct();
            if (en.ReadFirst())
            {
                FillForm(en);
                ShowSuccess("First product read successfully.");
            }
            else
            {
                ShowError("No products found in the database.");
            }
        }

        protected void btnReadPrev_Click(object sender, EventArgs e)
        {
            ENProduct en = new ENProduct();
            en.Code = txtCode.Text.Trim();
            if (en.ReadPrev())
            {
                FillForm(en);
                ShowSuccess("Previous product read successfully.");
            }
            else
            {
                ShowError("No previous product found.");
            }
        }

        protected void btnReadNext_Click(object sender, EventArgs e)
        {
            ENProduct en = new ENProduct();
            en.Code = txtCode.Text.Trim();
            if (en.ReadNext())
            {
                FillForm(en);
                ShowSuccess("Next product read successfully.");
            }
            else
            {
                ShowError("No next product found.");
            }
        }
    }
}
