using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.EntityModels;
using Scheduler.Model.Repositories;
using Scheduler.Model.Repositories.Interfaces;

namespace Scheduler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository user = new UserRepository();
            User example = user.GetUserById(1);
            Console.WriteLine(example.id + " " + example.Login + " " + example.Name + " " + example.Surname);
            Console.ReadLine();


        }
    }
}
