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

using System.Runtime.InteropServices;

namespace RAMSPDToolkit.I2CSMBus.Interop.Linux
{
    public static class Libc
    {
        public const int RTLD_NOW = 0x002;

        [DllImport("libc", EntryPoint = "ioctl")]
        public static extern int ioctl_data(int fd, int op, ref I2CSMBusIOCTLData data);

        [DllImport("libc", EntryPoint = "ioctl")]
        public static extern int ioctl_data_rdwr(int fd, int op, ref I2CRdwrIOCTLData data);

        [DllImport("libc", EntryPoint = "ioctl")]
        public static extern int ioctl_byte(int fd, int op, byte args);

        [DllImport("libc", EntryPoint = "ioctl")]
        public static extern int ioctl_ulong(int fd, int op, out ulong args);

        [DllImport("libc", EntryPoint = "opendir")]
        public static extern IntPtr opendir([MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport("libc", EntryPoint = "readdir")]
        public static extern IntPtr readdir(IntPtr dir);

        [DllImport("libc", EntryPoint = "closedir")]
        public static extern int closedir(IntPtr dir);

        [DllImport("libc", EntryPoint = "open")]
        public static extern int open([MarshalAs(UnmanagedType.LPStr)] string filepath, int flags);

        [DllImport("libc", EntryPoint = "open")]
        public static extern int open([MarshalAs(UnmanagedType.LPStr)] string filepath, int flags, [MarshalAs(UnmanagedType.LPStr)] string mode);

        [DllImport("libc", EntryPoint = "read")]
        public static extern uint read(int fd, byte[] buffer, uint count);

        [DllImport("libc", EntryPoint = "write")]
        public static extern uint write(int fd, byte[] buffer, uint count);

        [DllImport("libc", EntryPoint = "lseek")]
        public static extern int lseek(int fd, int offset, int whence);

        [DllImport("libc", EntryPoint = "close")]
        public static extern int close(int fd);

        [DllImport("libc", EntryPoint = "realpath")]
        public static extern string realpath([MarshalAs(UnmanagedType.LPStr)] string path, IntPtr resolvedPath);
    }
}