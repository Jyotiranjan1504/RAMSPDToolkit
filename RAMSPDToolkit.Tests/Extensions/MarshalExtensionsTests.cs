using RAMSPDToolkit.Extensions;
using System.Runtime.InteropServices;

namespace RAMSPDToolkit.Tests.Extensions
{
    [TestClass]
    public class MarshalExtensionsTests
    {
        [TestMethod]
        public void ReadUInt16()
        {
            ushort value = 60000;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf<ushort>());

            Assert.IsTrue(ptr != IntPtr.Zero);

            MarshalExtensions.WriteUInt16(ptr, value);

            Assert.AreEqual(value, MarshalExtensions.ReadUInt16(ptr));

            Marshal.FreeHGlobal(ptr);
        }

        [TestMethod]
        public void WriteUInt16()
        {
            ushort value = 50000;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf<ushort>());

            Assert.IsTrue(ptr != IntPtr.Zero);

            MarshalExtensions.WriteUInt16(ptr, value);

            Assert.AreEqual(value, MarshalExtensions.ReadUInt16(ptr));

            Marshal.FreeHGlobal(ptr);
        }
    }
}
