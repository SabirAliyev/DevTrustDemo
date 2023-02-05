using MvvmDialogsLibrary.Interfaces;

namespace DevTrustDemo.ViewModels.DialogViewModels.Preseneters
{
    internal interface IDialogPresenter<T> where T : IDialogViewModel
    {
        void Show(T viewModel);
    }
}
