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

namespace RAMSPDToolkit.I2CSMBus.Interop.Shared
{
    public class SharedConstants
    {
        internal const string GlobalSMBusMutexName = "Global\\Access_SMBUS.HTP.Method";

        public const int EIO        =   5; //I/O error
        public const int ENXIO      =   6; //No such device or address
        public const int EBADF      =   9; //Bad file number
        public const int EAGAIN     =  11; //Try again
        public const int EBUSY      =  16; //Device or resource is busy
        public const int EINVAL     =  22; //Invalid argument
        public const int ENOTSUP    = 129; //Not supported
        public const int EOPNOTSUPP = 130; //Operation is not supported on transport endpoint
        public const int EPROTO     = 134; //Protocol error
        public const int ETIMEDOUT  = 138; //Connection timed out

        internal const int MAX_RETRIES = 450;
    }
}
