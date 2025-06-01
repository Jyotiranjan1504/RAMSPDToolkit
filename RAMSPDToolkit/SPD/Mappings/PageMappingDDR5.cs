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

using RAMSPDToolkit.SPD.Enums;

namespace RAMSPDToolkit.SPD.Mappings
{
    /// <summary>
    /// SPD Page mapping.
    /// </summary>
    public class PageMappingDDR5
    {
        /// <summary>
        /// Page mapping. Key is page number, starting with 0, and value is <see cref="PageData"/>.
        /// </summary>
        public static readonly IReadOnlyDictionary<byte, PageData> PageDataMapping = new Dictionary<byte, PageData>()
        {
            //Page 0 - Volatile data
            { 0, PageData.ThermalData
               | PageData.MemoryType
               | PageData.SPDRevision
            },

            //Page 4 - Manufacturer Information
            { 4, PageData.ModuleManufacturerID
               | PageData.ModuleManufacturingLocation
               | PageData.ModuleManufacturingDate
               | PageData.ModuleSerialNumber
               | PageData.ModulePartNumber
               | PageData.ModuleRevisionCode
               | PageData.DRAMManufacturerCode
               | PageData.ManufacturersSpecificData
            },
        };
    }
}
