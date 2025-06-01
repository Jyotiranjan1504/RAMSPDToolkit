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

namespace RAMSPDToolkit.I2CSMBus.Interop.NCT6775
{
    internal sealed class NCT6775Constants
    {
        /* Control register */
        public const int NCT6775_SOFT_RESET   = 64;
        public const int NCT6775_MANUAL_START = 128;

        /* Command register */
        public const int NCT6775_READ_BYTE                  =  0;
        public const int NCT6775_READ_WORD                  =  1;
        public const int NCT6775_READ_BLOCK                 =  2;
        public const int NCT6775_BLOCK_WRITE_READ_PROC_CALL =  3;
        public const int NCT6775_PROC_CALL                  =  4;
        public const int NCT6775_WRITE_BYTE                 =  8;
        public const int NCT6775_WRITE_WORD                 =  9;
        public const int NCT6775_WRITE_BLOCK                = 10;

        /* Status register */
        public const int NCT6775_FIFO_EMPTY    = 1;
        public const int NCT6775_FIFO_FULL     = 2;
        public const int NCT6775_MANUAL_ACTIVE = 4;

        /* Error register */
        public const int NCT6775_NO_ACK = 32;
    }
}
