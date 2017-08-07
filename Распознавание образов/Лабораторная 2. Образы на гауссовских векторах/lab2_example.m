clc; clear;
% 1. ��������� �������� 3� �������
n=2;	% ����������� ������������ ���������
M=3;	% ����� �������
N1=100;             N2=200;         N3=150;	% ����� �������� ������� ������
m1=[2; 1];          m2=[-1; 4];     m3=[4; -5];	% ������� ��������
C1=[3 -1; -1 3];    C2=[5 1; 2 4];  C3=[4 0.5; 0.5 3];	% ������� ����������
x1=repmat(m1, [1, N1])+randncor(n, N1, C1);	% ������ 1�� ������
x2=repmat(m2, [1, N2])+randncor(n, N2, C2);	% ������ 2�� ������
x3=repmat(m3, [1, N3])+randncor(n, N3, C3);	% ������ 3�� ������
X=[x1, x2, x3];		% ����� �������
T=[zeros(size(x1)), 0.5*ones(size(x2)), ones(size(x3))];	% ����� �������
N=N1+N2+N3;		% ����� ����� ��������
pw1=N1/N;   pw2=N2/N;   pw3=N3/N;	% ��������� ����������� ��������� �������
plotpv(X, T);
title('�������� ������������ ��������');
% 2. �������������
dC1=det(C1);
dC2=det(C2);
dC3=det(C3);
% ns=repmat([0; 0.5; 1], [1, N]);
g=zeros(3, N);
for i=1:N
    xi=X(:, i);
    g(1, i)=0.5*(-log(dC1)-(xi-m1)'*C1^-1*(xi-m1)+log(pw1));
    g(2, i)=0.5*(-log(dC2)-(xi-m2)'*C2^-1*(xi-m2)+log(pw2));
    g(3, i)=0.5*(-log(dC3)-(xi-m3)'*C3^-1*(xi-m3)+log(pw3));
end;
[ggg, gmx]=max(g);
Tg=repmat((gmx - 1) ./ (M - 1), [2, 1]);
figure;
plotpv(X, Tg);
title('���������� �������������');
% 3. ������ ������ ������������ ������ �������������
% 1 � 2
Pt=zeros(M);
dm=m1-m2;
l0=log(pw2/pw1);
tr12=trace(C2^-1*C1-eye(n));
tr21=trace(eye(n)-C1^-1*C2);
mg1=0.5*(tr12+dm'*C2^-1*dm-log(dC1/dC2));
Dg1=0.5*(tr12^2+dm'*C2^-1*C1*C2^-1*dm);
mg2=0.5*(tr21-dm'*C1^-1*dm+log(dC2/dC1));
Dg2=0.5*(tr21^2+dm'*C1^-1*C2*C1^-1*dm);
Pt(1, 2)=normcdf(l0, mg1, sqrt(Dg1));
Pt(2, 1)=1-normcdf(l0, mg2, sqrt(Dg2));
% 1 � 3
dm=m1-m3;
l0=log(pw3/pw1);
tr13=trace(C3^-1*C1-eye(n));
tr31=trace(eye(n)-C1^-1*C3);
mg1=0.5*(tr13+dm'*C3^-1*dm-log(dC1/dC3));
Dg1=0.5*(tr13^2+dm'*C3^-1*C1*C3^-1*dm);
mg2=0.5*(tr31-dm'*C1^-1*dm+log(dC3/dC1));
Dg2=0.5*(tr31^2+dm'*C1^-1*C3*C1^-1*dm);
Pt(1, 3)=normcdf(l0, mg1, sqrt(Dg1));
Pt(3, 1)=1-normcdf(l0, mg2, sqrt(Dg2));
% 2 � 3
dm=m2-m3;
l0=log(pw3/pw2);
tr23=trace(C3^-1*C2-eye(n));
tr32=trace(eye(n)-C2^-1*C3);
mg1=0.5*(tr23+dm'*C3^-1*dm-log(dC2/dC3));
Dg1=0.5*(tr23^2+dm'*C3^-1*C2*C3^-1*dm);
mg2=0.5*(tr32-dm'*C2^-1*dm+log(dC3/dC2));
Dg2=0.5*(tr32^2+dm'*C2^-1*C3*C2^-1*dm);
Pt(2, 3)=normcdf(l0, mg1, sqrt(Dg1));
Pt(3, 2)=1-normcdf(l0, mg2, sqrt(Dg2));
% �������� ������������ ���������
Pt=Pt+diag(1-sum(Pt, 2));
disp('������������� ������� ������������ ������');
disp(Pt);
% 3. ������ ����������������� ������� ������������ ������
Pp=[sum(gmx(1:N1)==1)/N1            sum(gmx(1:N1)==2)/N1            sum(gmx(1:N1)==3)/N1;
    sum(gmx(N1+(1:N2))==1)/N2       sum(gmx(N1+(1:N2))==2)/N2       sum(gmx(N1+(1:N2))==3)/N2;
    sum(gmx(N1+N2+(1:N3))==1)/N3    sum(gmx(N1+N2+(1:N3))==2)/N3    sum(gmx(N1+N2+(1:N3))==3)/N3 ];
disp('����������������� ������� ������������ ������');
disp(Pp);



