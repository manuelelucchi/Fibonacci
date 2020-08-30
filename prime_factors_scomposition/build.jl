include("src/document.jl")

import Base.Filesystem

build_assets()

run(`pdflatex fibonacci_prime_factors_scomposition.tex`)