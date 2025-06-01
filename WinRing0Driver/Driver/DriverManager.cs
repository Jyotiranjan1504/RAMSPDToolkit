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

using WinRing0Driver.Driver.Enums;

namespace WinRing0Driver.Driver
{
    /// <summary>
    /// Manages driver loading.
    /// </summary>
    public static class DriverManager
    {
        #region Properties

        /// <summary>
        /// Instance of <see cref="OLS"/> driver.
        /// </summary>
        public static OLS Ols { get; private set; }

        #endregion

        #region Public

        /// <summary>
        /// Loads <see cref="OLS"/> driver.
        /// </summary>
        /// <returns>Boolean value whether driver load was successful.</returns>
        public static bool LoadDriver()
        {
            if (Ols == null)
            {
                Ols = new OLS();
            }

            return Ols.OLSStatus == OLSStatus.NO_ERROR
                && Ols.GetDllStatus() == 0;
        }

        /// <summary>
        /// Unloads <see cref="OLS"/> driver.
        /// </summary>
        public static void UnloadDriver()
        {
            if (Ols != null)
            {
                Ols.Dispose();
                Ols = null;
            }
        }

        #endregion
    }
}
