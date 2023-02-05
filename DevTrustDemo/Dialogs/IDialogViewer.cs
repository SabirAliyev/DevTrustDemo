using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTrustDemo.Dialogs
{
    public interface IDialogViewer
    {
        void ShowInformationDialog(string caption, string message);

        void ShowWarningDialog(string caption, string message);

    }
}
