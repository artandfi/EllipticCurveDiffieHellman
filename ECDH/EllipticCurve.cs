using System.Collections.Generic;
using LongArithmetics;
using static LongArithmetics.LongNumber;

namespace ECDH {
    // y^2 = x^3 + Ax + B (mod P)
    public class EllipticCurve {
        private LongNumber _n = null;
        public LongNumber P { get; private set; }
        public LongNumber A { get; private set; }
        public LongNumber B { get; private set; }
        public LongNumber N { get => _n ?? Points.Count; }
        public Point G { get; set; }
        public List<Point> Points { get; private set; } = new List<Point>();

        public EllipticCurve(LongNumber p, LongNumber a, LongNumber b) {
            P = p;
            A = a;
            B = b;
            FindPoints();
        }

        public EllipticCurve(LongNumber p, LongNumber a, LongNumber b, LongNumber n) {
            P = p;
            A = a;
            B = b;
            _n = n;
        }

        public void FindPoints() {
            LongNumber y;
            LongNumber y2;
            
            Points.Add(new Point(-1, -1, this));
            
            for (LongNumber x = 0; x < P; x++) {
                y2 = (Pow(x, 3) + A * x + B) % P;
                y = YSqrt(y2);

                if (y != null) {
                    Points.Add(new Point(x, y, this));
                    
                    if (y != 0) {
                        Points.Add(new Point(x, -y % P, this));
                    }
                }
            }
        }

        private LongNumber YSqrt(LongNumber y2) {
            for (LongNumber y = 0; y < P; y++) {
                if (MulMod(y, y, P) == y2) {
                    return y;
                }
            }

            return null;
        }
    }
}
