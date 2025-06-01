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

using RAMSPDToolkit.I2CSMBus;
using RAMSPDToolkit.Logging;
using RAMSPDToolkit.SPD.Interop;
using RAMSPDToolkit.SPD.Interop.Shared;
using RAMSPDToolkit.Utilities;

namespace RAMSPDToolkit.SPD
{
    /// <summary>
    /// Accessor for DDR5 SPD.<br/>
    /// This works for Windows and Linux.
    /// </summary>
    public sealed class DDR5Accessor : DDR5AccessorBase
    {
        #region Constructor

        public DDR5Accessor(SMBusInterface bus, byte address)
            : base(bus, address)
        {
            //We want these values to be read, just wait until SMBus is not busy
            ReadThermalSensorConfiguration(SPDConstants.SPD_CFG_RETRIES);

            if (HasThermalSensor)
            {
                //Initial read for volatile data
                Update();

                //For DDR5 the value is fixed
                TemperatureResolution = DDR5Constants.SPD_DDR5_TEMPERATURE_RESOLUTION;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Determines if thermal sensor is currently enabled.
        /// </summary>
        public bool ThermalSensorEnabled { get; private set; }

        /// <summary>
        /// Thermal sensor critical high limit temperature.
        /// </summary>
        public float ThermalSensorCriticalHighLimit { get; private set; }

        /// <summary>
        /// Thermal sensor critical low limit temperature.
        /// </summary>
        public float ThermalSensorCriticalLowLimit { get; private set; }

        /// <summary>
        /// Thermal sensor temperature status.
        /// </summary>
        public ThermalSensorStatus ThermalSensorStatus { get; private set; }

        #endregion

        #region Public

        /// <summary>
        /// Detects if a DDR5 RAM is available at specified address.
        /// </summary>
        /// <param name="bus">SMBus to check for RAM.</param>
        /// <param name="address">Address to check.</param>
        /// <returns>True if DDR5 is available at specified address; false otherwise.</returns>
        public static bool IsAvailable(SMBusInterface bus, byte address)
        {
            bool retry = true;

            while (true)
            {
                Thread.Sleep(SPDConstants.SPD_IO_DELAY);

                //This value should be 0x51
                int ddr5Magic = bus.i2c_smbus_read_byte_data(address, 0x00);

                Thread.Sleep(SPDConstants.SPD_IO_DELAY);

                //This value should be 0x18
                int ddr5Sensor = bus.i2c_smbus_read_byte_data(address, 0x01);

                Thread.Sleep(SPDConstants.SPD_IO_DELAY);

                if (ddr5Magic < 0 || ddr5Sensor < 0)
                {
                    break;
                }

                if (ddr5Magic == 0x51 && (ddr5Sensor & 0xEF) == 0x08)
                {
                    return true;
                }

                int page = bus.i2c_smbus_read_byte_data(address, DDR5Constants.SPD_DDR5_MREG_VIRTUAL_PAGE);

                Thread.Sleep(SPDConstants.SPD_IO_DELAY);

                if (page < 0)
                {
                    break;
                }
                else if (retry && page > 0 && page < (DDR5Constants.SPD_DDR5_EEPROM_LENGTH >> DDR5Constants.SPD_DDR5_EEPROM_PAGE_SHIFT))
                {
                    //Can only change page if we don't have write protection enabled
                    if (!bus.HasSPDWriteProtection)
                    {
                        //This device might still be a DDR5 module, just the page is off
                        bus.i2c_smbus_write_byte_data(address, DDR5Constants.SPD_DDR5_MREG_VIRTUAL_PAGE, 0);

                        Thread.Sleep(SPDConstants.SPD_IO_DELAY);
                    }
                    else
                    {
                        break;
                    }

                    retry = false;
                }
                else
                {
                    break;
                }
            }

            return false;
        }

        #endregion

        #region DDR5Accessor

        public override byte At(ushort address)
        {
            //Ensure address is valid
            if (address >= DDR5Constants.SPD_DDR5_EEPROM_LENGTH)
            {
                return 0xFF;
            }

            //Switch to the page containing address
            SetPage((byte)(address >> DDR5Constants.SPD_DDR5_EEPROM_PAGE_SHIFT));

            //Calculate offset
            byte offset = (byte)((address & DDR5Constants.SPD_DDR5_EEPROM_PAGE_MASK) | 0x80);

            //Read value at address
            RetryReadByteData(_Address, offset, SPDConstants.SPD_DATA_RETRIES, out var value);

            Thread.Sleep(SPDConstants.SPD_IO_DELAY);

            //Return value
            return value;
        }

        public override bool UpdateTemperature()
        {
            //Cannot read data if sensor is disabled
            if (!ThermalSensorEnabled)
            {
                return false;
            }

            //Set page to 0 to read volatile data
            SetPage(0);

            var status = RetryReadWordData(_Address, DDR5Constants.SPD_DDR5_TEMPERATURE_ADDRESS, SPDConstants.SPD_TS_RETRIES, out ushort temp);

            if (status >= 0)
            {
                Temperature = SPDTemperatureConverter.CheckAndConvertTemperature(temp);
            }
            else
            {
                LogSimple.LogTrace($"Temperature read failed with status {status}.");
            }

            return status >= 0;
        }

        public override void Update(int retries = SPDConstants.SPD_DATA_RETRIES)
        {
            if (HasThermalSensor)
            {
                //Set page to 0
                SetPage(0);

                byte byteTemp;

                //Thermal sensor enabled
                var status = RetryReadByteData(_Address, DDR5Constants.SPD_DDR5_THERMAL_SENSOR_ENABLED, retries, out byteTemp);
                if (status < 0)
                {
                    LogSimple.LogTrace($"Reading {nameof(DDR5Constants.SPD_DDR5_THERMAL_SENSOR_ENABLED)} failed with status {status}.");
                }
                else
                {
                    //0 = Enabled; 1 = Disabled
                    ThermalSensorEnabled = byteTemp == 0;
                    LogSimple.LogTrace($"{nameof(ThermalSensorEnabled)} = {ThermalSensorEnabled}.");
                }

                //Thermal sensor status
                status = RetryReadByteData(_Address, DDR5Constants.SPD_DDR5_THERMAL_SENSOR_STATUS, retries, out byteTemp);
                if (status < 0)
                {
                    LogSimple.LogTrace($"Reading {nameof(DDR5Constants.SPD_DDR5_THERMAL_SENSOR_STATUS)} failed with status {status}.");
                }
                else
                {
                    //Check critical limits first
                    if (BitHandler.IsBitSet(byteTemp, 2))
                    {
                        ThermalSensorStatus = ThermalSensorStatus.AboveCriticalHighLimit;
                    }
                    else if (BitHandler.IsBitSet(byteTemp, 3))
                    {
                        ThermalSensorStatus = ThermalSensorStatus.BelowCriticalLowLimit;
                    }
                    else if (BitHandler.IsBitSet(byteTemp, 0))
                    {
                        ThermalSensorStatus = ThermalSensorStatus.AboveHighLimit;
                    }
                    else if (BitHandler.IsBitSet(byteTemp, 1))
                    {
                        ThermalSensorStatus = ThermalSensorStatus.BelowLowLimit;
                    }
                    else
                    {
                        ThermalSensorStatus = ThermalSensorStatus.Good;
                    }

                    LogSimple.LogTrace($"{nameof(ThermalSensorStatus)} = {ThermalSensorStatus}.");
                }
            }
        }

        #endregion

        #region Private

        void ReadThermalSensorConfiguration(int retries)
        {
            //Set page to 0
            SetPage(0);

            byte byteTemp;
            ushort wordTemp;

            //Device capability
            var status = RetryReadByteData(_Address, DDR5Constants.SPD_DDR5_DEVICE_CAPABILITY, retries, out byteTemp);
            if (status < 0)
            {
                LogSimple.LogTrace($"Reading {nameof(DDR5Constants.SPD_DDR5_DEVICE_CAPABILITY)} failed with status {status}.");
            }
            else
            {
                //Bit 1 => 1 = Supports Temperature Sensor
                HasThermalSensor = BitHandler.IsBitSet(byteTemp, 1);

                LogSimple.LogTrace($"{nameof(HasThermalSensor)} = {HasThermalSensor}.");
            }

            if (!HasThermalSensor)
            {
                return;
            }

            //Sensor high limit
            status = RetryReadWordData(_Address, DDR5Constants.SPD_DDR5_THERMAL_SENSOR_HIGH_LIMIT_CONFIGURATION, retries, out wordTemp);
            if (status < 0)
            {
                LogSimple.LogTrace($"Reading {nameof(DDR5Constants.SPD_DDR5_THERMAL_SENSOR_HIGH_LIMIT_CONFIGURATION)} failed with status {status}.");
            }
            else
            {
                ThermalSensorHighLimit = SPDTemperatureConverter.CheckAndConvertTemperature(wordTemp);
                LogSimple.LogTrace($"{nameof(ThermalSensorHighLimit)} = {ThermalSensorHighLimit}.");
            }

            //Sensor low limit
            status = RetryReadWordData(_Address, DDR5Constants.SPD_DDR5_THERMAL_SENSOR_LOW_LIMIT_CONFIGURATION, retries, out wordTemp);
            if (status < 0)
            {
                LogSimple.LogTrace($"Reading {nameof(DDR5Constants.SPD_DDR5_THERMAL_SENSOR_LOW_LIMIT_CONFIGURATION)} failed with status {status}.");
            }
            else
            {
                ThermalSensorLowLimit = SPDTemperatureConverter.CheckAndConvertTemperature(wordTemp);
                LogSimple.LogTrace($"{nameof(ThermalSensorLowLimit)} = {ThermalSensorLowLimit}.");
            }

            //Sensor critical high limit
            status = RetryReadWordData(_Address, DDR5Constants.SPD_DDR5_THERMAL_SENSOR_CRITICAL_HIGH_LIMIT_CONFIGURATION, retries, out wordTemp);
            if (status < 0)
            {
                LogSimple.LogTrace($"Reading {nameof(DDR5Constants.SPD_DDR5_THERMAL_SENSOR_CRITICAL_HIGH_LIMIT_CONFIGURATION)} failed with status {status}.");
            }
            else
            {
                ThermalSensorCriticalHighLimit = SPDTemperatureConverter.CheckAndConvertTemperature(wordTemp);
                LogSimple.LogTrace($"{nameof(ThermalSensorCriticalHighLimit)} = {ThermalSensorCriticalHighLimit}.");
            }

            //Sensor critical low limit
            status = RetryReadWordData(_Address, DDR5Constants.SPD_DDR5_THERMAL_SENSOR_CRITICAL_LOW_LIMIT_CONFIGURATION, retries, out wordTemp);
            if (status < 0)
            {
                LogSimple.LogTrace($"Reading {nameof(DDR5Constants.SPD_DDR5_THERMAL_SENSOR_CRITICAL_LOW_LIMIT_CONFIGURATION)} failed with status {status}.");
            }
            else
            {
                ThermalSensorCriticalLowLimit = SPDTemperatureConverter.CheckAndConvertTemperature(wordTemp);
                LogSimple.LogTrace($"{nameof(ThermalSensorCriticalLowLimit)} = {ThermalSensorCriticalLowLimit}.");
            }
        }

        #endregion
    }
}
