using RAMSPDToolkit.Extensions;

namespace RAMSPDToolkit.Tests.Extensions
{
    [TestClass]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void RemoveIf()
        {
            var list = new List<int>() { 1, 2, 3, 10, 20, 30, 100, 200, 300 };
            var sizeNormal = list.Count;
            var sizeExpected = list.Count - 3;

            ListExtensions.RemoveIf(list, i => i > 9 && i < 100);

            Assert.AreEqual(sizeExpected, list.Count);
        }
    }
}
