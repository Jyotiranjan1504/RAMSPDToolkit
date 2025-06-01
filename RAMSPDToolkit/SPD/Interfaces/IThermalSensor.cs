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

namespace RAMSPDToolkit.SPD.Interfaces
{
    /// <summary>
    /// Thermal sensor interface. All temperatures stored are in °C.
    /// </summary>
    public interface IThermalSensor
    {
        /// <summary>
        /// Determines if a thermal sensor is present.
        /// </summary>
        bool HasThermalSensor { get; }

        /// <summary>
        /// Current measured temperature.
        /// </summary>
        float Temperature { get; }

        /// <summary>
        /// Temperature resolution (e.g. 0.0625; 0.125; 0.25; 0.5).
        /// </summary>
        float TemperatureResolution { get; }

        /// <summary>
        /// Thermal sensor low limit temperature.
        /// </summary>
        float ThermalSensorLowLimit { get; }

        /// <summary>
        /// Thermal sensor high limit temperature.
        /// </summary>
        float ThermalSensorHighLimit { get; }

        /// <summary>
        /// Update <see cref="Temperature"/>.
        /// </summary>
        /// <returns>Returns boolean value whether update of temperature was successful.</returns>
        bool UpdateTemperature();
    }
}
