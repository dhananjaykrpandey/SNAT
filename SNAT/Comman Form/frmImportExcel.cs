using SNAT.Comman_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SNAT.Comman_Form
{
    public partial class frmImportExcel : Form
    {
        DataTable dtBankExcel = new DataTable();
        DataSet mdsCreateDatatable = new DataSet();
        public string FileName { get; set; }
        public string SheetName { get; set; }
        public string strMonth { get; set; }
        public string strYear { get; set; }
        public enum enmImportType
        {
            PSPF,
            Treasury,
            Bank
        }

        private enmImportType _enmImportType = enmImportType.Bank;
        public enmImportType ImportType
        {
            get
            {
                return _enmImportType;
            }
            set
            {
                _enmImportType = value;
            }
        }
        public DataTable dtExcelData { get; set; }
        public frmImportExcel()
        {
            InitializeComponent();
        }
        private void CreateDataTable()
        {
            try
            {
                dtExcelData = new DataTable();
                dtExcelData.Columns.Add("nationalid", typeof(string));
                dtExcelData.Columns.Add("memberid", typeof(string));
                dtExcelData.Columns.Add("employeeno", typeof(string));
                dtExcelData.Columns.Add("tscno", typeof(string));
                dtExcelData.Columns.Add("membername", typeof(string));
                dtExcelData.Columns.Add("wagesamount", typeof(decimal));
                dtExcelData.Columns.Add("MonthYear", typeof(string));
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CreateBankDataTable()
        {
            try
            {
                dtBankExcel = new DataTable();
                dtBankExcel.Columns.Add("Date", typeof(string));
                dtBankExcel.Columns.Add("ValueDate", typeof(string));
                dtBankExcel.Columns.Add("StatementNumber", typeof(string));
                dtBankExcel.Columns.Add("Description", typeof(string));
                dtBankExcel.Columns.Add("Amount", typeof(string));
                dtBankExcel.Columns.Add("Balance", typeof(string));
                dtBankExcel.Columns.Add("BankReferenceNo", typeof(string));
                dtBankExcel.Columns.Add("TransactionTypeCode", typeof(string));
                dtBankExcel.Columns.Add("InputBranchNo", typeof(string));
                dtBankExcel.Columns.Add("AccountOwnerReference", typeof(string));
                dtBankExcel.Columns.Add("CustomerReference", typeof(string));
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void rbStandread_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtReadRow.Visible = false;
                lblMsg.Visible = false;
                lbltext.Visible = false;
                if (rbStandread.Checked)
                {
                    txtReadRow.Visible = true;
                    lblMsg.Visible = true;
                    lbltext.Visible = true;
                    lblMsg.Text = "Standard process will take a few minutes" + Environment.NewLine + "Please wait to complete process.";
                }
                if (rbAdvance.Checked)
                {

                    lblMsg.Visible = true;
                    lblMsg.Text = "Please make sure excel sheet first row will be columns header." + Environment.NewLine + "Import data will be start from second row.";
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //switch (_enmImportType)
                //{
                //    case enmImportType.Bank:

                //        break;

                //    case enmImportType.PSPF:

                //        break;
                //    case enmImportType.Treasury:
                //        break;
                //}

                if (rbStandread.Checked)
                {
                    if (string.IsNullOrEmpty(txtReadRow.Text.Trim()))
                    {
                        ClsMessage.ProjectExceptionMessage("Please enter data read row number.");
                        dtExcelData = null;
                        return;
                    }
                    else
                    {
                        ImportExcelStandrad(FileName, SheetName);

                        DialogResult = DialogResult.OK;
                        Close();
                    }

                }
                //if (rbAdvance.Checked)
                //{
                //    ImportExcelAdvance(FileName, SheetName);
                //    DialogResult = DialogResult.OK;
                //    Close();
                //}
                if (rbAdvance.Checked)
                {
                    ImportExcelAdvance(FileName, SheetName);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void ImportExcelStandrad(string strFileName, string strsheetName)
        {
            try
            {
                CreateDataTable();
                //Create COM Objects. Create a COM object for everything that is referenced
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(strFileName);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[strsheetName];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count - 1;
                int colCount = xlRange.Columns.Count;

                //iterate over the rows and columns and print to the console as it appears in the file
                //excel is not zero based!!
                //for (int i = 1; i <= rowCount; i++)
                //{
                //    for (int j = 1; j <= colCount; j++)
                //    {
                //        //new line
                //        if (j == 1)
                //            Console.Write("\r\n");

                //        //write the value to the console
                //        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                //            Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                //    }
                //}

                switch (_enmImportType)
                {
                    case enmImportType.Bank:
                        CreateBankDataTable();
                        for (int i = Convert.ToInt32(txtReadRow.Text.Trim()); i <= rowCount; i++)
                        {
                            DataRow dRow = dtBankExcel.NewRow();
                            if (xlRange.Cells[i, "A"] != null && xlRange.Cells[i, "A"].Value2 != null)
                            {
                                dRow["Date"] = xlRange.Cells[i, "A"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "B"] != null && xlRange.Cells[i, "B"].Value2 != null)
                            {
                                dRow["ValueDate"] = xlRange.Cells[i, "B"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "C"] != null && xlRange.Cells[i, "C"].Value2 != null)
                            {
                                dRow["StatementNumber"] = xlRange.Cells[i, "C"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "D"] != null && xlRange.Cells[i, "D"].Value2 != null)
                            {
                                dRow["Description"] = xlRange.Cells[i, "D"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "E"] != null && xlRange.Cells[i, "E"].Value2 != null)
                            {
                                dRow["Amount"] = xlRange.Cells[i, "E"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "F"] != null && xlRange.Cells[i, "F"].Value2 != null)
                            {
                                dRow["Balance"] = xlRange.Cells[i, "F"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "G"] != null && xlRange.Cells[i, "G"].Value2 != null)
                            {
                                dRow["BankReferenceNo"] = xlRange.Cells[i, "G"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "H"] != null && xlRange.Cells[i, "H"].Value2 != null)
                            {
                                dRow["TransactionTypeCode"] = xlRange.Cells[i, "H"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "I"] != null && xlRange.Cells[i, "I"].Value2 != null)
                            {
                                dRow["InputBranchNo"] = xlRange.Cells[i, "I"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "J"] != null && xlRange.Cells[i, "J"].Value2 != null)
                            {
                                dRow["AccountOwnerReference"] = xlRange.Cells[i, "J"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "K"] != null && xlRange.Cells[i, "K"].Value2 != null)
                            {
                                dRow["CustomerReference"] = xlRange.Cells[i, "K"].Value2.ToString().Trim();
                            }

                            dtBankExcel.Rows.Add(dRow);
                        }
                        dtExcelData = new DataTable();
                        dtExcelData = dtBankExcel;
                        //ProcessBankDetails(dtBankExcel);

                        InsertDumpData(dtExcelData);
                        GetMemberData();
                        break;
                    case enmImportType.PSPF:

                        for (int i = Convert.ToInt32(txtReadRow.Text.Trim()); i <= rowCount; i++)
                        {
                            DataRow dRow = dtExcelData.NewRow();
                            if (xlRange.Cells[i, "A"] != null && xlRange.Cells[i, "A"].Value2 != null)
                            {
                                dRow["memberid"] = xlRange.Cells[i, "A"].Value2.ToString().Trim();
                            }
                            if ((xlRange.Cells[i, "E"] != null && xlRange.Cells[i, "E"].Value2 != null) && (xlRange.Cells[i, "F"] != null && xlRange.Cells[i, "F"].Value2 != null))
                            {
                                dRow["membername"] = xlRange.Cells[i, "F"].Value2.ToString().Trim() + " " + xlRange.Cells[i, "E"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "J"] != null && xlRange.Cells[i, "J"].Value2 != null)
                            {
                                dRow["wagesamount"] = xlRange.Cells[i, "J"].Value2.ToString().Trim();
                            }
                            dRow["MonthYear"] = strMonth + "-" + strYear;
                            dtExcelData.Rows.Add(dRow);
                        }
                        InsertDumpData(dtExcelData);
                        GetMemberData();
                        break;
                    case enmImportType.Treasury:

                        for (int i = Convert.ToInt32(txtReadRow.Text.Trim()); i <= rowCount; i++)
                        {
                            DataRow dRow = dtExcelData.NewRow();
                            if (xlRange.Cells[i, "A"] != null && xlRange.Cells[i, "A"].Value2 != null)
                            {
                                dRow["employeeno"] = xlRange.Cells[i, "A"].Value2.ToString().Trim();
                            }
                            if ((xlRange.Cells[i, "B"] != null && xlRange.Cells[i, "B"].Value2 != null))
                            {
                                dRow["membername"] = xlRange.Cells[i, "B"].Value2.ToString().Trim();
                            }
                            if (xlRange.Cells[i, "F"] != null && xlRange.Cells[i, "F"].Value2 != null)
                            {
                                dRow["wagesamount"] = xlRange.Cells[i, "F"].Value2.ToString().Trim();
                            }
                            dRow["MonthYear"] = strMonth + "-" + strYear;
                            dtExcelData.Rows.Add(dRow);
                        }
                        InsertDumpData(dtExcelData);
                        GetMemberData();
                        break;


                }
                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:
                //  never use two dots, all COM objects must be referenced and released individually
                //  ex: [somthing].[something].[something] is bad

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void ImportExcelAdvance(string strFileName, string strsheetName)
        {
            try
            {
                CreateDataTable();
                System.Data.OleDb.OleDbConnection MyConnection = new System.Data.OleDb.OleDbConnection();
                System.Data.DataSet DtSet;
                System.Data.OleDb.OleDbDataAdapter MyCommand;
                if (System.IO.Path.GetExtension(strFileName).ToString().ToUpper() == ".XLS")
                {
                    MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + strFileName + "';Extended Properties=Excel 8.0;");
                }
                else if (System.IO.Path.GetExtension(strFileName).ToString().ToUpper() == ".XLSX")
                {
                    MyConnection = new System.Data.OleDb.OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; data source='" + strFileName + "'; Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"");
                }
                else if (System.IO.Path.GetExtension(strFileName).ToString().ToUpper() == ".CSV")
                {
                    txtReadRow.Text = "2";
                    ImportExcelStandrad(strFileName, strsheetName);
                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("File type not supported" + Environment.NewLine + "Please contact system administrator!!");
                    dtExcelData = null;
                    return;
                }
                MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [" + strsheetName + "$]", MyConnection);
                MyCommand.TableMappings.Add("Table", "TestTable");
                DtSet = new System.Data.DataSet();
                MyCommand.Fill(DtSet);
                // dtExcelData = DtSet.Tables[0];
                MyConnection.Close();
                if (DtSet.Tables[0].DefaultView != null && DtSet.Tables[0].DefaultView.Count > 0)
                {
                    switch (_enmImportType)
                    {
                        case enmImportType.Bank:
                            CreateBankDataTable();
                            foreach (DataRowView drvRow in DtSet.Tables[0].DefaultView)
                            {
                                DataRow dRow = dtBankExcel.NewRow();
                                if (drvRow[0] != null && drvRow[0].ToString().Trim() != "")
                                {
                                    dRow["Date"] = drvRow[0].ToString().Trim();
                                }
                                if (drvRow[1] != null && drvRow[1].ToString().Trim() != "")
                                {
                                    dRow["ValueDate"] = drvRow[1].ToString().Trim();
                                }
                                if (drvRow[2] != null && drvRow[2].ToString().Trim() != "")
                                {
                                    dRow["StatementNumber"] = drvRow[2].ToString().Trim();
                                }
                                if (drvRow[3] != null && drvRow[3].ToString().Trim() != "")
                                {
                                    dRow["Description"] = drvRow[3].ToString().Trim();
                                }
                                if (drvRow[4] != null && drvRow[4].ToString().Trim() != "")
                                {
                                    dRow["Amount"] = drvRow[4].ToString().Trim();
                                }
                                if (drvRow[5] != null && drvRow[5].ToString().Trim() != "")
                                {
                                    dRow["Balance"] = drvRow[5].ToString().Trim();
                                }
                                if (drvRow[6] != null && drvRow[6].ToString().Trim() != "")
                                {
                                    dRow["BankReferenceNo"] = drvRow[6].ToString().Trim();
                                }
                                if (drvRow[7] != null && drvRow[7].ToString().Trim() != "")
                                {
                                    dRow["TransactionTypeCode"] = drvRow[7].ToString().Trim();
                                }
                                if (drvRow[8] != null && drvRow[8].ToString().Trim() != "")
                                {
                                    dRow["InputBranchNo"] = drvRow[8].ToString().Trim();
                                }
                                if (drvRow[9] != null && drvRow[9].ToString().Trim() != "")
                                {
                                    dRow["AccountOwnerReference"] = drvRow[9].ToString().Trim();
                                }
                                if (drvRow[10] != null && drvRow[10].ToString().Trim() != "")
                                {
                                    dRow["CustomerReference"] = drvRow[10].ToString().Trim();
                                }

                                dtBankExcel.Rows.Add(dRow);
                            }
                            dtExcelData = new DataTable();
                            dtExcelData = dtBankExcel;


                            break;
                        case enmImportType.PSPF:
                            foreach (DataRowView drvRow in DtSet.Tables[0].DefaultView)
                            {
                                DataRow dRow = dtExcelData.NewRow();
                                if (drvRow[0] != null && drvRow[0].ToString().Trim() != "")
                                {
                                    dRow["memberid"] = drvRow[0].ToString().Trim();
                                }
                                if (((drvRow[4] != null && drvRow[4].ToString().Trim() != "") && (drvRow[5] != null && drvRow[5].ToString().Trim() != "")))
                                {
                                    dRow["membername"] = drvRow[5].ToString().Trim() + " " + drvRow[4].ToString().Trim();
                                }
                                if (drvRow[8] != null && drvRow[8].ToString().Trim() != "")
                                {
                                    dRow["wagesamount"] = drvRow[8].ToString().Trim();
                                }
                                dRow["MonthYear"] = strMonth + "-" + strYear;
                                dtExcelData.Rows.Add(dRow);
                            }
                            InsertDumpData(dtExcelData);
                            GetMemberData();
                            break;
                        case enmImportType.Treasury:
                            foreach (DataRowView drvRow in DtSet.Tables[0].DefaultView)
                            {
                                DataRow dRow = dtExcelData.NewRow();
                                if (drvRow[0] != null && drvRow[0].ToString().Trim() != "")
                                {
                                    dRow["employeeno"] = drvRow[0].ToString().Trim();
                                }
                                if ((drvRow[1] != null && drvRow[1].ToString().Trim() != ""))
                                {
                                    dRow["membername"] = drvRow[1].ToString().Trim();
                                }
                                if (drvRow[5] != null && drvRow[5].ToString().Trim() != "")
                                {
                                    dRow["wagesamount"] = drvRow[5].ToString().Trim();
                                }
                                dRow["MonthYear"] = strMonth + "-" + strYear;
                                dtExcelData.Rows.Add(dRow);
                            }
                            InsertDumpData(dtExcelData);
                            GetMemberData();
                            break;
                    }

                }
                else
                {
                    ClsMessage.ProjectExceptionMessage("No record found!!");
                    dtExcelData = null;
                    return;
                }



            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void InsertDumpData(DataTable dtExportData)
        {
            try
            {
                string sqlQuery = "";
                switch (_enmImportType)
                {
                    case enmImportType.Bank:
                        break;
                    case enmImportType.PSPF:
                        sqlQuery = "Truncate table SNAT.dbo.T_Dump_PSPF";
                        ClsDataLayer.UpdateData(sqlQuery);
                        sqlQuery = "SELECT id,memberid,membername,wagesamount,MonthYear  FROM SNAT.dbo.T_Dump_PSPF WHERE 1=2";
                        ClsDataLayer.UpdateDataAdapter(sqlQuery, dtExportData);

                        break;
                    case enmImportType.Treasury:
                        sqlQuery = "Truncate table SNAT.dbo.T_Dump_Treasury";
                        ClsDataLayer.UpdateData(sqlQuery);
                        sqlQuery = "SELECT id,employeeno,membername,wagesamount,MonthYear  FROM SNAT.dbo.T_Dump_Treasury WHERE 1=2";
                        ClsDataLayer.UpdateDataAdapter(sqlQuery, dtExportData);
                        break;


                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        string strSqlQuery = "";
        private void GetMemberData()
        {
            try
            {
                string strLinkOn = "employeeno";
                if (rbMemberID.Checked)
                {
                    strLinkOn = "memberid";
                }
                else
                {
                    strLinkOn = "employeeno";
                }

                switch (_enmImportType)
                {
                    

                    case enmImportType.Bank:
                        break;
                    case enmImportType.PSPF:
                        strSqlQuery = "SELECT tdp.id dumpid,tdp.memberid dumpmemberid,tdp.membername dumpmembername,tdp.MonthYear dumpMonthYear," + Environment.NewLine +
                                      " tdp.wagesamount dumpwagesamount, MM.id,  MM.nationalid,  MM.memberid,  MM.employeeno,  MM.tscno," + Environment.NewLine +
                                      " MM.membername,ISNULL(MM.wagesamount,0) wagesamount,  MM.wageseffectivedete , CAST(CASE WHEN ISNULL(MM.memberid,'')='' THEN 0 ELSE 1 END AS bit) lValidMember" + Environment.NewLine +
                                      " FROM SNAT.dbo.T_Dump_PSPF tdp " + Environment.NewLine +
                                      " LEFT OUTER JOIN  SNAT.dbo.T_Member MM (nolock) ON mm."+ strLinkOn + "=tdp.memberid AND mm.livingstatus='L'  Order By tdp.id";
                        //mm.memberid=tdp.memberid 
                        dtExcelData = ClsDataLayer.GetDataTable(strSqlQuery);
                        break;
                    case enmImportType.Treasury:
                        strSqlQuery = "SELECT tdp.id dumpid,tdp.employeeno dumpmemberid,tdp.membername dumpmembername,tdp.MonthYear dumpMonthYear," + Environment.NewLine +
                                     " tdp.wagesamount dumpwagesamount, MM.id,  MM.nationalid,  MM.memberid,  MM.employeeno,  MM.tscno," + Environment.NewLine +
                                     " MM.membername,ISNULL(MM.wagesamount,0) wagesamount,  MM.wageseffectivedete , CAST(CASE WHEN ISNULL(MM.memberid,'')='' THEN 0 ELSE 1 END AS bit) lValidMember" + Environment.NewLine +
                                     " FROM SNAT.dbo.T_Dump_Treasury tdp " + Environment.NewLine +
                                     " LEFT OUTER JOIN  SNAT.dbo.T_Member MM (nolock) ON mm." + strLinkOn + "=tdp.employeeno AND mm.livingstatus='L'  Order By tdp.id";
                        //mm.memberid=tdp.memberid 
                        dtExcelData = ClsDataLayer.GetDataTable(strSqlQuery);
                        break;
                        break;
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
    }
}
