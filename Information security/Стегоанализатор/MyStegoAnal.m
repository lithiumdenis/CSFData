clc; clear;

%%%%%%%%%%%¬изуальна€ атака: повышение LSBs
% Ёта программа устран€ет все 7 старших битов дл€ каждого пиксел€, 
% кроме последнего наименее значащего (LSB). 
% “аким образом, все байты изображени€ принимают значени€ 0 или 1. 
% ѕроблема в том, что 0 или 1 дл€ диапазона значений из 256 цветов 
% будет незаметно глазу. ѕоэтому программа усиливает эти наименее 
% значащие биты. ƒл€ нашего случа€, значение 0 остаетс€ равным 0, а 
% дл€ значени€ 1 становитс€ максимально возможно значение, то есть 255. 
% Ёто даст нам в итоговом изображении  €ркие цвета, и младшие разр€ды 
% исходного изображени€ станут достаточно хорошо заметны  дл€ визуальной 
% проверки. “аким образом, возможно, мы можем заметить некоторые закономерности.
%%%ћетод хорошо работает на bmp, за jpg говорит предупреждение о нарушении
%%%контейнера

target1 = imread('0002.jpg');
[m,n] = size(target1(:,:,1));

part1 = target1(:,:,1);
part2 = target1(:,:,2);
part3 = target1(:,:,3);

for i = 1:m
    for j = 1:n
        part1(i,j) = 255 .* returnLastBit(part1(i,j));
        part2(i,j) = 255 .* returnLastBit(part2(i,j));
        part3(i,j) = 255 .* returnLastBit(part3(i,j));
    end
end

target1(:,:,1) = part1;
target1(:,:,2) = part2;
target1(:,:,3) = part3;
imshow(target1);


% target1 = imread('0001.jpg');
% target2 = imread('0002.jpg'); %steg
% target3 = imread('0003.jpg'); %steg
% target4 = imread('0004.jpg');
% 
% 
% target5 = imread('0005.bmp');
% target6 = imread('0006.bmp'); %steg
% target7 = imread('0007.bmp');
% target8 = imread('0008.bmp'); %steg
% target9 = imread('0009.bmp'); %steg
% target10 = imread('0010.bmp');
% 
% image_002.jpg %steg
% image_003.jpg %steg
% image_004.jpg %steg
% image_005.jpg %steg
% image_006.jpg %steg
% image_007.jpg %steg
% Lena_1.jpg %steg
% Lena_2.jpg %steg
% sailboat_at_anchor_1.jpg %steg
% sailboat_at_anchor_2.jpg %steg


