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

using RAMSPDToolkit.I2CSMBus.Interop;
using RAMSPDToolkit.I2CSMBus.Interop.NVIDIA;

namespace RAMSPDToolkit.I2CSMBus
{
    /// <summary>
    /// SMBus class for NVAPI (NVIDIA GPUs).
    /// </summary>
    public sealed class SMBusNVAPI : SMBusInterface
    {
        #region Constructor

        static SMBusNVAPI()
        {
            _Module = new NVAPIModule();
        }

        SMBusNVAPI(IntPtr gpuHandle)
        {
            _GPUHandle = gpuHandle;
        }

        #endregion

        #region Fields

        static NVAPIModule _Module;
        IntPtr _GPUHandle;

        #endregion

        #region I2CSMBusInterface

        protected override int I2CSMBusXfer(byte addr, byte read_write, byte command, int mode, SMBusData data)
        {
            var i2cData = new NvI2CInfoV3();

            //Set up chip register address to command, one byte in length
            byte chipAddress = command;
            i2cData.Structure.I2CRegAddress = chipAddress;
            i2cData.Structure.RegAddressSize = 1;

            var buffer = new byte[I2CConstants.I2C_SMBUS_BLOCK_MAX];

            //Set up data buffer, zero bytes in length
            i2cData.Buffer = buffer;
            i2cData.Structure.Size = 0;

            //Use GPU port 1
            i2cData.Structure.IsDDCPort = 0;
            i2cData.Structure.PortID = 1;
            i2cData.Structure.IsPortIDSet = 1;

            //Use default speed
            i2cData.Structure.I2CSpeed = 0xFFFF;
            i2cData.Structure.I2CSpeedKHz = NvI2CSpeed.NVAPI_I2C_SPEED_DEFAULT;

            //Load device address
            i2cData.Structure.I2CDevAddress = (byte)(addr << 1);

            switch (mode)
            {
                case I2CConstants.I2C_SMBUS_BYTE:
                    //One byte of data with no register address
                    i2cData.Structure.RegAddressSize = 0;
                    buffer[0] = command;

                    i2cData.Structure.Size = 1;
                    break;
                case I2CConstants.I2C_SMBUS_BYTE_DATA:
                    //One byte of data with one byte of register address
                    buffer[0] = data.ByteData;

                    i2cData.Structure.Size = 1;
                    break;
                case I2CConstants.I2C_SMBUS_WORD_DATA:
                    //One word of data with one byte of register address
                    buffer[0] = (byte)(data.Word & 0x00ff);
                    buffer[1] = (byte)((data.Word & 0xff00) >> 8);

                    i2cData.Structure.Size = 2;
                    break;
                case I2CConstants.I2C_SMBUS_BLOCK_DATA:
                    buffer[0] = data[0];

                    Array.Copy(data.Block, 1, buffer, 1, data[0]);

                    i2cData.Structure.Size = data[0] + 1u;
                    break;
                case I2CConstants.I2C_SMBUS_I2C_BLOCK_DATA:
                    buffer[0] = data[0];

                    Array.Copy(data.Block, 1, buffer, 0, data[0]);

                    i2cData.Structure.Size = data[0];
                    break;
                case I2CConstants.I2C_SMBUS_QUICK:
                    return -1;
                default:
                    return -1;
            }

            int ret;

            //Perform read or write
            if (read_write == I2CConstants.I2C_SMBUS_WRITE)
            {
                ret = _Module.NvAPI_I2CWriteEx(_GPUHandle, ref i2cData.Structure, out _);
            }
            else
            {
                ret = _Module.NvAPI_I2CReadEx(_GPUHandle, ref i2cData.Structure, out _);

                switch (mode)
                {
                    case I2CConstants.I2C_SMBUS_BYTE:
                    case I2CConstants.I2C_SMBUS_BYTE_DATA:
                        data.ByteData = i2cData.Buffer[0];
                        break;
                    case I2CConstants.I2C_SMBUS_WORD_DATA:
                        data.Word = (ushort)(i2cData.Buffer[0] | (i2cData.Buffer[1] << 8));
                        break;
                    case I2CConstants.I2C_SMBUS_BLOCK_DATA:
                    case I2CConstants.I2C_SMBUS_I2C_BLOCK_DATA:
                        data[0] = (byte)i2cData.Structure.Size;

                        Array.Copy(i2cData.Buffer, 0, data.Block, 1, i2cData.Structure.Size);
                        break;
                    default:
                        break;
                }
            }

            return ret;
        }

        protected override int I2CXfer(byte addr, byte read_write, int? size, byte[] data)
        {
            var i2cData = new NvI2CInfoV3();

            i2cData.Structure.I2CRegAddress = null;
            i2cData.Structure.RegAddressSize = 0;

            //Set up data buffer, zero bytes in length
            i2cData.Buffer = data;
            i2cData.Structure.Size = (uint)size.GetValueOrDefault();

            //Use GPU port 1
            i2cData.Structure.IsDDCPort = 0;
            i2cData.Structure.PortID = 1;
            i2cData.Structure.IsPortIDSet = 1;

            //Use default speed
            i2cData.Structure.I2CSpeed = 0xFFFF;
            i2cData.Structure.I2CSpeedKHz = NvI2CSpeed.NVAPI_I2C_SPEED_DEFAULT;

            //Load device address
            i2cData.Structure.I2CDevAddress = (byte)(addr << 1);

            int ret;

            //Perform read or write
            if (read_write == I2CConstants.I2C_SMBUS_WRITE)
            {
                ret = _Module.NvAPI_I2CWriteEx(_GPUHandle, ref i2cData.Structure, out _);
            }
            else
            {
                ret = _Module.NvAPI_I2CReadEx(_GPUHandle, ref i2cData.Structure, out _);

                size = (int)i2cData.Structure.Size;

                Array.Copy(i2cData.Buffer, 0, data, 0, i2cData.Structure.Size);
            }

            return ret;
        }

        #endregion

        #region Public

        /// <summary>
        /// Detects if this SMBus is available for usage.<br/>
        /// If it is, an instance of this SMBus will be created and added into <see cref="SMBusManager.RegisteredSMBuses"/>.
        /// </summary>
        /// <returns>True if SMBus is available and false if not.</returns>
        public static bool SMBusDetect()
        {
            if (_Module == null || !_Module.IsModuleLoaded)
            {
                return false;
            }

            var gpuHandles = new IntPtr[NVIDIAConstants.MaxPhysicalGPUs];

            _Module.NvAPI_EnumPhysicalGPUs(gpuHandles, out int gpuCount);

            for (int gpuIndex = 0;  gpuIndex < gpuCount; ++gpuIndex)
            {
                var gpuHandle = gpuHandles[gpuIndex];

                var bus = new SMBusNVAPI(gpuHandle);

                var res = _Module.NvAPI_GPU_GetPCIIdentifiers(gpuHandle, out uint deviceID, out uint subSystemID, out uint revisionID, out uint extDeviceID);

                if (res == 0)
                {
                    var nameBuffer = new byte[NVIDIAConstants.NvShortStringSize];
                    _Module.NvAPI_GPU_GetFullName(gpuHandle, nameBuffer);

                    bus.DeviceName         = System.Text.Encoding.Default.GetString(nameBuffer).Trim('\0');
                    bus.PCIDevice          = (int)(deviceID >> 16);
                    bus.PCIVendor          = (int)(deviceID & 0xFFFF);
                    bus.PCISubsystemDevice = (int)(subSystemID >> 16);
                    bus.PCISubsystemVendor = (int)(subSystemID & 0xFFFF);
                    bus.PortID             = 1;
                }

                SMBusManager.AddSMBus(bus);
            }

            return true;
        }

        #endregion
    }
}
