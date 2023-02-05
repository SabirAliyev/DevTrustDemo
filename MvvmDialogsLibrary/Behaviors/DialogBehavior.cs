using DevTrustDemo.ViewModels.DialogViewModels.Preseneters;
using MvvmDialogsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace MvvmDialogsLibrary.Behaviors
{
    /// <summary>
    /// Manage the collection of custom dialogs.
    /// </summary>
    public static class DialogBehavior
    {
        private static readonly Dictionary<IDialogViewModel, Window> DialogBoxes = new Dictionary<IDialogViewModel, Window>();
        private static readonly Dictionary<Window, NotifyCollectionChangedEventHandler> ChangeNotificationHandlers = new Dictionary<Window, NotifyCollectionChangedEventHandler>();
        private static readonly Dictionary<ObservableCollection<IDialogViewModel>, List<IDialogViewModel>> DialogBoxViewModels = new Dictionary<ObservableCollection<IDialogViewModel>, List<IDialogViewModel>>();

        public static readonly DependencyProperty ClosingProperty = DependencyProperty.RegisterAttached(
            "Closing",
            typeof(bool),
            typeof(DialogBehavior),
            new PropertyMetadata(false));

        public static readonly DependencyProperty ClosedProperty = DependencyProperty.RegisterAttached(
            "Closed",
            typeof(bool),
            typeof(DialogBehavior),
            new PropertyMetadata(false));

        public static readonly DependencyProperty DialogViewModelsProperty = DependencyProperty.RegisterAttached(
            "DialogViewModels",
            typeof(object),
            typeof(DialogBehavior),
            new PropertyMetadata(null, OnDialogViewModelsChange));

        public static void SetDialogViewModels(DependencyObject source, object value)
        {
            source.SetValue(DialogViewModelsProperty, value);
        }

        public static object GetDialogViewModels(DependencyObject source)
        {
            return source.GetValue(DialogViewModelsProperty);
        }

        private static void OnDialogViewModelsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window parent)) {
                return;
            }

            // when the parent closes we don't need to track it anymore
            parent.Closed += (s, a) => ChangeNotificationHandlers.Remove(parent);

            // otherwise create a handler for it that responds to changes to the supplied collection
            if (!ChangeNotificationHandlers.ContainsKey(parent)) {
                ChangeNotificationHandlers[parent] = (sender, args) => {
                    if (sender is ObservableCollection<IDialogViewModel> collection) {
                        if (args.Action == NotifyCollectionChangedAction.Add ||
                            args.Action == NotifyCollectionChangedAction.Remove ||
                            args.Action == NotifyCollectionChangedAction.Replace) {
                            if (args.NewItems != null) {
                                foreach (IDialogViewModel viewModel in args.NewItems) {
                                    if (!DialogBoxViewModels.ContainsKey(collection)) {
                                        DialogBoxViewModels[collection] = new List<IDialogViewModel>();
                                    }

                                    DialogBoxViewModels[collection].Add(viewModel);
                                    AddDialog(viewModel, collection, d as Window);
                                }
                            }

                            if (args.OldItems != null) {
                                foreach (IDialogViewModel viewModel in args.OldItems) {
                                    try {
                                        RemoveDialog(viewModel);
                                        if (DialogBoxViewModels.Count != 0) {
                                            _ = DialogBoxViewModels[collection].Remove(viewModel);
                                        }

                                        if (DialogBoxViewModels[collection].Count == 0) {
                                            _ = DialogBoxViewModels.Remove(collection);
                                        }
                                    }
                                    catch (Exception) {
                                        //This try/catch was created because of not normal exit from Login Window after pressing Cancel button.
                                    }
                                }
                            }
                        }
                        else if (args.Action == NotifyCollectionChangedAction.Reset) {
                            // a reset event is typically generated in response to clearing the collection.
                            // unfortunately the framework doesn't provide us with the list of items being
                            // removed which is why we have to keep a mirror in DialogBoxViewModels
                            if (DialogBoxViewModels.ContainsKey(collection)) {
                                List<IDialogViewModel> viewModels = DialogBoxViewModels[collection];
                                foreach (IDialogViewModel viewModel in DialogBoxViewModels[collection]) {
                                    RemoveDialog(viewModel);
                                }

                                _ = DialogBoxViewModels.Remove(collection);
                            }
                        }
                    }
                };
            }

            // when the collection is first bound to this property we should create any initial
            // dialogs the user may have added in the main view model's constructor
            if (e.NewValue is ObservableCollection<IDialogViewModel> newCollection) {
                newCollection.CollectionChanged += ChangeNotificationHandlers[parent];
                foreach (IDialogViewModel viewModel in newCollection.ToList()) {
                    AddDialog(viewModel, newCollection, d as Window);
                }
            }

            // when we remove the binding we need to shut down any dialogs that have been left open
            if (e.OldValue is ObservableCollection<IDialogViewModel> oldCollection) {
                oldCollection.CollectionChanged -= ChangeNotificationHandlers[parent];
                foreach (IDialogViewModel viewModel in oldCollection.ToList()) {
                    RemoveDialog(viewModel);
                }
            }
        }

        private static void AddDialog(IDialogViewModel viewModel, ObservableCollection<IDialogViewModel> collection, Window owner)
        {
            // find the global resource that has been keyed to this view model type
            object resource = new object();
            resource = Application.Current.TryFindResource(viewModel.GetType());
            if (resource == null) {
                return;
            }

            if (IsAssignableToGenericType(resource.GetType(), typeof(IDialogPresenter<>))) {
                _ = resource.GetType().GetMethod("Show").Invoke(resource, new object[] { viewModel });
                _ = collection.Remove(viewModel);
            }

            // is this resource a dialog box window?
            else if (resource is Window) {
                if (!(viewModel is IUserDialogViewModel userViewModel)) {
                    return;
                }

                Window dialog = resource as Window;
                dialog.DataContext = userViewModel;
                DialogBoxes[userViewModel] = dialog;

                userViewModel.DialogClosing += (sender, args) => collection.Remove(sender as IUserDialogViewModel);

                dialog.Closing += (sender, args) => {
                    if (!(bool)dialog.GetValue(ClosingProperty)) {
                        dialog.SetValue(ClosingProperty, true);
                        userViewModel.RequestClose();
                        if (!(bool)dialog.GetValue(ClosedProperty)) {
                            args.Cancel = true;
                            dialog.SetValue(ClosingProperty, false);
                        }
                    }
                };

                dialog.Closed += (sender, args) => {
                    Debug.Assert(DialogBoxes.ContainsKey(userViewModel));
                    _ = DialogBoxes.Remove(userViewModel);
                    return;
                };

                dialog.Owner = owner;

                if (userViewModel.IsModal) {
                    _ = (dialog?.ShowDialog());
                }
                else {
                    dialog?.Show();
                }
            }
        }

        private static void RemoveDialog(IDialogViewModel viewModel)
        {
            if (DialogBoxes.ContainsKey(viewModel)) {
                Window dialog = DialogBoxes[viewModel];
                if (!(bool)dialog.GetValue(ClosingProperty)) {
                    dialog.SetValue(ClosingProperty, true);
                    DialogBoxes[viewModel].Close();
                }

                dialog.SetValue(ClosedProperty, true);
            }
        }

        private static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            Type[] interfaceTypes = givenType.GetInterfaces();

            foreach (Type it in interfaceTypes) {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType) {
                    return true;
                }
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType) {
                return true;
            }

            Type baseType = givenType.BaseType;
            if (baseType == null) {
                return false;
            }

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}
