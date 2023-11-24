namespace AG;

public class EllipticCurve
{
   public static void Main()
    {
        Console.WriteLine("P: ");
        int p = int.Parse(Console.ReadLine());
        int n = p;

        int[,] LHS = new int[2, n];
        int[,] RHS = new int[2, n];

        Console.WriteLine("a: ");
        int a = int.Parse(Console.ReadLine());
        Console.WriteLine("b: ");
        int b = int.Parse(Console.ReadLine());

        Console.WriteLine($"elliptic curve: y^2 mod {p} = (x^3  + {a}*x + {b}) mod {p}");

        List<int> arr_x = new List<int>();
        List<int> arr_y = new List<int>();

        for (int i = 0; i < n; i++)
        {
            LHS[0, i] = i;
            RHS[0, i] = i;
            LHS[1, i] = (int)((Math.Pow(i, 3) + a * i + b) % p);
            RHS[1, i] = (i * i) % p;
        }

        int in_c = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (LHS[1, i] == RHS[1, j])
                {
                    in_c++;
                    arr_x.Add(LHS[0, i]);
                    arr_y.Add(RHS[0, j]);
                }
            }
        }

        Console.WriteLine("\npoints are:");
        for (int i = 0; i < in_c; i++)
        {
            Console.WriteLine($"{i + 1}\t{arr_x[i]} , {arr_y[i]}");
        }

        Console.WriteLine($"Base: {arr_x[0]},{arr_y[0]}");

        Console.WriteLine("Enter the random number d");
        int d = int.Parse(Console.ReadLine());

        int Qx = d * arr_x[0];
        int Qy = d * arr_y[0];

        Console.WriteLine("Enter the random number k");
        int k = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the message to be sent");
        int M = int.Parse(Console.ReadLine());
        Console.WriteLine("The message: " + M);

        int c1x = k * arr_x[0];
        int c1y = k * arr_y[0];
        Console.WriteLine($"Value of C1: {c1x}, {c1y}");

        int c2x = k * Qx + M;
        int c2y = k * Qy + M;
        Console.WriteLine($"Value of C2: {c2x}, {c2y}");

        Console.WriteLine("The message received:");
        int Mx = c2x - d * c1x;
        Console.WriteLine(Mx);
    }
}