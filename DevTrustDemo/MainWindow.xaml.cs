using DevExpress.Xpf.Core;
using DevTrustDemo.Startup;
using DevTrustDemo.ViewModels;
using System.ComponentModel;

namespace DevTrustDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            BootsrtapContainer.OnStartUp();
        }
    }
}
