﻿using System;
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

            #region old
            /* UserRepository user = new UserRepository();
            ProjectRepository project = new ProjectRepository();

            Console.WriteLine("1: Wyswietlenie usera po ID \n2: Dodanie nowego projektu \n3: Pobranie wszystkich projektow \n4: Dodanie nowego usera \n5: Wypisanie wszystkich userow");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Podaj id usera ");
                    int idUser = Convert.ToInt32(Console.ReadLine());
                    User example = user.GetUserById(idUser);
                    Console.WriteLine(example.id + " " + example.Login + " " + example.Name + " " + example.Surname);
                    break;
                case 2:
                    Console.WriteLine("Podaj nazwe projektu do dodania ");
                    string projectName = Console.ReadLine();
                    int newProjectID =  project.CreateNewPorject(projectName);
                    Console.WriteLine(newProjectID);
                    break;
                case 3:
                    IEnumerable<Project> allProject = project.GetAllProjects();
                    foreach (var temp in allProject)
                    {
                        Console.WriteLine(temp.id + " " + temp.NameProject);
                    }
                    break;
                case 4:
                    Console.WriteLine("Podaj Imie usera");
                    string name = Console.ReadLine();
                    Console.WriteLine("Podaj Nazwisko usera");
                    string surename = Console.ReadLine();
                    Console.WriteLine("Podaj login usera");
                    string login = Console.ReadLine();
                    Console.WriteLine("Podaj haslo usera");
                    string password = Console.ReadLine();
                    User newUser = user.CreateUser(name, surename, login, password);
                    Console.WriteLine(newUser.Name + " " + newUser.Surname + " " + newUser.Login + " " + newUser.Password);
                    break;
                case 5:
                    IEnumerable<User> allUsers = user.GetAllUsers();
                    foreach (var us in allUsers)
                    {
                        Console.WriteLine(us.Name + " " + us.Surname + " " + us.Login + " " + us.Password);
                    }
                    break;
                case 6:
                    Console.WriteLine("Podaj Nazwe grupy ");
                    string groupName = Console.ReadLine();
                    Console.WriteLine("Podaj nazwe projektu ");
                    projectName = Console.ReadLine();
                    user.CreateGroup(groupName, projectName);
                    break;
                case 7:
                    Console.WriteLine("Podaj nazwe usera ");
                    login = Console.ReadLine();
                    Console.WriteLine("Podaj nazwe grupy do ktorej chesz dolaczyc ");
                    groupName = Console.ReadLine();
                    user.AddUserToGroup(login, groupName);
                    break;

            }*/
            #endregion 
            UserRepository UserRepo = new UserRepository();
            GroupRepository GroupRepo = new GroupRepository();
            DateTime creat = new DateTime(2012, 11, 10);
            //UserRepo.addNewRole("Worker");
           // UserRepo.addNewUser("Adam", "Monroe", "Kavinsky", "123", "Worker");
           // UserRepo.addNewUser("Adam", "Monroe", "Kavinsky", "123", "Worker", "Sos");
           // UserRepo.addNewUser("Harry", "Osborn", "Goblin", "123", "Worker", "Sos");
            //UserRepo.addNewUser("Hiro", "Nakamura", "Django", "123", "Worker",1);
            //UserRepo.addNewUser("Pitter", "Petrelli", "Ojson", "123", "Menager");
            //GroupRepo.addNewGroups("Ojson", "Sos", creat);
            //UserRepo.addUserToGroup("Kavinsky", "Sos");
           // UserRepo.addMassage("Goblin", "Kavinsky", "Witaj", "Mam nadzieje ze bedzie nam sie milo pracowalo");
            //GroupRepo.deleteGroup("Sos");
            //UserRepo.deleteUser("Ojson");
            UserRepo.deleteUser("Goblin");

            Console.WriteLine("ok");
            Console.ReadLine();


        }
    }
}
