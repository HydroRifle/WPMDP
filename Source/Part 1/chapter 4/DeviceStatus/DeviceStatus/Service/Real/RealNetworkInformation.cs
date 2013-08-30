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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Net.NetworkInformation;

namespace DeviceStatus
{
    /// <summary>
    /// Provides network information for an actual device.
    /// </summary>
    public class RealNetworkInformation : NetworkInformation
    {
        /// <summary>
        /// Queries the device for its network information.
        /// </summary>
        public override void RefreshData()
        {
            IsConnected = Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.IsNetworkAvailable;
            ConnectionType = GetInterfaceTypeString(Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType);
            MobileOperator = Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.CellularMobileOperator;

            if (String.IsNullOrEmpty(MobileOperator))
            {
                MobileOperator = "N/A";
            }

            IsCellularDataEnabled = Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.IsCellularDataEnabled;
            IsCellularDataRoamingEnabled = Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.IsCellularDataRoamingEnabled;
            IsWifiEnabled = Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.IsWiFiEnabled;
        }

        #region Private methods


        private string GetInterfaceTypeString(Microsoft.Phone.Net.NetworkInformation.NetworkInterfaceType networkInterfaceType)
        {            
            switch (networkInterfaceType)
            {
                case NetworkInterfaceType.AsymmetricDsl:
                    return "Asymmetric DSL";
                case NetworkInterfaceType.Atm:
                    return "Atm";
                case NetworkInterfaceType.BasicIsdn:
                    return "Basic ISDN";
                case NetworkInterfaceType.Ethernet:
                    return "Ethernet";
                case NetworkInterfaceType.Ethernet3Megabit:
                    return "3 Mbit Ethernet";
                case NetworkInterfaceType.FastEthernetFx:
                    return "Fast Ethernet";
                case NetworkInterfaceType.FastEthernetT:
                    return "Fast Ethernet";
                case NetworkInterfaceType.Fddi:
                    return "FDDI";
                case NetworkInterfaceType.GenericModem:
                    return "Generic Modem";
                case NetworkInterfaceType.GigabitEthernet:
                    return "Gigabit Ethernet";
                case NetworkInterfaceType.HighPerformanceSerialBus:
                    return "High Performance Serial Bus";
                case NetworkInterfaceType.IPOverAtm:
                    return "IP Over Atm";
                case NetworkInterfaceType.Isdn:
                    return "ISDN";
                case NetworkInterfaceType.Loopback:
                    return "Loopback";
                case NetworkInterfaceType.MobileBroadbandCdma:
                    return "CDMA Broadband Connection";
                case NetworkInterfaceType.MobileBroadbandGsm:
                    return "GSM Broadband Connection";
                case NetworkInterfaceType.MultiRateSymmetricDsl:
                    return "Multi-Rate Symmetrical DSL";
                case NetworkInterfaceType.None:
                    return "None";
                case NetworkInterfaceType.Ppp:
                    return "PPP";
                case NetworkInterfaceType.PrimaryIsdn:
                    return "Primary ISDN";
                case NetworkInterfaceType.RateAdaptDsl:
                    return "Rate Adapt DSL";
                case NetworkInterfaceType.Slip:
                    return "Slip";
                case NetworkInterfaceType.SymmetricDsl:
                    return "Symmetric DSL";
                case NetworkInterfaceType.TokenRing:
                    return "Token Ring";
                case NetworkInterfaceType.Tunnel:
                    return "Tunnel";
                case NetworkInterfaceType.Unknown:
                    return "Unknown";
                case NetworkInterfaceType.VeryHighSpeedDsl:
                    return "Very High Speed DSL";
                case NetworkInterfaceType.Wireless80211:
                    return "Wireless";
                default:
                    return "Classified";
            }
        }


        #endregion
    }
}
