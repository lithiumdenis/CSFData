close all;
clear all;
clc;

% ---------------------------- BMP Lenna -----------------------------

img1 = imread('lab_6_images\0001.jpg');
img2 = imread('lab_6_images\0002.jpg');

% -------------------- ������������ ������� ������� ������������������ -------------------

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
% ������� ��� ������������ �����������
frequencyDomain = dct2(img1(:, :, 3));
% ���������� ������� � ������
frequencyDomain = frequencyDomain(:);
% �������� ��� ������������ � ��������� ����� 20
frequencyDomain = frequencyDomain(abs(frequencyDomain) < 20);
% ������ ����������� �� ��� �������������
figure(6); hist(frequencyDomain(:), K);
xlabel('�������� ��� ������������');
ylabel('���-�� ��� �������������');
title('����������� �1')

% ������� ��� ������������ �����������
frequencyDomain = dct2(img2(:, :, 3));
frequencyDomain = frequencyDomain(:);
frequencyDomain = frequencyDomain(abs(frequencyDomain) < 20);
% ������ ����������� �� ��� �������������
figure(8); hist(frequencyDomain(:), K);
xlabel('�������� ��� ������������');
ylabel('���-�� ��� �������������');
title('����������� �2')
img1 = imread('lab_6_images\Lena_1.jpg');
img2 = imread('lab_6_images\Lena_2.jpg');

% ---------------------������ ��������� ����������� ---------------------------



% ------------------- ����� �� ������ �������� �� ������� ------------------

% --------------- �������� ����������� �� ����� --------------------
BLOCK_NUMBER = 64; % ���-�� ������
BLOCK_SIZE = 8; % ��-�� �������, ����� ����
for i = 1 : 8
    for j = 1 : 8
        % �������� �����
        % (i * BLOCK_SIZE) : ((i + 1) * BLOCK_SIZE) - �������� ����������
        % �����
        % (j * BLOCK_SIZE) : ((j + 1) * BLOCK_SIZE) - �������� ����������
        % ��������
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
        
        % ������� ��� ������������ �����
        frequencyDomain = dct2(blocks(:, :, i, j));
        frequencyDomain = frequencyDomain(:);
        frequencyDomain = frequencyDomain(abs(frequencyDomain) < 20);
        % ������ ����������� �� ��� �������������
        H1 = hist(frequencyDomain(:), 100);

        % ������� ��� ������������ �����
        frequencyDomain = dct2(blocks1(:, :, i, j));
        frequencyDomain = frequencyDomain(:);
        frequencyDomain = frequencyDomain(abs(frequencyDomain) < 20);
        % ������ ����������� �� ��� �������������
        H2 = hist(frequencyDomain(:), 100);

        % image 1
        % �� ������ ����������� ��������� �� ������� ��� �����
        [a3, sizeH1] = size(H1);
        for k = 1 : 2 : (sizeH1 - 1)
            Z1(k) = (H1(k) + H1(k + 1)) / 2;
        end
        % �� ������� ��� ����� ������� �����������
        XI21 = 0;
        v = 1;
        s = 0;
        for k = 1 : (sizeH1 - 1)
            if (Z1(k) ~= 0)
                XI21 = XI21 + (H1(k) - Z1(k))^2 / Z1(k);
            end
        end
        % image 2
        % �� ������ ����������� ��������� �� ������� ��� �����
        [a4, sizeH2] = size(H2);
        for k = 1 : 2 : (sizeH2 - 1)
            Z2(k) = (H2(k) + H2(k + 1)) / 2;
        end
        % �� ������� ��� ����� ������� �����������
        XI22 = 0;
        v = 1;
        s1 = 0;
        for k = 1 : (sizeH2 - 1)
            if (Z2(k) ~= 0)
                XI22 = XI22 + (H2(k) - Z2(k))^2 / Z2(k);
            end
        end
        % �� �������� ������������ � �������
        XI2MATRIX1(i, j) = XI21;
        XI2MATRIX2(i, j) = XI22;
    end
end

% ���������� ������� � �������
XI2MATRIX1 = XI2MATRIX1(:);
XI2MATRIX2 = XI2MATRIX2(:);

% ������ ������ ������� �� ��������� ������ �����������
% ��� �� ������� ��������� - ��� ������ ���������
%figure; plot(XI2MATRIX1 - XI2MATRIX2); title('������� �� �������� ������� ����������� �� �� �������� ������� ����������� �� ������');
XI2DIFF = sum(XI2MATRIX1 - XI2MATRIX2)