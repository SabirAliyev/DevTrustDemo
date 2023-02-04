using DevExpress.Mvvm;
using DevTrustDemo.Services;
using Microsoft.Win32;
using NorthwindData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace DevTrustDemo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        NorthwindEntities northwindDbContext;

        private IOrderToCsvFile CsvOrderSerialize { get; set; }

        public MainWindowViewModel()
        {
            CsvOrderSerialize = new OrderToCsvFile();

            if (IsInDesignMode) {
                Orders = new ObservableCollection<Order>();
                Shippers = new ObservableCollection<Shipper>();
                Employees = new ObservableCollection<Employee>();
            }
            else {
                northwindDbContext = new NorthwindEntities();

                northwindDbContext.Orders.Load();
                Orders = northwindDbContext.Orders.Local;

                northwindDbContext.Shippers.Load();
                Shippers = northwindDbContext.Shippers.Local;

                northwindDbContext.Employees.Load();
                Employees = northwindDbContext.Employees.Local;
            }

            ExportToCsvCommand = new DelegateCommand<int?>(ExportToCsv);
            ExportToTxtCommand = new DelegateCommand<int?>(ExportToTxt);
        }

        public ObservableCollection<Order> Orders {
            get => GetValue<ObservableCollection<Order>>();
            private set => SetValue(value);
        }

        public ObservableCollection<Shipper> Shippers {
            get => GetValue<ObservableCollection<Shipper>>();
            private set => SetValue(value);
        }

        public ObservableCollection<Employee> Employees {
            get => GetValue<ObservableCollection<Employee>>();
            private set => SetValue(value);
        }

        public DelegateCommand<int?> ExportToCsvCommand { get; private set; }
        public DelegateCommand<int?> ExportToTxtCommand { get; private set; }


        public ObservableCollection<Order> SelectedOrders { get; } = new ObservableCollection<Order>();

        public static string FileSaveDialog()
        {
            string filename = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "csv",
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            try {
                if (saveFileDialog.ShowDialog() == true) {
                    if (!saveFileDialog.FileName.EndsWith(".csv")) {
                        filename = saveFileDialog.FileName + ".csv";
                    }
                    else {
                        filename = saveFileDialog.FileName;
                    }
                }

            }
            catch (System.Exception ex) {

                var ErrMessage = ex.Message;
                throw;
            }

            return filename;
        }


        void ExportToCsv(int? param)
        {
            List<Order> orderList = new List<Order>();

            bool ok = false;
            string filename = FileSaveDialog();
            // string filename = "C:\\Users\\sa\\Desktop\\test.csv";

            if (filename != null) {
                if (SelectedOrders != null && SelectedOrders.Count != 0) {
                    foreach (var so in SelectedOrders) {
                        orderList.Add(so);
                    }
                }

                ok = CsvOrderSerialize.WriteToCsvFile(orderList, filename);
            }

            if (ok) {
                // TODO show dialog
            } else {
                // TODO show dialog
            }
        }

        void ExportToTxt(int? param)
        {

        }


    }
}
