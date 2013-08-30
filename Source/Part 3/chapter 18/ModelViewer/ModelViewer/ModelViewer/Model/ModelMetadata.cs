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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ModelViewer
{
    public class ModelMetadata : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void ModelDownloadedEventHandler(object sender, ModelDownloadedEventArgs e);
        public event ModelDownloadedEventHandler ModelDownloaded;

        List<Download> activeDownloads = new List<Download>();

        public void StartTransfer()
        {
            if (!IsInProgress)
            {
                long totalSize = 0;
                activeDownloads.Clear();

                Dictionary<string, long> sizes = new Dictionary<string, long>();
                Dictionary<string, long> progress = new Dictionary<string, long>();

                for (int i = 0; i < Assets.Length; i++)
                {
                    string asset = Assets[i];
                    sizes[asset] = 0;
                    Download download = DownloadManager.Create(asset);
                    activeDownloads.Add(download);
                    DownloadManager.Add(download);

                    download.DownloadFinished += (s, e) => RemoveDownload(download, false);
                    download.DownloadAborted += (s, e) => RemoveDownload(download, true);
                    download.DownloadProgress += (s, e) =>
                    {
                        if (sizes[asset] == 0)
                        {
                            sizes[asset] = e.Total;
                            totalSize = sizes.Values.Sum();
                        }

                        progress[asset] = e.Progress;

                        DownloadProgress = progress.Values.Sum() * 100f / totalSize;
                    };

                    DownloadManager.StartDownload(download);
                }

                IsInProgress = true;
            }
        }

        public void AbortTransfer()
        {
            var downloadsToAbort = activeDownloads.ToArray();
            foreach (var download in downloadsToAbort)
            {
                if (!DownloadManager.AbortDownload(download))
                {
                    RemoveDownload(download, true);
                }
            }
        }

        private void RemoveDownload(Download download, bool isAborted)
        {
            DownloadManager.Remove(download);
            activeDownloads.Remove(download);

            if (activeDownloads.Count == 0)
            {
                IsInProgress = false;
                DownloadProgress = 0;

                if(!isAborted)
                {
                    if(ModelDownloaded != null)
                        ModelDownloaded(this, new ModelDownloadedEventArgs());
                }
            }
        }

        //General metadata
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Assets { get; set; }
        public bool IsLocal { get; set; }
        public bool IsContent { get; set; }

        //3D world display helpers
        public Matrix ViewMatrix { get; set; }
        public Matrix World { get; set; }
        public float DefaultYRotation { get; set; }
        public float DefaultXRotation { get; set; }

        public float FieldOfViewDivisor { get; set; }
        public float AspectRatio { get; set; }
        public float NearPlaneDistance { get; set; }
        public float FarPlaneDistance { get; set; }

        //Download helpers
        private bool isInProgress;
        public bool IsInProgress 
        {
            get { return isInProgress; }
            set
            {
                isInProgress = value;
                FirePropertyChanged("IsInProgress");
            }
        }

        private float downloadProgress;
        public float DownloadProgress
        {
            get { return downloadProgress; }
            set
            {
                downloadProgress = value;
                FirePropertyChanged("DownloadProgress");
            }
        }

        private void FirePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
