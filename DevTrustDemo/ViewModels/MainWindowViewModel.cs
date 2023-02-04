using DevExpress.Mvvm;
using DevTrustDemo.Dialogs;
using DevTrustDemo.Services;
using DevTrustDemoSerializationLibrary.CsvFile;
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
        private ISaveToFileDialog SaveDialog { get; set; }

        #region Constructor
        public MainWindowViewModel()
        {
            CsvOrderSerialize = new OrderToCsvFile();
            SaveDialog = new SaveToFileDialog();

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
        #endregion

        #region Collections
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

        public ObservableCollection<Order> SelectedOrders { get; } = new ObservableCollection<Order>();
        #endregion

        #region Commands
        public DelegateCommand<int?> ExportToCsvCommand { get; private set; }
        public DelegateCommand<int?> ExportToTxtCommand { get; private set; }
        #endregion

        #region Command Methods
        void ExportToCsv(int? param)
        {
            List<Order> orderList = new List<Order>();

            string filename = SaveDialog.CsvFileSaveDialog();
            // string filename = "C:\\Users\\sa\\Desktop\\test.csv";

            if (filename != null) {
                if (SelectedOrders != null && SelectedOrders.Count != 0) {
                    foreach (var so in SelectedOrders) {
                        orderList.Add(so);
                    }
                }

                CsvOrderSerialize.WriteToCsvFile(orderList, filename);

                // TODO show info dialog
            } 
            else {
                // TODO show error dialog
            }
        }

        void ExportToTxt(int? param)
        {
            List<Order> orderList = new List<Order>();

            string filename = SaveDialog.TxtFileSaveDialog();
            // string filename = "C:\\Users\\sa\\Desktop\\test.txt";

            if (filename != null) {
                if (SelectedOrders != null && SelectedOrders.Count != 0) {
                    foreach (var so in SelectedOrders) {
                        orderList.Add(so);
                    }
                }

                 CsvOrderSerialize.WriteToCsvFile(orderList, filename);

                // TODO show info dialog
            }
            else {
                // TODO show error dialog
            }

        }
        #endregion



    }
}
