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

using RAMSPDToolkit.Logging;

using OS = WinRing0Driver.Utilities.OperatingSystem;

namespace RAMSPDToolkit.I2CSMBus
{
    /// <summary>
    /// Manager class for SMBuses.
    /// </summary>
    public static class SMBusManager
    {
        #region Constructor

        static SMBusManager()
        {
            Initialize();

            LogSimple.LogTrace($"Detecting SMBuses - amount of available detection methods are {SMBusDetectMethods.Count}.");

            foreach (var detection in SMBusDetectMethods)
            {
                detection?.Invoke();
            }
        }

        #endregion

        #region Fields

        static bool _IsInitialized;

        /// <summary>
        /// Holds SMBus detection methods.
        /// </summary>
        public static List<Func<bool>> SMBusDetectMethods = new List<Func<bool>>();

        /// <summary>
        /// Holds all registered SMBus instances.
        /// </summary>
        public static List<SMBusInterface> RegisteredSMBuses = new List<SMBusInterface>();

        #endregion

        #region Public

        /// <summary>
        /// Gets first SMBus in <see cref="RegisteredSMBuses"/> with specified type.
        /// </summary>
        /// <typeparam name="TSMBus">Type of SMBus to get.</typeparam>
        /// <returns>Found SMBus of given type or null.</returns>
        public static TSMBus GetSMBus<TSMBus>()
            where TSMBus : SMBusInterface
        {
            return RegisteredSMBuses.Find(i => i is TSMBus) as TSMBus;
        }

        #endregion

        #region Private

        static void Initialize()
        {
            if (_IsInitialized)
            {
                return;
            }

            if (OS.IsWindows())
            {
                //SMBusDetectMethods.Add(I2CSMBusAmdAdl .SMBusDetect);
                SMBusDetectMethods.Add(SMBusI801   .SMBusDetect);
                SMBusDetectMethods.Add(SMBusPiix4  .SMBusDetect);
                SMBusDetectMethods.Add(SMBusNVAPI  .SMBusDetect);
                SMBusDetectMethods.Add(SMBusNCT6775.SMBusDetect);
            }
            else if (OS.IsLinux())
            {
                SMBusDetectMethods.Add(SMBusLinux  .SMBusDetect);
            }

            _IsInitialized = true;
        }

        #endregion
    }
}
