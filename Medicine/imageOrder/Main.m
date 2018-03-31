clc; clear;

%Получаем список папок nameFolds
d = dir(pwd);
isub = [d(:).isdir];
nameFolds = {d(isub).name}';
nameFolds(ismember(nameFolds,{'.','..'})) = [];


lsize = size(nameFolds);

for i = 1:lsize(1)
    tic;
    CodeDiff(nameFolds{i,1});        
            
    time = toc;
    fprintf(1, 'Этап: %d (из %d). Время выполнения: %g секунд\n', i, lsize(1), time);
end