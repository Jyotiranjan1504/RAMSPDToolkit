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
    public abstract class MarshalDynamicArrayBase<TStructure, TBufferType>
        where TStructure : struct
    {
        #region Constructor

        public MarshalDynamicArrayBase()
        {
            SetBuffer(_InternalBuffer);
        }

        #endregion

        #region Fields

        public TStructure Structure;

        TBufferType[] _InternalBuffer;

        #endregion

        #region Properties

        public TBufferType[] Buffer
        {
            get { return GetBuffer(); }
            set { SetBuffer(value); }
        }

        #endregion

        #region Abstract

        public abstract void AssignBufferPointer(IntPtr buffer);

        #endregion

        #region Private

        TBufferType[] GetBuffer()
        {
            return _InternalBuffer;
        }

        unsafe void SetBuffer(TBufferType[] buffer)
        {
            _InternalBuffer = buffer;

            fixed (TBufferType* ptr = _InternalBuffer)
            {
                AssignBufferPointer(ptr == null ? IntPtr.Zero : (IntPtr)ptr);
            }
        }

        #endregion
    }
}
