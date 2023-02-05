using DevExpress.Mvvm;
using DevTrustDemo.Dialogs;
using MvvmDialogsLibrary.Interfaces;
using DevTrustDemoSerializationLibrary.CsvFile;
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

        public ObservableCollection<IDialogViewModel> Dialogs { get; } = new ObservableCollection<IDialogViewModel>();
        private readonly IDialogViewer dialogViewer;

        #region Constructor
        public MainWindowViewModel()
        {
            dialogViewer = new DialogViewer(this);

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
            bool ok = true;

            string filename = SaveDialog.CsvFileSaveDialog();
            //string filename = "C:\\Users\\sa\\Desktop\\test.csv";

            if (filename != null) {
                if (SelectedOrders != null && SelectedOrders.Count != 0) {
                    foreach (var so in SelectedOrders) {
                        orderList.Add(so);
                    }
                }

                ok = CsvOrderSerialize.WriteToCsvFile(orderList, filename);

                if (filename != null) {
                    if (ok) {
                        dialogViewer.ShowInformationDialog("Information", "Saving selected records into CSV file was successfully done.");
                    }
                    else {
                        dialogViewer.ShowWarningDialog("Warning", "Saving selected records into CSV file was caused an unexpected error");
                    }
                }
            }
        }

        void ExportToTxt(int? param)
        {
            List<Order> orderList = new List<Order>();
            bool ok = true;

            string filename = SaveDialog.TxtFileSaveDialog();
            // string filename = "C:\\Users\\sa\\Desktop\\test.txt";

            if (filename != null) {
                if (SelectedOrders != null && SelectedOrders.Count != 0) {
                    foreach (var so in SelectedOrders) {
                        orderList.Add(so);
                    }
                }

                ok = CsvOrderSerialize.WriteToCsvFile(orderList, filename);                
            }

            if (filename != null) {
                if (ok) {
                    dialogViewer.ShowInformationDialog("Information", "Saving selected records into TXT file was successfully done.");
                }
                else {
                    dialogViewer.ShowWarningDialog("Warning", "Saving selected records into TXT file was caused an unexpected error");
                }
            }
        }
        #endregion



    }
}
