using System;
using Xunit;
using LongArithmetics;
using ECDH;
using System.Collections.Generic;

namespace EllipticCurveTests {
    public class TestCurve {
        private EllipticCurve _curve;

        public TestCurve() {
            _curve = new EllipticCurve(7, 3, 1);
        }

        [Fact]
        public void TestFindPoints() {
            var pointsExpected = new List<Point> {
                new Point(-1, -1, _curve),
                new Point(0, 1, _curve),
                new Point(0, 6, _curve),
                new Point(2, 1, _curve),
                new Point(2, 6, _curve),
                new Point(3, 3, _curve),
                new Point(3, 4, _curve),
                new Point(4, 0, _curve),
                new Point(5, 1, _curve),
                new Point(5, 6, _curve),
                new Point(6, 2, _curve),
                new Point(6, 5, _curve)
            };

            Assert.Equal(pointsExpected, _curve.Points);
        }
    }
}
