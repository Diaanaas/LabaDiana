using System.Numerics;
using System.Security.Cryptography;

namespace AG;

public class MillerRabin
{
    private static readonly RandomNumberGenerator random = RandomNumberGenerator.Create();

    private static BigInteger ModExp(BigInteger @base, BigInteger exp, BigInteger mod)
    {
        return BigInteger.ModPow(@base, exp, mod);
    }

    public static bool IsPrime(BigInteger n, int iterations)
    {
        if (n == 2 || n == 3)
        {
            return true;
        }
        if (n < 2 || n % 2 == 0)
        {
            return false;
        }

        BigInteger d = n - 1;
        int r = 0;
        while (d % 2 == 0)
        {
            d /= 2;
            r++;
        }

        for (int i = 0; i < iterations; i++)
        {
            BigInteger a = BigIntegerZeroToOne(n);
            BigInteger x = ModExp(a, d, n);
            if (x == 1 || x == n - 1)
            {
                continue;
            }
            int j = 0;
            for (; j < r - 1; j++)
            {
                x = ModExp(x, 2, n);
                if (x == n - 1)
                {
                    break;
                }
            }
            if (j == r - 1)
            {
                return false;
            }
        }
        return true;
    }

    private static BigInteger BigIntegerZeroToOne(BigInteger n)
    {
        byte[] bytes = new byte[n.ToByteArray().LongLength];
        BigInteger result;
        do
        {
            random.GetBytes(bytes);
            result = new BigInteger(bytes) % (n - 1) + 1;
        }
        while (result <= 0 || result >= n);
        return result;
    }

    public static void Main()
    {
        BigInteger n = new BigInteger(104729);
        int iterations = 5;

        Console.WriteLine($"{n} is prime: {IsPrime(n, iterations)}");
    }
}