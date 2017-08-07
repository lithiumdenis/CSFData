s = ABC(:, [19, 4]);            % ��������� �������� ���� "S" � "D";
N = 20;                         % ����� ������������� ������������ ���������;
pI = eps : 1 / N : 1 - eps;     % ����������� ��������� �������� ����������� 
pI_ = 1 - pI;                  	% � �� ��������������� ��������.
s1 = s(:, 1);       		% ����������� ����������� 1�� �������;
s2 = s(:, 2);       		% ����������� ����������� 2�� �������.
ks10 = s1&s2_;
ks01 = s1_&s2;
% ���������� ������������� ����������� �������:
g1 = log((s1 * pI_ + s1_ * pI) ./ (s2 * pI_ + s2_ * pI));
g2 = log((s1 * pI + s1_ * pI_) ./ (s2 * pI + s2_ * pI_));
% ����������� ������� �������� �������:
l0_ = log(pw(2) / pw(1));           % l0';
LI = log(pI_) - log(pI);            % LI;
L0 = 0.5 * (l0_ ./ LI + ns);        % L0;
Pt = zeros(2, N);  % ������������� ������� ������������ ������ �������������.
K = 2000;          % ���������� ���������� ������������;
Pe = zeros(2, N);  % ������������� ����������������� ������� ������������ ������.
for j = 1 : N      % ���� �� ��������� ����������� ��������� ��������� �����������
    for kk = 1 : K   % ���� �� ����� ���������� ������������
        for i = 1 : M   % ���� �� �������
            % ������������ ������������ (���������������) �����������:
            x = s(:, i);    % ��������� ����� �����������;
            r = rand(n, 1); % ��������� ����� (��� ��������� � pI);
            x(r < pI(j)) = 1 - x(r < pI(j));% �������� ��������� �������� ������������ �����������.
            % �������������:
            x_ = 1 - x;
            Lx01 = sum(x(ks01)); 
            Lx10 = sum(x(ks10));
            Px01 = sum(x_(ks01));
            Px10 = sum(x_(ks10));      
            %����������� �������			
            g = LI(j) * Lx10 - LI(j) * Lx01 + LI(j) * Px01 - LI(j) * Px10 - l0_; 
            ia = 1 .* (g > 0) + 2 .* (g <= 0); % ����� ������ ������������� ������;
            Pe(i, j) = Pe(i, j) + (i ~= ia);   % �������� ���������� �������������.
        end;
    end;
    if j == fix(N / 3)
        IX = x;  % �������� ��������������� ������ ��� ������������.
    end;
end;