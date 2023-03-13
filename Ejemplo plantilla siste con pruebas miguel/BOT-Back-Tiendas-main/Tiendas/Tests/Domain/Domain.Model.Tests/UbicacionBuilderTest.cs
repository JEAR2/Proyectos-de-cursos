using Domain.Model.Entities;

namespace Domain.Model.Tests
{
    public class UbicacionBuilderTest
    {
        private readonly decimal _latitud = 0;
        private readonly decimal _longitud = 0;

        public Ubicacion Build() => new(_latitud, _longitud);

        public UbicacionBuilderTest()
        {
        }

        public UbicacionBuilderTest(decimal latitud, decimal longitud)
        {
            _latitud = latitud;
            _longitud = longitud;
        }
    }
}