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
using System.Security;

namespace RAMSPDToolkit.I2CSMBus.Interop.Shared
{
    internal static class Kernel32
    {
        [DllImport("kernel32.dll")]
        public static extern uint WaitForSingleObject(nint hHandle, uint dwMilliseconds);

        [DllImport("kernel32.dll")]
        public static extern bool ReleaseMutex(nint hMutex);

        [DllImport("kernel32.dll")]
        public static extern nint CreateMutex(nint lpMutexAttributes, bool bInitialOwner, string lpName);

        [DllImport("kernel32.dll")]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(nint hObject);
    }
}
