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
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace UsingBingMaps.Models
{
    /// <summary>
    /// Represents a catalog for a well known pushpins.
    /// </summary>
    public class PushpinCatalog
    {
        private List<PushpinModel> _items;

        /// <summary>
        /// Gets an enumerator to a well known pushpins collection.
        /// </summary>
        public IEnumerable<PushpinModel> Items
        {
            get { return _items; }
        }        

        /// <summary>
        /// Initializes a new instance of this type.
        /// </summary>
        public PushpinCatalog()
        {
            InitializePuspins();
        }

        /// <summary>
        /// Populate the items collection with instances of type PushpinModel,
        /// one per pushpin icon located in the pushpin icons folder.
        /// </summary>
        private void InitializePuspins()
        {
            string[] pushpinIcons =
            {
                "PushpinBicycle.png",
                "PushpinCar.png",
                "PushpinDrink.png",
                "PushpinFuel.png",
                "PushpinHouse.png",
                "PushpinRestaurant.png",
                "PushpinShop.png"
            };

            var pushpins = from icon in pushpinIcons
                           select new PushpinModel
                           {
                               Icon = new Uri("/Resources/Icons/Pushpins/" + icon, UriKind.Relative),
                               TypeName = Path.GetFileNameWithoutExtension(icon)
                           };

            _items = pushpins.ToList();
        }
    }
}