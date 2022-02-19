using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Models
{
    public class Position
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public int ElectionId { get; set; }
      public  Election Election { get; set; }
       public virtual List<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
