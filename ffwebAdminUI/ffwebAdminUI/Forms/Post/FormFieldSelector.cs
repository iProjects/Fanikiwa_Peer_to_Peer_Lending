using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using fPeerLending.Entities;
using fPeerLending.Business;
using fanikiwaGL.Entities;

namespace ffwebAdminUI
{
    public partial class FormFieldSelector : Form
    {
        PrintFields printerFields;
        //delegate
        public delegate void ReceiptItemSelectHandler(object sender, ReceiptItemEventArgs e);
        //event
        public event ReceiptItemSelectHandler OnReceiptItemListSelected;

        public FormFieldSelector()
        {
            InitializeComponent();
            printerFields = new PrintFields();
        }

        private void FormFieldSelector_Load(object sender, EventArgs e)
        {
            lstSelectFields.DataSource = printerFields.GetFieldList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lstSelectFields.SelectedIndex != -1)
            {
                PrintField selectedPrintField = (PrintField)lstSelectFields.SelectedItem;
                OnReceiptItemListSelected(this, new ReceiptItemEventArgs(selectedPrintField));
                this.Close();
            }
        }

        private void lstSelectFields_DoubleClick(object sender, EventArgs e)
        {
            if (lstSelectFields.SelectedIndex != -1)
            {
                PrintField selectedPrintField = (PrintField)lstSelectFields.SelectedItem;
                OnReceiptItemListSelected(this, new ReceiptItemEventArgs(selectedPrintField));
                this.Close();
            }
        }
    }

    public class PrintFields
    {
        Dictionary<int, PrintField> fields = new Dictionary<int, PrintField>();
        
        public PrintFields()
        {
            fields.Add(0, new PrintField(0,"Date Now", new DateTime())); //Date now
            fields.Add(1, new PrintField(1, "School Name", null)); //School StudentName
            fields.Add(2, new PrintField(2, "School Address", null)); //School Address
            fields.Add(3, new PrintField(3, "Receipt Number", null)); //Receipt Number
            fields.Add(4, new PrintField(4, "Debit Account", null)); //Debit Account
            fields.Add(5, new PrintField(5, "Credit Account", null)); //Credit Account            
            fields.Add(6, new PrintField(6, "Debit Narrative", null)); //Debit Narrative
            fields.Add(7, new PrintField(7, "Credit Narrative", null)); //Credit Narrative
            fields.Add(8, new PrintField(8, "Amount", null)); //Amount
            fields.Add(9, new PrintField(9, "Name", null)); //StudentId StudentName
            fields.Add(10, new PrintField(10, "Admino", null)); //Admino
            fields.Add(11, new PrintField(11, "Class", null)); //Class
            
        }

        //Build all the fields

       public  Dictionary<int, PrintField> GetFields()
        {
            return fields;
        }

       public List<PrintField> GetFieldList()
       {
           var ls = from f in fields
                    select f.Value;
           return ls.ToList();
       }
    }

    public class PrintField
    {
        public int Id;
        public string Name;
        public object Value;  

        public PrintField(int id, string name, object val)
        {
            Id = id;
            Name = name;
            if (val != null) Value = val;
        }

        public PrintField(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

     

      public class ReceiptItemEventArgs : System.EventArgs
      {

          // add local member variables to hold text 
          private PrintField _printField;

          // class constructor
          public ReceiptItemEventArgs(PrintField printField)
          {
              this._printField = printField;
          }

          // Properties - Viewable by each listener  
          public PrintField _PrintField
          {
              get
              {
                  return _printField;
              }
          }
      }
}
