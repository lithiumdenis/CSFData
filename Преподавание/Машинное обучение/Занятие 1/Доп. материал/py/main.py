import math              #������ ���������� � ��������������� ���������
import collections       #������ �������� ������
import random            #������ ���������� ��� ��������� ��������� �����
import greatFunctions    #������ �������, ����������� � ������ ����� � ������ ����� �����

#�������
def getTrueRandomNumber():
    k = 6;
    l = 7;
    k += l
    return 4

#�����������

myString1 = 'Hello'           #�������� ������
myString2 = '%Username!%'     #�������� ������ ������

print("getTrueRandomNumber returns ", getTrueRandomNumber())           #����� print � Python 3 � ������� � ��

myStrings = [myString1, myString2];                                    #�������� ������� �����

print(myString1, myString2[9])                                         #������ ���� �����, ������ �� ������ ������� ���� ���� ������

a = 2;                                                                 #�������� ������������� ����������
c = int('3');                                                          #�������� ������������� ���������� �� ������

#������������� ��������� ��������� if
if a > 1:
    print('c is', c, ' '.join(myStrings));
elif a == 1:
    print("res2");
else:
    print("res3");


#������������� foreach
for country in ["Russia", "Poland", "New Zeland"]:
    print(country)

#���� � �������, ����������� � ������������� �����������
'''
s = input("Enter an integer: ")
try:
    i = int(s)
    print("Entered integer is ", i)
except ValueError:
    print("Not a number")
'''

print(math.pi, ascii("12345"));        #������������� �������� �� ������������ ���������

#������� (������������, �������������)
cor = (1,2,3,4,5)
#����������� �������
Human = collections.namedtuple("Human", "humanId Weight FootSize MaritalStatus");
humans = [];
humans.append(Human(44, 56, 39, 'Single'));

#������ (����������, �����������)
L = [1,6,3]

#random
rand = random.randint(1,100)
print(rand)

#��������� (����������, ��� ����������)
Mn = {4,5,2,1}

#�������
myDictionary = dict(id=1980, name='Anna', MaritalStatus='Single')

for item in myDictionary.items():
    print(item[0], item[1])

#�����
fin = open('strings.txt', encoding="utf8")
dataFromFile = fin.readlines()
for item in dataFromFile:
    print(item)

print(greatFunctions.getNamedHello('Anna'))



