close all;
clear all;
clc;

% ---------------------------- BMP Lenna -----------------------------

img1 = imread('lab_6_images\0001.jpg');
img2 = imread('lab_6_images\0002.jpg');

% -------------------- Визуализация младшей битовой последовательности -------------------

[height, width, a1] = size(img1);
img1LeastSignificantBits = zeros(height, width);
for i = 1 : height
    for j = 1 : width
        img1LeastSignificantBits(i, j) = bitget(uint8(img1(i, j, 3)), 1);
    end
end
figure(3); imshow(img1LeastSignificantBits); title('Least significant bits of the first input image');

[height, width, a2] = size(img2);
img2LeastSignificantBits = zeros(height, width);
for i = 1 : height
    for j = 1 : width
        img2LeastSignificantBits(i, j) = bitget(uint8(img2(i, j, 3)), 1);
    end
end
figure(4); imshow(img2LeastSignificantBits); title('Least significant bits of the second input image');

% ------------------------- JPG Lenna ----------------------------------
K = 10000;
% находим ДКП коэффициенты изображения
frequencyDomain = dct2(img1(:, :, 3));
% вытягиваем матрицу в вектор
frequencyDomain = frequencyDomain(:);
% обрезаем ДКТ коэффициенты с частотами менее 20
frequencyDomain = frequencyDomain(abs(frequencyDomain) < 20);
% строим гистограмму по ДКП коэффициентам
figure(6); hist(frequencyDomain(:), K);
xlabel('Значение ДКП коэффициента');
ylabel('Кол-во ДКП коэффициентов');
title('Изображение №1')

% находим ДКП коэффициенты изображения
frequencyDomain = dct2(img2(:, :, 3));
frequencyDomain = frequencyDomain(:);
frequencyDomain = frequencyDomain(abs(frequencyDomain) < 20);
% строим гистограмму по ДКП коэффициентам
figure(8); hist(frequencyDomain(:), K);
xlabel('Значение ДКП коэффициента');
ylabel('Кол-во ДКП коэффициентов');
title('Изображение №2')
img1 = imread('lab_6_images\Lena_1.jpg');
img2 = imread('lab_6_images\Lena_2.jpg');

% ---------------------Анализ частотной гистограммы ---------------------------



% ------------------- Атака на основе критерия ХИ квадрат ------------------

% --------------- Разбитие изображения на блоки --------------------
BLOCK_NUMBER = 64; % кол-во блоков
BLOCK_SIZE = 8; % из-за размера, знаем сами
for i = 1 : 8
    for j = 1 : 8
        % выбираем блоки
        % (i * BLOCK_SIZE) : ((i + 1) * BLOCK_SIZE) - диапазон выбираемых
        % строк
        % (j * BLOCK_SIZE) : ((j + 1) * BLOCK_SIZE) - диапазон выбираемых
        % столбцов
        blocks(:, :, i, j) = img1((i * BLOCK_SIZE) : ((i + 1) * BLOCK_SIZE), ...
            (j * BLOCK_SIZE) : ((j + 1) * BLOCK_SIZE), 3);
    end
end

for i = 1 : 8
    for j = 1 : 8
        blocks1(:, :, i, j) = img2((i * BLOCK_SIZE) : ((i + 1) * BLOCK_SIZE), ...
            (j * BLOCK_SIZE) : ((j + 1) * BLOCK_SIZE), 3);
    end
end

for i = 1 : 8
    for j = 1 : 8
        
        % находим ДКП коэффициенты блока
        frequencyDomain = dct2(blocks(:, :, i, j));
        frequencyDomain = frequencyDomain(:);
        frequencyDomain = frequencyDomain(abs(frequencyDomain) < 20);
        % строим гистограмму по ДКП коэффициентам
        H1 = hist(frequencyDomain(:), 100);

        % находим ДКП коэффициенты блока
        frequencyDomain = dct2(blocks1(:, :, i, j));
        frequencyDomain = frequencyDomain(:);
        frequencyDomain = frequencyDomain(abs(frequencyDomain) < 20);
        % строим гистограмму по ДКП коэффициентам
        H2 = hist(frequencyDomain(:), 100);

        % image 1
        % на основе гистограммы находится ХИ квадрат для блока
        [a3, sizeH1] = size(H1);
        for k = 1 : 2 : (sizeH1 - 1)
            Z1(k) = (H1(k) + H1(k + 1)) / 2;
        end
        % ХИ квадрат для блока первого изображения
        XI21 = 0;
        v = 1;
        s = 0;
        for k = 1 : (sizeH1 - 1)
            if (Z1(k) ~= 0)
                XI21 = XI21 + (H1(k) - Z1(k))^2 / Z1(k);
            end
        end
        % image 2
        % на основе гистограммы находится ХИ квадрат для блока
        [a4, sizeH2] = size(H2);
        for k = 1 : 2 : (sizeH2 - 1)
            Z2(k) = (H2(k) + H2(k + 1)) / 2;
        end
        % ХИ квадрат для блока второго изображения
        XI22 = 0;
        v = 1;
        s1 = 0;
        for k = 1 : (sizeH2 - 1)
            if (Z2(k) ~= 0)
                XI22 = XI22 + (H2(k) - Z2(k))^2 / Z2(k);
            end
        end
        % ХИ квадраты записываются в матрицу
        XI2MATRIX1(i, j) = XI21;
        XI2MATRIX2(i, j) = XI22;
    end
end

% вытягиваем матрицы в вектора
XI2MATRIX1 = XI2MATRIX1(:);
XI2MATRIX2 = XI2MATRIX2(:);

% строим график разницы ХИ квадратов блоков изображений
% где ХИ квадрат маленький - там скрыто сообщение
%figure; plot(XI2MATRIX1 - XI2MATRIX2); title('Отличие Хи квадрата первого изображения от Хи квадрата второго изображения по блокам');
XI2DIFF = sum(XI2MATRIX1 - XI2MATRIX2)