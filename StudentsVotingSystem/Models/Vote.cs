using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public string VoterRegId { get; set; }
        public int ElectionId { get; set; }
        public  Election Election { get; set; }
        public int VoterId { get; set; }
        public Voter Voters { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidates { get; set; }
        public int PositionId { get; set; }
        public Position Positions { get; set; }
        public List<VoterVote> VoterVoting { get; set; } = new List<VoterVote>();
    }
}
