% ������� ��� ��������� ������������ ����������� ��������� �������� 
% � ������� �������������� ��������� � �������� ���������� �
function [x,m]=randncor(n,N,C)
% n-�������� ������� ������� ���������� C (������ n*n)
% N-���������� ������������ ��������
% ������������ ������� ����������� ������� ���������� ���������
% � ������������ ������� ������� � ����������� ������������ ��������
[A,r]=chol(C);
% ����������� ����������� ������������ ��������  
if r==0, m=n; else m=r-1; end;
% ��������� ������� ���������� m*N ����������� ����������� ��������� ������� 
u=randn(m,N);
% ��������� ������� N ���������� ����������� ��������� �������� ����������� m 
x=A'*u;


