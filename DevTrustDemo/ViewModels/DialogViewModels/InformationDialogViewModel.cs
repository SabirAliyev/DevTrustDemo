using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using MvvmDialogsLibrary.Interfaces;
using MvvmDialogsLibrary;

namespace DevTrustDemo.ViewModels.DialogViewModels
{
    internal class InformationDialogViewModel : ViewModelBase, IUserDialogViewModel
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
        public InformationDialogViewModel()
        {
            OkCommand = new DelegateCommand<int?>(Ok);
        }
        #endregion

        #region Actions
        public Action<InformationDialogViewModel> OnOk { get; set; }
        public Action<InformationDialogViewModel> OnCloseRequest { get; set; }
        #endregion

        #region Dialog Methods
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
            if (OnOk != null) {
                OnOk(this);
                Close();
            }
            else {
                Close();
            }
        }
        #endregion Commands
    }
}
