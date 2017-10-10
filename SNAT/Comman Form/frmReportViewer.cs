using CrystalDecisions.CrystalReports.Engine;
using SNAT.Comman_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNAT.Comman_Form
{
    public partial class frmReportViewer : Form
    {
        public frmReportViewer()
        {
            InitializeComponent();
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            this.crViewer.RefreshReport();
        }
        public void printReport(string strReportName, DataTable dtReportData, string strReportDataTableName, Dictionary<string, string> dicFormula = null)
        {
            try
            {
                DataSet dsReportDataSet = new DataSet();
                DataTable dtReportDataDummy = new DataTable();
                dtReportDataDummy = dtReportData.Copy();
                dsReportDataSet.Tables.Add(dtReportDataDummy);
                if (strReportDataTableName != null && !string.IsNullOrEmpty(strReportDataTableName.Trim()))
                {
                    dsReportDataSet.Tables[0].TableName = strReportDataTableName;
                }
                if (dsReportDataSet.Tables[0].Rows.Count == 0)
                {
                    ClsMessage.ProjectExceptionMessage("No data found to print.");
                    return;
                }
                //crViewer.ReportSource = dsReportDataSet.Tables[0];
                ReportDocument cryRpt = new ReportDocument();
                cryRpt.Load(Application.StartupPath + @"\Reports\" + strReportName);
                cryRpt.SetDataSource(dsReportDataSet);
                //plc.DataDefinition.FormulaFields["Formula Fild Name*"].Text = "" + textBox1.Text + "";
                if (dicFormula != null && dicFormula.Count > 0)
                {
                    foreach (KeyValuePair<string,string> strFormulaItem in dicFormula)
                    {
                        cryRpt.DataDefinition.FormulaFields[strFormulaItem.Key.ToString()].Text = "'"+ strFormulaItem.Value.ToString() +"'";
                    }
                }


                crViewer.ReportSource = cryRpt;
                crViewer.Refresh();
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }
    }
}
