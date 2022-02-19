using System;
using System.Linq;
using StudentsVotingSystem.Models;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StudentsVotingSystem.Repository
{
    public class VoteRepo
    {
        private readonly ApplicationContext _context = new();
        public ElectionRepo ElectionRepo;

        public VoteRepo(ApplicationContext context, ElectionRepo electionRepo)
        {
            _context = context;
            ElectionRepo = electionRepo;
        }

        public void Voting()
        {
            Console.Write("Enter your Voter Registration Id: ");
            var voterRegId = Console.ReadLine();
            var verifyvoterById = _context.Voters.SingleOrDefault(vot => vot.VoterRegId == voterRegId);
            if (verifyvoterById != null )
            {
                ElectionRepo.PrintAllElections();
                Console.Write("Select the Election Id : ");
                var id = int.Parse(Console.ReadLine());
                var VerifyElectioneById = _context.Elections.Find(id);
                var verifyElectionDate = DateTime.Now >= VerifyElectioneById.ElectionOpenDate && DateTime.Now <= VerifyElectioneById.ElectionCloseDate;
                var doublevoteverification = _context.Votes.SingleOrDefault(vot => vot.VoterId == verifyvoterById.Id);
                if (VerifyElectioneById != null && verifyElectionDate == true)
                {
                    if (doublevoteverification != null)
                    {
                        Console.WriteLine("You have Voted Already");
                    }
                    else
                    {
                        ElectionRepo.GetPositionByElectionID(VerifyElectioneById.Id);
                        Console.Write("Select the Position Id : ");
                        var Positionid = int.Parse(Console.ReadLine());
                        var verifyPositionById = _context.Positions.Find(Positionid);
                        if (verifyPositionById != null)
                        {
                            ElectionRepo.GetCandidatesInPositionByID(verifyPositionById.Id);
                            Console.Write("Select the Candidate Id : ");
                            var Candidateid = int.Parse(Console.ReadLine());
                            var verifyCandidateid = _context.Candidates.Find(Candidateid);
                            if (verifyCandidateid != null)
                            {
                                var Vote = new Vote
                                {
                                    VoterRegId = verifyvoterById.VoterRegId,
                                    VoterId = verifyPositionById.Id,
                                    CandidateId = verifyCandidateid.ID,
                                    PositionId = verifyPositionById.Id,
                                    ElectionId = VerifyElectioneById.Id
                                };
                                _context.Votes.Add(Vote);
                                _context.SaveChanges();
                                Console.WriteLine("Voting Scuccessful!");
                                Console.WriteLine("Do you still want to cast another vote if yes press 1 and 2 to return to the previous menu");
                                var result = int.Parse(Console.ReadLine());
                                if (result ==1)
                                {
                                    Voting();
                                }
                                else if (result ==2)
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
                                Console.WriteLine("No Candidate Match the Id");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid Position Id");
                        }
                    
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Election Id Or Election is Yet commence or has closed");
                }

            }
            else
            {
                Console.WriteLine("Incorrect Detail");
            }
        }
        public void ViewElectionVotingTable()
        {
            ElectionRepo.PrintAllElections();
            Console.WriteLine("Enter the Election Id ");
            var id = int.Parse(Console.ReadLine());
            var VerifyElectioneById = _context.Elections.Find(id);
            if (VerifyElectioneById != null)
            {
                var query = _context.Votes.Where(d => d.ElectionId == id).Include(f => f.Voters).Include(b => b.Positions).ThenInclude(a => a.Candidates).ToList();
                foreach (var item in query)
                {

                    Console.WriteLine($"Election{VerifyElectioneById.Name} Result");
                    Console.Write($"Position: {item.Positions.Name} Candididate FullName: {item.Candidates.FullName} ");
                    var voterspercandidate = item.Candidates.Votes;
                    Console.WriteLine();
                    Console.WriteLine("Voters:");
                    foreach (var vot in voterspercandidate)
                    {
                        Console.WriteLine($"{vot.Voters.FirstName + " " + vot.Voters.LastName} {vot.Voters.MatricNumber}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Election Id");
            }
        }
        public void ViewVoteCount()
        {
            ElectionRepo.PrintAllElections();
            Console.WriteLine("Enter the Election Id ");
            var id = int.Parse(Console.ReadLine());
            var VerifyElectioneById = _context.Elections.Find(id);
            if (VerifyElectioneById != null)
            {
                var query = _context.Votes.Where(d => d.ElectionId == id).Include(f => f.Voters).Include(b => b.Positions).ThenInclude(a => a.Candidates).ToList();
                foreach (var item in query)
                {
                    var votecount = item.Candidates.Votes.Count;
                    Console.WriteLine($"Election{VerifyElectioneById.Name} Result");
                    Console.Write($"Position: {item.Positions.Name} Candididate FullName: {item.Candidates.FullName} Votes: {votecount}");
                }
            }
            else
            {
                Console.WriteLine("Invalid Election Id");
            }
        }
        public void ViewWinners()
        {
            Dictionary<string, int> valuePairs = new Dictionary<string, int>();
            ElectionRepo.PrintAllElections();
            Console.WriteLine("Enter the Election Id ");
            var id = int.Parse(Console.ReadLine());
            var VerifyElectioneById = _context.Elections.Find(id);
            if (VerifyElectioneById != null)
            {
                var query = _context.Votes.Where(d => d.ElectionId == id).Include(f => f.Voters).Include(b => b.Positions).ThenInclude(a => a.Candidates).ToList();
                foreach (var item in query)
                {
                    var FullName = item.Candidates.FullName;
                    var positionName = item.Candidates.Positions.Name;
                    var votecount = item.Candidates.Votes.Count;
                    var positionAndCandidateName = $"Position Name: {positionName} Candidate Name: {FullName}";
                    valuePairs.Add(positionAndCandidateName, votecount);
                }
                var winner = valuePairs.Max();
                Console.WriteLine("Winner");
                Console.WriteLine($"Full Name: {winner.Key} Full Name: {winner.Value}");
            }
            else
            {
                Console.WriteLine("Invalid Election Id");
            }
        }

        /*public void ElectionWinners()
        {
            var results = _context.Positions.Include(c => c.Candidates.Select(h => h.Votes).Max()).ThenInclude(e => e.Votes).Select(x => new
            {
                PositionName = x.Name,
                CandidateName = x.Candidates.Where
            });

        }*/
    }
}
