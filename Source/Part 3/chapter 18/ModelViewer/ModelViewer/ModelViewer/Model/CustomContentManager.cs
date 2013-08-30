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
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Content;

namespace ModelViewer
{
    public class CustomContentManager : ContentManager
    {
        private const string IsolateStorageRoot = @"\";
        private const string LocalExt = ".xnb";

        public CustomContentManager() : base (App.Current as IServiceProvider)
        {
        }

        //Override default behavior and get the asset from IsolatedStorage instead of installation location
        protected override Stream OpenStream(string assetName)
        {
            Stream stream = null;
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists(IsolateStorageRoot + assetName))
                    return null;

                stream = store.OpenFile(assetName, FileMode.Open);
            }
            
            return stream;
        }

        //Override the default behavior and tweak asset name:
        //In IsolatedStorage the files has real names with extension
        public override T Load<T>(string assetName)
        {
            if (assetName.IndexOf(LocalExt) == -1)
                assetName += LocalExt;

            T content = base.Load<T>(assetName);

            return content;
        }
    }
}
