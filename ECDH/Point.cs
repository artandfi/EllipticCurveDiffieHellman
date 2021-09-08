using LongArithmetics;
using static LongArithmetics.LongNumber;

namespace ECDH {
    // Elliptic curve point, (-1, -1) corresponds to infinity point
    public struct Point {
        public LongNumber X { get; set; }
        public LongNumber Y { get; set; }
        public EllipticCurve Curve { get; set; }
        public LongNumber P { get => Curve.P; }
        public LongNumber A { get => Curve.A; }
        public LongNumber B { get => Curve.B; }

        public Point(LongNumber x, LongNumber y, EllipticCurve curve) {
            X = x;
            Y = y;
            Curve = curve;
        }

        public static Point operator -(Point a) {
            if (a.IsInf()) {
                return new Point(-1, -1, a.Curve);
            }

            var n = -a.Y % a.P;

            return new Point(a.X, -a.Y % a.P, a.Curve);
        }

        public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Point a, Point b) => !(a == b);

        public static Point operator +(Point a, Point b) {
            if (a.IsInf())
                return b;
            if (b.IsInf())
                return a;

            LongNumber s;
            if (a.X == b.X) {
                if (a.Y != b.Y || a.Y == 0) {
                    return new Point(-1, -1, a.Curve);
                }

                s = ((3 * a.X * a.X + a.A) * MulInverse(2 * a.Y, a.P)) % a.P;
            }
            else {
                s = ((b.Y - a.Y) * MulInverse(b.X - a.X, a.P)) % a.P;
            }

            LongNumber resX = (s * s - a.X - b.X) % a.P;
            LongNumber resY = (s * (a.X - resX) - a.Y) % a.P;

            return new Point(resX, resY, a.Curve);
        }

        public static Point operator *(LongNumber k, Point p) {
            Point res = p;

            for (int i = 1; i < k; i++) {
                res += p;
            }

            return res;
        }

        public static Point operator *(Point p, LongNumber k) => k * p;

        public bool IsInf() {
            return X == -1 && Y == -1;
        }

        public override string ToString() {
            return "(" + X.ToString() + "," + Y.ToString() + ")";
        }

        public override bool Equals(object obj) {
            if (obj is Point) {
                return this == (Point)obj;
            }
            return false;
        }
    }
}
