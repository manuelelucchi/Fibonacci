import Base.Filesystem

cwd = Filesystem.pwd()

run(`cd`, "$cwd\\alternative_representation")
run(`latex fibonacci_alternative_representation.tex`)