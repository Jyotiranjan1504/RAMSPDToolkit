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

namespace RAMSPDToolkit.SuperInOut
{
    internal sealed class SuperIOConstants
    {
        /******************************************************************************************\
        *                                                                                          *
        *   Nuvoton Super IO constants                                                             *
        *                                                                                          *
        \******************************************************************************************/

        public const int SIO_NCT5577_ID      = 0xC330; /* Device ID for NCT5577D (C333)        */
        public const int SIO_NCT6102_ID      = 0x1060; /* Device ID for NCT6102D/6106D (1061)  */
        public const int SIO_NCT6793_ID      = 0xd120; /* Device ID for NCT6793D (D121)        */
        public const int SIO_NCT6795_ID      = 0xd350; /* Device ID for NCT6795D (D350)        */
        public const int SIO_NCT6796_ID      = 0xd420; /* Device ID for NCT6796D (D421)        */
        public const int SIO_NCT6797_ID      = 0xd450; /* Device ID for NCT6797D (D450)        */
        public const int SIO_NCT6798_ID      = 0xd428; /* Device ID for NCT6798D (D428)        */
        public const int SIO_ITE8688_ID      = 0x8688; /* Device ID for ITE8688  (8688)        */
        public const int SIO_REG_LOGDEV      = 0x07  ; /* Logical Device Register              */
        public const int SIO_REG_DEVID       = 0x20  ; /* Device ID Register                   */
        public const int SIO_REG_SMBA        = 0x62  ; /* SMBus Base Address Register          */
        public const int SIO_LOGDEV_SMBUS    = 0x0B  ; /* Logical Device for SMBus             */
        public const int SIO_ID_MASK         = 0xFFF8; /* Device ID mask                       */
    }
}
