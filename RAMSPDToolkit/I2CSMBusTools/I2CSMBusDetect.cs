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

using RAMSPDToolkit.I2CSMBus;
using RAMSPDToolkit.I2CSMBus.Interop;
using RAMSPDToolkit.I2CSMBus.Interop.Shared;
using System.Text;

namespace RAMSPDToolkit.I2CSMBusTools
{
    /// <summary>
    /// Detection methods for SMBus.
    /// </summary>
    public sealed class I2CSMBusDetect
    {
        #region Constants

        public const int MODE_AUTO  = 0;
        public const int MODE_QUICK = 1;
        public const int MODE_READ  = 2;
        public const int MODE_FUNC  = 3;

        #endregion

        #region Public

        /******************************************************************************************\
        *                                                                                          *
        *   I2CDetect                                                                              *
        *                                                                                          *
        *       Prints a list of all detected I2C addresses on the given bus                       *
        *                                                                                          *
        *           bus - pointer to I2CSMBusInterface to scan                                     *
        *           mode - one of AUTO, QUICK, READ, FUNC - method of access                       *
        *                                                                                          *
        *       Code adapted from i2cdetect.c from i2c-tools Linux package                         *
        *                                                                                          *
        \******************************************************************************************/

        public static string I2CDetect(SMBusInterface bus, int mode)
        {
            var sb = new StringBuilder();

            sb.AppendLine("     0  1  2  3  4  5  6  7  8  9  a  b  c  d  e  f");

            for (byte i = 0; i < 128; i += 16)
            {
                sb.Append($"{i:X2}  ");

                for (byte j = 0; j < 16; ++j)
                {
                    /* Skip unwanted addresses */
                    if (i + j < 0x03 || i + j > 0x77)
                    {
                        sb.Append("   ");
                        continue;
                    }

                    /* Set slave address */
                    var slave_addr = (byte)(i + j);

                    int result;

                    /* Probe this address */
                    switch (mode)
                    {
                        case MODE_QUICK:
                            result = bus.i2c_smbus_write_quick(slave_addr, I2CConstants.I2C_SMBUS_WRITE);
                            break;
                        case MODE_READ:
                            result = bus.i2c_smbus_read_byte(slave_addr);
                            break;
                        default:
                            if ( (i + j >= 0x30 && i + j <= 0x37) ||
                                 (i + j >= 0x50 && i + j <= 0x5F) )
                            {
                                result = bus.i2c_smbus_read_byte(slave_addr);
                            }
                            else
                            {
                                result = bus.i2c_smbus_write_quick(slave_addr, I2CConstants.I2C_SMBUS_WRITE);
                            }
                            break;
                    }

                    if (result < 0)
                    {
                        if (Math.Abs(result) == SharedConstants.EBUSY)
                        {
                            sb.Append("UU ");
                        }
                        else
                        {
                            sb.Append("-- ");
                        }
                    }
                    else
                    {
                        sb.Append($"{(i + j):X2} ");
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        /******************************************************************************************\
        *                                                                                          *
        *   i2c_dump                                                                               *
        *                                                                                          *
        *       Prints the values at each address of a given SMBus device                          *
        *                                                                                          *
        *           bus - pointer to I2CSMBusInterface to scan                                     *
        *           address - SMBus device address to scan                                         *
        *                                                                                          *
        \******************************************************************************************/
        public static string I2CDump(SMBusInterface bus, byte address)
        {
            var sb = new StringBuilder();

            sb.AppendLine("       0  1  2  3  4  5  6  7  8  9  a  b  c  d  e  f");

            for (byte i = 0; i < 0xFF; i += 16)
            {
                sb.Append($"{i:X4}");

                for (byte j = 0; j < 16; ++j)
                {
                    var data = bus.i2c_smbus_read_byte_data(address, (byte)(i + j));
                    sb.Append($"{data:X2}");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        /******************************************************************************************\
        *                                                                                          *
        *   i2c_read                                                                               *
        *                                                                                          *
        *       Prints <size> values read from register address regaddr of a given SMBus device    *
        *                                                                                          *
        *           bus - pointer to I2CSMBusInterface to scan                                     *
        *           address - SMBus device address to scan                                         *
        *           regaddr - register address to read from                                        *
        *           size - number of bytes to read                                                 *
        *                                                                                          *
        \******************************************************************************************/
        public static string I2CRead(SMBusInterface bus, byte address, byte regAddr, byte size)
        {
            bus.i2c_smbus_write_byte(address, regAddr);

            var sb = new StringBuilder();

            for (byte i = 0; i < size; ++i)
            {
                var data = bus.i2c_smbus_read_byte(address);
                sb.Append($"{data:02x} ");
            }

            return sb.ToString();
        }

        #endregion
    }
}
