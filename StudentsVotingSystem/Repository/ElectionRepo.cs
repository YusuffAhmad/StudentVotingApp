using StudentsVotingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsVotingSystem.Repository
{
    public class ElectionRepo
    {
        private readonly ApplicationContext _context = new();

        public ElectionRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateElection()
        {
                Console.Write("Enter Election Name: ");
                var ElectionName = Console.ReadLine();
                Console.Write("Enter Election Opening Date: ");
                var ElectionOPD = DateTime.Parse((Console.ReadLine()));
                Console.Write("Enter Election Closing Date: ");
                var ElectionCLD = DateTime.Parse((Console.ReadLine()));
                var Election = new Election
                {
                    ElectionCloseDate = ElectionCLD,
                    ElectionOpenDate = ElectionOPD,
                    Name = ElectionName
                };
                _context.Elections.Add(Election);
                _context.SaveChanges();
                Console.WriteLine("Election Created Sucessfully!");
                Console.WriteLine("Do you still want to Create Another Election? if yes press 1 and 2 to return to the previous menu");
                var result = int.Parse(Console.ReadLine());
            if (result == 1)
            {
                CreateElection();
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
        public void UpdateElection()
        {
                Console.Write("Enter the Election ID");
                var electionID = int.Parse(Console.ReadLine());
                var verifyELevtion = _context.Elections.Find(electionID);
                if (verifyELevtion != null)
                {
                    Console.Write("Enter Election Name: ");
                    var ElectionName = Console.ReadLine();
                    Console.Write("Enter Election Opening Date: ");
                    var ElectionOPD = DateTime.Parse((Console.ReadLine()));
                    Console.Write("Enter Election Closing Date: ");
                    var ElectionCLD = DateTime.Parse((Console.ReadLine()));

                    verifyELevtion.ElectionCloseDate = ElectionCLD;
                    verifyELevtion.ElectionOpenDate = ElectionOPD;
                    verifyELevtion.Name = ElectionName;
                    
                    _context.Elections.Update(verifyELevtion);
                    _context.SaveChanges();
                    Console.WriteLine("Election Updated Sucessfully!");
                }
                else
                {
                    Console.WriteLine("Invalid Election Password");
                }
        }
        public void DeleteteElection()
        {
            Console.Write("Enter the Election ID");
            var electionID = int.Parse(Console.ReadLine());
            var verifyELevtion = _context.Elections.Find(electionID);
            if (verifyELevtion != null)
            {
                    
                _context.Elections.Remove(verifyELevtion);
                _context.SaveChanges();
                Console.WriteLine("Election Deleted Sucessfully!");
            }
            else
            {
                Console.WriteLine("Invalid Election Password");
            }
        }
        public void PrintAllElections()
        {
            var ElectionList = _context.Elections;
            Console.WriteLine($"ID\t\tName\t\tOpen Date\t\tClose Date");
            foreach (var item in ElectionList)
            {
                Console.Write($"{item.Id} {item.Name} {item.ElectionOpenDate} {item.ElectionCloseDate}");
                Console.WriteLine();
            }
        }
        public void GetPositionByElectionID(int id)
        {
            var verifyPositionById = _context.Positions.Find(id);
            if (verifyPositionById != null)
            {
                var ElectionPositions = _context.Elections.Where(x => x.Id == id);

                foreach (var item in ElectionPositions)
                {
                    var listofitem = item.Positions;
                    foreach (var list in listofitem)
                    {
                        Console.WriteLine($"{item.Id} {list.Name} ");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Position Id");
            }
        }
        public void GetCandidatesInPositionByID(int id)
        {
            var verifyCandidateid = _context.Candidates.Find(id);
            if (verifyCandidateid != null)
            {
                var CandidatesInPosition = _context.Positions.Where(x => x.Id == id);
                foreach (var item in CandidatesInPosition)
                {
                    var listofitem = item.Candidates;
                    foreach (var list in listofitem)
                    {
                        Console.WriteLine($"{item.Id} {list.FullName} ");
                    }
                }
            }
            else
            {

            }
        }
        public void GetPositionInAnElection()
        {
            Console.Write("Enter The Election ID: ");
            var electionId = int.Parse(Console.ReadLine());
            var verifyPositionById = _context.Elections.Find(electionId);
            if (verifyPositionById != null && verifyPositionById.Positions != null)
            {
                var ElectionPositions = _context.Elections.Where(x => x.Id == electionId);

                foreach (var item in ElectionPositions)
                {
                    var listofitem = item.Positions;
                    foreach (var list in listofitem)
                    {
                        Console.WriteLine($"{item.Id} {list.Name} ");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Election Id Or No Registered Position For this Election");
            }
        }
        public void GetCandidatesInPosition()
        {

            Console.Write("Enter The Position ID: ");
            var positionId = int.Parse(Console.ReadLine());
            var verifypostionid = _context.Positions.Find(positionId);
            if (verifypostionid != null && verifypostionid.Candidates != null)
            {
                var CandidatesInPosition = _context.Positions.Where(x => x.Id == positionId);
                foreach (var item in CandidatesInPosition)
                {
                    var listofitem = item.Candidates;
                    foreach (var list in listofitem)
                    {
                        Console.WriteLine($"{item.Id} {list.FullName} ");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Position ID Or No Registered Candidate for this position ");
            }
        }
    }
}
