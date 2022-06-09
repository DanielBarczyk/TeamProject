


public class Weapon
{
    public string name;
    public int atk;
    public int delay;
    public float range;

    public Weapon()
    {
        name="Starting Weapon";
        atk=1;
        delay=120;
        range=4.0f;
    }

    public Weapon(string name,int atk, int delay,float range)
    {
        this.name=name;
        this.atk=atk;
        this.delay=delay;
        this.range=range;
    }
}