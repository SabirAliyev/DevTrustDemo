using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTrustDemo.ViewModels.Interfacees
{
    public interface IUserDialogViewModel
    {
        bool IsModal { get; }
        void RequestClose();
        event EventHandler DialogClosing;
    }
}
