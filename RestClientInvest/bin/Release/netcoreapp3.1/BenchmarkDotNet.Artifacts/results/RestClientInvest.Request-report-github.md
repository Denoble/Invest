``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.17763.973 (1809/October2018Update/Redstone5)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT


```
|      Method |     Mean |     Error |   StdDev |   Median |      Min |      Max |
|------------ |---------:|----------:|---------:|---------:|---------:|---------:|
| MainRequest | 6.981 ms | 0.5492 ms | 1.619 ms | 5.825 ms | 5.618 ms | 9.750 ms |
