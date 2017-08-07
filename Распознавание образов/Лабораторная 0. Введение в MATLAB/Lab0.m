clc;
clear;
a = 1;
V = [1 2 3];
M = [1 2; 3 4];
N = 10;
z  = zeros(N, 1);
zt = zeros(1, N);
zm = zeros(N);
zo = ones(N);
E = eye(N);
R = rand(N);
Ru = triu(R);
Rl = tril(R);
d = diag(R);
D = diag(d);
K = 10;
x = 1:K;
y = K:-2:-K;
M = 5; N = 6;
A = rand(K);
B = A(1:M, 2:N);
C = sqrt(B);
B2 = B .^ 2;
D = B * C;
d = B .* C;

a = 0;
if(a > 0)
    b = 2;
else
    b = 3;
end;

x = zeros(10, 1);
for i = 1:10
    x(i) = i;
end;
for j = 10:-2:1
    x(j) = -x(j);
end;

K = 5;
A = rand(K);
ap = A > 0.5;
am = A < 0.5;

B = A .^ 2 .* ap + sqrt(A) .* am;

for i = 1:K
    for j = 1:K
        if A(i,j) > 0.5
            B(i,j) = A(i,j)^2;
        elseif A(i,j) < 0.5
            B(i,j) = sqrt(A(i,j));
        end;
    end;
end
            
r = 1:K;
c = (1:K)';
R = repmat(r, [4, 1]);
C = repmat(c, [1, K]);

clear all;

m = [-2, 3];
a1 = [0; 0];
a2 = [-1; 0];
a3 = [0; 1];
a4 = [-1; 1];
a = [a1, a2, a3, a4];
ms = repmat(m, [1,4]);
d = sqrt(sum((ms - a) .^ 2));

v = (1:10)';
su = sum(v);
pro = prod(v);
me = mean(v); %avg
de = dev(v);% dispersia






