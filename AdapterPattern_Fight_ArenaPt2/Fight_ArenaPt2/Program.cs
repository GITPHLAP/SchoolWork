using System;

namespace Fight_ArenaPt2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Declare the 2 knights and get them ready to fight
            Knight k1 = new Knight();
            Knight k2 = new Knight();

            IFighter winnerfighter = Fight(k1, k2);

            Console.WriteLine("Winner: {0}", winnerfighter);



            //Declare new "wizard" and a knight
            Knight k3 = new Knight();
            WizardAdapter w1 = new WizardAdapter(new Wizard());

            IFighter winnerfighter2 = Fight(k3, w1);

            Console.WriteLine("Winner: {0}", winnerfighter2);



            DragonAdapter d1 = new DragonAdapter(new Dragon());
            Knight k4 = new Knight();

            IFighter winnerfighter3 = Fight(d1, k4);

            Console.WriteLine("Winner: {0}", winnerfighter3);


            DragonAdapter d2 = new DragonAdapter(new Dragon());
            WizardAdapter w2 = new WizardAdapter(new Wizard());

            IFighter winnerfighter4 = Fight(d2, w2);

            Console.WriteLine("Winner: {0}", winnerfighter4);

        }
        public static IFighter Fight(IFighter f1, IFighter f2)
        {

            while ((!f1.HasEscaped() && !f2.HasEscaped()) && ((f1.DefenseLeft > 0) && (f2.DefenseLeft > 0)))
            {
                // Første fighter henter attack
                int attack = f1.Attack();
                // Anden fighter skal forsvare sigA
                f2.Defend(attack);
                // Anden fighter henter attack
                attack = f2.Attack();
                // Første fighter skal forsvare sig
                f1.Defend(attack);
            }

            IFighter winner = null;

            // kampen er afsluttet
            if ((f1.DefenseLeft > 0) && (!f1.HasEscaped()))
                winner = f1;

            if ((f2.DefenseLeft > 0) && (!f2.HasEscaped()))
                winner = f2;

            // Hvis der returneres null, så har kampen været jævnbyrdig
            return winner;

        }
    }
}
