using SNAT.Comman_Classes;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;
namespace SNAT.Comman_Form
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }


        public string infSearchFormName { get; set; }
        private string _infCodeFieldName = "";
        public string infCodeFieldName { get { return _infCodeFieldName; } set { _infCodeFieldName = value; } }
        public string infDescriptionFieldName { get; set; }

        public string infGridFieldName { get; set; }
        public string infGridFieldCaption { get; set; }
        public string infGridFieldSize { get; set; }

        public string infSqlSelectQuery { get; set; }
        public string infSqlWhereCondtion { get; set; }
        public string infSqlOrderBy { get; set; }

        public string infCodeFieldText { get; set; }
        public string infDescriptionFieldText { get; set; }

        private bool _infMultiSelect = false;
        public bool infMultiSelect { get { return _infMultiSelect; } set { _infMultiSelect = value; } }


        DataTable infSearchDatatable = new DataTable();

        public DataView infSearchReturnDataView { get; set; }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            try
            {
                //pnlSelect.Visible = _infMultiSelect;
                grdSearch.MultiSelect = _infMultiSelect;
                lblSearch.Text = infSearchFormName;
                LoadDataTable();
                BindGrid();

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }
        private void LoadDataTable()
        {
            try
            {

                string SqlQuery = "";
                if (infSqlSelectQuery != null && infSqlSelectQuery.Trim() != "")
                {
                    SqlQuery = infSqlSelectQuery;
                }
                if (infSqlWhereCondtion != null && infSqlWhereCondtion.Trim() != "")
                {
                    SqlQuery = SqlQuery + " Where " + infSqlWhereCondtion.Trim();
                }

                if (infSqlOrderBy != null && infSqlOrderBy.Trim() != "")
                {
                    SqlQuery = SqlQuery + " Order By " + infSqlOrderBy.Trim();
                }



                infSearchDatatable = ClsDataLayer.GetDataTable(SqlQuery);




                System.Data.DataColumn newColumn = new System.Data.DataColumn("lSelect", typeof(System.Boolean));
                newColumn.DefaultValue = false;
                infSearchDatatable.Columns.Add(newColumn);


            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
            finally
            {

            }

        }

        private void BindGrid()
        {
            try
            {
                if (infSearchDatatable != null && infSearchDatatable.DefaultView.Count > 0)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = infSearchDatatable.DefaultView;
                    //
                    grdSearch.AutoGenerateColumns = false;


                    string[] GridColCaption = infGridFieldCaption.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] GridColName = infGridFieldName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] GridColSize = infGridFieldSize.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    GridViewCheckBoxColumn gcColChk = new GridViewCheckBoxColumn();
                    gcColChk.Name = "lSelect".Trim();
                    gcColChk.FieldName = "lSelect".Trim();
                    gcColChk.HeaderText = "[ ]";
                    gcColChk.Width = 30;
                    gcColChk.IsVisible = _infMultiSelect;
                    grdSearch.Columns.Add(gcColChk);


                    for (int i = 0; i <= GridColName.Length - 1; i++)
                    {
                        GridViewTextBoxColumn gcCol = new GridViewTextBoxColumn();
                        GridViewTextBoxColumn dataGridViewCell = new GridViewTextBoxColumn();
                        gcCol.Name = GridColName[i].Trim();
                        gcCol.HeaderText = GridColCaption[i].Trim();
                        gcCol.Width = Convert.ToInt32(GridColSize[i].Trim());
                        gcCol.FieldName = GridColName[i].Trim();
                        //gcCol.CellTemplate = dataGridViewCell;
                        gcCol.ReadOnly = true;
                        if (Convert.ToInt32(GridColSize[i].Trim()) == 0)
                        {
                            gcCol.IsVisible = false;
                        }
                        grdSearch.Columns.Add(gcCol);
                    }
                    grdSearch.DataSource = infSearchDatatable.DefaultView;
                    // var lastColIndex = grdSearch.Columns.Count - 1;
                    // var lastCol = grdSearch.Columns[lastColIndex];
                    //// lastCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }


        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {

            try
            {
                if (infSearchDatatable != null && infSearchDatatable.DefaultView.Count > 0)
                {
                    foreach (DataRowView drvRow in infSearchDatatable.DefaultView)
                    {
                        drvRow.BeginEdit();
                        drvRow["lSelect"] = true;
                        drvRow.EndEdit();
                    }
                    infSearchDatatable.AcceptChanges();
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (infSearchDatatable != null && infSearchDatatable.DefaultView.Count > 0)
                {
                    foreach (DataRowView drvRow in infSearchDatatable.DefaultView)
                    {
                        drvRow.BeginEdit();
                        drvRow["lSelect"] = false;
                        drvRow.EndEdit();
                    }
                    infSearchDatatable.AcceptChanges();
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }
        private void SearchText(bool Searchcol = true, string columnName = "")
        {
            try
            {
                if (infSearchDatatable != null)
                {
                    infSearchDatatable.DefaultView.RowFilter = "";
                    bool UseContains = Searchcol;
                    int colCount = infSearchDatatable.Columns.Count;
                    System.Text.StringBuilder query = new System.Text.StringBuilder();
                    string filterString = "";


                    string likeStatement = (UseContains) ? "Like '%{0}%'" : " Like '{0}%'";

                    if (Searchcol == true && columnName == "")
                    {
                        for (int i = 0; i < colCount; i++)
                        {
                            string colName = infSearchDatatable.Columns[i].ColumnName;
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
                    DataRow[] tmpRows = infSearchDatatable.Select(currFilter);
                    infSearchDatatable.DefaultView.RowFilter = currFilter;
                }
                //MessageBox.Show("");
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            SearchText(true, "");

            //}
            //else
            //{
            //    SearchText(false, cmbColName.SelectedValue.ToString().Trim());
            //}
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (infSearchDatatable != null && infSearchDatatable.DefaultView.Count > 0)
                {
                    if (infMultiSelect == false)
                    {
                        int iRow = 0;
                        iRow = grdSearch.SelectedRows[0].Index;
                        infSearchDatatable.DefaultView[iRow].BeginEdit();
                        infSearchDatatable.DefaultView[iRow]["lSelect"] = 1;
                        infSearchDatatable.DefaultView[iRow].EndEdit();
                        infCodeFieldText = infSearchDatatable.DefaultView[iRow][infCodeFieldName].ToString().Trim();
                        infDescriptionFieldText = infSearchDatatable.DefaultView[iRow][infDescriptionFieldName].ToString().Trim();
                        infSearchDatatable.AcceptChanges();
                        infSearchReturnDataView = infSearchDatatable.DefaultView;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        infSearchReturnDataView = infSearchDatatable.DefaultView;
                        this.DialogResult = DialogResult.OK;
                    }

                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void grdSearch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (infMultiSelect == false)
                {
                    int iRow = 0;
                    iRow = grdSearch.SelectedRows[0].Index;
                    infSearchDatatable.DefaultView[iRow].BeginEdit();
                    infSearchDatatable.DefaultView[iRow]["lSelect"] = 1;
                    infSearchDatatable.DefaultView[iRow].EndEdit();

                    infCodeFieldText = infSearchDatatable.DefaultView[iRow][infCodeFieldName].ToString().Trim();
                    infDescriptionFieldText = infSearchDatatable.DefaultView[iRow][infDescriptionFieldName].ToString().Trim();
                    infSearchReturnDataView = infSearchDatatable.DefaultView;
                    infSearchDatatable.AcceptChanges();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    //infSearchReturnDataView = infSearchDatatable.DefaultView;
                    //this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex);
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void grdSearch_CellClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName.ToString().ToUpper())
                {
                    case "LSELECT":
                        int iRow = 0;
                        iRow =e.RowIndex;
                        infSearchDatatable.DefaultView[iRow].BeginEdit();
                        infSearchDatatable.DefaultView[iRow]["lSelect"] =!Convert.ToBoolean(infSearchDatatable.DefaultView[iRow]["lSelect"]);
                        infSearchDatatable.DefaultView[iRow].EndEdit();
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
