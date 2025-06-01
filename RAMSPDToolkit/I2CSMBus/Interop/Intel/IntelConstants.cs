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

namespace RAMSPDToolkit.I2CSMBus.Interop.Intel
{
    internal sealed class IntelConstants
    {
        public const int SMBUS_FIXED_ADDRESS = 0xEFA0;

        /* PCI Address Constants */
        public const int SMBHSTCFG = 0x040;

        /* Host configuration bits for SMBHSTCFG */
        public const int SMBHSTCFG_HST_EN = 1 << 0;
        public const int SMBHSTCFG_SPD_WD = 1 << 4;

        /* Auxiliary control register bits, ICH4+ only */
        public const int SMBAUXCTL_CRC  = 1 << 0;
        public const int SMBAUXCTL_E32B = 1 << 1;

        /* I801 command constants */
        public const int I801_QUICK           = 0x00;
        public const int I801_BYTE            = 0x04;
        public const int I801_BYTE_DATA       = 0x08;
        public const int I801_WORD_DATA       = 0x0C;
        public const int I801_PROC_CALL       = 0x10; //Not implemented
        public const int I801_BLOCK_DATA      = 0x14;
        public const int I801_I2C_BLOCK_DATA  = 0x18; /* ICH5 and later */
        public const int I801_BLOCK_PROC_CALL = 0x1C; //Not implemented

        /* I801 Host Control register bits */
        public const int SMBHSTCNT_INTREN    = 1 << 0;
        public const int SMBHSTCNT_KILL      = 1 << 1;
        public const int SMBHSTCNT_LAST_BYTE = 1 << 5;
        public const int SMBHSTCNT_START     = 1 << 6;
        public const int SMBHSTCNT_PEC_EN    = 1 << 7; /* ICH3 and later */

        /* I801 Hosts Status register bits */
        public const int SMBHSTSTS_BYTE_DONE    = 1 << 7;
        public const int SMBHSTSTS_INUSE_STS    = 1 << 6;
        public const int SMBHSTSTS_SMBALERT_STS = 1 << 5;
        public const int SMBHSTSTS_FAILED       = 1 << 4;
        public const int SMBHSTSTS_BUS_ERR      = 1 << 3;
        public const int SMBHSTSTS_DEV_ERR      = 1 << 2;
        public const int SMBHSTSTS_INTR         = 1 << 1;
        public const int SMBHSTSTS_HOST_BUSY    = 1 << 0;

        public const int STATUS_ERROR_FLAGS = SMBHSTSTS_FAILED | SMBHSTSTS_BUS_ERR | SMBHSTSTS_DEV_ERR;
        public const int STATUS_FLAGS = SMBHSTSTS_BYTE_DONE | SMBHSTSTS_INTR | STATUS_ERROR_FLAGS;
    }
}
