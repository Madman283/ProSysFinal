using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Systems
{
    internal class Enviroment
    {
        public List<Entity> entities = new List<Entity>();

        public Item hawkDecoy = new Item() {name = "HawDe", cost = 3, timeLeft = 0 };
        public Item addHunter = new Item() { name = "Addhunter", cost = 5, subtractEntity = 1 };


        public int day = 1;
        public string GetAllEntityINFO()
        {
            string output = "";
            foreach (var item in entities)
            {
                output += $"\n{item.Name} {item.Amount}\n";

            }

            return output;
        }

        public void TimePasses()
        {
            day++;
            int numberofBats = EntityAmount("Bat");
            int numberofHawks = EntityAmount("Hawk");
           
            Random random = new Random();

            if (random.Next(1, 10) == 4)
            {
                //add hawk
                ChangeEntityAmount("Hawk", 1);
            }

            if (hawkDecoy.timeLeft < 1 && numberofHawks > 0 && numberofBats > 0)
            {
                if (random.Next(1, 10) == 4)
                {
                    //loss of Bats
                    ChangeEntityAmount("Bat",-1);
                }
            }

            if (hawkDecoy.timeLeft > 0)
            {
                hawkDecoy.timeLeft--;
            }

            int numberofBugs = EntityAmount("Corn earworm") + EntityAmount("Cotton Bollworm");
            numberofBats = EntityAmount("Bat");

            int bugsNeeded = numberofBats * random.Next(500, 1000);
            int halfBugsNeeded = bugsNeeded / 2;
            if (numberofBugs >= bugsNeeded)
            {
                ChangeEntityAmount("Corn earworm", -halfBugsNeeded);
                ChangeEntityAmount("Cotton Bollworm", -halfBugsNeeded);
                if (EntityAmount("Corn earworm") < 0)
                {
                    SetAmountZero("Corn earworm");
                }

                if (EntityAmount("Cotton Bollworm") < 0)
                {
                    SetAmountZero("Cotton Bollworm");
                }
            }
            else
            {
                ChangeEntityAmount("Bat", -1);
            }
            int numberofCrop = EntityAmount("Corn") + EntityAmount("Cotton");
            //worms eating crops
            numberofBugs = EntityAmount("Corn earworm") + EntityAmount("Cotton Bollworm");
            int numberofCropsEaten = (numberofBugs / 2) / 30;

            if (numberofCropsEaten >= numberofCrop)
            {
                ChangeEntityAmount("Corn", numberofCropsEaten / 2);
                ChangeEntityAmount("Cotton", numberofCropsEaten / 2);
            }
            else
            {
                ChangeEntityAmount("Corn earworm", -100);
                ChangeEntityAmount("Cotton Bollworm", -100);
            }

            //determine bug population growth
            int newBugs = (numberofBugs / 2) * (random.Next(50, 100) / 30);
            ChangeEntityAmount("Corn earworm", newBugs / 2);
            ChangeEntityAmount("Cotton Bollworm", newBugs / 2);
            
            //determine crop growth - cotton every 6 weeks, corn 3 months (requires planting)
            int newCropsPerDay = (numberofCrop * 4) / 90;
            ChangeEntityAmount("Corn", newCropsPerDay / 2);
            ChangeEntityAmount("Cotton", newCropsPerDay / 2);

        }
        //(number of total worms /2) * random number(500, 3000) /30
        private int EntityAmount(string name)
        {
            foreach (var item in entities)
            {
                if (item.Name == name)
                {
                    return item.Amount;
                }
            }
            return 0;
        }

        public void ChangeEntityAmount(string name, int amount)
        {
            foreach (var item in entities)
            {
                if (item.Name == name)
                {
                    item.Amount += amount;
                    if (item.Amount < 0)
                    {
                        item.Amount = 0;
                    }
                }
            }
            
        }

        private void SetAmountZero(string name)
        {
            foreach (var item in entities)
            {
                if (item.Name == name)
                {
                    item.Amount = 0;
                }
            }

        }


    }
}
