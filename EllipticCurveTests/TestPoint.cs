using Xunit;
using ECDH;

namespace EllipticCurveTests {
    public class TestPoint {
        private Point INF;
        private EllipticCurve _curve;

        public TestPoint() {
            _curve = new EllipticCurve(13, 7, 2);
            INF = new Point(-1, -1, _curve);
        }

        [Fact]
        public void TestPointUnaryMinus1() {
            Assert.Equal(INF, -INF);
        }

        [Fact]
        public void TestPointUnaryMinus2() {
            var p = new Point(9, 12, _curve);
            var pNeg = new Point(9, 1, _curve);
            Assert.Equal(pNeg, -p);
        }

        [Fact]
        public void TestPointUnaryMinus3() {
            var p = new Point(6, 0, _curve);
            Assert.Equal(p, -p);
        }

        [Fact]
        public void TestPointEquality() {
            var p1 = new Point(4, 9, _curve);
            var p2 = new Point(4, 9, _curve);
            Assert.True(p1 == p2);
        }

        [Fact]
        public void TestPointInequality() {
            var p1 = new Point(4, 4, _curve);
            var p2 = new Point(7, 2, _curve);
            Assert.True(p1 != p2);
        }

        [Fact]
        public void TestPointAddition1() {
            var p1 = INF;
            var p2 = new Point(1, 6, _curve);
            Assert.Equal(p2, p1 + p2);
        }

        [Fact]
        public void TestPointAddition2() {
            var p1 = new Point(7, 2, _curve);
            var p2 = INF;
            Assert.Equal(p1, p1 + p2);
        }

        [Fact]
        public void TestPointAddition3() {
            var p = new Point(6, 0, _curve);
            Assert.Equal(INF, p + p);
        }

        [Fact]
        public void TestPointAddition4() {
            var p = new Point(4, 9, _curve);
            var sum = new Point(9, 1, _curve);
            Assert.Equal(sum, p + p);
        }

        [Fact]
        public void TestPointAddition5() {
            var p1 = new Point(7, 2, _curve);
            var p2 = new Point(7, 11, _curve);
            Assert.Equal(INF, p1 + p2);
        }

        [Fact]
        public void TestPoindAddition6() {
            var p1 = new Point(6, 0, _curve);
            var p2 = new Point(9, 1, _curve);
            var sum = new Point(1, 6, _curve);
            Assert.Equal(sum, p1 + p2);
        }

        [Fact]
        public void TestMultiplyingPointByScalar1() {
            var p = new Point(9, 12, _curve);
            Assert.Equal(p, 1 * p);
        }

        [Fact]
        public void TestMultiplyingPointByScalar2() {
            var p = new Point(7, 2, _curve);
            var res = new Point(9, 1, _curve);
            Assert.Equal(res, 3 * p);
        }
    }
}
