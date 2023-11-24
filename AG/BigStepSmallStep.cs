using System.Numerics;

namespace AG;

public class BigStepSmallStep
{
    private static BigInteger ModExp(BigInteger baseValue, BigInteger exponent, BigInteger mod)
    {
        return BigInteger.ModPow(baseValue, exponent, mod);
    }

    public static BigInteger BigStepSmallStepAlgorithm(BigInteger g, BigInteger h, BigInteger p)
    {
        BigInteger n =
            (BigInteger.One +
                                     BigInteger.Divide(p - BigInteger.One, BigInteger.One + BigInteger.One));

        Dictionary<BigInteger, BigInteger> value = new Dictionary<BigInteger, BigInteger>();
        for (BigInteger j = BigInteger.Zero; j < n; j++)
        {
            BigInteger value1 = ModExp(g, j, p);
            value[value1] = j;
        }

        BigInteger gn = ModExp(g, n * (p - BigInteger.Add(BigInteger.One, BigInteger.One)), p);

        BigInteger cur = h;
        for (BigInteger i = BigInteger.Zero; i < n; i++)
        {
            if (value.ContainsKey(cur))
            {
                return i * n + value[cur];
            }
            else
            {
                cur = cur * gn % p;
            }
        }

        return BigInteger.MinusOne;
    }

    public static void Main()
    {
        BigInteger g = new BigInteger(2);
        BigInteger h = new BigInteger(22);
        BigInteger p = new BigInteger(29);

        BigInteger result = BigStepSmallStepAlgorithm(g, h, p);
        Console.WriteLine($"The discrete logarithm of {h} base {g} modulo {p} is: {result}");
    }
}
