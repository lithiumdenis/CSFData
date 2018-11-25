import math              #Импорт библиотеки с математическими функциями
import collections       #Импорт структур данных
import random            #Импорт библиотеки для генерации случайных чисел
import greatFunctions    #Импорт функций, находящихся в данной папке в другом питон файле

#Функция
def getTrueRandomNumber():
    k = 6;
    l = 7;
    k += l
    return 4

#Комментарий

myString1 = 'Hello'           #Создание строки
myString2 = '%Username!%'     #Создание другой строки

print("getTrueRandomNumber returns ", getTrueRandomNumber())           #Вызов print в Python 3 и функции в нём

myStrings = [myString1, myString2];                                    #Создание массива строк

print(myString1, myString2[9])                                         #Печать двух строк, причём из второй берется лишь один символ

a = 2;                                                                 #Создание целочисленной переменной
c = int('3');                                                          #Создание целочисленной переменной из строки

#Использование условного оператора if
if a > 1:
    print('c is', c, ' '.join(myStrings));
elif a == 1:
    print("res2");
else:
    print("res3");


#Использование foreach
for country in ["Russia", "Poland", "New Zeland"]:
    print(country)

#Ввод с консоли, находящийся в многострочном комментарии
'''
s = input("Enter an integer: ")
try:
    i = int(s)
    print("Entered integer is ", i)
except ValueError:
    print("Not a number")
'''

print(math.pi, ascii("12345"));        #Использование констант из подключенных библиотек

#Кортежи (неизменяемые, несортируемые)
cor = (1,2,3,4,5)
#Именованные кортежи
Human = collections.namedtuple("Human", "humanId Weight FootSize MaritalStatus");
humans = [];
humans.append(Human(44, 56, 39, 'Single'));

#Списки (изменяемые, сортируемые)
L = [1,6,3]

#random
rand = random.randint(1,100)
print(rand)

#Множества (изменяемые, без повторений)
Mn = {4,5,2,1}

#Словари
myDictionary = dict(id=1980, name='Anna', MaritalStatus='Single')

for item in myDictionary.items():
    print(item[0], item[1])

#Файлы
fin = open('strings.txt', encoding="utf8")
dataFromFile = fin.readlines()
for item in dataFromFile:
    print(item)

print(greatFunctions.getNamedHello('Anna'))



