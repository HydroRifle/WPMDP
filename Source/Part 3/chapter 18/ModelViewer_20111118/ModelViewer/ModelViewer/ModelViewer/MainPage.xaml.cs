// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace ModelViewer
{
    public partial class MainPage : PhoneApplicationPage
    {
        bool isChangingSelection;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            InitializeModelLists();

            DataContext = this;
        }

        private void InitializeModelLists()
        {
            LocalModels = new ObservableCollection<ModelMetadata>();
            RemoteModels = new ObservableCollection<ModelMetadata>();

            var modelsStore = (Application.Current as App).ModelsStore;
            string[] modelNames = new[] { "polo", "tank", "spaceship", "marble" };
            foreach (var modelName in modelNames)
            {
                ModelMetadata modelMetadata = modelsStore[modelName];
                modelMetadata.ModelDownloaded -= selectedModel_ModelDownloaded;
                modelMetadata.ModelDownloaded += selectedModel_ModelDownloaded;
                if (!modelMetadata.IsLocal && !Storage.ModelDownloaded(modelMetadata.ID, true))
                    RemoteModels.Add(modelMetadata);
                else
                    LocalModels.Add(modelMetadata);
            }
        }

        private void showSelected(object sender, RoutedEventArgs e)
        {
            if (lstLocalModels.SelectedIndex > -1)
            {
                ModelMetadata model = lstLocalModels.SelectedItem as ModelMetadata;
                NavigationService.Navigate(new Uri("/GamePage.xaml?ID=" + model.ID, UriKind.Relative));
            }
            else
                MessageBox.Show("Please select local model and try again");
        }

        private void lstLocalModels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!isChangingSelection)
            {
                try
                {
                    isChangingSelection = true;
                    lstRemoteModels.SelectedIndex = -1;
                }
                finally
                {
                    ToggleButtons();
                    isChangingSelection = false;
                }
            }
        }

        private void lstRemoteModels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!isChangingSelection)
            {
                try
                {
                    isChangingSelection = true;
                    lstLocalModels.SelectedIndex = -1;
                }
                finally
                {
                    ToggleButtons();
                    isChangingSelection = false;
                }
            }
        }

        private void btnDownladSelected_Click(object sender, RoutedEventArgs e)
        {
            if (lstRemoteModels.SelectedIndex > -1)
            {
                ModelMetadata selectedModel = lstRemoteModels.SelectedItem as ModelMetadata;
                if (!selectedModel.IsInProgress)
                {
                    selectedModel.StartTransfer();
                }
            }
            else
                MessageBox.Show("Please select remote model to download and try again");
        }

        void selectedModel_ModelDownloaded(object sender, ModelDownloadedEventArgs e)
        {
            //Transfer to local models list
            ModelMetadata model = sender as ModelMetadata;
            RemoteModels.Remove(model);
            LocalModels.Add(model);
        }

        #region Properties
        public ObservableCollection<ModelMetadata> LocalModels
        {
            get { return (ObservableCollection<ModelMetadata>)GetValue(LocalModelsProperty); }
            set { SetValue(LocalModelsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LocalModels.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocalModelsProperty =
            DependencyProperty.Register("LocalModels", typeof(ObservableCollection<ModelMetadata>), typeof(MainPage), null);


        public ObservableCollection<ModelMetadata> RemoteModels
        {
            get { return (ObservableCollection<ModelMetadata>)GetValue(RemoteModelsProperty); }
            set { SetValue(RemoteModelsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RemoteModels.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemoteModelsProperty =
            DependencyProperty.Register("RemoteModels", typeof(ObservableCollection<ModelMetadata>), typeof(MainPage), null);

        #endregion

        private void appBarDeleteLocalModels_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete all downloaded models?", "Delete Files", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Storage.DeleteLocalModels();

                LocalModels.Clear();

                foreach (var model in RemoteModels)
                {
                    if (model.IsInProgress)
                    {
                        model.AbortTransfer();
                    }
                }
                RemoteModels.Clear();

                InitializeModelLists();
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            lstLocalModels.SelectedIndex = 0;
            ToggleButtons();
        }

        private void ToggleButtons()
        {
            btnDownloadSelected.IsEnabled = lstRemoteModels.SelectedIndex > -1;
            btnShowModel.IsEnabled = lstLocalModels.SelectedIndex > -1;
        }

        private void abortButton_Click(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)e.OriginalSource;
            var modelMetadata = (ModelMetadata)element.DataContext;
            modelMetadata.AbortTransfer();
        }
    }
}