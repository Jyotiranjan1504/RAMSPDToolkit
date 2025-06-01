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

namespace RAMSPDToolkit.I2CSMBus.Interop.Linux
{
    internal class I2CDeviceHandle : IDisposable
    {
        #region Constructor

        public I2CDeviceHandle(string device)
        {
            _Handle = Libc.open(device, LinuxConstants.O_RDWR);
        }

        ~I2CDeviceHandle()
        {
            Dispose();
        }

        #endregion

        #region Fields

        bool _Disposed;
        int _Handle = -1;

        #endregion

        #region Properties

        public int Handle
        {
            get { return _Handle; }
        }

        public bool IsValid
        {
            get { return false == (_Handle < 0); }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (!_Disposed)
            {
                Libc.close(_Handle);
                _Handle = -1;

                _Disposed = true;
            }
        }

        #endregion
    }
}