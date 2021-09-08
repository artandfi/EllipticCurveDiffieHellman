using Xunit;
using ECDH;
using System.Collections.Generic;

namespace Tests {
    public class TestDiffieHellman {
        private EllipticCurve _curve;
        
        public TestDiffieHellman() {
            _curve = new EllipticCurve(23, 5, 3);
        }

        [Fact]
        public void TestDiffieHellman2Parties() {
            var dh = new DiffieHellman(_curve, "Alice", "Bob");
            
            dh.Run();
            AssertListElementsAreEqual(dh.CommonKeys);
        }

        [Fact]
        public void TestDiffieHellman3Parties() {
            var dh = new DiffieHellman(_curve, "Alice", "Bob", "Carol");

            dh.Run();
            AssertListElementsAreEqual(dh.CommonKeys);
        }

        [Fact]
        public void TestDiffieHellman4Parties() {
            var dh = new DiffieHellman(_curve, "Alice", "Bob", "Carol", "Dave");

            dh.Run();
            AssertListElementsAreEqual(dh.CommonKeys);
        }

        private void AssertListElementsAreEqual<T>(T[] list) {
            if (list.Length > 1) {
                for (int i = 1; i < list.Length; i++) {
                    Assert.Equal(list[i - 1], list[i]);
                }
            }
        }
    }
}
