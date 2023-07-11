using fCommon.Utility;
using fanikiwaGL.Entities;
using fPeerLending.Business;
using fPeerLending.Entities; 
using System;
using System.Collections.Generic; 
using System.ComponentModel;
using System.Data; 
using System.Drawing; 
using System.Linq;
using System.Text; 
using System.Windows.Forms;

namespace ffwebAdminUI
{
    public partial class COAForm : Form
    {
        #region "Private Fields"
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent tc;
        AccountsComponent ac;
        List<COA> coas;
        List<int> ids = new List<int>();
        List<int> PL2incids = new List<int>();
        List<int> PL2expids = new List<int>();
        List<int> BS2assids = new List<int>();
        List<int> BS2liabids = new List<int>();
        List<int> BS2capbids = new List<int>();
        string user;
        bool busy = false;
        #endregion "Private Fields"

        #region "Constructor"
        public COAForm(string _user)
        {
            InitializeComponent();

            tc = new TransactionsComponent();
            ac = new AccountsComponent();
        
            user = _user;
        }
        #endregion "Constructor"

        #region "Private Methods"
        public void BuildTree()
        {
            try
            {
                treeViewChartofAccounts.Nodes.Clear(); // Clear any existing items
                treeViewChartofAccounts.BeginUpdate(); // prevent overhead and flicker
                // load all the lowest tree nodes
                TreeBuilder tb = new TreeBuilder();
                List<Item> coa = (from s in tc.GetAllCOAs()
                                  select new Item
                                  {
                                      ItemID = s.Id,
                                      ParentID = s.Parent,
                                      Text = s.Description,
                                      //Payload = null
                                  }).ToList();

                tb.PopulateTreeViewEnumerable(treeViewChartofAccounts, coa);
                treeViewChartofAccounts.EndUpdate(); // re-enable the tree
                treeViewChartofAccounts.Refresh(); // refresh the treeview display
                groupBox9.Text = coa.Count.ToString();
                treeViewChartofAccounts.ExpandAll(); // expand all nodes
                if (treeViewChartofAccounts.Nodes.Count > 0)
                {
                    // Select the root node
                    treeViewChartofAccounts.SelectedNode = treeViewChartofAccounts.Nodes[0];
                }
                int count = treeViewChartofAccounts.GetNodeCount(true);
                groupBox9.Text =  "Charts  " + count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void ComputeBookBalanceTotal(List<Account> _Accounts)
        {
            try
            {

                if (_Accounts != null)
                {
                    decimal _totalCredits = _Accounts
                       .Where(t => t.BookBalance > 0)
                       .Select(t => t.BookBalance).Sum();
                    txtTotalCredits.Text = _totalCredits.ToString("C2");

                    decimal _totalDebits = _Accounts
                      .Where(t => t.BookBalance < 0)
                      .Select(t => t.BookBalance).Sum();
                    txtTotalDebits.Text = _totalDebits.ToString("C2");

                    decimal _totalBalance = _totalCredits + _totalDebits;
                    txtBalance.Text = _totalBalance.ToString("C2");
                }

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void COAForm_Load(object sender, EventArgs e)
        {
            try
            {
                BuildTree();

                //var coasQuery = tc.GetAllCOAs().Include("Accounts");
                //coas = coasQuery.ToList();
                //bindingSourceAccounts.DataSource = ac.GetAllAccounts();

                dataGridViewAccounts.AutoGenerateColumns = false;
                this.dataGridViewAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewAccounts.DataSource = bindingSourceAccounts;

            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        private void CreateIdList(int ParentId, List<int> Ids)
        {
            try
            {
                List<int> ChildIds = (from i in tc.GetAllCOAs()
                                      where i.Parent == ParentId
                                      select i.Id).ToList();
                Ids.Add(ParentId);
                foreach (int child in ChildIds)
                {
                    CreateIdList(child, Ids);
                }
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        private void CreateIdList(TreeNode node)
        {
            try
            {
                int itemId = int.Parse(node.Name);
                ids.Add(itemId);
                foreach (TreeNode child in node.Nodes)
                {
                    CreateIdList(child);
                }
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        private void treeViewChartofAccounts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // Get the node that was Selected
                TreeNode selectedNode = e.Node;
                if (selectedNode != null)
                {
                    ids.Clear();
                    int itemId = int.Parse(selectedNode.Name);
                    CreateIdList(itemId, ids);
                    var accQuery = ac.GetAllAccounts().Where(crtu => ids.Contains(crtu.COAId));
                    bindingSourceAccounts.DataSource = accQuery;
                    groupBox8.Text = "Accounts  " + bindingSourceAccounts.Count.ToString();
                    List<Account> _Accounts = accQuery.ToList();
                    ComputeBookBalanceTotal(_Accounts);
                }
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        private void addChartOfAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeViewChartofAccounts.SelectedNode;
                int itemId = int.Parse(selectedNode.Name);
                var _coaquery = (from coa in tc.GetAllCOAs()
                                where coa.Id == itemId
                                select coa).FirstOrDefault();
                COA _Coa = _coaquery;
               
                AddCOAForm f = new AddCOAForm(_Coa) { Owner = this };
                f.ShowDialog();
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        public void RefreshGrid()
        {
            try
            {
                //bindingSourceCOA.DataSource = null;
                //var coasQuery = tc.GetAllCOAs().Include("Accounts");
                //coas = coasQuery.ToList();
                //bindingSourceCOA.DataSource = coas;
                //groupBox9.Text = bindingSourceCOA.Count.ToString();

                BuildTree();
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrintPL_Click(object sender, EventArgs e)
        {
            try
            {
                //Reports.Viewer.PDFViewer _viewer = new Reports.Viewer.PDFViewer(user);
                //_viewer.WindowState = FormWindowState.Normal;
                //_viewer.StartPosition = FormStartPosition.CenterScreen;
                //_viewer.Show();
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        private void btnPrintBS_Click(object sender, EventArgs e)
        {
            try
            {
                //Reports.Viewer.PDFViewer _viewer = new Reports.Viewer.PDFViewer(user, connection);
                //_viewer.WindowState = FormWindowState.Normal;
                //_viewer.StartPosition = FormStartPosition.CenterScreen;
                //_viewer.Show();
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        private void editChartOfAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeViewChartofAccounts.SelectedNode;
                int itemId = int.Parse(selectedNode.Name);
                var _COAquery = (from coa in tc.GetAllCOAs()
                                 where coa.Id == itemId
                                 select coa).FirstOrDefault();
                COA _COA = _COAquery;
                EditCOAForm f = new EditCOAForm(itemId, _COA) { Owner = this };
                f.Text = selectedNode.Text.ToString().Trim().ToUpper();
                f.ShowDialog();
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeViewChartofAccounts.SelectedNode;
                int itemId = int.Parse(selectedNode.Name);
                var _COAquery = (from coa in tc.GetAllCOAs()
                                 where coa.Id == itemId
                                 select coa).FirstOrDefault();
                COA _COA = _COAquery;
                EditCOAForm f = new EditCOAForm(itemId, _COA) { Owner = this };
                f.Text = selectedNode.Text.ToString().Trim().ToUpper();
                f.DisableControls();
                f.ShowDialog();
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }

        private void deleteChartOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeViewChartofAccounts.SelectedNode;

                StringBuilder strb = new StringBuilder();
                printTree(selectedNode, strb);
                string message = "Are you sure you want to delete Chart of Account with the following children \n";
                message += strb.ToString();

                if (DialogResult.Yes == MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    //confirm first no FK constraint
                    int itemId = int.Parse(selectedNode.Name);
                    var _COAquery = (from coa in tc.GetAllCOAs()
                                     where coa.Id == itemId
                                     select coa).FirstOrDefault();
                    COA _COA = _COAquery;
                    var _Accountsquery = from acnts in ac.GetAllAccounts()
                                         where acnts.COAId == _COA.Id
                                         select acnts;
                    List<Account> _Accounts = _Accountsquery.ToList();
                    if (_Accounts.Count > 0)
                    {
                        MessageBox.Show("There is an Account Associated with this Chart of Account.\n Remove the Account Reference First!", "Confirm Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    //delete now
                    deleteTree(selectedNode);
                    MessageBox.Show("Deleted all", "Fanikiwa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void addAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeViewChartofAccounts.SelectedNode;
                int itemId = int.Parse(selectedNode.Name);
                AddAccountForm f = new AddAccountForm(itemId) { Owner = this };
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);

            }
        }
        private void printTree(TreeNode node, StringBuilder str)
        {
            try
            {
                printNode(node, 0, str);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void printNode(TreeNode node, int level, StringBuilder str)
        {
            try
            {
                //printTitle(node.title);
                string indent = "".PadLeft(level * 2, '-');
                str.AppendLine(indent + "  " + node.Name + " / " + node.Text + "\n");
                foreach (TreeNode child in node.Nodes)
                {
                    printNode(child, level + 1, str); //<-- recursive
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void deleteTree(TreeNode node)
        {
            try
            {
                deleteNode(node, 0);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void deleteNode(TreeNode node, int level)
        {
            try
            {
                //delete COA Item 
                int itemid = int.Parse(node.Name);
                var coa = (from c in tc.GetAllCOAs()
                           where c.Id == itemid
                           select c).SingleOrDefault();
                COA _COA = coa;
                var _Accountsquery = from acnts in ac.GetAllAccounts()
                                     where acnts.COAId == _COA.Id
                                     select acnts;
                List<Account> _Accounts = _Accountsquery.ToList();
                if (_Accounts.Count > 0)
                {
                    MessageBox.Show("There is an Account Associated with this Chart of Account.\n" + node.Text + "\n Remove the Account Reference First!", "Confirm Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                tc.DeleteCOA(coa.Id); 

                //delete others
                foreach (TreeNode child in node.Nodes)
                {
                    deleteNode(child, level + 1); //<-- recursive
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private decimal ComputeCoaAccTotals(int coaid)
        {
            try
            {
                decimal res = 0;
                List<int> lstIds = new List<int>();
                CreateIdList(coaid, lstIds);
                var accQuery = ac.GetAllAccounts().Where(crtu => lstIds.Contains(crtu.COAId));
                if (accQuery.Count() > 0)
                    res = accQuery.Sum(i => i.BookBalance);

                return res;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return 0;
            }
        }
        private void CreatePL2List(int ParentId, List<int> PL2ids)
        {
            try
            {
                List<int> ChildIds = (from i in tc.GetAllCOAs()
                                      where i.Parent == ParentId
                                      select i.Id).ToList();
                PL2ids.Add(ParentId);
                foreach (int child in ChildIds)
                {
                    CreatePL2List(child, PL2ids);
                }
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);
            }
        }
        //private void PopulatePL_Level2()
        //{
        //    try
        //    {
        //        db = null;

        //        db = new SBSchoolDBEntities(connection);

        //        tc.ExecuteStoreCommand("TRUNCATE TABLE PL_Level2");

        //        //INCOME
        //        var inc = (from pl1 in tc.PL_Level1
        //                   where pl1.PLGroup == "INC"
        //                   select pl1).FirstOrDefault();
        //        var incomequery = (from s in tc.GetAllCOAs()
        //                           where s.Description == "INCOME"
        //                           select s).FirstOrDefault();
        //        COA income = incomequery;
        //        int incId = income.Id;
        //        List<int> incidstoexclude = new List<int> { incId };
        //        CreatePL2List(incId, PL2incids);
        //        List<int> pl2incintlst = PL2incids.Except(incidstoexclude).ToList();
        //        var PL2incquery = tc.GetAllCOAs().Where(crtu => pl2incintlst.Contains(crtu.Id)).ToList();

        //        foreach (var pll2 in PL2incquery.ToList())
        //        {
        //            PL_Level2 pl2 = new PL_Level2();
        //            pl2.ParentId = inc.Id;
        //            pl2.Description = pll2.Description;
        //            pl2.AccField = "BookBalance";
        //            pl2.PLCriteria = "COAId=" + pll2.Id;
        //            pl2.Yr1Amount = ComputeCoaAccTotals(pll2.Id);
        //            pl2.Yr2Amount = ComputeCoaAccTotals(pll2.Id);
        //            pl2.ROrder = pll2.COALevel;

        //            if (!tc.PL_Level2.Any(i => i.Description == pl2.Description && i.ParentId == pl2.ParentId))
        //            {
        //                tc.PL_Level2.AddObject(pl2);
        //                tc.SaveChanges();
        //            }
        //        }

        //        //EXPENSES
        //        var exp = (from pl1 in tc.PL_Level1
        //                   where pl1.PLGroup == "EXP"
        //                   select pl1).FirstOrDefault();
        //        var expensequery = (from s in tc.GetAllCOAs()
        //                            where s.Description == "EXPENSES"
        //                            select s).FirstOrDefault();
        //        COA expense = expensequery;
        //        int expenseId = expense.Id;
        //        List<int> expidstoexclude = new List<int> { expenseId };
        //        CreatePL2List(expenseId, PL2expids);
        //        List<int> pl2expintlst = PL2expids.Except(expidstoexclude).ToList();
        //        var PL2expquery = tc.GetAllCOAs().Where(crtu => pl2expintlst.Contains(crtu.Id));

        //        foreach (var pll2 in PL2expquery.ToList())
        //        {
        //            PL_Level2 pl2 = new PL_Level2();
        //            pl2.ParentId = exp.Id;
        //            pl2.Description = pll2.Description;
        //            pl2.AccField = "BookBalance";
        //            pl2.PLCriteria = "COAId=" + pll2.Id;
        //            pl2.Yr1Amount = ComputeCoaAccTotals(pll2.Id);
        //            pl2.Yr2Amount = ComputeCoaAccTotals(pll2.Id);
        //            pl2.ROrder = pll2.COALevel;

        //            if (!tc.PL_Level2.Any(i => i.Description == pl2.Description && i.ParentId == pl2.ParentId))
        //            {
        //                tc.PL_Level2.AddObject(pl2);
        //                tc.SaveChanges();
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.ShowError(ex);
        //    }
        //} 
        //private void btnGeneratePL_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Populate PL_Level2
        //        PopulatePL_Level2();

        //        DataTable PLdt = new DataTable("PL");
        //        DataColumn l1des = new DataColumn("L1Description");
        //        DataColumn l2des = new DataColumn("L2Description");
        //        DataColumn yr1 = new DataColumn("Yr1");
        //        DataColumn yr2 = new DataColumn("Yr2");
        //        PLdt.Columns.AddRange(new DataColumn[] { l1des, l2des, yr1, yr2 });

        //        //generate PL
        //        GeneratePL(PLdt);

        //        dataGridViewProfitLoss.AutoGenerateColumns = true;
        //        dataGridViewProfitLoss.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //        bindingSourcePL.DataSource = PLdt;
        //        dataGridViewProfitLoss.DataSource = bindingSourcePL;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.ShowError(ex);
        //    }
        //}
        //private void GeneratePL(DataTable dt)
        //{
        //    try
        //    {               

        //        //INCOME
        //        DataRow dr = dt.NewRow();
        //        dr["L1Description"] = "INCOME";
        //        dt.Rows.Add(dr);

        //        var inc = (from pl2 in tc.PL_Level2
        //                   join pl1 in tc.PL_Level1 on pl2.ParentId equals pl1.Id
        //                   where pl1.PLGroup == "INC"
        //                   select pl2).ToList();
        //        decimal incTotal = 0;
        //        decimal incTotalYr2 = 0;
        //        foreach (PL_Level2 item in inc)
        //        {
        //            decimal total = ExecuteCriteriaPL(item.PLCriteria);
        //            incTotal += total;
        //            DataRow idr = dt.NewRow();
        //            idr["L1Description"] = "";
        //            idr["L2Description"] = item.Description;
        //            decimal yr1Amt = 0;
        //            decimal.TryParse(idr["Yr1"].ToString(), out yr1Amt);
        //            incTotalYr2 += yr1Amt;
        //            idr["Yr2"] = yr1Amt;
        //            idr["Yr1"] = total;

        //            dt.Rows.Add(idr);
        //        }
        //        DataRow incTotaldr = dt.NewRow();
        //        incTotaldr["L1Description"] = "TOTAL INCOME";
        //        incTotaldr["L2Description"] = "";
        //        incTotaldr["Yr2"] = incTotalYr2;
        //        incTotaldr["Yr1"] = incTotal;

        //        dt.Rows.Add(incTotaldr);

        //        //EXPENSES 
        //        DataRow edr = dt.NewRow();
        //        edr["L1Description"] = "EXPENSES";
        //        dt.Rows.Add(edr);

        //        var exp = (from pl2 in tc.PL_Level2
        //                   join pl1 in tc.PL_Level1 on pl2.ParentId equals pl1.Id
        //                   where pl1.PLGroup == "EXP"
        //                   select pl2).ToList();
        //        decimal expTotal = 0;
        //        decimal expTotalYr2 = 0;
        //        foreach (PL_Level2 item in exp)
        //        {
        //            decimal total = ExecuteCriteriaPL(item.PLCriteria);
        //            expTotal += total;
        //            DataRow idr = dt.NewRow();
        //            idr["L1Description"] = "";
        //            idr["L2Description"] = item.Description;
        //            decimal yr1Amt = 0;
        //            decimal.TryParse(idr["Yr1"].ToString(), out yr1Amt);
        //            expTotalYr2 += yr1Amt;
        //            idr["Yr2"] = yr1Amt;
        //            idr["Yr1"] = total;

        //            dt.Rows.Add(idr);
        //        }
        //        DataRow expTotaldr = dt.NewRow();
        //        expTotaldr["L1Description"] = "TOTAL EXPENSES";
        //        expTotaldr["L2Description"] = "";
        //        expTotaldr["Yr2"] = expTotalYr2;
        //        expTotaldr["Yr1"] = expTotal;

        //        dt.Rows.Add(expTotaldr);

        //        //Computation of PL 
        //        decimal netIncome = incTotal - expTotal;
        //        decimal netIncomeYr2 = incTotalYr2 - expTotalYr2;
        //        DataRow netIncomeDr = dt.NewRow();
        //        netIncomeDr["L1Description"] = "NET INCOME";
        //        netIncomeDr["L2Description"] = "";
        //        netIncomeDr["Yr2"] = netIncomeYr2;
        //        netIncomeDr["Yr1"] = netIncome;
        //        dt.Rows.Add(netIncomeDr);

        //        //COMPUTE TAXES


        //        //net income after tax
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.ShowError(ex);
        //    }
        //}
        //private decimal ExecuteCriteriaPL(string Criteria)
        //{
        //    try
        //    {
        //        decimal res = 0;
        //        string[] items = Criteria.Split('=');
        //        switch (items[0])
        //        {
        //            case "COAId":
        //                int parentId = int.Parse(items[1]);
        //                List<int> Ids = new List<int>();
        //                CreateIdList(parentId, Ids);
        //                var accQuery = ac.GetAllAccounts().Where(crtu => Ids.Contains(crtu.COAId));
        //                if (accQuery.Count() > 0)
        //                    res = accQuery.Sum(i => i.BookBalance);
        //                break;
        //        }
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.ShowError(ex);
        //        return 0;
        //    }
        //}
        //private void PopulateBS_Level2()
        //{
        //    try
        //    {
        //        db = null;

        //        db = new SBSchoolDBEntities(connection);

        //        tc.ExecuteStoreCommand("TRUNCATE TABLE BS_Level2");

        //        //ASSETS
        //        var ass = (from bs1 in tc.BS_Level1
        //                   where bs1.BSGroup == "ASS"
        //                   select bs1).FirstOrDefault();
        //        var assetsquery = (from s in tc.GetAllCOAs()
        //                           where s.Description == "ASSET"
        //                           select s).FirstOrDefault();
        //        COA assests = assetsquery;
        //        int assestsId = assests.Id;
        //        List<int> assidstoexclude = new List<int> { assestsId };
        //        CreatePL2List(assestsId, BS2assids);
        //        List<int> bs2assintlst = BS2assids.Except(assidstoexclude).ToList();
        //        var BS2assquery = tc.GetAllCOAs().Where(crtu => bs2assintlst.Contains(crtu.Id)).ToList();

        //        foreach (var bsl2 in BS2assquery.ToList())
        //        {
        //            BS_Level2 bs2 = new BS_Level2();
        //            bs2.ParentId = ass.Id;
        //            bs2.Description = bsl2.Description;
        //            bs2.AccField = "BookBalance";
        //            bs2.BSCriteria = "COAId=" + bsl2.Id;
        //            bs2.Yr1Amount = ComputeCoaAccTotals(bsl2.Id);
        //            bs2.Yr2Amount = ComputeCoaAccTotals(bsl2.Id);
        //            bs2.ROrder = bsl2.COALevel;

        //            if (!tc.BS_Level2.Any(i => i.Description == bs2.Description && i.ParentId == bs2.ParentId))
        //            {
        //                tc.BS_Level2.AddObject(bs2);
        //                tc.SaveChanges();
        //            }
        //        }

        //        //LIABILITIES
        //        var liab = (from bs1 in tc.BS_Level1
        //                    where bs1.BSGroup == "LIAB"
        //                    select bs1).FirstOrDefault();
        //        var liabilitiesquery = (from s in tc.GetAllCOAs()
        //                                where s.Description == "LIABILITIES"
        //                                select s).FirstOrDefault();
        //        COA liabilities = liabilitiesquery;
        //        int liabilitiesId = liabilities.Id;
        //        List<int> liabidstoexclude = new List<int> { liabilitiesId };
        //        CreatePL2List(liabilitiesId, BS2liabids);
        //        List<int> bs2liabintlst = BS2liabids.Except(liabidstoexclude).ToList();
        //        var BS2liabquery = tc.GetAllCOAs().Where(crtu => bs2liabintlst.Contains(crtu.Id)).ToList();

        //        foreach (var bsl2 in BS2liabquery.ToList())
        //        {
        //            BS_Level2 bs2 = new BS_Level2();
        //            bs2.ParentId = liab.Id;
        //            bs2.Description = bsl2.Description;
        //            bs2.AccField = "BookBalance";
        //            bs2.BSCriteria = "COAId=" + bsl2.Id;
        //            bs2.Yr1Amount = ComputeCoaAccTotals(bsl2.Id);
        //            bs2.Yr2Amount = ComputeCoaAccTotals(bsl2.Id);
        //            bs2.ROrder = bsl2.COALevel;

        //            if (!tc.BS_Level2.Any(i => i.Description == bs2.Description && i.ParentId == bs2.ParentId))
        //            {
        //                tc.BS_Level2.AddObject(bs2);
        //                tc.SaveChanges();
        //            }
        //        }

        //        //CAPITAL
        //        var cap = (from bs1 in tc.BS_Level1
        //                   where bs1.BSGroup == "CAP"
        //                   select bs1).FirstOrDefault();
        //        var capitalquery = (from s in tc.GetAllCOAs()
        //                            where s.Description == "CAPITAL"
        //                            select s).FirstOrDefault();
        //        COA capital = capitalquery;
        //        int capitalId = capital.Id;
        //        List<int> capidstoexclude = new List<int> { capitalId };
        //        CreatePL2List(capitalId, BS2capbids);
        //        List<int> bs2capintlst = BS2capbids.Except(capidstoexclude).ToList();
        //        var BS2capquery = tc.GetAllCOAs().Where(crtu => bs2capintlst.Contains(crtu.Id)).ToList();

        //        foreach (var bsl2 in BS2capquery.ToList())
        //        {
        //            BS_Level2 bs2 = new BS_Level2();
        //            bs2.ParentId = cap.Id;
        //            bs2.Description = bsl2.Description;
        //            bs2.AccField = "BookBalance";
        //            bs2.BSCriteria = "COAId=" + bsl2.Id;
        //            bs2.Yr1Amount = ComputeCoaAccTotals(bsl2.Id);
        //            bs2.Yr2Amount = ComputeCoaAccTotals(bsl2.Id);
        //            bs2.ROrder = bsl2.COALevel;

        //            if (!tc.BS_Level2.Any(i => i.Description == bs2.Description && i.ParentId == bs2.ParentId))
        //            {
        //                tc.BS_Level2.AddObject(bs2);
        //                tc.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.ShowError(ex);
        //    }
        //} 
        //private void btnGenerateBS_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Populate BS_Level2
        //        PopulateBS_Level2();

        //        DataTable BSdt = new DataTable("BS");
        //        DataColumn l1des = new DataColumn("L1Description");
        //        DataColumn l2des = new DataColumn("L2Description");
        //        DataColumn yr1 = new DataColumn("Yr1");
        //        DataColumn yr2 = new DataColumn("Yr2");
        //        BSdt.Columns.AddRange(new DataColumn[] { l1des, l2des, yr1, yr2 });

        //        //generate BS
        //        GenerateBS(BSdt);

        //        dataGridViewBalanceSheet.AutoGenerateColumns = true;
        //        dataGridViewBalanceSheet.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //        bindingSourceBS.DataSource = BSdt;
        //        dataGridViewBalanceSheet.DataSource = bindingSourceBS;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.ShowError(ex);
        //    }
        //}
        //private void GenerateBS(DataTable bsdt)
        //{
        //    try
        //    {
        //        //ASSETS
        //        DataRow dr = bsdt.NewRow();
        //        dr["L1Description"] = "ASSETS";
        //        bsdt.Rows.Add(dr);

        //        var ass = (from bs2 in tc.BS_Level2
        //                   join bs1 in tc.BS_Level1 on bs2.ParentId equals bs1.Id
        //                   where bs1.BSGroup == "ASS"
        //                   select bs2).ToList();
        //        decimal assTotal = 0;
        //        decimal assTotalYr2 = 0;
        //        foreach (BS_Level2 item in ass)
        //        {
        //            decimal total = ExecuteCriteriaBS(item.BSCriteria);
        //            assTotal += total;
        //            DataRow idr = bsdt.NewRow();
        //            idr["L1Description"] = "";
        //            idr["L2Description"] = item.Description;
        //            decimal yr1Amt = 0;
        //            decimal.TryParse(idr["Yr1"].ToString(), out yr1Amt);
        //            assTotalYr2 += yr1Amt;
        //            idr["Yr2"] = yr1Amt;
        //            idr["Yr1"] = total;

        //            bsdt.Rows.Add(idr);
        //        }
        //        DataRow assTotaldr = bsdt.NewRow();
        //        assTotaldr["L1Description"] = "TOTAL ASSETS";
        //        assTotaldr["L2Description"] = "";
        //        assTotaldr["Yr2"] = assTotalYr2;
        //        assTotaldr["Yr1"] = assTotal;

        //        bsdt.Rows.Add(assTotaldr);

        //        //LIABILITIES
        //        DataRow edr = bsdt.NewRow();
        //        edr["L1Description"] = "LIABILITIES";
        //        bsdt.Rows.Add(edr);

        //        var liab = (from bs2 in tc.BS_Level2
        //                    join bs1 in tc.BS_Level1 on bs2.ParentId equals bs1.Id
        //                    where bs1.BSGroup == "LIAB"
        //                    select bs2).ToList();
        //        decimal liabTotal = 0;
        //        decimal liabTotalYr2 = 0;
        //        foreach (BS_Level2 item in liab)
        //        {
        //            decimal total = ExecuteCriteriaBS(item.BSCriteria);
        //            liabTotal += total;
        //            DataRow idr = bsdt.NewRow();
        //            idr["L1Description"] = "";
        //            idr["L2Description"] = item.Description;
        //            decimal yr1Amt = 0;
        //            decimal.TryParse(idr["Yr1"].ToString(), out yr1Amt);
        //            liabTotalYr2 += yr1Amt;
        //            idr["Yr2"] = yr1Amt;
        //            idr["Yr1"] = total;

        //            bsdt.Rows.Add(idr);
        //        }
        //        DataRow liabTotaldr = bsdt.NewRow();
        //        liabTotaldr["L1Description"] = "TOTAL LIABILITIES";
        //        liabTotaldr["L2Description"] = "";
        //        liabTotaldr["Yr2"] = liabTotalYr2;
        //        liabTotaldr["Yr1"] = liabTotal;

        //        bsdt.Rows.Add(liabTotaldr);

        //        //CAPITAL 
        //        DataRow cdr = bsdt.NewRow();
        //        cdr["L1Description"] = "CAPITAL";
        //        bsdt.Rows.Add(cdr);

        //        var cap = (from bs2 in tc.BS_Level2
        //                   join bs1 in tc.BS_Level1 on bs2.ParentId equals bs1.Id
        //                   where bs1.BSGroup == "CAP"
        //                   select bs2).ToList();
        //        decimal capTotal = 0;
        //        decimal capTotalYr2 = 0;
        //        foreach (BS_Level2 item in cap)
        //        {
        //            decimal total = ExecuteCriteriaBS(item.BSCriteria);
        //            capTotal += total;
        //            DataRow idr = bsdt.NewRow();
        //            idr["L1Description"] = "";
        //            idr["L2Description"] = item.Description;
        //            decimal yr1Amt = 0;
        //            decimal.TryParse(idr["Yr1"].ToString(), out yr1Amt);
        //            capTotalYr2 += yr1Amt;
        //            idr["Yr2"] = yr1Amt;
        //            idr["Yr1"] = total;

        //            bsdt.Rows.Add(idr);
        //        }
        //        DataRow capTotaldr = bsdt.NewRow();
        //        capTotaldr["L1Description"] = "TOTAL CAPITAL";
        //        capTotaldr["L2Description"] = "";
        //        capTotaldr["Yr2"] = capTotalYr2;
        //        capTotaldr["Yr1"] = capTotal;

        //        bsdt.Rows.Add(capTotaldr);

        //        //TOTAL LIABILITIES & CAPITAL
        //        decimal totliabcap = capTotal + liabTotal;
        //        decimal totliabcapYr2 = capTotalYr2 + liabTotalYr2;
        //        DataRow totliabcapDr = bsdt.NewRow();
        //        totliabcapDr["L1Description"] = "TOTAL LIABILITIES & CAPITAL";
        //        totliabcapDr["L2Description"] = "";
        //        totliabcapDr["Yr2"] = totliabcapYr2;
        //        totliabcapDr["Yr1"] = totliabcap;
        //        bsdt.Rows.Add(totliabcapDr);

        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.ShowError(ex);
        //    }
        //}
        //private decimal ExecuteCriteriaBS(string Criteria)
        //{
        //    try
        //    {
        //        decimal res = 0;
        //        //COAId=32
        //        string[] items = Criteria.Split('=');
        //        switch (items[0])
        //        {
        //            case "COAId":
        //                int parentId = int.Parse(items[1]);
        //                List<int> Ids = new List<int>();
        //                CreateIdList(parentId, Ids);
        //                var accQuery = ac.GetAllAccounts().Where(crtu => Ids.Contains(crtu.COAId));
        //                if (accQuery.Count() > 0)
        //                    res = accQuery.Sum(i => i.BookBalance);
        //                break;
        //        }
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.ShowError(ex);
        //        return 0;
        //    }
        //} 
        //private void dataGridViewAccounts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dataGridViewAccounts.SelectedRows.Count != 0)
        //    {
        //        try
        //        {
        //            Account _Account = (Account)bindingSourceAccounts.Current;
        //            WinSBSchool.Reports.Views.Screen.EnquiryViewForm f = new WinSBSchool.Reports.Views.Screen.EnquiryViewForm(_Account, user, connection);
        //            f.ShowDialog();
        //        }
        //        catch (Exception ex)
        //        {
        //            Utils.ShowError(ex);
        //        }
        //    }
        //}
        private void treeViewChartofAccounts_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (busy) return;
            busy = true;
            try
            {
                checkNodes(e.Node, e.Node.Checked);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
            finally
            {
                busy = false;
            }
        }
        private void checkNodes(TreeNode node, bool check)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = check;
                checkNodes(child, check);
            }
        }
        private void btnExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                treeViewChartofAccounts.ExpandAll();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnCollapseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                treeViewChartofAccounts.CollapseAll();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        } 
        #endregion "Private Methods"

    }


    public class ProfitandLossDTO
    {
        public int ItemID { get; set; }
        public int ParentID { get; set; }
        public string Text { get; set; }
        public object Payload { get; set; }
    }

    public class BalanceSheetDTO
    {
        public int ItemID { get; set; }
        public int ParentID { get; set; }
        public string Text { get; set; }
        public object Payload { get; set; }
    }


}