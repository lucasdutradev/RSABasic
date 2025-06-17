using System.Numerics;
using System.Security.Cryptography;

var numeroASerCriptografado = 777;

var prime = GeneratePrimeTwoNumbers(numeroASerCriptografado);

var n = prime.primoInicialp * prime.primoInicialq;

var totienFunction = (prime.primoInicialp - 1) * (prime.primoInicialq - 1);


var choseE = 0;

for (int e = 2; e < totienFunction; e++)
{
  if (GCD(e, totienFunction) == 1)
  {
    choseE = e;
    break;
  }
}


var inverseMod = ModInverse(choseE, totienFunction);


var publicKey = (n, choseE);
var privateKey = (n, inverseMod);

var crypt = BigInteger.ModPow(numeroASerCriptografado, publicKey.choseE, publicKey.n);

Console.WriteLine($"Resultado da criptografia RSA: {crypt}");

var decrypt = BigInteger.ModPow(crypt, privateKey.inverseMod, privateKey.n);

Console.WriteLine($"Resultado do Decript RSA: {decrypt}");


int ModInverse(int e, int phi)
{
  int t = 0, newT = 1;
  int r = phi, newR = e;

  while (newR != 0)
  {
    int quotient = r / newR;

    (t, newT) = (newT, t - quotient * newT);
    (r, newR) = (newR, r - quotient * newR);
  }

  if (r > 1) throw new Exception("e não tem inverso modular");
  if (t < 0) t += phi;

  return t;
}


int GCD(int a, int b)
{
  while (b != 0)
  {
    int temp = b;
    b = a % b;
    a = temp;
  }
  return a;
}


(int primoInicialp, int primoInicialq) GeneratePrimeTwoNumbers(int leastThen)
{

  var primoInicialp = 2;
  var primoInicialq = 3;

  while (primoInicialp * primoInicialq < leastThen || primoInicialp == primoInicialq)
  {
    primoInicialp = GeneratePrime(16);
    primoInicialq = GeneratePrime(16);
  }
  return (primoInicialp, primoInicialq);
}

bool IsPrime(int n, int k = 5)
{
  if (n < 2 || n % 2 == 0) return n == 2;

  int s = 0;
  int d = n - 1;
  while (d % 2 == 0)
  {
    d /= 2;
    s++;
  }

  for (int i = 0; i < k; i++)
  {
    int a = Random.Shared.Next(2, n - 2);
    BigInteger x = BigInteger.ModPow(a, d, n);
    if (x == 1 || x == n - 1) continue;

    bool continueLoop = false;
    for (int r = 1; r < s; r++)
    {
      x = BigInteger.ModPow(x, 2, n);
      if (x == n - 1)
      {
        continueLoop = true;
        break;
      }
    }

    if (!continueLoop) return false;
  }

  return true;
}


int GeneratePrime(int bitLength)
{
  RandomNumberGenerator rnd = RandomNumberGenerator.Create();
  while (true)
  {
    var bytes = new byte[4];
    rnd.GetBytes(bytes);

    int candidate = BitConverter.ToInt32(bytes, 0) & ((1 << bitLength) - 1);
    candidate = Math.Abs(candidate) | 1;


    if (IsPrime(candidate))
      return candidate;
  }
}

