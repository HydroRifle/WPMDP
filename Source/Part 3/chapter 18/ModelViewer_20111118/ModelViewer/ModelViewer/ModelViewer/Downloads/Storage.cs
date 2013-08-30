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
using System.Windows;
using System.IO.IsolatedStorage;

namespace ModelViewer
{
    public static class Storage
    {
        private const string AssetExt = ".xnb";

        //Clean isolated storage from previously downloaded models
        public static void DeleteLocalModels()
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string[] fileNames = store.GetFileNames();

                foreach (string fileName in fileNames)
                    store.DeleteFile(fileName);
            }
        }

        //Check if model (and all related assets) were downloaded before. 
        //If not, then function could download partial downloaded assets
        public static bool ModelDownloaded(string ModelID, bool DeletePartial)
        {
            bool bRes = true;

            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                ModelMetadata modelMetadata = (Application.Current as App).ModelsStore[ModelID];

                foreach (string fileName in modelMetadata.Assets)
                {
                    if (!store.FileExists(fileName + AssetExt))
                        bRes &= false;
                }

                //If at least one of assets didn't downloaded before, delete the rest assets
                if (!bRes && DeletePartial)
                {
                    foreach (string fileName in modelMetadata.Assets)
                    {
                        if (store.FileExists(fileName + AssetExt))
                            store.DeleteFile(fileName + AssetExt);
                    }
                }
            }

            return bRes;
        }

        internal static void MoveFile(string sourceFileName, string destinationFileName)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    if (store.FileExists(sourceFileName))
                    {
                        //Delete old file if already exists
                        if (store.FileExists(destinationFileName))
                            store.DeleteFile(destinationFileName);

                        store.MoveFile(sourceFileName, destinationFileName);
                    }
                }
                catch (Exception)
                {
                    //TODO: log exception
                }
            }
        }

        internal static void DeleteFile(string fullPath)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    if (store.FileExists(fullPath))
                    {
                        store.DeleteFile(fullPath);
                    }
                }
                catch (Exception)
                {
                    //TODO: log exception
                }
            }
        }
    }
}
