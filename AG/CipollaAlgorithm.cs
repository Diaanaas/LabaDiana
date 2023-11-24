using System.Numerics;
using System.Security.Cryptography;

namespace AG;

public class CipollaAlgorithm
{
    private class Complex
    {
        public BigInteger Real { get; }
        public BigInteger Imag { get; }

        public Complex(BigInteger real, BigInteger imag)
        {
            Real = real;
            Imag = imag;
        }

        public Complex Multiply(Complex other, BigInteger p)
        {
            BigInteger r = (this.Real * other.Real + this.Imag * other.Imag * p) % p;
            BigInteger i = (this.Real * other.Imag + this.Imag * other.Real) % p;
            return new Complex(r, i);
        }

        public Complex Pow(BigInteger exponent, BigInteger p)
        {
            Complex result = new Complex(BigInteger.One, BigInteger.Zero);
            Complex baseValue = this;
            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                {
                    result = result.Multiply(baseValue, p);
                }
                exponent /= 2;
                baseValue = baseValue.Multiply(baseValue, p);
            }
            return result;
        }
    }

    public static BigInteger ModInverse(BigInteger a, BigInteger m)
    {
        return BigInteger.ModPow(a, m - 2, m);
    }

    public static BigInteger Cipolla(BigInteger n, BigInteger p)
    {
        BigInteger a;
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            do
            {
                byte[] bytes = new byte[p.ToByteArray().LongLength];
                rng.GetBytes(bytes);
                a = new BigInteger(bytes) % p;
            }
            while (BigInteger.ModPow(a * a - n, (p - 1) / 2, p) != p - 1);
        }

        Complex w = new Complex(a, BigInteger.One);
        Complex res = w.Pow((p + 1) / 2, p);
        return res.Real;
    }

    public static void Main()
    {
        BigInteger n = new BigInteger(10);
        BigInteger p = new BigInteger(13);

        BigInteger result = Cipolla(n, p);
        if (!result.IsZero)
        {
            Console.WriteLine($"A square root of {n} mod {p} is: {result}");
        }
        else
        {
            Console.WriteLine($"No square root exists for {n} mod {p}");
        }
    }
}