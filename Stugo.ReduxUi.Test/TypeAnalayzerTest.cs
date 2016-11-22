using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stugo.ReduxUi.Test
{
    public class TypeAnalayzerTest
    {
        [Fact]
        public void GetAllTypes_returns_all_types()
        {
            var type = typeof(Z);

            var types = TypeAnalayser.GetAllTypes(type).OrderBy(x => x.Name);

            var expected = new Type[]
            {
                typeof(IA), typeof(IB), typeof(IC),
                typeof(X), typeof(Y), typeof(Z),
                typeof(object)
            }.OrderBy(x => x.Name);

            Assert.Equal(expected, types);
        }



        private interface IA { }

        private interface IB : IA { }

        private interface IC { }

        private class X { }
        private class Y : X, IC { }
        private class Z : Y, IB { }
    }
}
