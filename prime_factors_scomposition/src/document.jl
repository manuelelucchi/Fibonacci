include("plots.jl")

division(a, b) = a ./ b

function build_assets()    
    plot_fibo(100)
    plot_factor(100, 2)
    plot_factor(1000, 2)
    plot_factor(100, 3)
    plot_factor(100, 5)
    plot_factor(10000, 23)
    plot_factor(100, 4) 
    plot_factor_except(100, 2, 3)
    plot_factor_only(100, 2, 3)
    plot_factor(100, [2, 4])
    plot_factor(100, [2, 3])
    plot_distance(1000, 2, 4, 1, -)
    plot_distance(1000, 3, 9, 1, -)
    plot_distance(500, 2, 3, 1, -)
    plot_distance(1000, 2, 4, 1, division)
    plot_distance(1000, 3, 9, 1, division)
    plot_distance(500, 3, 9, 2, division)
    plot_distance(500, 2, 3, 1, division)
end

