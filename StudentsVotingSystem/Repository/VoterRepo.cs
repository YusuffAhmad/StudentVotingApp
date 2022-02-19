using System;
using System.Linq;
using StudentsVotingSystem.Models;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Repository
{
    public class VoterRepo
    {
        private readonly ApplicationContext _context;
        public ElectionRepo ElectionRepo;
        public PositionRepo PositionRepo;
        public VoteRepo VoteRepo;
        public CandidateRepo CandidateRepo;

        public VoterRepo(ApplicationContext context, ElectionRepo electionRepo, PositionRepo positionRepo, VoteRepo voteRepo, CandidateRepo candidateRepo)
        {
            _context = context;
            ElectionRepo = electionRepo;
            PositionRepo = positionRepo;
            VoteRepo = voteRepo;
            CandidateRepo = candidateRepo;
        }

        public void RegisterVoter()
        {
            Console.Write("Enter you matric Number: ");
            var matricNumber = Console.ReadLine();
            var studenshipVerification = StudentRepo.students.FirstOrDefault(sid => sid.MatricNumber == matricNumber);
            var notAdmin = _context.Admins.Any(sid => sid.MatricNum == studenshipVerification.MatricNumber);
            if (studenshipVerification != null && !_context.Voters.Any(s => s.MatricNumber == matricNumber) && notAdmin ==false)
            {
                Random random = new Random();
                var Voter = new Voter
                {
                    FirstName = studenshipVerification.FirstName,
                    LastName = studenshipVerification.LastName,
                    PhoneNUmber = studenshipVerification.PhoneNUmber,
                    EmailAddress = studenshipVerification.EmailAddress,
                    MatricNumber = studenshipVerification.MatricNumber,
                    VoterRegId = $"STEL{random.Next(45, 99).ToString("000")}",
                };
                _context.Voters.Add(Voter);
                _context.SaveChanges();
                Console.WriteLine("Registration Scuccessful!");
                Console.WriteLine($"Your Registration Id is {Voter.VoterRegId}");
            }
            else
            {
                Console.WriteLine("Studensthip Authentication Failed Or Already Registered As A Voter Or Admin!");
            }
            
        }
        public void UpdateVotersDetails()
        {
            Console.Write("Enter  matric Number: ");
            var matricNumber = Console.ReadLine();
            var VoterVerification = _context.Voters.SingleOrDefault(vot => vot.MatricNumber == matricNumber);
            if (VoterVerification != null)
            {
                    Console.Write("Enter the voter First name: ");
                    var firstName = Console.ReadLine();
                    Console.Write("Enter  the voter Last name: ");
                    var lastName = Console.ReadLine();
                    Console.Write("Enter  the voter Phone Number: ");
                    var phoneNumber = Console.ReadLine();
                    Console.Write("Enter  the voter email: ");
                    var email = Console.ReadLine();
                    Console.Write("Enter  the voter Matric Number: ");

                VoterVerification.FirstName = firstName;
                VoterVerification.LastName = lastName;
                VoterVerification.PhoneNUmber = phoneNumber;
                VoterVerification.EmailAddress = email;

                    _context.Voters.Update(VoterVerification);
                    _context.SaveChanges();
                    Console.WriteLine("Voters Details Update Scuccessfull!");
            }
            else
            {
                Console.WriteLine("Voter Password Verification Fail");
            }
        }
        public void Login()
        {
            Console.Write("Enter Voter Regiostration Id: ");
            var Id = Console.ReadLine();
            var VoterVerification = _context.Voters.SingleOrDefault(vot => vot.VoterRegId == Id);
            if (VoterVerification != null)
            {
                Console.WriteLine($"{VoterVerification.FirstName + " " + VoterVerification.LastName} Welcome back ");
                var exit = false;
                while (!exit)
                {
                    Console.WriteLine("Select one of the following options to proceed ");
                    Console.WriteLine("1. Vote \n2. Contest  \n3.View Election Table \n4.View Candidates And Their Vote Count\n5.View Winners for Each " +
                        "Position \n6. Update Details \n7. View Available ELections \n8. View Available Positions " +
                        "\n9. View Contestants For a position \n10. Log out");

                    var request = Convert.ToInt32(Console.ReadLine());
                    
                    switch (request)
                    {
                        case 1:
                            VoteRepo.Voting();
                            break;
                        case 2:
                            CandidateRepo.Contest();
                            break;
                        case 3:
                            VoteRepo.ViewElectionVotingTable();
                            break;
                        case 4:
                            VoteRepo.ViewWinners();
                            break;
                        case 5:
                            VoteRepo.ViewVoteCount();
                            break;
                        case 6:
                            UpdateVotersDetails();
                            break;
                        case 7:
                            ElectionRepo.PrintAllElections();
                            break;
                        case 8:
                            ElectionRepo.GetPositionInAnElection();
                            break;
                        case 9:
                            ElectionRepo.GetCandidatesInPosition();
                            break;
                        case 10:
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
                Console.WriteLine("Voter Password Verification Failed");
            }
        }
    }
}
