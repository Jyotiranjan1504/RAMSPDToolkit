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

namespace RAMSPDToolkit.SPD.Interop.Shared
{
    /// <summary>
    /// Memory type based on SPD read.
    /// </summary>
    public enum SPDMemoryType : int
    {
        SPD_RESERVED      =  0,
        SPD_FPM_DRAM      =  1,
        SPD_EDO           =  2,
        SPD_NIBBLE        =  3,
        SPD_SDR_SDRAM     =  4,
        SPD_MUX_ROM       =  5,
        SPD_DDR_SGRAM     =  6,
        SPD_DDR_SDRAM     =  7,
        SPD_DDR2_SDRAM    =  8,
        SPD_FB_DIMM       =  9,
        SPD_FB_PROBE      = 10,
        SPD_DDR3_SDRAM    = 11,
        SPD_DDR4_SDRAM    = 12,
        SPD_RESERVED2     = 13,
        SPD_DDR4E_SDRAM   = 14,
        SPD_LPDDR3_SDRAM  = 15,
        SPD_LPDDR4_SDRAM  = 16,
        SPD_LPDDR4X_SDRAM = 17,
        SPD_DDR5_SDRAM    = 18,
        SPD_LPDDR5_SDRAM  = 19,
    }
}
