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
    internal static class SPDTemperatureConverter
    {
        #region Public

        /// <summary>
        /// Checks temperature for positive or negative value, adjusts value accordingly and returns temperature in celsius.
        /// </summary>
        /// <param name="temperatureRaw">Raw temperature value directly read from SPD.</param>
        /// <returns>Returns temperature in celsius.</returns>
        public static float CheckAndConvertTemperature(ushort temperatureRaw)
        {
            float temperature = 0f;

            //Check if sign bit is negative
            if ((temperatureRaw & 0x1000) != 0)
            {
                //Sign bit is negative
                temperatureRaw = (ushort)(temperatureRaw & ~0x1000);
                temperature = temperatureRaw * 0.0625f - 256;
            }
            else //Positive
            {
                temperature = temperatureRaw * 0.0625f;
            }

            return temperature;
        }

        #endregion
    }
}
