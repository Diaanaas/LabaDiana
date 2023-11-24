using System.Numerics;

namespace AG;

public class SymbolCalculator
{
    public static int LegendreSymbol(BigInteger a, BigInteger p) {
        BigInteger result = BigInteger.ModPow(a, (p - 1) / 2, p);
        if (result == 1) return 1;
        if (result == p - 1) return -1;
        return 0;
    }

    public static int JacobiSymbol(BigInteger a, BigInteger n) {
        if (a == 1) return 1;
        if (a == 0) return 0;
        if (a % 2 == 0) {
            int s = JacobiSymbol(a / 2, n) * ((n % 8 == 1 || n % 8 == 7) ? 1 : -1);
            return s;
        }
        int t = JacobiSymbol(n % a, a) * ((a % 4 == 1 || n % 4 == 1) ? 1 : -1);
        return t;
    }

    public static void Main() {
        BigInteger a = new BigInteger(65537);
        BigInteger p = new BigInteger(97);
        BigInteger n = new BigInteger(121);

        // Output the results
        Console.WriteLine("The Legendre symbol (a|p) is: " + LegendreSymbol(a, p));
        Console.WriteLine("The Jacobi symbol (a|n) is: " + JacobiSymbol(a, n));
    }
}