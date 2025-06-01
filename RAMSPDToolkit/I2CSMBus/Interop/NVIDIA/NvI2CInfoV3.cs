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

using RAMSPDToolkit.I2CSMBus.Interop.Shared;

namespace RAMSPDToolkit.I2CSMBus.Interop.NVIDIA
{
    internal class NvI2CInfoV3 : MarshalDynamicArrayBase<NvI2CInfoV3Struct, byte>
    {
        #region MarshalDynamicArrayBase

        public override void AssignBufferPointer(IntPtr buffer)
        {
            Structure.Data = buffer;
        }

        #endregion
    }

    internal struct NvI2CInfoV3Struct
    {
        public uint Version;
        public uint DisplayMask;
        public byte IsDDCPort;
        public byte I2CDevAddress;
        public byte? I2CRegAddress;
        public uint RegAddressSize;
        public IntPtr Data;
        public uint Size;
        public uint I2CSpeed;
        public NvI2CSpeed I2CSpeedKHz;
        public byte PortID;
        public uint IsPortIDSet;
    }
}
