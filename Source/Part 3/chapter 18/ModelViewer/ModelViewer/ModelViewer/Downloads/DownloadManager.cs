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
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Phone.BackgroundTransfer;

namespace ModelViewer
{
    public static class DownloadManager
    {
        private static readonly Collection<Download> downloads = new Collection<Download>();
        private static readonly Queue<Download> pendingDownloads = new Queue<Download>();

        public static Download Create(string asset)
        {
            return new Download(asset);
        }

        public static void Add(Download download)
        {
            lock(downloads)
            {
                downloads.Add(download);
            }
        }

        public static void Remove(Download download)
        {
            lock(downloads)
            {
                downloads.Remove(download);
            }
        }

        public static IEnumerable<Download> GetDownloads()
        {
            lock(downloads)
            {
                return downloads.ToArray();
            }
        }

        public static void StartDownload(Download download)
        {
            if(BackgroundTransferService.Requests.Count() < 5)
            {
                download.DownloadFinished += download_DownloadFinished;
                download.DownloadAborted += download_DownloadAborted;

                download.Start();
            }
            else
            {
                pendingDownloads.Enqueue(download);
            }
        }

        public static bool AbortDownload(Download download)
        {
            return download.Abort();
        }

        private static void download_DownloadFinished(object sender, EventArgs e)
        {
            OnDownloadCompleted((Download)sender);
        }

        private static void download_DownloadAborted(object sender, EventArgs e)
        {
            OnDownloadCompleted((Download)sender);
        }

        private static void OnDownloadCompleted(Download download)
        {
            download.DownloadFinished -= download_DownloadFinished;
            download.DownloadAborted -= download_DownloadAborted;

            ProcessPendingDownloads();
        }

        private static void ProcessPendingDownloads()
        {
            if(pendingDownloads.Count > 0)
            {
                Download download = pendingDownloads.Dequeue();
                StartDownload(download);
            }
        }

        public static void RemoveAllRequests()
        {
            foreach(var request in BackgroundTransferService.Requests)
            {
                BackgroundTransferService.Remove(request);
            }
        }
    }
}
