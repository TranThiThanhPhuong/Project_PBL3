using DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data;
using BUS;

namespace GUI
{
    public partial class Form_TempBill : Form
    {
        private List<BillInfo2> tempBills;
        private int tableID;
        private int billID;
        private int discount;
        private int finalPrice;
        private int totalPrice;
        private DateTime ngayvao;
        private DateTime ngayra;

        public Form_TempBill(List<TempBill> tempBills, int tableid, int billID, int discount, int finalPrice, int totalPrice)
        {
            InitializeComponent();
            
            
            this.tableID = tableid;
            this.billID = billID;
            this.discount = discount;
            this.finalPrice = finalPrice;
            this.totalPrice = totalPrice;
            
        }

        private void Form_TempBill_Load(object sender, EventArgs e)
        {
            try
            {
            
                List<BillInfo2> temp = BillInfoBUS.Instance.GetListBillInfoByBillID(billID);
                this.tempBills = temp;
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet2";
                reportDataSource.Value = tempBills;

                DataTable dataTable = new DataTable();
                dataTable = BillBUS.Instance.GetUnCheckBillIDByTableID2(billID);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    this.ngayvao = Convert.ToDateTime(row["CheckIn"]);
                    this.ngayra = row["CheckOut"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CheckOut"]);
                }

                var reportInfo = new List<Bill2>
                {
                    new Bill2
                    {
                        ID = this.billID,
                        TableID = this.tableID,
                        Discount = this.discount,
                        CheckIn = this.ngayvao,
                        CheckOut = this.ngayra,
                        TotalPrice = this.finalPrice
                    }
                };
               
                ReportDataSource reportDataSource1 = new ReportDataSource
                {
                    Name = "DataSet1",
                    Value = reportInfo
                };


                reportViewer1.LocalReport.DataSources.Clear(); 
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.RefreshReport();
            }
            catch   (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
