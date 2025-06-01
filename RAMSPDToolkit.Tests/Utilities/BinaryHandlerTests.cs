using RAMSPDToolkit.Utilities;

namespace RAMSPDToolkit.Tests.Utilities
{
    [TestClass]
    public class BinaryHandlerTests
    {
        [TestMethod]
        public void NormalizeBcd()
        {
            const byte bcd = 0x42;
            const byte expected = 42;

            Assert.AreEqual(expected, BinaryHandler.NormalizeBcd(bcd));
        }
    }
}
