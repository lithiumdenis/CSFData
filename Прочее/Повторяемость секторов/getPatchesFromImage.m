function [p_x, p_y, patches] = getPatchesFromImage(im , border)
%getPatchesFromImage Выделяет 5х5 патчи из входного изображения.
%
%Аргументы:
%im - входное ЧБ изображение размера m х n.
%border - целое число, которое хотим задать в качестве неиспользуемой
%границы по краям изображения.
%p_x - (m - 2 · (2 + border)) * (n - 2 · (2 + border)) матрица с Х
%индексами центров патчей
%p_y - (m - 2 · (2 + border)) * (n - 2 · (2 + border)) матрица с Y
%индексами центров патчей
%patches - (m - 2 · (2 + border)) * (n - 2 · (2 + border)) * 5 * 5 патчи
%
%Пример: если border = 0, то центр первого патча находится в точке (3,3), а центр 
%последнего находится в точке (end - 2, end - 2). Так количество патчей,
%которое получится в итоге, равно (m - 4) * (n - 4) штук.
%А если же border = 1, то центр первого патча будет находиться в точке
%(4,4), центр последнего в (end - 3, end - 3). Т.е. тогда в общем случае
%число получаемых патчей будет вычисляться по формуле 
%(m - 2 · (2 + border)) * (n - 2 · (2 + border))

%Зададим размер патча
PATCH_SIZE = 5;
%Смещение на границах относительно border
offset = border + (PATCH_SIZE - 1) / 2;
%Размеры входной матрицы
[m, n] = size(im);
%Число строк в patches
m_patches = (m - 2 * offset);
%Число столбцов в patches
n_patches = (n - 2 * offset);
%Предварительное заполнение p_x и p_y
[p_x, p_y] = meshgrid(1:n_patches, 1:m_patches);
p_x = p_x + offset;
p_y = p_y + offset;
%Создание патчей
patches = zeros( m_patches, n_patches, 5, 5 );
for i=1:m_patches,
        for j=1:n_patches,
            patches(i,j,:,:) = im(i + border:i + border + PATCH_SIZE - 1,...
                                  j + border:j + border + PATCH_SIZE - 1);
        end
end
end