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

namespace RAMSPDToolkit.I2CSMBus.Interop.AMD
{
    internal class ADLI2C : MarshalDynamicArrayBase<ADLI2CStructure, byte>
    {
        #region MarshalDynamicArrayBase

        public override void AssignBufferPointer(IntPtr buffer)
        {
            Structure.PcData = buffer;
        }

        #endregion
    }

    /// <summary>
    /// Structure containing information about I2C.
    /// </summary>
    internal struct ADLI2CStructure
    {
        /// <summary>
        /// Size of the structure.
        /// </summary>
        public int Size;

        /// <summary>
        /// Numerical value representing hardware I2C.
        /// </summary>
        public int Line;

        /// <summary>
        /// The 7-bit I2C slave device address, shifted one bit to the left.
        /// </summary>
        public int Address;

        /// <summary>
        /// The offset of the data from the address.
        /// </summary>
        public int Offset;

        /// <summary>
        /// Read from or write to slave device. ADL_DL_I2C_ACTIONREAD or ADL_DL_I2C_ACTIONWRITE or ADL_DL_I2C_ACTIONREAD_REPEATEDSTART.
        /// </summary>
        public int Action;

        /// <summary>
        /// I2C clock speed in KHz.
        /// </summary>
        public int Speed;

        /// <summary>
        /// A numerical value representing the number of bytes to be sent or received on the I2C bus.
        /// </summary>
        public int DataSize;

        /// <summary>
        /// Address of the characters which are to be sent or received on the I2C bus.
        /// </summary>
        public IntPtr PcData;
    }
}
