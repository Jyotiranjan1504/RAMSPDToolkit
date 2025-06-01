using RAMSPDToolkit.Utilities;

namespace RAMSPDToolkit.Tests.Utilities
{
    [TestClass]
    public class TemperatureConverterTests
    {
        [TestMethod]
        public void CelsiusToFahrenheit()
        {
            Assert.AreEqual(32, TemperatureConverter.CelsiusToFahrenheit(0));
        }

        [TestMethod]
        public void FahrenheitToCelsius()
        {
            Assert.AreEqual(0, TemperatureConverter.FahrenheitToCelsius(32));
        }
    }
}
