using System;
using LongArithmetics;
using static LongArithmetics.LongNumber;

namespace ECDH {
    class Program {
        static void Main() {
            var curve = new EllipticCurve(53, 41, 36);
            var dh = new DiffieHellman(curve, "Alice", "Bob", "Carol", "Dave");
            dh.Run();
        }
    }
}
