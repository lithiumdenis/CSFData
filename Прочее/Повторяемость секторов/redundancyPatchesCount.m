function countRedundancy = redundancyPatchesCount( im )
%redundancyPatchesCount находит количество повторяющихся секторов в
%чёрно-белом изображении
%   Аргументы:
%   im - входное чёрно-белое изображение
%   countRedundancy - количество повторяющихся секторов

%Обнуляем переменную для подсчёта повторений
countRedundancy = 0;
%Получаем сектора изображения функцией основного приложения
[pxIm, pyIm, imPatches] = getPatchesFromImage(im, 0);

%Область патча
PATCH_AREA = 5 * 5;
%Выявляем размеры для imPatches
[h, w, ~] = size(imPatches);
%Считаем число патчей в imPatches
N = h * w;
%Превращаем секторы 5 на 5 в векторы 25
imPatches_flat = reshape(imPatches, N, PATCH_AREA);

%Округлили до 2 знака после запятой, чтобы было более похоже
imPatches_flat_rounded = roundn(imPatches_flat,-2);

%Сравниваем два патча, т.е. каждый с каждым
for i=1:N
    for j=1:N
        if(imPatches_flat_rounded(i,:) == imPatches_flat_rounded(j,:) )
            if(j ~= i) %Исключаем эквивалентность самому себе
                countRedundancy = countRedundancy + 1;
            end
        end
    end
end
%Исключаем выявление двух одинаковых два раза
%т.е. по одному массиву сначала находим два одинаковых,
%а потом по второму тот же, но в другом порядке
countRedundancy = countRedundancy ./ 2;