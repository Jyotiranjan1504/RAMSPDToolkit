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

namespace RAMSPDToolkit.I2CSMBus.Interop.NVIDIA
{
    internal enum NvI2CSpeed : uint
    {
        NVAPI_I2C_SPEED_DEFAULT,
        NVAPI_I2C_SPEED_3KHZ,
        NVAPI_I2C_SPEED_10KHZ,
        NVAPI_I2C_SPEED_33KHZ,
        NVAPI_I2C_SPEED_100KHZ,
        NVAPI_I2C_SPEED_200KHZ,
        NVAPI_I2C_SPEED_400KHZ,
    }
}
