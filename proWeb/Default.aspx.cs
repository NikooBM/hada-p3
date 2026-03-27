using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using library;

namespace proWeb
{
    public partial class Default : System.Web.UI.Page
    {
        // ════════════════════════════════════════════════════════════════
        // PAGE LOAD
        // ════════════════════════════════════════════════════════════════
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                lblMessage.Text = "";
            }
        }

        // ════════════════════════════════════════════════════════════════
        // HELPERS PRIVADOS
        // ════════════════════════════════════════════════════════════════

        /// <summary>Rellena el DropDownList con las categorías de la BD.</summary>
        private void LoadCategories()
        {
            ddlCategory.Items.Clear();

            try
            {
                ENCategory enCat = new ENCategory();
                List<ENCategory> cats = enCat.ReadAll();

                if (cats != null && cats.Count > 0)
                {
                    // Carga desde la BD si funciona
                    foreach (ENCategory cat in cats)
                        ddlCategory.Items.Add(new ListItem(cat.Name, cat.Id.ToString()));
                }
                else
                {
                    // Fallback: categorías hardcodeadas si la BD está vacía
                    CargarCategoriasPorDefecto();
                }
            }
            catch
            {
                // Fallback: categorías hardcodeadas si la BD no está disponible
                CargarCategoriasPorDefecto();
            }
        }

        private void CargarCategoriasPorDefecto()
        {
            ddlCategory.Items.Add(new ListItem("Computing", "1"));
            ddlCategory.Items.Add(new ListItem("Telephony", "2"));
            ddlCategory.Items.Add(new ListItem("Gaming", "3"));
            ddlCategory.Items.Add(new ListItem("Home appliances", "4"));
        }

        /// <summary>Lee los datos del formulario y devuelve un ENProduct.</summary>
        private ENProduct GetProductFromForm()
        {
            ENProduct en = new ENProduct();

            en.Code = txtCode.Text.Trim();
            en.Name = txtName.Text.Trim();

            // Amount — entero
            int amount = 0;
            int.TryParse(txtAmount.Text.Trim(), out amount);
            en.Amount = amount;

            // Price — real, acepta coma o punto decimal
            float price = 0f;
            float.TryParse(
                txtPrice.Text.Trim().Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out price);
            en.Price = price;

            // Category — id de la BD (1..4)
            en.Category = ddlCategory.SelectedItem != null ? int.Parse(ddlCategory.SelectedValue) : 1;

            // Creation Date — formato dd/MM/yyyy HH:mm:ss
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
            txtCode.Text         = en.Code;
            txtName.Text         = en.Name;
            txtAmount.Text       = en.Amount.ToString();
            // Price con dos decimales, punto como separador
            txtPrice.Text        = en.Price.ToString("F2", CultureInfo.InvariantCulture);
            // Fecha en el formato exacto del enunciado
            txtCreationDate.Text = en.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

            ListItem item = ddlCategory.Items.FindByValue(en.Category.ToString());
            if (item != null)
                ddlCategory.SelectedValue = en.Category.ToString();
        }

        /// <summary>
        /// Valida todos los campos del formulario.
        /// Devuelve true si todo es correcto; false y rellena lblMessage si hay error.
        /// </summary>
        private bool ValidateForm()
        {
            // Code: 1–16 caracteres
            string code = txtCode.Text.Trim();
            if (code.Length < 1 || code.Length > 16)
            {
                ShowError("Code must be between 1 and 16 characters.");
                return false;
            }

            // Name: 1–32 caracteres (sin restricción de tipo de carácter)
            string name = txtName.Text.Trim();
            if (name.Length < 1 || name.Length > 32)
            {
                ShowError("Name must be between 1 and 32 characters.");
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

            // Price: real 0–9999.99 (acepta coma o punto)
            float price;
            if (!float.TryParse(
                    txtPrice.Text.Trim().Replace(',', '.'),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out price)
                || price < 0f || price > 9999.99f)
            {
                ShowError("Price must be a value between 0 and 9999.99.");
                return false;
            }

            // Creation Date: formato exacto dd/MM/yyyy HH:mm:ss
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

            if (ddlCategory.Items.Count == 0)
            {
                ShowError("No categories loaded. Check database connection.");
                return false;
            }

            return true;
        }

        private void ShowError(string msg)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text      = "Error: " + msg;
        }

        private void ShowSuccess(string msg)
        {
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text      = msg;
        }

        // ════════════════════════════════════════════════════════════════
        // BOTONES — CRUD
        // ════════════════════════════════════════════════════════════════

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

            // Verificar que existe antes de borrar
            if (!en.Read())
            {
                ShowError("No product found with this Code.");
                return;
            }

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
            string code = txtCode.Text.Trim();
            if (code.Length == 0)
            {
                ShowError("Please enter a Code to read.");
                return;
            }

            ENProduct en = new ENProduct();
            en.Code = code;
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
            string code = txtCode.Text.Trim();
            if (code.Length == 0)
            {
                ShowError("Please enter a Code before searching the previous product.");
                return;
            }

            ENProduct en = new ENProduct();
            en.Code = code;
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
            string code = txtCode.Text.Trim();
            if (code.Length == 0)
            {
                ShowError("Please enter a Code before searching the next product.");
                return;
            }

            ENProduct en = new ENProduct();
            en.Code = code;
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
