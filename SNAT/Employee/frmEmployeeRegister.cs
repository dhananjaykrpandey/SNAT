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
using System.IO;

namespace SNAT.Employee
{
    public partial class frmEmployeeRegister : Form
    {
        BindingSource bsEmployee = new BindingSource();
        string strSqlQuery = "";
        string imageFinalLocation = "";
        DataTable dtEmployee = new DataTable();
        public frmEmployeeRegister()
        {
            InitializeComponent();
            BindControl();
        }
        private void FillDataTable()
        {
            try
            {

                 strSqlQuery = " SELECT ed.id, ed.employeeno,ed.nationalid, ed.name,ed.sex, ed.dob, ed.deptcode,dpt.name AS deptname, ed.desigcode,td.Name AS designame," +
                       " ed.contactno1, ed.contactno2, ed.email, ed.physicaladdress, ed.dateofjoining, ed.wrokstatus, ed.leaveingdate,imagelocation" +
                          " FROM SNAT.dbo.T_EmployeeDetails ed (nolock)" +
                          " INNER JOIN  SNAT.dbo.T_Designation td(nolock) ON td.code=ed.desigcode" +
                          " LEFT OUTER JOIN  SNAT.dbo.T_Department dpt (nolock) ON dpt.code = td.DepartCode" +
                          " ORDER BY ed.employeeno, ed.name";
                dtEmployee = ClsDataLayer.GetDataTable(strSqlQuery);
                //  dtEmployee = ClsDataLayer.GetDataTable(strSqlQuery);

                dtEmployee.Columns.Add("ImageLoc", typeof(byte[]));

                foreach (DataRowView drvRow in dtEmployee.DefaultView)
                {
                    drvRow.BeginEdit();
                    if (drvRow["imagelocation"] != null && string.IsNullOrEmpty(drvRow["imagelocation"].ToString()) == false)
                    {
                        drvRow["ImageLoc"] = GetByteArray(drvRow["imagelocation"].ToString());

                    }
                    else
                    {
                        if (File.Exists(Application.StartupPath + @"\img_not_available120X120.png"))
                        {
                            drvRow["ImageLoc"] = GetByteArray(Application.StartupPath + @"\img_not_available120X120.png");
                        }

                    }

                    drvRow.EndEdit();
                }
                dtEmployee.AcceptChanges();
                bsEmployee.DataSource = dtEmployee.DefaultView;


            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }
        private byte[] GetByteArray(String strFileName)
        {
            GC.Collect();
            byte[] imgbyte = null;
            if (File.Exists(strFileName))
            {
                string newfilelocation = Path.GetTempPath();
                newfilelocation = newfilelocation +  Path.GetFileName(strFileName);
                File.Copy(strFileName, newfilelocation, true);

                using (var fs = new FileStream(newfilelocation, FileMode.Open))
                {
                    // initialise the binary reader from file streamobject
                    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                    // define the byte array of filelength

                    imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader

                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                    // add the image in bytearray

                    br.Close();
                    // close the binary reader

                    fs.Close();
                    // close the file stream

                }

                File.Delete(newfilelocation);
                
            }
            return imgbyte;
        }

        private void BindControl()
        {
            try
            {
                FillDataTable();



                this.dtpDOB.NullableValue = null;
                this.dtpDOB.SetToNullValue();
                this.dtpDOB.NullText = "No Date";

                this.dtpJoiningDate.NullableValue = null;
                this.dtpJoiningDate.SetToNullValue();
                this.dtpJoiningDate.NullText = "No Date";

                this.dtpLeavingDate.NullableValue = null;
                this.dtpLeavingDate.SetToNullValue();
                this.dtpLeavingDate.NullText = "No Date";

                txtEmployeeNo.DataBindings.Add("Text", bsEmployee, "employeeno", false, DataSourceUpdateMode.OnValidation);
                txtNationalId.DataBindings.Add("Text", bsEmployee, "nationalid", false, DataSourceUpdateMode.OnValidation);
                txtEmployeeName.DataBindings.Add("Text", bsEmployee, "name", false, DataSourceUpdateMode.OnValidation);

                txtSex.DataBindings.Add("Text", bsEmployee, "sex", false, DataSourceUpdateMode.OnPropertyChanged);

                dtpDOB.DataBindings.Add("Text", bsEmployee, "dob", false, DataSourceUpdateMode.OnPropertyChanged);

                //  txtDesgCode.DataBindings.Add("Text", bsEmployee, "desigcode", false, DataSourceUpdateMode.OnValidation);
                txtDesgName.DataBindings.Add("Text", bsEmployee, "designame", false, DataSourceUpdateMode.OnPropertyChanged);
                txtDeptCode.DataBindings.Add("Text", bsEmployee, "deptcode", false, DataSourceUpdateMode.OnPropertyChanged);
                txtDeptDesc.DataBindings.Add("Text", bsEmployee, "deptname", false, DataSourceUpdateMode.OnPropertyChanged);

                txtContactNo1.DataBindings.Add("Text", bsEmployee, "contactno1", false, DataSourceUpdateMode.OnValidation);
                txtContactNo2.DataBindings.Add("Text", bsEmployee, "contactno2", false, DataSourceUpdateMode.OnValidation);

                txtEmail.DataBindings.Add("Text", bsEmployee, "email", false, DataSourceUpdateMode.OnValidation);
                txtResidentalAddress.DataBindings.Add("Text", bsEmployee, "physicaladdress", false, DataSourceUpdateMode.OnValidation);


                dtpJoiningDate.DataBindings.Add("Text", bsEmployee, "dateofjoining", false, DataSourceUpdateMode.OnPropertyChanged);
                txtStatus.DataBindings.Add("Text", bsEmployee, "wrokstatus", false, DataSourceUpdateMode.OnPropertyChanged);
                // dtpLeavingDate.DataBindings.Add("Text", bsEmployee, "leaveingdate", false, DataSourceUpdateMode.OnPropertyChanged);
                //txtImageLocation.DataBindings.Add("Text", bsEmployee, "imagelocation", false, DataSourceUpdateMode.OnPropertyChanged);

                this.dtpLeavingDate.DataBindings.Add(new Binding("Value", this.bsEmployee, "leaveingdate", true, DataSourceUpdateMode.OnValidation, System.DateTime.Now.Date.AddSeconds(1), ""));

                // chkStatus.DataBindings.Add("Checked", bsDepartmetn, "status", false);
                grdList.DataSource = bsEmployee;

                //foreach (var dvRow in dtEmployee.DefaultView)
                //{


                //}



            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void txtStatus_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStatus.Text.Trim()) == false)
            {
                if (txtStatus.Text.ToUpper().Trim() == "W")
                {
                    rbWorking.Checked = true;
                }
                if (txtStatus.Text.ToUpper().Trim() == "R")
                {
                    rbResgine.Checked = true;
                }
            }
        }
        private void txtSex_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSex.Text.Trim()) == false)
            {
                if (txtSex.Text.ToUpper().Trim() == "M")
                {
                    rbMale.Checked = true;
                }
                if (txtSex.Text.ToUpper().Trim() == "F")
                {
                    rbFemale.Checked = true;
                }
            }
        }
        private void frmEmployeeRegister_Load(object sender, EventArgs e)
        {
            splitContainerDetails.Panel2Collapsed = true;
            chkShowImageColumn.Checked = true;
            chkEmployeeDetails.Checked = false;
        }

        private void grdList_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (grdList.Columns[e.ColumnIndex].Name == "dcImage")
            {
                // Your code would go here - below is just the code I used to test 
                string imageloc = dtEmployee.DefaultView[e.RowIndex]["imagelocation"].ToString();
                if (imageloc != "")
                {
                    // Bitmap image = new Bitmap(imageloc);
                    //grdList.Rows[e.RowIndex].Cells["dcImage"].Value = image;

                }

            }
        }

        private void chkEmployeeDetails_CheckedChanged(object sender, EventArgs e)
        {
            splitContainerDetails.Panel2Collapsed = !chkEmployeeDetails.Checked;
        }

        private void chkShowImageColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (grdList.ColumnCount>0 )
            {
                if (grdList.Columns.Contains("dcImage") ==true)
                {
                    grdList.Columns["dcImage"].IsVisible = chkShowImageColumn.Checked;
                    grdList.Columns["dcImage"].Width = 150;
                }

            }
        }

        private void txtSearchKeyWord_TextChanged(object sender, EventArgs e)
        {
            SearchText(true, "");
        }
        private void SearchText(bool Searchcol = true, string columnName = "")
        {
            try
            {
                if (dtEmployee != null)
                {
                    dtEmployee.DefaultView.RowFilter = "";
                    bool UseContains = Searchcol;
                    int colCount = dtEmployee.Columns.Count;
                    System.Text.StringBuilder query = new System.Text.StringBuilder();
                    string filterString = "";


                    string likeStatement = (UseContains) ? "Like '%{0}%'" : " Like '{0}%'";

                    if (Searchcol == true && columnName == "")
                    {
                        for (int i = 0; i < colCount; i++)
                        {
                            string colName = dtEmployee.Columns[i].ColumnName;
                            query.Append(string.Concat("Convert(", colName, ", 'System.String')", likeStatement));


                            if (i != colCount - 1)
                                query.Append(" OR ");
                        }
                    }
                    else
                    {
                        query.Append(string.Concat("Convert(", columnName, ", 'System.String')", likeStatement));
                    }



                    filterString = query.ToString();
                    string currFilter = string.Format(filterString, txtSearchKeyWord.Text.Trim());
                    DataRow[] tmpRows = dtEmployee.Select(currFilter);
                    dtEmployee.DefaultView.RowFilter = currFilter;
                }
                //MessageBox.Show("");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchText(true, "");
        }
    }
}
