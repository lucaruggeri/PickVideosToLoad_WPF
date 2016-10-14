using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Utilities
{
    public static class RandomUtility
    {
        public static int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);

            SleepToChangeTheSeed();

            int randomNumber = random.Next(min, max + 1); //TODO +1???

            return randomNumber;
        }

        private static void SleepToChangeTheSeed()
        {
            //pausa per cambiare il seed
            for (int i = 0; i < 1000; i++)
            {
                //DO NOTHING
            }
            
            //DOESN'T WORK ON PORTABLE
            Thread.Sleep(1);
        }

		public enum RarityLevel
		{
			Common_80P = 1,
			Uncommon_15P = 2,
			Rare_4P = 3,
			UltraRare_1P = 4
		}

		public static int PickUpARarityLevel()
		{
			int randomNumber = GenerateRandomNumber (1, 100);

			if (randomNumber <= 80) {
				return (int)RarityLevel.Common_80P;
			} 
			else if ((randomNumber > 80) && (randomNumber <= 95)) {
				return (int)RarityLevel.Uncommon_15P;
			} 
			else if ((randomNumber > 95) && (randomNumber <= 99)) {
				return (int)RarityLevel.Rare_4P;
			} 
			else if (randomNumber == 100) {
				return (int)RarityLevel.UltraRare_1P;
			} 
			else {
				return (int)RarityLevel.Common_80P;
			}
		}

		public static bool PickUpItemByRarity(int rarityCode)
		{
			bool outcome = false;

//			if (rarityCode == RarityLevel.Common) {
//				if (GenerateRandomNumber(1, 100) < 95) 
//			} 
//			else {
//			}

			return outcome;
		}
	}
}