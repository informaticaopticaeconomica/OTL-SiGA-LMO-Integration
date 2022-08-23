using System;
using System.Windows.Forms;
using Services;

namespace Integration_OTL_SIGA_LMO
{
    public partial class frmLayoutDesktop : Form
    {
       
        public frmLayoutDesktop(IServiceDB serviceDB)
        {

            InitializeComponent();

            //Invoke Method
            MessageBox.Show(serviceDB.GetOrdersGenerated());

        }

        private void frmLayoutDesktop_Load(object sender, EventArgs e)
        {

           
            


            //SqlConnection localSqlConnection = new SqlConnection();
            //MySqlConnection localMySqlConnection = new MySqlConnection();

            ////OTL
            //localSqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionToSeeOTL"].ToString();
            //localSqlConnection.Open();
            //MessageBox.Show("Connection: --> To See OTL <-- is Stable... Ok.. ");
            //localSqlConnection.Close();

            ////OLM
            //localSqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionToSeeOLM"].ToString();
            //localSqlConnection.Open();
            //MessageBox.Show("Connection: --> To See OLM <-- is Stable... Ok.. ");
            //localSqlConnection.Close();

            ////SiGA
            //localMySqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionToSeeSiGA"].ToString();
            //localMySqlConnection.Open();
            //MessageBox.Show("Connection: --> To See SiGA <-- is Stable... Ok.. ");
            //localMySqlConnection.Close();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
