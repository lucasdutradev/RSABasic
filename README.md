# RSABasic

Minimal RSA in C# for learning/experimentation.

## What it does
- Generates two small primes (`GeneratePrime`, Miller–Rabin style)
- Builds keys: `n = p*q`, `φ = (p−1)(q−1)`
- Picks the smallest `e` coprime to `φ`
- Computes `d = e⁻¹ mod φ` (extended Euclid)
- Encrypts/decrypts a hardcoded integer via `BigInteger.ModPow`

## Files
- `Program.cs` — all logic (keygen, GCD, mod inverse, primality test)

## Requirements
- .NET 8 SDK

## Run
```bash
git clone https://github.com/lucasdutradev/RSABasic.git
cd RSABasic
dotnet run
