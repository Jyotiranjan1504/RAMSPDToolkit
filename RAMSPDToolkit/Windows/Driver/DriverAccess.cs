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

using WinRing0Driver.Driver;

namespace RAMSPDToolkit.Windows.Driver
{
    /// <summary>
    /// Forward to driver. Wrapper class to abstract calls into driver.
    /// </summary>
    public static class DriverAccess
    {
        #region Properties

        /// <inheritdoc cref="OLS.IsOpen"/>
        public static bool IsOpen
        {
            get { return DriverManager.Ols.IsOpen; }
        }

        #endregion

        #region Public

        public static void WriteIoPortByte(ushort port, byte value)
        {
            DriverManager.Ols.WriteIoPortByte(port, value);
        }

        public static byte ReadIoPortByte(ushort port)
        {
            return DriverManager.Ols.ReadIoPortByte(port);
        }

        public static uint FindPciDeviceById(ushort vendorId, ushort deviceId, byte index)
        {
            return DriverManager.Ols.FindPciDeviceById(vendorId, deviceId, index);
        }

        public static ushort ReadPciConfigWord(uint pciAddress, byte regAddress)
        {
            return DriverManager.Ols.ReadPciConfigWord(pciAddress, regAddress);
        }

        #endregion
    }
}
