using System.Numerics;

namespace AG;

public class NumberFunctions {
    public static BigInteger Gcd(BigInteger a, BigInteger b) {
        return b == BigInteger.Zero ? a : Gcd(b, a % b);
    }

    public static BigInteger EulerTotient(BigInteger n) {
        BigInteger result = BigInteger.One;
        for (BigInteger i = 2; i < n; i++) {
            if (Gcd(i, n) == BigInteger.One) {
                result++;
            }
        }
        return result;
    }

    public static int Mobius(BigInteger n) {
        if (n == BigInteger.One) return 1;
        BigInteger p = 2;
        int primeFactors = 0;
        while (p * p <= n) {
            if (n % p == BigInteger.Zero) {
                n /= p;
                primeFactors++;
                if (n % p == BigInteger.Zero) return 0;  
            }
            p = p == 2 ? 3 : p + 2;
        }
        if (n > 1) primeFactors++;  
        return primeFactors % 2 == 0 ? 1 : -1;
    }

    public static BigInteger Lcm(List<BigInteger> nums) {
        BigInteger result = BigInteger.One;
        foreach (BigInteger num in nums) {
            result = result * num / Gcd(result, num);
        }
        return result;
    }
    public static void Main() {
        BigInteger n = new BigInteger(12345);

        Console.WriteLine("Euler's Totient of " + n + " is " + EulerTotient(n));
        Console.WriteLine("Möbius function of " + n + " is " + Mobius(n));

        List<BigInteger> nums = new List<BigInteger> { n, n + 1, n + 2 };
        Console.WriteLine("LCM of the set is " + Lcm(nums));
    }
}