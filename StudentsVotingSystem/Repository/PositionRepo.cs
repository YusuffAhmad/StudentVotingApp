using System;
using System.Linq;
using StudentsVotingSystem.Models;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Repository
{
    public class PositionRepo
    {
        private readonly ApplicationContext _context = new();

        public PositionRepo(ApplicationContext context)
        {
            _context = context;
        }
        public void CreatePositions()
        {
           
                Console.Write("Enter Position Name: ");
                var positionName = Console.ReadLine();
                Console.Write("Enter Election ID: ");
                var ElectionID = int.Parse(Console.ReadLine());
                var position = new Position
                {
                    ElectionId = ElectionID,
                    Name = positionName,
                };
                _context.Positions.Add(position);
                _context.SaveChanges();
                Console.WriteLine("Position Created Sucessfully!");
                Console.WriteLine("Do you still want to Create Another Position? if yes press 1 and 2 to return to the previous menu");
                var result = int.Parse(Console.ReadLine());
                if (result == 1)
                {
                    CreatePositions();
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
        public void UpdatePositionDetails()
        {
                Console.WriteLine("Enter the Position Id");
                var positionId = int.Parse(Console.ReadLine());
                var position = _context.Positions.Find(positionId);
                if (position != null)
                {
                    Console.Write("Enter the new Position Name: ");
                    var positionName = Console.ReadLine();
                    Console.Write("Enter Election ID: ");
                    var ElectionID = int.Parse(Console.ReadLine());
                    position.Name = positionName;
                    position.ElectionId = ElectionID;
                    _context.Positions.Update(position);
                    _context.SaveChanges();
                    Console.WriteLine("Position Details Updated Sucessfully!");
                }
                else
                {
                    Console.WriteLine("There is no position that correspond the provided Id");
                }
        }
        public void DeletePosition()
        {
            
                Console.WriteLine("Enter the Position Id");
                var positionId = int.Parse(Console.ReadLine());
                var position = _context.Positions.Find(positionId);
                if (position != null)
                {
                    _context.Positions.Remove(position);
                    _context.SaveChanges();
                    Console.WriteLine("Position Deleted Sucessfully!");
                }
                else
                {
                    Console.WriteLine("There is no position that correspond the provided Id");
                }
        }
    }
}
