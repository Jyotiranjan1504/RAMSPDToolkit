using RAMSPDToolkit.Extensions;

namespace RAMSPDToolkit.Tests.Extensions
{
    [TestClass]
    public class ExceptionExtensionsTests
    {
        [TestMethod]
        public void FullExceptionString()
        {
            var exceptionStr = "Some exception message.";

            try
            {
                throw new Exception(exceptionStr);
            }
            catch (Exception e)
            {
                var str = ExceptionExtensions.FullExceptionString(e);

                Assert.IsTrue(str.Contains("Info:"));
                Assert.IsTrue(str.Contains(exceptionStr));

                Assert.IsTrue(str.Contains("Details:"));
                Assert.IsTrue(str.Contains(nameof(ExceptionExtensionsTests)));
                Assert.IsTrue(str.Contains(nameof(FullExceptionString)));
            }
        }
    }
}
