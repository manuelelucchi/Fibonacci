include("plots.jl")

plot_fibo(100)

#= z = factor_data(500, 2) |>
    zip |>
    collect |>
    k -> filter(d -> d[1] == 1, k) =#

# filter((x, y) -> y == 1, z)

# plot_distance(100, 2, 3, 1, -)