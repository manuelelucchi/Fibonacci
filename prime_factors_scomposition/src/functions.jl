using Lazy
import Primes

include("utils.jl")

fibs = @lazy BigInt(0):BigInt(1):(fibs + drop(BigInt(1), fibs));

fibonacci(n) = (n, fibs) |> take |> collect

fibonaccinum(n) = fibonacci(n)[end]

factor(n, base) = begin
    counter = 0
    if base == 0 || base == 1 || n == 0 return 0 end
    val = n
    while val % base == 0
        counter ++
        val = val / base    
    end
    return counter
end