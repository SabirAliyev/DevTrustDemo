using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DevTrustDemo.Services;

namespace DevTrustDemo.Strartup
{
    public static class BootsrtapContainer
    {
        public static void OnStartUp()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IOrderToCsvFile>().ImplementedBy<OrderToCsvFile>());



            container.Resolve<IOrderToCsvFile>();
        }
    }
}
