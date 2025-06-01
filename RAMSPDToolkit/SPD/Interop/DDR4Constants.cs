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

namespace RAMSPDToolkit.SPD.Interop
{
    internal sealed class DDR4Constants
    {
        public const byte SPD_DDR4_ADDRESS_PAGE        = 0x36;
        public const byte SPD_DDR4_THERMAL_SENSOR_BYTE = 0x0E;
        public const byte SPD_DDR4_THERMAL_SENSOR_BIT  = 0x07;

        public const ushort SPD_DDR4_EEPROM_LENGTH     = 512;
        public const byte   SPD_DDR4_EEPROM_PAGE_SHIFT = 8;
        public const byte   SPD_DDR4_EEPROM_PAGE_MASK  = 0xFF;

        public const byte SPD_DDR4_PAGE_MIN = 0;
        public const byte SPD_DDR4_PAGE_MAX = 1;

        public const byte SPD_DDR4_THERMAL_SENSOR_CAPABILITIES_REGISTER  = 0x00;
        public const byte SPD_DDR4_THERMAL_SENSOR_CONFIGURATION_REGISTER = 0x01;
        public const byte SPD_DDR4_THERMAL_SENSOR_HIGH_LIMIT             = 0x02;
        public const byte SPD_DDR4_THERMAL_SENSOR_LOW_LIMIT              = 0x03;
        public const byte SPD_DDR4_THERMAL_SENSOR_CRIT_LIMIT             = 0x04;
        public const byte SPD_DDR4_TEMPERATURE_ADDRESS                   = 0x05;
        public const byte SPD_DDR4_THERMAL_SENSOR_MANUFACTURER           = 0x06;
        public const byte SPD_DDR4_THERMAL_SENSOR_DEVICEID               = 0x07;

        public const ushort SPD_DDR4_MODULE_SPD_REVISION = 0x01;
        public const ushort SPD_DDR4_MODULE_MEMORY_TYPE  = 0x02;

        public const ushort SPD_DDR4_MODULE_MANUFACTURER_CONTINUATION_CODE   = 0x140;
        public const ushort SPD_DDR4_MODULE_MANUFACTURER_ID_CODE             = 0x141;
        public const ushort SPD_DDR4_MODULE_MANUFACTURING_LOCATION           = 0x142;
        public const ushort SPD_DDR4_MODULE_MANUFACTURING_DATE_BEGIN         = 0x143;
        public const ushort SPD_DDR4_MODULE_MANUFACTURING_DATE_END           = 0x144;
        public const ushort SPD_DDR4_MODULE_SERIAL_NUMBER_BEGIN              = 0x145;
        public const ushort SPD_DDR4_MODULE_SERIAL_NUMBER_END                = 0x148;
        public const ushort SPD_DDR4_MODULE_PART_NUMBER_BEGIN                = 0x149;
        public const ushort SPD_DDR4_MODULE_PART_NUMBER_END                  = 0x15C;
        public const ushort SPD_DDR4_MODULE_REVISION_CODE                    = 0x15D;
        public const ushort SPD_DDR4_DRAM_MANUFACTURER_CONTINUATION_CODE     = 0x15E;
        public const ushort SPD_DDR4_DRAM_MANUFACTURER_ID_CODE               = 0x15F;
        public const ushort SPD_DDR4_MANUFACTURER_SPECIFIC_DATA_BEGIN        = 0x161;
        public const ushort SPD_DDR4_MANUFACTURER_SPECIFIC_DATA_END          = 0x17D;

        public const byte SPD_DDR4_MANUFACTURER_CONTINUATION_CODE_ODD_PARITY_BIT = 7;

        public const byte SPD_DDR4_MODULE_PART_NUMBER_UNUSED = 0x20;
    }
}
