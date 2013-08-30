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
using Microsoft.Phone.BackgroundTransfer;

namespace ModelViewer
{
    public class Download
    {
        public event EventHandler DownloadFinished;
        public event EventHandler DownloadAborted;
        public event EventHandler<DownloadProgressEventArgs> DownloadProgress;

        private const string BaseURL = "http://flashcardsshowstorage.blob.core.windows.net/windowsphonetk/";
        private const string RemoteExt = ".zip";
        private const string LocalExt = ".xnb";
        private const string Root = @"\";
        private const string TransfersFiles = "transfers"; //Default download location

        private readonly Uri requestUri;
        private readonly Uri downloadUri;
        private readonly string downloadPath;
        private readonly string targetPath;

        private string requestId;
        private bool isAborted;

        public Download(string assetName)
        {
            requestUri = new Uri(BaseURL + assetName + RemoteExt, UriKind.Absolute);
            downloadUri = new Uri(TransfersFiles + @"\" + assetName + LocalExt, UriKind.Relative);

            downloadPath = Path.Combine(TransfersFiles, Path.GetFileName(assetName + LocalExt));
            targetPath = Root + assetName + LocalExt;
        }

        public void Start()
        {
            //Create a new background transfer request
            BackgroundTransferRequest request = new BackgroundTransferRequest(requestUri, downloadUri);
            request.TransferPreferences = TransferPreferences.AllowCellularAndBattery;
            requestId = request.RequestId;

            //Subscribe to the events
            request.TransferStatusChanged += request_TransferStatusChanged;
            request.TransferProgressChanged += request_TransferProgressChanged;

            //Add new request to the queue
            BackgroundTransferService.Add(request);
        }

        public bool Abort()
        {
            BackgroundTransferRequest request = BackgroundTransferService.Find(requestId);

            if(request != null)
            {
                isAborted = true;
                BackgroundTransferService.Remove(request);
            }

            return request != null;
        }

        private void request_TransferStatusChanged(object sender, BackgroundTransferEventArgs e)
        {
            BackgroundTransferRequest request = e.Request;

            if(request.TransferStatus == TransferStatus.Completed)
            {
                request.TransferStatusChanged -= request_TransferStatusChanged;
                request.TransferProgressChanged -= request_TransferProgressChanged;

                if(isAborted)
                    OnDownloadAborted();
                else
                    OnDownloadFinished(request);
            }
        }

        private void request_TransferProgressChanged(object sender, BackgroundTransferEventArgs e)
        {
            BackgroundTransferRequest request = e.Request;
            //While the transfer is still active
            if(request.TransferStatus == TransferStatus.Transferring)
            {
                //Notify about progress change
                if(DownloadProgress != null)
                    DownloadProgress(this, new DownloadProgressEventArgs(request.BytesReceived, request.TotalBytesToReceive));
            }
        }

        private void OnDownloadFinished(BackgroundTransferRequest request)
        {
            //Move downloaded file to its final location
            Storage.MoveFile(downloadPath, targetPath);

            //Notify listeners (UI) about download complete
            if(DownloadFinished != null)
                DownloadFinished(this, EventArgs.Empty);

            //Remove the request from the queue
            BackgroundTransferService.Remove(request);
        }

        private void OnDownloadAborted()
        {
            //Delete the temporary download file
            Storage.DeleteFile(downloadPath);

            //Notify about the aborted download
            if(DownloadAborted != null)
                DownloadAborted(this, EventArgs.Empty);
        }
    }
}
