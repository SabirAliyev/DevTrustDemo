using DevExpress.Mvvm;
using MvvmDialogsLibrary;
using MvvmDialogsLibrary.Interfaces;
using System;
using System.Collections.Generic;

namespace DevTrustDemo.ViewModels.DialogViewModels
{
    class WarningDialogViewModel : ViewModelBase, IUserDialogViewModel
    {
        #region IUserDialogViewModel Implementation
        public virtual bool IsModal { get { return true; } }
        public virtual event EventHandler DialogClosing;
        public virtual void RequestClose()
        {
            if (OnCloseRequest != null)
                OnCloseRequest(this);
            else
                Close();
        }
        #endregion

        #region Constructor
        public WarningDialogViewModel()
        {
            OkCommand = new DelegateCommand<int?>(Ok);

        }
        #endregion

        #region Actions
        public Action<WarningDialogViewModel> OnOk { get; set; }
        public Action<WarningDialogViewModel> OnCloseRequest { get; set; }
        #endregion

        #region Methods
        public void Close()
        {
            DialogClosing?.Invoke(this, new EventArgs());
        }

        public void Show(IList<IDialogViewModel> collection)
        {
            collection.Add(this);
        }
        #endregion

        #region Dialog Messages
        private string _message;
        public string Message {
            get { return _message; }
            set
            {
                _message = value;
            }
        }

        private string _caption;
        public string Caption {
            get { return _caption; }
            set
            {
                _caption = value;
            }
        }
        #endregion

        #region Commands
        public DelegateCommand<int?> OkCommand { get; private set; }

        protected virtual void Ok(int? param)
        {
            if (OnOk != null)
                OnOk(this);
            else {
                Close();
            }
        }
        #endregion Commands

    }
}
