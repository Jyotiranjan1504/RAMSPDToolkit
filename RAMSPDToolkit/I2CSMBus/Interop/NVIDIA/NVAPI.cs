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
    /// <summary>
    /// Delegates for Nvidia API.
    /// </summary>
    internal sealed class NVAPI
    {
        internal delegate IntPtr _NvAPI_QueryInterface(uint interfaceID);

        internal delegate int _NvAPI_EnumPhysicalGPUs(IntPtr[] physicalGPUHandles, out int gpuCount);
        internal delegate int _NvAPI_GPU_GetFullName(IntPtr physicalGPUHandle, byte[] name);
        internal delegate int _NvAPI_GPU_GetPCIIdentifiers(IntPtr physicalGPUHandle, out uint deviceID, out uint subSystemID, out uint revisionID, out uint extDeviceID);
        internal delegate int _NvAPI_I2CWriteEx(IntPtr physicalGPUHandle, ref NvI2CInfoV3Struct i2cInfo, out uint unknown);
        internal delegate int _NvAPI_I2CReadEx(IntPtr physicalGPUHandle, ref NvI2CInfoV3Struct i2cInfo, out uint unknown);
    }
}
