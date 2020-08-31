using Plots

include("functions.jl")

assets_path = "$(@__DIR__)/../assets"

plot_fibo(n) = begin  
    (0:(n - 1), fibonacci(n)) |> plot
    title!("Fibonacci Sequence")
    xlabel!("N")
    ylabel!("Fibonacci(N)")
    savefig("$assets_path/fibonacci($n).png")
end

factor_data(n, base, filt=nothing) = begin
    0:(n - 1) |> 
    (x -> (collect(x), [factor(i, base) for i ∈ fibonacci(n)])) |>
    zip |>
    collect |>
    (x -> filt === nothing ? x : filter(a -> filt(a[2]), x)) |>
    unzip
end

plot_factor(n, base) = begin
    factor_data(n, base) |> plot
    title!("Factor $base in Fibonacci Numbers") 
    xlabel!("N")
    ylabel!("Factor $base in Fibonacci(N)")
    savefig("$assets_path/factor($n,$base).png")
end

plot_factor_except(n, base, except) = begin
    factor_data(n, base, x -> x % except ≠ 0) |> plot
    title!("Factor $base in Fibonacci Numbers except exponent $except")
    xlabel!("N")
    ylabel!("Factor $base in Fibonacci(N) except exponent $except")
    savefig("$assets_path/factor($n,$base)except($except).png")
end

plot_factor_only(n, base, only) = begin
    factor_data(n, base, x -> x % only == 0) |> plot
    title!("Factor $base in Fibonacci Numbers only for exponent $only")
    xlabel!("N")
    ylabel!("Factor $base in Fibonacci(N) only for exponent $only")
    savefig("$assets_path/factor($n,$base)only($only).png")
end

plot_factor(n, base::Vector) = begin
    p = factor_data(n, base[begin]) |> (a, b) -> plot(a, b, label="Base $(base[begin])")
    for i ∈ base[begin + 1:end - 1]
        data = factor_data(n, i)
        plot!(p, data..., label="Base $i")
    end
    factor_data(n, base[end]) |> (a, b) -> plot!(a, b, label="Base $(base[end])")
    title!("Factor $base in Fibonacci Numbers")
    xlabel!("N")
    ylabel!("Multiple factors in Fibonacci(N)")
    bases = map(x -> "$x", base) |> x -> join(x, ",")
    savefig("$assets_path/factor($n,[$bases]).png")
end


function plot_distance(n, base1, base2, e, op)
    (x1, _) = factor_data(n, base1) |>
        zip |>
        collect |>
        k -> filter(d -> d[2] == e, k) |> 
        unzip
    (x2, _) = factor_data(n, base2) |>
        zip |>
        collect |>
        k -> filter(d -> d[2] == e, k) |>
        unzip

    (length(x1), length(x2)) |>
    min |>
    (l -> (x1[begin:l], x2[begin:l])) |>
    op |> 
    enumerate |> 
    unzip |> 
    plot

    optitle = if (op == -) "Difference" else "Division" end
    title!("$optitle of factors $base1 and $base2 in Fibonacci(N) for exponent $e")
    xlabel!("N")
    ylabel!("$optitle(N)")
    opname = if (op == -) "minus" else "div" end
    savefig("$assets_path/distance($n,$base1,$base2,$e,$opname).png")
end