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

using RAMSPDToolkit.I2CSMBus.Interop.Linux;

namespace RAMSPDToolkit.SuperInOut
{
    internal sealed class SuperIOLinux
    {
        #region Public

        public static void SuperIOEnter(int ioReg)
        {
            byte[] value = new byte[1] { 0x87 };

            var devPortFd = Libc.open("/dev/port", LinuxConstants.O_RDWR, "rw");

            if (devPortFd >= 0)
            {
                Libc.lseek(devPortFd, ioReg, LinuxConstants.SEEK_SET);
                if (Libc.write(devPortFd, value, (uint)value.Length) == uint.MaxValue)
                {
                    return;
                }

                Libc.lseek(devPortFd, ioReg, LinuxConstants.SEEK_SET);
                if (Libc.write(devPortFd, value, (uint)value.Length) == uint.MaxValue)
                {
                    return;
                }

                Libc.close(devPortFd);
            }
        }

        public static void SuperIOWriteByte(int ioReg, byte reg, byte val)
        {
            byte[] regValue = new byte[1] { reg };
            byte[] value    = new byte[1] { val };

            var devPortFd = Libc.open("/dev/port", LinuxConstants.O_RDWR, "rw");

            if (devPortFd >= 0)
            {
                Libc.lseek(devPortFd, ioReg, LinuxConstants.SEEK_SET);
                if (Libc.write(devPortFd, regValue, (uint)regValue.Length) == uint.MaxValue)
                {
                    return;
                }

                if (Libc.write(devPortFd, value, (uint)value.Length) == uint.MaxValue)
                {
                    return;
                }

                Libc.close(devPortFd);
            }
        }

        public static byte SuperIOReadByte(int ioReg, byte reg)
        {
            byte[] regValue = new byte[1] { reg };
            byte[] temp     = new byte[1];

            var devPortFd = Libc.open("/dev/port", LinuxConstants.O_RDWR, "rw");

            if (devPortFd >= 0)
            {
                Libc.lseek(devPortFd, ioReg, LinuxConstants.SEEK_SET);
                if (Libc.write(devPortFd, regValue, (uint)regValue.Length) == uint.MaxValue)
                {
                    return byte.MaxValue;
                }

                if (Libc.read(devPortFd, temp, (uint)temp.Length) == uint.MaxValue)
                {
                    return byte.MaxValue;
                }

                Libc.close(devPortFd);

                return temp[0];
            }
            else
            {
                return byte.MaxValue;
            }
        }

        #endregion
    }
}