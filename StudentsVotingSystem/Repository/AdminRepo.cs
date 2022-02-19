using System;
using System.Linq;
using StudentsVotingSystem.Models;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Repository
{
    public class AdminRepo
    {
        private readonly ApplicationContext _context = new();
        public ElectionRepo ElectionRepo;
        public VoteRepo VoteRepo;
        public PositionRepo PositionRepo;

        public AdminRepo(ApplicationContext context, ElectionRepo electionRepo, VoteRepo voteRepo, PositionRepo positionRepo)
        {
            _context = context;
            ElectionRepo = electionRepo;
            VoteRepo = voteRepo;
            PositionRepo = positionRepo;
        }

        public void RegisterAdmin()
        {
            Console.Write("Enter matric number: ");
            var matricnumber = Console.ReadLine();
            var studentshipVerification = StudentRepo.students.SingleOrDefault(stt => stt.MatricNumber == matricnumber);
            var levelVerification = studentshipVerification.Level == Level.Level400 || studentshipVerification.Level == Level.Level500;
            if (studentshipVerification != null && levelVerification == true)
            {
                Console.Write("Enter your First name: ");
                var firstName = Console.ReadLine();
                Console.Write("Enter your Last name: ");
                var lastName = Console.ReadLine();
                Console.Write("Enter your Phone Number: ");
                var phoneNumber = Console.ReadLine();
                Console.Write("Enter your email: ");
                var email = Console.ReadLine();
                Console.Write("Enter your Password: ");
                var Password = Console.ReadLine();
                var admin = new Admin()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNUmber = phoneNumber,
                    EmailAddress = email,
                    Password = Password
                };
                _context.Admins.Add(admin);
                _context.SaveChanges();
                Console.WriteLine("Admin Added Scuccessful!");
                Console.WriteLine("Do you still want Register Another Admin? if yes press 1 Or 2 to return to the previous menu");
                var result = int.Parse(Console.ReadLine());
                if (result == 1)
                {
                    RegisterAdmin();
                }
                else if (result == 2)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid Input ");
                }
            }
            else
            {
                Console.WriteLine("You are not Eligible to register for Admin");
            }
            
        }
        public void Login()
        {
            Console.Write("Enter the Previous Admin password: ");
            var password = Console.ReadLine();
            var passwordVerification = _context.Admins.SingleOrDefault(adm => adm.Password == password);
            if (passwordVerification != null)
            {
                Console.WriteLine($"{passwordVerification.FirstName + " " + passwordVerification.LastName} Welcome back ");
                var exit = false;
                while (!exit)
                {
                    Console.WriteLine("Select one of the following options to proceed  ");
                    Console.WriteLine("1. Add New Admin \n2. Update Admin Details \n3. Create Election \n4. View Available ELections \n5. Delete Election " +
                        " \n6. Update Election\n7. Create Position  \n8. View Available Positions \n9. Delete Position  \n10. Update Position \n11. View Contestants For a position\n12." +
                        " View Election Table \n14. View Candidates And Their Vote Count\n14. View Winners for Each Position\n15. Log out");

                    var request = Convert.ToInt32(Console.ReadLine());
                    switch (request)
                    {
                        case 1:
                            RegisterAdmin();
                            break;
                        case 2:
                            UpdateAdminDetails();
                            break;
                        case 3:
                            ElectionRepo.CreateElection();
                            break;
                        case 4:
                            ElectionRepo.PrintAllElections();
                            break;
                        case 5:
                            ElectionRepo.DeleteteElection();
                            break;
                        case 6:
                            ElectionRepo.UpdateElection();
                            break;
                        case 7:
                            PositionRepo.CreatePositions();
                            break;
                        case 8:
                            ElectionRepo.GetPositionInAnElection();
                            break;
                        case 9:
                            PositionRepo.DeletePosition();
                            break;
                        case 10:
                            PositionRepo.UpdatePositionDetails();
                            break;
                        case 11:
                            ElectionRepo.GetCandidatesInPosition();
                            break;
                        case 12:
                            VoteRepo.ViewElectionVotingTable();
                            break;
                        case 13:
                            VoteRepo.ViewWinners();
                            break;
                        case 14:
                            VoteRepo.ViewVoteCount();
                            break;
                        case 15:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Password Verification Fail");
            }
        }
        public void UpdateAdminDetails()
        {
            Console.Write("Enter password: ");
            var password = Console.ReadLine();
            var passwordVerification = _context.Admins.SingleOrDefault(adm => adm.Password == password);
            if (passwordVerification != null)
            {
                Console.Write("Enter your First name: ");
                var firstName = Console.ReadLine();
                Console.Write("Enter your Last name: ");
                var lastName = Console.ReadLine();
                Console.Write("Enter your Phone Number: ");
                var phoneNumber = Console.ReadLine();
                Console.Write("Enter your email: ");
                var email = Console.ReadLine();
                Console.Write("Enter your Password: ");
                var Password = Console.ReadLine();

                passwordVerification.FirstName = firstName;
                passwordVerification.LastName = lastName;
                passwordVerification.PhoneNUmber = phoneNumber;
                passwordVerification.EmailAddress = email;
                passwordVerification.Password = password;

                    _context.Admins.Update(passwordVerification);
                    _context.SaveChanges();
                    Console.WriteLine("Details Update Scuccessfull!");
            }
            else
            {
                Console.WriteLine("Password Verification Fail");
            }
        }
    }
}
