using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Models
{
    public class CandidateVoter
    {
        public int id { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int Voterd { get; set; }
        public Voter Voter { get; set; }

    }
}
