using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Services;
using Services.Models;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;

namespace Integration_OTL_SIGA_LMO
{
    public partial class frmLayoutDesktop : Form
    {

        //Declare service scope: dependency injection
        private readonly IServiceDB _serviceDB;

        public static frmLayoutShowMessage showMessage;      
        private DateTimePicker dtpFechaInicial;
        private DateTimePicker dtpFechaFinal;
        private CheckBox chkActivaRangoFechas;
        private Button btnQueryByDates;
        private TextBox txtOrderNumber;

        public frmLayoutDesktop(IServiceDB serviceDB)
        {

            //Initialize Component
            InitializeComponent();

            //Set service scope: dependency injection
            _serviceDB = serviceDB;

        }

        //Load
        private void frmLayoutDesktop_Load(object sender, EventArgs e)
        {

            //Initial text
            txtOrderNumber.Text = "Please enter Order number";

            //Activate Timer
            ActivateTimer();

            //Create Tooltips
            CreateToolTips();

        }


        //Active date range
        private void chkActivaRangoFechas_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaInicial.Value = DateTime.Today;
            dtpFechaFinal.Value = DateTime.Today;
            if (chkActivaRangoFechas.Checked)
            {
                dtpFechaInicial.Enabled = true;
                dtpFechaFinal.Enabled = true;
                dtpFechaInicial.Font = new Font(dtpFechaInicial.Font, System.Drawing.FontStyle.Bold);
                dtpFechaFinal.Font = new Font(dtpFechaFinal.Font, System.Drawing.FontStyle.Bold);
                btnQueryByDates.Enabled = true;
            }
            else
            {
                dtpFechaInicial.Enabled = false;
                dtpFechaFinal.Enabled = false;
                dtpFechaInicial.Font = new Font(dtpFechaInicial.Font, FontStyle.Regular);
                dtpFechaFinal.Font = new Font(dtpFechaFinal.Font, FontStyle.Regular);
                btnQueryByDates.Enabled = false;
            }
        }


        //Execute Query Orders
        private void onGoToExecuteQueryOrders()
        {
            dgvStationRows.DataSource = null;
            dgvStationRows.Rows.Clear();
            dgvStationRows.ColumnCount = 13;
            dgvStationRows.Columns[0].Name = "Identificador";
            dgvStationRows.Columns[1].Name = "Fecha";
            dgvStationRows.Columns[2].Name = "Nombre Cliente";
            dgvStationRows.Columns[3].Name = "OPC OD";
            dgvStationRows.Columns[4].Name = "Producto OD";
            dgvStationRows.Columns[5].Name = "OPC OI";
            dgvStationRows.Columns[6].Name = "Producto OI";
            dgvStationRows.Columns[7].Name = "No. Factura";
            dgvStationRows.Columns[8].Name = "IdPedido";
            dgvStationRows.Columns[9].Name = "IdCliente";
            dgvStationRows.Columns[10].Name = "IdProductoOD";
            dgvStationRows.Columns[11].Name = "IdProductoOI";
            dgvStationRows.Columns[12].Name = "Caja";
            dgvStationRows.Columns[8].Visible = false;
            dgvStationRows.Columns[9].Visible = false;
            dgvStationRows.Columns[10].Visible = false;
            dgvStationRows.Columns[11].Visible = false;
            dgvStationRows.Columns[12].Visible = false;
            dgvStationRows.Columns[0].Width = 80;
            dgvStationRows.Columns[1].Width = 60;
            dgvStationRows.Columns[2].Width = 130;
            dgvStationRows.Columns[3].Width = 75;
            dgvStationRows.Columns[4].Width = 200;
            dgvStationRows.Columns[5].Width = 75;
            dgvStationRows.Columns[6].Width = 200;
            dgvStationRows.Columns[7].Width = 100;
            dgvStationRows.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStationRows.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStationRows.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStationRows.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStationRows.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStationRows.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStationRows.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStationRows.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvStationRows.RowHeadersVisible = false;
            new Thread(ExecuteQueryOrders).Start();
        }


        //Query Orders on Database
        private void ExecuteQueryOrders()
        {
            int countNextOrder = 0;
            int actionExecute = 0;
            DateTime startDate = DateTime.Today;
            DateTime finalDate = DateTime.Today;
            if (chkActivaRangoFechas.Checked)
            {
                actionExecute = 1;
                startDate = dtpFechaInicial.Value;
                finalDate = dtpFechaFinal.Value;
            }
            DataTable ordersGenerated = _serviceDB.GetOrdersGenerated(actionExecute, DateTime.Today, startDate, finalDate);
            if (ordersGenerated.Rows.Count <= 0)
            {
                return;
            }
            BeginInvoke((Action)delegate
            {
                dgvStationRows.Cursor = Cursors.AppStarting;
                int count = ordersGenerated.Rows.Count;
                lblCount.Text = "Count: " + count;
                lblCount.Refresh();
                foreach (DataRow dataRow in ordersGenerated.Rows)
                {
                    if (!CheckForm("showMessage"))
                    {
                        showMessage = new frmLayoutShowMessage();
                    }
                    imgLaptopHome.IconColor = Color.Green;
                    imgLaptopHome.Refresh();
                    if (dataRow["OPCOD"].ToString()!.Trim() != "" || dataRow["OPCOI"].ToString()!.Trim() != "")
                    {
                        DataGridViewRow dataGridViewRow = new DataGridViewRow();
                        dataGridViewRow.CreateCells(dgvStationRows);
                        dataGridViewRow.Cells[0].Value = dataRow["Identificador"];
                        dataGridViewRow.Cells[1].Value = dataRow["Fecha"];
                        dataGridViewRow.Cells[2].Value = dataRow["Paciente"];
                        dataGridViewRow.Cells[3].Value = dataRow["OPCOD"];
                        dataGridViewRow.Cells[4].Value = dataRow["NombreProductoOD"];
                        dataGridViewRow.Cells[5].Value = dataRow["OPCOI"];
                        dataGridViewRow.Cells[6].Value = dataRow["NombreProductoOI"];
                        dataGridViewRow.Cells[7].Value = dataRow["Factura"];
                        dataGridViewRow.Cells[8].Value = dataRow["IdPedido"];
                        dataGridViewRow.Cells[9].Value = dataRow["IdCliente"];
                        dataGridViewRow.Cells[10].Value = dataRow["IdProductoOD"];
                        dataGridViewRow.Cells[11].Value = dataRow["IdProductoOI"];
                        dataGridViewRow.Cells[12].Value = dataRow["Caja"];
                        dgvStationRows.Rows.Insert(0, dataGridViewRow);
                        dgvStationRows.Refresh();
                        countNextOrder++;
                        lblNext.Text = "Next: " + countNextOrder;
                        lblNext.Refresh();
                        try
                        {
                            showMessage.Hide();
                            LinkToSiGAInsertOrder(dataRow);
                            showMessage.Close();
                            showMessage.Dispose();
                            showMessage = null;
                        }
                        catch (Exception ex)
                        {
                            dgvStationRows.Cursor = Cursors.Default;
                            if (CheckForm("showMessage"))
                            {
                                showMessage.StringMessageFromForm = ex.Message;
                                showMessage.Show();
                            }
                        }
                        finally
                        {
                            imgLaptopHome.IconColor = Color.Moccasin;
                            imgLaptopHome.Refresh();
                            if (countNextOrder >= count)
                            {
                                ActivateTimer();
                                chkActivaRangoFechas.Checked = false;
                            }
                        }
                    }
                }
                dgvStationRows.Cursor = Cursors.Default;
            });
        }


        //Check form
        private bool CheckForm(string formName)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == formName)
                {
                    return true;
                }
            }
            return false;
        }


        //Button Query by dates
        private void btnQueryByDates_Click(object sender, EventArgs e)
        {
            if (!VerifiedDatesAreAligned())
            {
                System.Windows.MessageBox.Show("The date selection order is not correct", "Trace Orders", (System.Windows.MessageBoxButton)MessageBoxButtons.OK);
            }
            else
            {
                onGoToExecuteMethods();
            }
        }


        //Verify dates are aligned
        private bool VerifiedDatesAreAligned()
        {
            bool valueToReturn = true;
            if (dtpFechaInicial.Value > dtpFechaFinal.Value)
            {
                valueToReturn = false;
            }
            return valueToReturn;
        }


        //Text Order number
        private void txtOrderNumber_Enter(object sender, EventArgs e)
        {
            Font MediumFont = new Font("Century Gothic", 10f);
            txtOrderNumber.Text = "";
            txtOrderNumber.Font = MediumFont;
            txtOrderNumber.ForeColor = Color.Indigo;
            txtOrderNumber.Font = new Font(txtOrderNumber.Font, System.Drawing.FontStyle.Bold);
        }


        //Text Order leave
        private void txtOrderNumber_Leave(object sender, EventArgs e)
        {
            Font SmallFont = new Font("Segoe UI", 9f);
            txtOrderNumber.Text = "Please enter Order number";
            txtOrderNumber.Font = SmallFont;
            txtOrderNumber.ForeColor = Color.LightSlateGray;
            txtOrderNumber.Font = new Font(txtOrderNumber.Font, System.Drawing.FontStyle.Regular);
        }


        //Close dtp initial
        private void dtpFechaInicial_CloseUp(object sender, EventArgs e)
        {
            dtpFechaFinal.Focus();
        }


        //Close dtp Final
        private void dtpFechaFinal_CloseUp(object sender, EventArgs e)
        {
            btnQueryByDates.Focus();
        }


        //Text Changed
        private void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
        }


        //Text Key press
        private void txtOrderNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                System.Windows.MessageBox.Show(" Enter pressed ");
            }
        }


        //Get invoiced products from SiGA (Order OTL that had been invoiced), then Update products inventory on LMO Database
        private void GetInvoicedProductsInSiGA()
        {
            BeginInvoke((Action)delegate
            {
                imgLaptopHome.IconColor = Color.Green;
                imgLaptopHome.Refresh();
                Cursor = Cursors.AppStarting;
                frmLayoutShowMessage frmLayoutShowMessage2 = new frmLayoutShowMessage();
                try
                {
                    List<ProductsFromSiGA> list = new List<ProductsFromSiGA>();
                    List<ProductsFromSiGAUpdatedOut> list2 = new List<ProductsFromSiGAUpdatedOut>();
                    list = _serviceDB.GetInvoicedProductsInSiGA();
                    if (list != null && list.Count != 0)
                    {
                        list2 = _serviceDB.UpdateProductsInventoryInLMO(list);
                    }
                    if (list2 != null && list2.Count != 0)
                    {
                        foreach (ProductsFromSiGAUpdatedOut current in list2)
                        {
                            _serviceDB.UpdateRerenceNumberFromLMOToSiGA(current);
                        }
                    }
                    if (list != null && list.Count != 0)
                    {
                        foreach (ProductsFromSiGA current2 in list)
                        {
                            _serviceDB.UpdateInvoiceDataFieldsInOTL(current2);
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    imgLaptopHome.IconColor = Color.Moccasin;
                    imgLaptopHome.Refresh();
                    frmLayoutShowMessage2.StringMessageFromForm = ex.Message;
                    Cursor = Cursors.Default;
                    frmLayoutShowMessage2.Show();
                }
                finally
                {
                    Cursor = Cursors.Default;
                    imgLaptopHome.IconColor = Color.Moccasin;
                    imgLaptopHome.Refresh();
                }
            });
        }


        ////Will go to OTL to Write down 1.- Invoice number (OTL Field: Factura) 2.- Date of Invoice number (OTL Field: FechaFactura)
        //if ((oProductsListSiGAUpdateOut != null) && (oProductsListSiGAUpdateOut.Count != 0))
        //{

        //    //Update OTL
        //    foreach (var oProductsSiGAUpdateOut in oProductsListSiGAUpdateOut)
        //    {
        //        _serviceDB.UpdateInvoiceDataFieldsInOTL(oProductsSiGAUpdateOut);
        //    }
        //}
     

        //Activate Timer
        private void ActivateTimer()
        {
            //Set values for timer / 1,000 miliseconds = 1 second
            timerIntegration.Interval = 1800000;
            timerIntegration.Enabled = true;
            timerIntegration.Start();
        }


        //Deactivate Timer
        private void DeactivateTimer()
        {
            //Set values for timer
            timerIntegration.Interval = 100;
            timerIntegration.Enabled = false; //Initial
            timerIntegration.Stop();
        }


        //Create ToolTips
        private void CreateToolTips()
        {

            //Image Tooltip
            ToolTip toolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;

            // Set up the ToolTip text for the Image
            toolTip.SetToolTip(this.lblCount, "Lines on Grid");
            toolTip.SetToolTip(this.imgLaptopHome, "Click: Refresh");
            toolTip.SetToolTip(this.btnExit, "Exit");
        }


        //Link to SiGA
        private void LinkToSiGAInsertOrder(DataRow itemGridRow)
        {
            try
            {
                Order oOrder = new Order
                {
                    OrderNumber = itemGridRow[2].ToString(),
                    BoxNumber = itemGridRow[8].ToString(),
                    CodigoProductoOD = itemGridRow[12].ToString(),
                    CodigoProductoOI = itemGridRow[13].ToString(),
                    OPCRightEye = itemGridRow[37].ToString(),
                    OPCLeftEye = itemGridRow[38].ToString()
                };
                _serviceDB.InsertOrderIntoSiGA(oOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Exit bottom
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //Execute methods once timer go on
        private void timerIntegration_Tick(object sender, EventArgs e)
        {

            //Execute Methods
            onGoToExecuteMethods();

        }


        //Refresh data / Laptop image
        private void imgLaptopHome_Click(object sender, EventArgs e)
        {

            //Execute Methods
            onGoToExecuteMethods();

        }


        //Execute Methods
        private void onGoToExecuteMethods()
        {

            //Call Desactivate Timer
            DeactivateTimer();

            //Execute Query Orders
            onGoToExecuteQueryOrders();

            //Generate Thread
            new Thread(GetInvoicedProductsInSiGA).Start();

        }

    }
}
