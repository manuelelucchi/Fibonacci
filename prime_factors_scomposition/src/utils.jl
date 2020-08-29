import Base.|>

|>(x::Tuple, f) = f(x...)

|>(x, f) = f(x)