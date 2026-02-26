using System;

class StudentSys
{
    static void Main()
    {
        Student a = new Undergrad("samir", 22, 76, 84.999, 94);
        a.ShowInfo();
        a.ShowInfo(true);
        Console.WriteLine(IsEligible(a));

        Student ab = new Aftergrad("Kamal", 3245, 91, 83, 97);
        ab.ShowInfo();
        ab.ShowInfo(true);
        Console.WriteLine(IsEligible(ab));
    }

    class Student
    {
        protected string name;
        protected int id;

        public Student(string n, int i)
        {
            this.name = n;
            this.id = i;
        }

        public void ShowInfo()
        {
            Console.Write($"name: {this.name} and id: {this.id}\n");
        }

        public void ShowInfo(bool sakam)
        {
            if (!sakam)
                Console.Write($"name: {this.name} and id: {this.id}\n");
            else
                Console.Write($"name: {this.name} and id: {this.id} and grade avg: {CalculateGrade()}\n");
        }

        public virtual double CalculateGrade()
        {
            return 0;
        }
    }

    class Undergrad : Student
    {
        double exam1;
        double exam2;
        double exam3;

        public Undergrad(string n, int i, double e1, double e2, double e3)
            : base(n, i)
        {
            this.exam1 = e1;
            this.exam2 = e2;
            this.exam3 = e3;
        }

        public override double CalculateGrade()
        {
            return (exam3 + exam2 + exam1) / 3;
        }
    }

    class Aftergrad : Student
    {
        double exam1;
        double exam2;
        double thesis;

        public Aftergrad(string n, int i, double e1, double e2, double e3)
            : base(n, i)
        {
            this.exam1 = e1;
            this.exam2 = e2;
            this.thesis = e3;
        }

        public override double CalculateGrade()
        {
            return (exam1 + exam2 + thesis) / 3;
        }
    }

    static bool IsEligible(Student aa)
    {
        return aa.CalculateGrade() >= 85;
    }
}
