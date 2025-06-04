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
            //Initialize fixed detect methods
            Initialize();

            //Do initial detect for SMBuses
            DetectSMBuses();
        }

        #endregion

        #region Fields

        static bool _IsInitialized;

        static List<Func<bool>> _SMBusDetectMethods = new List<Func<bool>>();
        static List<SMBusInterface> _RegisteredSMBuses = new List<SMBusInterface>();

        #endregion

        #region Properties

        /// <summary>
        /// Holds SMBus detection methods.
        /// </summary>
        public static IReadOnlyList<Func<bool>> SMBusDetectMethods
        {
            get { return _SMBusDetectMethods; }
        }

        /// <summary>
        /// Holds all registered SMBus instances.
        /// </summary>
        public static IReadOnlyList<SMBusInterface> RegisteredSMBuses
        {
            get { return _RegisteredSMBuses; }
        }

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
            return _RegisteredSMBuses.Find(i => i is TSMBus) as TSMBus;
        }

        /// <summary>
        /// Uses <see cref="SMBusDetectMethods"/> to detect all currently available SMBuses.<br/>
        /// This also clears current <see cref="RegisteredSMBuses"/>.
        /// </summary>
        public static void DetectSMBuses()
        {
            _RegisteredSMBuses.Clear();

            LogSimple.LogTrace($"Detecting SMBuses - amount of available detection methods are {SMBusDetectMethods.Count}.");

            foreach (var detection in SMBusDetectMethods)
            {
                detection?.Invoke();
            }
        }

        #endregion

        #region Internal

        internal static void AddSMBus(SMBusInterface smbus)
        {
            _RegisteredSMBuses.Add(smbus);
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
                //_SMBusDetectMethods.Add(I2CSMBusAmdAdl .SMBusDetect);
                _SMBusDetectMethods.Add(SMBusI801   .SMBusDetect);
                _SMBusDetectMethods.Add(SMBusPiix4  .SMBusDetect);
                _SMBusDetectMethods.Add(SMBusNVAPI  .SMBusDetect);
                _SMBusDetectMethods.Add(SMBusNCT6775.SMBusDetect);
            }
            else if (OS.IsLinux())
            {
                _SMBusDetectMethods.Add(SMBusLinux  .SMBusDetect);
            }

            _IsInitialized = true;
        }

        #endregion
    }
}
