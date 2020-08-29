using Plots

include("functions.jl")

plot_fibo(n) = begin  
    (0:(n - 1), fibonacci(n)) |> plot
    title!("Titolo")
    xlabel!("X")
    ylabel!("Y")
    savefig("myplot.png")
end

factor_data(n, base, filt=nothing) = 0:(n - 1) |> 
    (x -> filt === nothing ? x : filter(filt, x)) |> 
    (x -> (x, [factor(i, base) for i ∈ fibonacci(n)]))

plot_factor(n, base) = begin
    factor_data(n, base) |> plot
    title!("Titolo") 
    xlabel!("X")
    ylabel!("Y")
    savefig("myplot.png")
end

plot_factor_except(n, base, except) = begin
    factor_data(n, base, x -> x % except ≠ 0) |> plot
    title!("Titolo")
    xlabel!("X")
    ylabel!("Y")
    savefig("myplot.png")
end

plot_factor_only(n, base, only) = begin
    factor_data(n, base, x -> x % only == 0) |> plot
    title!("Titolo")
    xlabel!("X")
    ylabel!("Y")
    savefig("myplot.png")
end

plot_factor(n, base::Vector) = begin
    p = factor_data(n, base[begin]) |> plot
    for i ∈ base[begin + 1:end]
        data = factor_data(n, i)
        plot!(p, data...)
    end
    title!("Titolo")
    xlabel!("X")
    ylabel!("Y")
    savefig("myplot.png")
end

plot_distance(n, base1, base2, e, op) = begin
    (x1, _) = factor_data(n, base1) |>
        ((x, y) -> filter(k -> k == 1, y))
    (x2, _) = factor_data(n, base2) |>
        (v -> filter((x, y) -> y == e, v))
    (x1, x2) |> op |> enumerate |> collect |> (x -> begin
        print(x)
        plot(x)
    end) 
    title!("Titolo")
    xlabel!("X")
    ylabel!("Y")
    savefig("myplot.png")
end