class MagiciansGame
{

    /*!
        Deterministic random seed 
    */
    private static Random rnd = new Random(0xBEEF);
    public enum MagicianTypes
    {
        Firebender,
        Waterbender,
        Earthbender
    };

    public class Magician
    {
        private MagicianTypes typee;
        private int health;

        /*!
            Experience attribute stays the same thoughout the program
        */
        private int experience;

        /*!
            Mana attribute and its related method(s) are or is unused
        */
        private int mana;

        public int Health { get => health; 
        protected set => health =  Math.Max(0,Math.Min(100,value)); }
        public int Experience { get => experience;
        protected set => experience = Math.Max(1,Math.Min(100,value)); }
        public int Mana { get => mana;
        protected set => mana = Math.Max(0,Math.Min(100,value));}
        public MagicianTypes Typee { get => typee;
        protected set => typee = value; }

        protected Magician (MagicianTypes m)
        {
            Typee = m;
            Health = 100;
            Experience = 1;
            Mana = 100;
        }

        public void HealPotion (int amoutOfHours)
        {
            Health += 10 * amoutOfHours;
        }

        
        public void ManaPotion (int amountOfPotions)
        {
            Mana += amountOfPotions;
        }

        /*!
            Added TakeDamage(int x) method in order not to expose Health field publicly
        */
        public void TakeDamage (int damage)
        {
            Health -= damage;
        }
    }

    public interface IDistanceAttack
    {
        int Accuracy { get; }
        void DistanceAttack (Magician enemy);
    }

    public interface ICloseAttack
    {
        int Power { get; }
        void CloseAttack (Magician enemy);
    }

    class Firebender : Magician, IDistanceAttack
    {
        private int accuracy = rnd.Next(1,11);

        public int Accuracy { get => accuracy; }

        public Firebender () : base(MagicianTypes.Firebender) {}

        public void DistanceAttack (Magician enemy)
        {
            /*!
                Experience is casted to double in order not to get
                constantly 0 between the experience levels 1-49
            */
            double damage = Accuracy + (double)Experience / 50;
            if (enemy.Typee == MagicianTypes.Waterbender) damage /= 2;
            if (enemy.Typee == MagicianTypes.Earthbender) damage *= 2;
            enemy.TakeDamage((int)damage);
        }


    }

    
    class Waterbender : Magician, IDistanceAttack, ICloseAttack
    {
        private int accuracy = rnd.Next(1,11);
        private int power = rnd.Next(1,11);

        public int Accuracy { get => accuracy; }
        public int Power { get => power; }


        public Waterbender () : base(MagicianTypes.Waterbender) {}

        public void DistanceAttack (Magician enemy)
        {

            /*!
                Experience is casted to double in order not to get
                constantly 0 between the experience levels 1-49
            */
            double damage = Accuracy + (double)Experience / 50;
            if (enemy.Typee == MagicianTypes.Firebender) damage *= 2;
            if (enemy.Typee == MagicianTypes.Earthbender) damage /= 2;
            enemy.TakeDamage((int)damage);
        }

        public void CloseAttack (Magician enemy)
        {
            /*!
                Experience is casted to double in order not to get
                constantly 0 between the experience levels 1-49
            */
            double damage = Power + (double)Experience / 50;
            if (enemy.Typee == MagicianTypes.Firebender) damage *= 2;
            if (enemy.Typee == MagicianTypes.Earthbender) damage /= 2;
            enemy.TakeDamage((int)damage);
        }


    }

    class Earthbender : Magician, ICloseAttack
    {
        private int power = rnd.Next(1,11);

        public int Power { get => power; }


        public Earthbender () : base(MagicianTypes.Earthbender) {}

        public void CloseAttack (Magician enemy)
        {
            /*!
                Experience is casted to double in order not to get
                constantly 0 between the experience levels 1-49
            */
            double damage = Power + (double)Experience / 50;
            if (enemy.Typee == MagicianTypes.Firebender) damage /= 2;
            if (enemy.Typee == MagicianTypes.Waterbender) damage *= 2;
            enemy.TakeDamage((int)damage);
        }


    }

    static void Main()
    {
        Firebender magicFire = new Firebender();
        Waterbender magicWater = new Waterbender();
        Earthbender magicEarth = new Earthbender();

        magicEarth.CloseAttack(magicWater);

        magicWater.DistanceAttack(magicFire);
        magicWater.DistanceAttack(magicFire);

        magicFire.DistanceAttack(magicEarth);
        magicFire.DistanceAttack(magicWater);

        magicEarth.CloseAttack(magicWater);

        magicWater.DistanceAttack(magicFire);
        magicWater.DistanceAttack(magicFire);

        magicFire.DistanceAttack(magicEarth);
        magicFire.DistanceAttack(magicWater);

        Console.WriteLine(
            $"Fire health before HealPotion: {magicFire.Health}"
        );

        magicFire.HealPotion(1);

        Console.WriteLine(
            $"Fire health after HealPotion: {magicFire.Health}"
        );

        Console.WriteLine(
            $"Water health: {magicWater.Health}"
        );

        Console.WriteLine(
            $"Earth before HealPotion: {magicEarth.Health}"
        );

        magicEarth.HealPotion(2);

        Console.WriteLine(
            $"Earth after HealPotion: {magicEarth.Health}"
        );


    }

}