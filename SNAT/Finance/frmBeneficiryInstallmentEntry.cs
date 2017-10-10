using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNAT.Finance
{
    public partial class frmBeneficiryInstallmentEntry : Form
    {
        public frmBeneficiryInstallmentEntry()
        {
            InitializeComponent();
        }

        private void frmBeneficiryInstallmentEntry_Load(object sender, EventArgs e)
        {

            BenfeList();
        }
        private void BenfeList()
        {
            grdInstSummary.Rows.Add("", "989796465798465", "98979646579846","Seesa", "10400", "100", "0", "0");
            grdInstSummary.Rows.Add("", "123654799654758","123654799654758", "Roleen", "8000", "120", "0", "0");
            grdInstSummary.Rows.Add("", "654123987456321","654123987456321", "Samy", "9250", "105", "0", "0");
            grdInstSummary.Rows.Add("", "852369874125585","852369874125585", "Zed", "23625", "200", "0", "0");
            grdInstSummary.Rows.Add("", "258741258745125","258741258745125", "Ray", "25870", "150", "0", "0");


            grdInstList.Rows.Add("", "", "555005225", "01-2016", "100", "0", "0");
            grdInstList.Rows.Add("", "", "555005226", "02-2016", "100", "0", "0");
            grdInstList.Rows.Add("", "", "555005227", "03-2016", "100", "0", "0");
            grdInstList.Rows.Add("", "", "555005228", "04-2016", "100", "0", "0");
            grdInstList.Rows.Add("", "", "555005229", "05-2016", "100", "0", "0");


        }
    }
}
