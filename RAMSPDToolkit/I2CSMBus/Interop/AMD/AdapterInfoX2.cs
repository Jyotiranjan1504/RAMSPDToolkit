/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 *
 * Copyright (c) 2025 Florian K.
 *
 * Code inspiration, improvements and fixes are from, but not limited to, following projects:
 * LibreHardwareMonitor; Linux Kernel; OpenRGB; WinRing0 (QCute)
 */

using System.Runtime.InteropServices;

namespace RAMSPDToolkit.I2CSMBus.Interop.AMD
{
    /// <summary>
    /// Structure containing information about the graphics adapter with extended caps.
    /// </summary>
    internal struct AdapterInfoX2
    {
        public AdapterInfoX2()
        {
            StrUDID          = new byte[AMDConstants.ADL_MAX_PATH];
            StrAdapterName   = new byte[AMDConstants.ADL_MAX_PATH];
            StrDisplayName   = new byte[AMDConstants.ADL_MAX_PATH];
            StrDriverPath    = new byte[AMDConstants.ADL_MAX_PATH];
            StrDriverPathExt = new byte[AMDConstants.ADL_MAX_PATH];
            StrPNPString     = new byte[AMDConstants.ADL_MAX_PATH];
        }

        /// <summary>
        /// Size of the structure.
        /// </summary>
        public int Size;

        /// <summary>
        /// The ADL index handle. One GPU may be associated with one or two index handles.
        /// </summary>
        public int AdapterIndex;

        /// <summary>
        /// The unique device ID associated with this adapter.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = AMDConstants.ADL_MAX_PATH)]
        public byte[] StrUDID;

        /// <summary>
        /// The BUS number associated with this adapter.
        /// </summary>
        public int BusNumber;

        /// <summary>
        /// The driver number associated with this adapter.
        /// </summary>
        public int DeviceNumber;

        /// <summary>
        /// The function number.
        /// </summary>
        public int FunctionNumber;

        /// <summary>
        /// The vendor ID associated with this adapter.
        /// </summary>
        public int VendorID;

        /// <summary>
        /// Adapter name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = AMDConstants.ADL_MAX_PATH)]
        public byte[] StrAdapterName;

        /// <summary>
        /// Display name. For example, "\\\\Display0"
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = AMDConstants.ADL_MAX_PATH)]
        public byte[] StrDisplayName;

        /// <summary>
        /// Present or not; 1 if present and 0 if not present. If the logical adapter is present, the display name such as \\\\.\\Display1 can be found from OS.
        /// </summary>
        public int Present;

        /// <summary>
        /// Exist or not; 1 is exist and 0 is not present.
        /// </summary>
        public int Exist;

        /// <summary>
        /// Driver registry path.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = AMDConstants.ADL_MAX_PATH)]
        public byte[] StrDriverPath;

        /// <summary>
        /// Driver registry path Ext for.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = AMDConstants.ADL_MAX_PATH)]
        public byte[] StrDriverPathExt;

        /// <summary>
        /// PNP string from Windows.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = AMDConstants.ADL_MAX_PATH)]
        public byte[] StrPNPString;

        /// <summary>
        /// It is generated from EnumDisplayDevices.
        /// </summary>
        public int OSDisplayIndex;

        /// <summary>
        /// The bit mask identifies the adapter info.
        /// </summary>
        public int InfoMask;

        /// <summary>
        /// The bit identifies the adapter info define_adapter_info.
        /// </summary>
        public int InfoValue;
    }
}
