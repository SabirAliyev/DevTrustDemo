using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DevTrustDemo.Dialogs;
using DevTrustDemoSerializationLibrary.CsvFile;
using DevTrustDemoSerializationLibrary.TxtFile;

namespace DevTrustDemo.Startup
{
    public static class BootsrtapContainer
    {
        public static void OnStartUp()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IOrderToCsvFile>().ImplementedBy<OrderToCsvFile>());
            container.Register(Component.For<IOrderToTxtFile>().ImplementedBy<OrderToTxtFile>());
            container.Register(Component.For<ISaveToFileDialog>().ImplementedBy<SaveToFileDialog>());
            container.Register(Component.For<IDialogViewer>().ImplementedBy<DialogViewer>());

            container.Resolve<IOrderToCsvFile>();
        }
    }
}
