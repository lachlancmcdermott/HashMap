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

    public bool Alive
    {
        get 
        {
            if (Age > 40) return true;
            return false;
        }
        set 
        {
            if (value)
            {
                Age = Math.Clamp(Age, 0, 40);
            }
            else
            {
                Age = 41;
            }
        }
    }

    public bool IsAlive()
    {
        if (Age > 40) return true;
        return false;
    }

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

        if (spotty.Alive)
        {

        }

        bool a = spotty == bird;
        bool b
        ;
    }
}