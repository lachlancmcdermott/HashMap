struct Cat
{
    public string Name;
    public int Age;

    OtherCat friend;

    public Cat (string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Giraffe
{
    public string Name;
    public int Age;

    public Giraffe(string name, int age)
    {
        Name = name;
        Age = age;
    }
}


class Program
{
    static Cat bob = new Cat("ommnicient cat", int.MaxValue);
    static Cat GetCat()
    {
        return bob;
    }

    public static void Main()
    {
        Cat fluffy = new Cat("bob", 20);
        Cat bear = new Cat("big cat", 42);

       // GetCat().Age = 1;

        Giraffe spotty = new Giraffe("bob", 20);
        Giraffe bird = new Giraffe("bob", 20);

        bool a = spotty == bird;
        bool b
        ;
    }
}