using System;
using System.Collections.Generic;
using LongArithmetics;
using static LongArithmetics.LongNumber;

namespace ECDH {
    public class DiffieHellman {
        public EllipticCurve Curve { get; private set; }
        public string[] Parties { get; private set; }
        public LongNumber[] PrivateKeys { get; private set; }
        public Point[] CommonKeys { get; private set; }

        public DiffieHellman(EllipticCurve curve, params string[] parties) {
            Curve = curve;
            Parties = parties;
            PrivateKeys = new LongNumber[parties.Length];
            CommonKeys = new Point[parties.Length];
        }

        public void Run() {
            InitBasePoint();
            InitPrivateKeys();
            ExchangePublicKeys();
        }

        private void InitBasePoint() {
            var zero = new Point(0, 0, Curve);

            do {
                Curve.G = Curve.Points[Rand(1, Curve.N)];
            } while (Curve.G == zero);
        }

        private void InitPrivateKeys() {
            for (int i = 0; i < Parties.Length; i++) {
                LongNumber key = Rand(1, Curve.N);
                PrivateKeys[i] = key;
                Console.WriteLine($"{Parties[i]}'s private key is {key}");
            }

            Console.WriteLine("------");
        }

        private void ExchangePublicKeys() {
            Console.WriteLine($"Base point is G = {Curve.G}");
            Console.WriteLine("------");

            for (int i = 0; i < Parties.Length; i++) {
                Point point = Curve.G;
                Point commonKey = PrivateKeys[i] * point;
                Point prevKey = new Point();

                for (int j = 1; j < Parties.Length; j++) {
                    int curr = (i + j - 1) % PrivateKeys.Length;
                    int next = (i + j) % PrivateKeys.Length;

                    Console.WriteLine($"{Parties[curr]}: {PrivateKeys[curr]} * {point} = {commonKey}");
                    Console.WriteLine($"{Parties[curr]} -{commonKey}-> {Parties[next]}");
                    Console.WriteLine();
                    prevKey = commonKey;
                    commonKey *= PrivateKeys[next];
                    point = commonKey;
                }

                int prev = (i + PrivateKeys.Length - 1) % PrivateKeys.Length;

                Console.WriteLine($"{Parties[prev]} calculated K = {PrivateKeys[prev]} * {prevKey} = {commonKey}");
                Console.WriteLine("------");
                CommonKeys[prev] = commonKey;
            }
        }
    }
}
