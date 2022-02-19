using System;
using System.Linq;
using System.Collections.Generic;
using StudentsVotingSystem.Models;
using System.Text;

namespace StudentsVotingSystem.Repository
{
    public class CandidateRepo
    {
        private readonly ApplicationContext _context = new();
        public PositionRepo CandidatePositionRepository;
        public ElectionRepo ElectionRepo;

        public CandidateRepo(ApplicationContext context, PositionRepo candidatePositionRepository, ElectionRepo electionRepo)
        {
            _context = context;
            CandidatePositionRepository = candidatePositionRepository;
            ElectionRepo = electionRepo;
        }

        public void Contest()
        {
            ElectionRepo.PrintAllElections();
            Console.Write("Select the Election Id : ");
            var id = int.Parse(Console.ReadLine());
            var VerifyElectioneById = _context.Elections.Find(id);
            if (VerifyElectioneById != null)
            {
                Console.Write("Enter your Matric Number");
                var matricnum = Console.ReadLine();
                var verifyvoterById = _context.Voters.SingleOrDefault(vot => vot.MatricNumber == matricnum);
                var doublecandidate = _context.Candidates.SingleOrDefault(can => can.ID == verifyvoterById.Id);
                var verify = DateTime.Now >= VerifyElectioneById.ElectionOpenDate && DateTime.Now <= VerifyElectioneById.ElectionCloseDate;
                if (VerifyElectioneById != null && verify == true && doublecandidate == null)
                {
                    ElectionRepo.GetPositionByElectionID(VerifyElectioneById.Id);
                    Console.Write("Select the Position Id : ");
                    var Positionid = int.Parse(Console.ReadLine());
                    var verifyPositionById = _context.Positions.Find(Positionid);
                    if (verifyPositionById != null)
                    {
                        var candidate = new Candidate
                        {
                            MatricNum = verifyvoterById.MatricNumber,
                            FullName = $"{verifyvoterById.FirstName + " " + verifyvoterById.LastName}",
                            ElectionId = id,
                            PositionId = verifyPositionById.Id
                        };
                        _context.Candidates.Add(candidate);
                        _context.SaveChanges();
                        Console.WriteLine("Candiate Registration Sucessfull!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Position Id ");
                    }
                   
                }
                else
                {
                    Console.WriteLine("User Details Authentication Failed Or candidate alredy vie for a post in this Election Or Election Not Available ");
                }
            }
            else
            {
                Console.WriteLine("Invalid Election Id ");
            } 
        }
        public void ChangePosition()
        {
            Console.Write("Enter your Matric Number : ");
            var candidatemat= Console.ReadLine();
            var CandidateIdverification = _context.Candidates.FirstOrDefault(can => can.MatricNum == candidatemat);
            if (CandidateIdverification != null)
            {
                var poition = int.Parse(Console.ReadLine());

                CandidateIdverification.PositionId = poition;

                _context.Candidates.Update(CandidateIdverification);
                _context.SaveChanges();
                Console.WriteLine("Candidate Details Update Scuccessfull!");
            }
            else
            {
                Console.WriteLine("Invalid Voter Id");
            }
        }
    }
}
