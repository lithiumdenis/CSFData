clc; clear;
K = 1000;                       % Count of realizations
s = 4;                          % Sigma
r = rand(1, K);                 % Alpha from generation algorithm
x = s .* sqrt(-2 .* log(r));    % Generation algorithm

%Dispersion
dispValue = (2 - (pi ./ 2)) .* (s .^ 2);

figure; hold on;
t = 1 : K;
ds = zeros(1, K);
for k = 1 : K
    ds(k) = var(x(1 : k));
end;
plot(t, ds);
plot(t, repmat(dispValue, [1, K]), 'g');
title('Выборочная Дисперсия');