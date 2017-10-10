using System;
using System.Data;
using Telerik.WinControls.UI;


namespace SNAT.Comman_Classes
{
    class clsMenu
    {
        static string strSqlQuery = "";
        public static RadTreeView _menuTree { get; set; }
        static DataTable dtMenuRigths = new DataTable();
        static DataSet mdsCreateDefaultView = new DataSet();
        static DataView dvChildMenuRights = new DataView();
        static DataView dvSubChildMenuRights = new DataView();
        public static DataSet GetmenuDataSet()
        {
            try
            {
                DataSet menuDataSet = new DataSet();

                // strSqlQuery = "select mh.id, mh.name headername,mh.position headerpostion ,isnull(mh.image,'') headerimage,isnull(mh.imagelocation,'') headerimagelocation from RNRExpanceMangment.dbo.M_MenuHeader mh";
                strSqlQuery = "select mh.id, mh.name headername,mh.position headerpostion ,isnull(mh.image,'') headerimage,isnull(mh.imagelocation,'') headerimagelocation from SNAT.dbo.M_MenuHeader mh (nolock) order by mh.position";
                DataTable dtHeaderMenu = new DataTable();
                dtHeaderMenu = ClsDataLayer.GetDataTable(strSqlQuery);

                // strSqlQuery = "select md.id childid, md.childname,md.childposition,md.isparent ,isnull(md.image,'') childimage, isnull(md.imagelocation,'') childimagelocation,md.menuname from RNRExpanceMangment.dbo.M_MenuDetails md ";
                strSqlQuery = "select md.id childid, md.childname,md.childposition,md.parentid,md.isparent ,isnull(md.image,'') childimage, isnull(md.imagelocation,'') childimagelocation,md.menuname,ISNULL(md.formName,'') formName from SNAT.dbo.M_MenuDetails md (nolock)  where Isnull(md.parentid,'')='' and ISNULL(md.isparent,0)=1 order by md.menuname,md.childposition";
                var dtChildMenu = new DataTable();
                dtChildMenu = ClsDataLayer.GetDataTable(strSqlQuery);

                strSqlQuery = "select mcd.id childid, mcd.childname,mcd.childposition,mcd.parentid,mcd.isparent ,isnull(mcd.image,'') childimage, isnull(mcd.imagelocation,'') childimagelocation,mcd.menuname,ISNULL(mcd.formName,'') formName from SNAT.dbo.M_MenuDetails mcd (nolock) where Isnull(mcd.parentid,'')<>'' and ISNULL(mcd.isparent,0)=0 order by mcd.menuname,mcd.parentid,mcd.childposition";
                var dtSubChildMenu = new DataTable();
                dtSubChildMenu = ClsDataLayer.GetDataTable(strSqlQuery);

                strSqlQuery = "SELECT  mmr.id, mmr.userid, mmr.menuid, mmr.childmenuid,status FROM  SNAT.dbo.M_MenuRights AS mmr WHERE mmr.userid='" + ClsSettings.userid + "'";
                dtMenuRigths = new DataTable();
                dtMenuRigths = ClsDataLayer.GetDataTable(strSqlQuery);

                dtHeaderMenu.TableName = "MenuHeader";
                menuDataSet.Tables.Add(dtHeaderMenu);

                dtChildMenu.TableName = "MenuChild";
                menuDataSet.Tables.Add(dtChildMenu);

                dtSubChildMenu.TableName = "MenuSubChild";
                menuDataSet.Tables.Add(dtSubChildMenu);


                 dvChildMenuRights = new DataView();
                 dvSubChildMenuRights = new DataView();

                dvChildMenuRights = mdsCreateDefaultView.DefaultViewManager.CreateDataView(dtMenuRigths);
                dvSubChildMenuRights = mdsCreateDefaultView.DefaultViewManager.CreateDataView(dtMenuRigths);
                return menuDataSet;

            }
            catch (Exception ex)
            {

                ClsMessage.ProjectExceptionMessage(ex.Message);
                return null;
            }



        }
        public static void FillMenu()
        {
            try
            {
                var dsTreeNode = new DataSet();
                dsTreeNode = GetmenuDataSet();

                if (dsTreeNode.Tables["MenuHeader"] != null && dsTreeNode.Tables["MenuHeader"].DefaultView.Count > 0 &&
                    dsTreeNode.Tables["MenuChild"] != null && dsTreeNode.Tables["MenuChild"].DefaultView.Count > 0)
                {

                  
                    foreach (DataRowView drvHdr in dsTreeNode.Tables["MenuHeader"].DefaultView)
                    {
                        AddHeaderMenu(drvHdr, dsTreeNode.Tables["MenuChild"], dsTreeNode.Tables["MenuSubChild"]);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ProjectExceptionMessage(ex.Message);

            }
        }
        public static void AddHeaderMenu(DataRowView drvMenu, DataTable dtChild, DataTable dtSubChild)
        {
            try
            {
             

                var rdNode = new RadTreeNode();
                rdNode.Text = drvMenu["headername"].ToString();
                rdNode.Tag = drvMenu["headerpostion"].ToString();
                _menuTree.Nodes.Add(rdNode);

                dtChild.DefaultView.RowFilter = "menuname='" + drvMenu["headerpostion"] + "'";

                foreach (DataRowView drvChild in dtChild.DefaultView)
                {
                    bool lEnable = false;

                    dvChildMenuRights.RowFilter = "childmenuid='" + drvChild["childid"].ToString() + "'";
                    if (dvChildMenuRights !=null && dvChildMenuRights.Count > 0) { lEnable = Convert.ToBoolean(dvChildMenuRights[0]["Status"]); }

                    var rdchNode = new RadTreeNode();
                    rdchNode.Text = drvChild["childname"].ToString();
                    rdchNode.Tag = drvChild["childid"].ToString();
                    rdchNode.ImageKey = drvChild["formName"].ToString();
                    if (ClsSettings.usertype.ToUpper() != "ADMIN") { rdchNode.Enabled = lEnable; }
                    rdNode.Nodes.Add(rdchNode);

                    dvChildMenuRights.RowFilter = "";
                    lEnable = false;

                    dtSubChild.DefaultView.RowFilter = "menuname='" + drvChild["menuname"] + "' And parentid='" + drvChild["childposition"] + "'";

                    foreach (DataRowView drvSubChild in dtSubChild.DefaultView)
                    {
                        dvSubChildMenuRights.RowFilter = "childmenuid='" + drvChild["childid"].ToString() + "'";
                        if (dvSubChildMenuRights != null && dvSubChildMenuRights.Count > 0) { lEnable = Convert.ToBoolean(dvSubChildMenuRights[0]["Status"]); }

                        var rdSubchNode = new RadTreeNode();
                        rdSubchNode.Text = drvSubChild["childname"].ToString();
                        rdSubchNode.Tag = drvSubChild["childid"].ToString();
                        rdSubchNode.ImageKey = drvSubChild["formName"].ToString();
                        if (ClsSettings.usertype.ToUpper() != "ADMIN") { rdSubchNode.Enabled = lEnable; }
                        rdchNode.Nodes.Add(rdSubchNode);

                        dvSubChildMenuRights.RowFilter = "";
                        lEnable = false;
                    }
                    dtSubChild.DefaultView.RowFilter = "";
                }

                dtChild.DefaultView.RowFilter = "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
