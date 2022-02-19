using StudentsVotingSystem.Models;
using StudentsVotingSystem.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem
{
    public class Menu
    {
        private readonly ApplicationContext _context;
        public AdminRepo AdminRepo;
        public LoginMenu LoginMenu;
        public RegistrationMenu RegistrationMenu;

        public Menu(ApplicationContext context, AdminRepo adminRepo, LoginMenu loginMenu, RegistrationMenu registrationMenu)
        {
            _context = context;
            AdminRepo = adminRepo;
            LoginMenu = loginMenu;
            RegistrationMenu = registrationMenu;
        }

        public void MainMenu()
        {
            var exit = false;
            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"+++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine($"+++++ WELCOME TO THE STUDENT VOTING PORTAL+++++");
                Console.WriteLine($"++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Select one of the following options to proceed ");
                Console.WriteLine("1. Sign Up \n2. Sign in \n3. Exit");

                var request = Convert.ToInt32(Console.ReadLine());

                switch (request)
                {
                    case 1:
                        RegistrationMenu.Registration();
                        break;
                    case 2:
                        LoginMenu.LogIn();
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }
       
        
    }
    public class RegistrationMenu
    {
        private readonly ApplicationContext _context;
        public VoterRepo VoterRepo;
        public AdminRepo AdminRepo;

        public RegistrationMenu(ApplicationContext context, VoterRepo voteRepo, AdminRepo adminRepo)
        {
            _context = context;
            VoterRepo = voteRepo;
            AdminRepo = adminRepo;
        }

        public void Registration()
        {
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Select one of the following options to proceed ");
                Console.WriteLine("1. Voter Registration \n2. Back To Main Menu");

                var request = Convert.ToInt32(Console.ReadLine());
                switch (request)
                {
                    case 1:
                        VoterRepo.RegisterVoter();
                        break;
                    case 2:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }
    }
    public class LoginMenu
    {
        private readonly ApplicationContext _context = new();
        public VoterRepo VoterRepo;
        public AdminRepo AdminRepo;

        public LoginMenu(ApplicationContext context, VoterRepo voterRepo, AdminRepo adminRepo)
        {
            _context = context;
            VoterRepo = voterRepo;
            AdminRepo = adminRepo;
        }

        public void LogIn()
        {
            while (true)
            {
                Console.WriteLine("Select one of the following options to continue");
                Console.WriteLine("1. Voter Login");
                Console.WriteLine("2. Admin Login ");
                Console.WriteLine("3. Previous Menu");
                var request = int.Parse(Console.ReadLine());
                
                switch (request)
                {
                    case 1:
                        VoterRepo.Login();
                        break;
                    case 2:
                        AdminRepo.Login();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }

        }

    }
}


