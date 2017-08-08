package myjavaapplication;
import java.util.Random;
import static java.lang.Math.*;
import java.util.Scanner; //чтение из консоли и файла
import java.io.*; //вывод в файл
import java.io.File;
import java.nio.file.Paths;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

/*
* Amazing comments for documentation's
* @version 0.1 2016
* @author Elizabeth
*/
public class MyJavaApplication
{
    public static final double MY_CRAZY_PI = 3.1415;
    enum Size {small, medium, large, incredible};
    
    public static void main(String[] args)
    {
        Random rand = new Random();
        int age = (int)(Math.abs(rand.nextInt()) % 20 + rand.nextDouble());
        double pancakes = Math.abs(rand.nextDouble() + (rand.nextInt() % 100));
        double realPancakes = abs(tan(sin(22) * exp(2))) * (rand.nextInt() % 1000);
        
        System.out.println("She want's to be good with Alice, her 'daughter'!");
        System.out.println("Alice is a good girl. She went to the school " + age + " years");
        System.out.println("Today she eat " + pancakes + " pancakes on morning? May be " + realPancakes + " only.");
        
        double infty = realPancakes / 0;
        if(Double.isNaN(infty) || Double.isInfinite(infty))
        {
            System.out.println("LOL, divide by zero! She eat " + infty + " pancakes!");
        }
        else
        {
            System.out.println(infty);
        }
        
        char[] character = {'\u0041', '\u006C', '\u0069', '\u0063', '\u0065',
        '\u0020', '\u0069', '\u0073', '\u0020', '\u0063', '\u0072', '\u0061', '\u007A', '\u0079', '\u2122'}; 
        for(int i = 0; i < character.length; i++)
        {
            System.out.print("" + character[i]);
        }
        System.out.print("\n");
        
        boolean myDirtyBool = rand.nextBoolean();
        boolean myGoodBool = false && myDirtyBool;
        System.out.println("Alice " + myGoodBool + " where she is " + myDirtyBool + ". Pi is " + MY_CRAZY_PI);
        
        boolean myNextBool = realPancakes > pancakes ? true : false;
        Size S = Size.medium;
        String myString = myNextBool + " fat. Real size is " + S;
        myString = myString.toUpperCase();
        System.out.println(myString);
        
        /*
        Scanner in = new Scanner(System.in); //ввод данных с консоли
        System.out.println("What is your name? ");
        String name = in.nextLine();
        //name = name.trim();
        if("Luke".equals(name))
            System.out.println("I am your father, Luke!");
        else
            System.out.println("Hello " + name + "!");
        */
        System.out.printf("%s", "Strings are crazy. Output like a C!\n");
        
        //Work with file
        //Запись в файл
        try(FileWriter writer = new FileWriter("E:\\myfile.txt", false))
        {
           // запись всей строки
            String text = "Good text, bro";
            writer.write(text);
            
            writer.flush();
        }
        catch(IOException ex){
             
            System.out.println(ex.getMessage());
        } 
        
        //Чтение из файла
        try(FileReader reader = new FileReader("E:\\myfile.txt"))
        {
           // читаем посимвольно
            int c;
            while((c = reader.read())!= -1){
                 
                System.out.print((char)c);
            } 
            System.out.println();
        }
        catch(IOException ex){
             
            System.out.println(ex.getMessage());
        }
        
        //N-мерные массивы
        int[][]superArray = 
        {
            {1, 2},
            {3, 4}
        };
    
        for(int i = 0; i < superArray.length; i++){
            for(int j = 0; j < superArray.length; j++)
                System.out.print(superArray[i][j] + " ");
            System.out.println();
        }
        
        GregorianCalendar deadline = new GregorianCalendar(1999, Calendar.NOVEMBER, 25, 10, 00, 00);
        System.out.println(deadline.getTime());
        
        Girl Alice = new Girl("Алиса", 20, new Date());
        System.out.println(Alice.getName() + ", " + Alice.getAge() + " лет.");
        
        GirlFriend Elizabeth = new GirlFriend("Mark", "Elizabeth", 22, deadline.getTime());
        Elizabeth.printInfo();
        System.out.println(Elizabeth.getAge());
        
        //Сортировка массива
        int[] mass = {4, 2, 8, 8, 3, 1, 0, 5, 6, 11, 9};
        MySort(mass);
        for(int i = 0; i < mass.length; i++)
            System.out.print(mass[i] + " ");
        System.out.println();
        
        
    }
    
    public static void MySwap(int[] arr, int k)
    {
        int temp = 0;
        temp = arr[k];
        arr[k] = arr[k + 1];
        arr[k + 1] = temp;
    }
    
    public static void MySort(int[] arr)
    {
        for(int i = 0; i < arr.length - 1; i++)//счетчик скольких с конца не смотрим
            for(int j = 0; j < arr.length - i - 1; j++)
            {
                if(arr[j] < arr[j + 1])
                {
                    MySwap(arr, j);
                }
            }
    }
}

class Girl
{
    //поля
    private String name; //или protected, тогда для дочерних будет прямой доступ
    private int age;
    private Date birthday;
    //конструкторы
    public Girl()
    {
        name = "";
        age = 0;
        birthday = new Date();
    }
    public Girl(String Name, int Age, Date Birthday)
    {
        name = Name;
        age = Age;
        birthday = Birthday;
    }
    //методы
    public int getAge()
    {   
        return age;
    }
    public String getName()
    {   
        return name; //.clone() чтобы отправить не ссылку на объект, а новый идентичный объект
    }
}

class GirlFriend extends Girl
{
    String boyFriendName;
    public GirlFriend(String BoyFriendName, String Name, int Age, Date Birthday)
    {
        super(Name, Age, Birthday); //вызов конструктора родительского класса Girl
        boyFriendName = BoyFriendName;
        //name = Name;
        //age = Age;
        //birthday = Birthday;
    }
    public void printInfo()
    {
        System.out.println("Her name is " + super.getName() + ", age " + 
                super.getAge() + " years and her boyfriend is " + boyFriendName);
    }
    public int getAge() //переопределение метода, выполнится оно
    {   
        //return super.getAge(); выполнит метод родительского класса
        return 18;
    }
}