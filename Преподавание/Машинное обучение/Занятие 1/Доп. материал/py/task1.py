import math

def fib(n):
    if n==1 or n==2:
        return 1
    return fib(n-1) + fib(n-2)

def fac(n):
     if n == 0:
          return 1
     return fac(n-1) * n

var1 = fib(17)
var2 = fac(8)
print(var1)
print(var2)

var3 = (var2 / var1) * math.pi

print(var3)



