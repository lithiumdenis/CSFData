K = 1000;   % число реализаций
n = 12;
m = 1;  % mu
s = 4;  % sigma
r = rand(n, K);
x = m + s .* (sum(r) - 6);  % значения реализации ГСВ

figure; hold on;
t = 1 : K;
ms = zeros(1, K);
for k = 1 : K
    ms(k) = mean(x(1 : k));
end;
% X = repmat(x', [1, K]);
% X = triu(X);
% ms_ = sum(X) ./ t;
plot(t, ms);
plot(t, repmat(m, [1, K]), 'g');
title('Выборочное Среднее');

figure; hold on;
% ds = zeros(1, K);
% for k = 1 : K
%     ds(k) = var(x(1 : k));
% end;
X = repmat(x', [1, K]);
ds_ = sum(triu(X - repmat(ms, [K, 1])) .^ 2) ./ t;
plot(t, ds_);
plot(t, repmat(s ^ 2, [1, K]), 'g');
title('Выборочная Дисперсия');