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

namespace RAMSPDToolkit.I2CSMBus.Interop.Linux
{
    internal class I2CMsg : MarshalDynamicArrayBase<I2CMsgStructure, byte>
    {
        #region MarshalDynamicArrayBase

        public override void AssignBufferPointer(IntPtr buffer)
        {
            Structure.Buffer = buffer;
        }

        #endregion
    }

    public struct I2CMsgStructure
    {
        public ushort Address;
        public ushort Flags;
        public ushort Length;
        public IntPtr Buffer;
    }
}