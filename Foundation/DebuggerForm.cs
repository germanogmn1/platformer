using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Foundation
{
    public partial class DebuggerForm : Form
    {
        private Dictionary<string, DataGridViewCell> existingCells = new Dictionary<string,DataGridViewCell>();

        public DebuggerForm()
        {
            InitializeComponent();
        }

        public void Debug(string key, string value)
        {
            if (existingCells.ContainsKey(key))
            {
                existingCells[key].Value = value;
            }
            else
            {
                dataGrid.Rows.Insert(dataGrid.Rows.Count, 1);
                var row = dataGrid.Rows.Cast<DataGridViewRow>().Last();
                
                row.Cells["Key"].Value = key;
                row.Cells["Value"].Value = value;
                
                existingCells[key] = row.Cells["Value"];
            }
        }
    }
}
