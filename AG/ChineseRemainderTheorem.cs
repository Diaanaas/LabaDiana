using System.Numerics;

namespace AG;

public class ChineseRemainderTheorem
{
    public static BigInteger ModInverse(BigInteger a, BigInteger m)
    {
        BigInteger m0 = m;
        BigInteger y = 0, x = 1;

        if (m == 1)
            return 0;

        while (a > 1)
        {
            BigInteger q = a / m;
            BigInteger t = m;
            
            m = a % m;
            a = t;
            t = y;
            
            y = x - q * y;
            x = t;
        }
        
        if (x < 0)
            x += m0;

        return x;
    }

    public static BigInteger ChineseRemainder(BigInteger[] num, BigInteger[] rem)
    {
        int k = num.Length;
        BigInteger prod = BigInteger.One;
        for (int i = 0; i < k; i++)
        {
            prod *= num[i];
        }

        BigInteger result = BigInteger.Zero;
        for (int i = 0; i < k; i++)
        {
            BigInteger pp = prod / num[i];
            result += rem[i] * ModInverse(pp, num[i]) * pp;
        }

        return result % prod;
    }

    public static void Main()
    {
        BigInteger[] num = {
            new BigInteger(3),
            new BigInteger(4),
            new BigInteger(5)
        };
        BigInteger[] rem = {
            new BigInteger(2),
            new BigInteger(3),
            new BigInteger(1)
        };

        Console.WriteLine("The solution to the system of congruences is: " + ChineseRemainder(num, rem));
    }
}