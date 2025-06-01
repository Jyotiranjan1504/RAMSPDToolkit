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

namespace RAMSPDToolkit.Utilities
{
    /// <summary>
    /// Helper class for bitwise operations.
    /// </summary>
    public static class BitHandler
    {
        /// <summary>
        /// Check if bit is set in given value.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="bitPos">Position of bit to check.</param>
        /// <returns>Returns whether specified bit is set in byte.</returns>
        public static bool IsBitSet(byte value, int bitPos)
        {
            return (value & (1 << bitPos)) != 0;
        }

        /// <summary>
        /// Check if bit is set in given value.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="bitPos">Position of bit to check.</param>
        /// <returns>Returns whether specified bit is set in byte.</returns>
        public static bool IsBitSet(ushort value, int bitPos)
        {
            return (value & (1 << bitPos)) != 0;
        }

        /// <summary>
        /// Manually swap bits in given byte.
        /// </summary>
        /// <param name="b">Byte to swap bits in.</param>
        /// <returns>Byte with swapped bits.</returns>
        public static byte SwapBits(byte b)
        {
            byte temp = 0;

            for (byte i = 0; i < 8; ++i)
            {
                temp |= (byte)((b & (1 << i)) != 0 ? (1 << (7 - i)) : 0);
            }

            return temp;
        }

        /// <summary>
        /// Unset bit at given index.
        /// </summary>
        /// <param name="value">Value to unset bit on.</param>
        /// <param name="bitIndexToUnset">Index of bit to unset.</param>
        /// <returns>Byte value with bit unset.</returns>
        public static byte UnsetBit(byte value, byte bitIndexToUnset)
        {
            return (byte)(value & ~(1 << bitIndexToUnset));
        }
    }
}
