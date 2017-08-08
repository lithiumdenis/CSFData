function countRedundancy = redundancyPatchesCount( im )
%redundancyPatchesCount ������� ���������� ������������� �������� �
%�����-����� �����������
%   ���������:
%   im - ������� �����-����� �����������
%   countRedundancy - ���������� ������������� ��������

%�������� ���������� ��� �������� ����������
countRedundancy = 0;
%�������� ������� ����������� �������� ��������� ����������
[pxIm, pyIm, imPatches] = getPatchesFromImage(im, 0);

%������� �����
PATCH_AREA = 5 * 5;
%�������� ������� ��� imPatches
[h, w, ~] = size(imPatches);
%������� ����� ������ � imPatches
N = h * w;
%���������� ������� 5 �� 5 � ������� 25
imPatches_flat = reshape(imPatches, N, PATCH_AREA);

%��������� �� 2 ����� ����� �������, ����� ���� ����� ������
imPatches_flat_rounded = roundn(imPatches_flat,-2);

%���������� ��� �����, �.�. ������ � ������
for i=1:N
    for j=1:N
        if(imPatches_flat_rounded(i,:) == imPatches_flat_rounded(j,:) )
            if(j ~= i) %��������� ��������������� ������ ����
                countRedundancy = countRedundancy + 1;
            end
        end
    end
end
%��������� ��������� ���� ���������� ��� ����
%�.�. �� ������ ������� ������� ������� ��� ����������,
%� ����� �� ������� ��� ��, �� � ������ �������
countRedundancy = countRedundancy ./ 2;