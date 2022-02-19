using System;
using StudentsVotingSystem.Models;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Models
{
    public class Candidate 
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string MatricNum { get; set; }
        public int PositionId { get; set; }
        public Position Positions { get; set; }
        public int ElectionId { get; set; }
        public Election Election { get; set; }
        public virtual List<Vote> Votes { get; set; } = new List<Vote>();
        public virtual List<CandidateVoter> Voters { get; set; } = new List<CandidateVoter>();
    }
}
