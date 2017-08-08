function [p_x, p_y, patches] = getPatchesFromImage(im , border)
%getPatchesFromImage �������� 5�5 ����� �� �������� �����������.
%
%���������:
%im - ������� �� ����������� ������� m � n.
%border - ����� �����, ������� ����� ������ � �������� ��������������
%������� �� ����� �����������.
%p_x - (m - 2 � (2 + border)) * (n - 2 � (2 + border)) ������� � �
%��������� ������� ������
%p_y - (m - 2 � (2 + border)) * (n - 2 � (2 + border)) ������� � Y
%��������� ������� ������
%patches - (m - 2 � (2 + border)) * (n - 2 � (2 + border)) * 5 * 5 �����
%
%������: ���� border = 0, �� ����� ������� ����� ��������� � ����� (3,3), � ����� 
%���������� ��������� � ����� (end - 2, end - 2). ��� ���������� ������,
%������� ��������� � �����, ����� (m - 4) * (n - 4) ����.
%� ���� �� border = 1, �� ����� ������� ����� ����� ���������� � �����
%(4,4), ����� ���������� � (end - 3, end - 3). �.�. ����� � ����� ������
%����� ���������� ������ ����� ����������� �� ������� 
%(m - 2 � (2 + border)) * (n - 2 � (2 + border))

%������� ������ �����
PATCH_SIZE = 5;
%�������� �� �������� ������������ border
offset = border + (PATCH_SIZE - 1) / 2;
%������� ������� �������
[m, n] = size(im);
%����� ����� � patches
m_patches = (m - 2 * offset);
%����� �������� � patches
n_patches = (n - 2 * offset);
%��������������� ���������� p_x � p_y
[p_x, p_y] = meshgrid(1:n_patches, 1:m_patches);
p_x = p_x + offset;
p_y = p_y + offset;
%�������� ������
patches = zeros( m_patches, n_patches, 5, 5 );
for i=1:m_patches,
        for j=1:n_patches,
            patches(i,j,:,:) = im(i + border:i + border + PATCH_SIZE - 1,...
                                  j + border:j + border + PATCH_SIZE - 1);
        end
end
end