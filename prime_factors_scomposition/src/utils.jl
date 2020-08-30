import Base.|>

|>(x::Tuple, f) = f(x...)

|>(x, f) = f(x)

unzip(a) = map(x -> getfield.(a, x), fieldnames(eltype(a)))