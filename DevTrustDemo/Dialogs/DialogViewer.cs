using DevTrustDemo.ViewModels;
using DevTrustDemo.ViewModels.DialogViewModels;
using System;

namespace DevTrustDemo.Dialogs
{
    public class DialogViewer : IDialogViewer
    {
        private readonly MainWindowViewModel view;

        public DialogViewer(MainWindowViewModel view)
        {
            this.view = view;
        }

        public void ShowInformationDialog(string caption, string message)
        {
            Action<InformationDialogViewModel> confirmClose = new Action<InformationDialogViewModel>((sender) => { sender.Close(); });
            new InformationDialogViewModel()
            {
                Caption = caption,
                Message = message,
                OnOk = confirmClose,
                OnCloseRequest = confirmClose
            }.Show(this.view.Dialogs);
        }

        public void ShowWarningDialog(string caption, string message)
        {
            Action<WarningDialogViewModel> confirmClose = new Action<WarningDialogViewModel>((sender) => { sender.Close(); });
            new WarningDialogViewModel()
            {
                Caption = caption,
                Message = message,
                OnOk = confirmClose,
                OnCloseRequest = confirmClose
            }.Show(this.view.Dialogs);
        }

    }
}
