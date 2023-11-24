
namespace AG;

using System;
using System.Numerics;
using System.Security.Cryptography;

public class RSA {
    private BigInteger n, d, e;

    public RSA(int bits) {
        BigInteger p = GenerateLargePrime(bits / 2);
        BigInteger q = GenerateLargePrime(bits / 2);
        n = p * q;

        BigInteger phi = (p - 1) * (q - 1);
        e = 65537; // Common choice for e
        d = ModInverse(e, phi);
    }

    public BigInteger Encrypt(BigInteger message) {
        return BigInteger.ModPow(message, e, n);
    }

    public BigInteger Decrypt(BigInteger ciphertext) {
        return BigInteger.ModPow(ciphertext, d, n);
    }

    private BigInteger GenerateLargePrime(int bits) {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] bytes = new byte[bits / 8];
        BigInteger prime;

        do {
            rng.GetBytes(bytes);
            bytes[bytes.Length - 1] &= (byte)0x7F; 
            prime = new BigInteger(bytes);
            prime |= 1; // Make it odd
        } while (!IsProbablyPrime(prime, 10));

        return prime;
    }

    private bool IsProbablyPrime(BigInteger value, int witnesses) {
        if (value < 2 || value % 2 == 0)
            return value == 2;

        BigInteger d = value - 1;
        int s = 0;

        while (d % 2 == 0) {
            d /= 2;
            s += 1;
        }

        Random rnd = new Random();
        for (int i = 0; i < witnesses; i++) {
            BigInteger a = RandomBigIntegerBelow(value - 1) + 1;
            BigInteger x = BigInteger.ModPow(a, d, value);
            if (x == 1 || x == value - 1)
                continue;

            for (int r = 1; r < s; r++) {
                x = BigInteger.ModPow(x, 2, value);
                if (x == 1) return false;
                if (x == value - 1) break;
            }

            if (x != value - 1) return false;
        }

        return true;
    }

    private BigInteger RandomBigIntegerBelow(BigInteger max) {
        byte[] bytes = max.ToByteArray();
        BigInteger result;

        do {
            Random rnd = new Random();
            rnd.NextBytes(bytes);
            bytes[bytes.Length - 1] &= 0x7F; 
            result = new BigInteger(bytes);
        } while (result < 0 || result >= max);

        return result;
    }

    private BigInteger ModInverse(BigInteger a, BigInteger n) {
        BigInteger i = n, v = 0, d = 1;

        while (a > 0) {
            BigInteger t = i / a, x = a;
            a = i % x;
            i = x;
            x = d;
            d = v - t * x;
            v = x;
        }

        v %= n;
        if (v < 0) v = (v + n) % n;
        return v;
    }

    public static void Main() {
        RSA rsa = new RSA(512);

        BigInteger message = new BigInteger(1234567890);
        BigInteger ciphertext = rsa.Encrypt(message);
        Console.WriteLine("Encrypted message: " + ciphertext);

        BigInteger decryptedMessage = rsa.Decrypt(ciphertext);
        Console.WriteLine("Decrypted message: " + decryptedMessage);
    }
}
