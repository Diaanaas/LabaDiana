using System.Numerics;

namespace AG;

using System;
using System.Numerics;

public class PollardsRhoAlg
{
    private static readonly Random random = new Random();

    private static BigInteger Gcd(BigInteger a, BigInteger b)
    {
        return BigInteger.GreatestCommonDivisor(a, b);
    }

    public static BigInteger PollardsRho(BigInteger n)
    {
        if (n % 2 == 0) return 2;

        BigInteger x = BigIntegerRandom(n);
        BigInteger y = x;
        BigInteger c = BigIntegerRandom(n);
        BigInteger d = 1;

        while (d == 1)
        {
            x = BigInteger.ModPow(x, 2, n) + c % n;
            y = BigInteger.ModPow(y, 2, n) + c % n;
            y = BigInteger.ModPow(y, 2, n) + c % n;
            d = Gcd(BigInteger.Abs(x - y), n);
        }

        return d;
    }

    private static BigInteger BigIntegerRandom(BigInteger n)
    {
        byte[] bytes = n.ToByteArray();
        BigInteger result;

        do
        {
            random.NextBytes(bytes);
            bytes[bytes.Length - 1] &= 0x7F; // Ensure the number is positive
            result = new BigInteger(bytes);
        }
        while (result < 0 || result >= n);

        return result % 50; // Restricting the random number to be less than 50
    }

    public static void Main()
    {
        BigInteger n = 104729;

        Console.WriteLine($"One non-trivial factor of {n} is: {PollardsRho(n)}");
    }
}

