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

namespace RAMSPDToolkit.I2CSMBus.Interop.AMD
{
    /// <summary>
    /// Constant values for AMD.
    /// </summary>
    internal sealed class AMDConstants
    {
        /// <summary>
        /// ADL function completed successfully.
        /// </summary>
        public const int ADL_OK  = 0;

        /// <summary>
        /// Generic Error. Most likely one or more of the Escape calls to the driver failed!
        /// </summary>
        public const int ADL_ERR = -1;

        public const int ADL_DL_I2C_ACTIONREAD  = 0x00000001;
        public const int ADL_DL_I2C_ACTIONWRITE = 0x00000002;

        /// <summary>
        /// Defines the maximum string length.
        /// </summary>
        public const int ADL_MAX_PATH = 256;
    }
}
