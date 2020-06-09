using CliqueLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsoleApp
{
    /// <summary>Тестовый класс</summary>
    public sealed class MyTestClass : IInput
    {
        /// <summary>Поле номера</summary>
        public int Id { get; private set; }
        public MyTestClass(int num)
        {
            Id = num;
        }

        public Boolean CheckInconsistency(IInput seq2)
        {
            MyTestClass other = seq2 as MyTestClass;
            ///деление на 4
            if ((other.Id % 4 == 1) && (Id % 4 == 1))
                return true;
            ///непротиворечивость - если одинаковая четность
            if ((other.Id % 2 == 0) && (this.Id % 2 == 0))
            {
                return true;
            }

            return false;


            /*
            ///случайный выбор непротиворечивости
                Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            if (randomNumber%2 == 0) return true;
            else return false;
            */
        }

        public Boolean? IsWeaker(IInput seq2)
        {
            if (Id % 3 == 0)
                return true;
            return null;
        }

    }
}
