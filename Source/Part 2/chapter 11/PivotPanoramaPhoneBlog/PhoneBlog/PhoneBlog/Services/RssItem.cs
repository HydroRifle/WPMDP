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

using System.Net;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace PhoneBlog.Services
{
    /// <summary>
    /// Model for RSS item
    /// </summary>
    [DataContract]
    public class RssItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RssItem"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="publishedDate">The published date.</param>
        /// <param name="url">The URL.</param>
        public RssItem(string title, string summary, string publishedDate, string url)
        {
            Title = title;
            Summary = summary;
            PublishedDate = publishedDate;
            Url = url;

            // Get plain text from html
            PlainSummary = HttpUtility.HtmlDecode(Regex.Replace(summary, "<[^>]+?>", ""));
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        [DataMember]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the published date.
        /// </summary>
        /// <value>The published date.</value>
        [DataMember]
        public string PublishedDate { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [DataMember]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the plain summary.
        /// </summary>
        /// <value>The plain summary.</value>
        [DataMember]
        public string PlainSummary { get; set; }
    }
}