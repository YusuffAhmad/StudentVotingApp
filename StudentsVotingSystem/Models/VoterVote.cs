using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Models
{
    public class VoterVote
    {
        public int Id { get; set; }
        public int VoterId { get; set; }
        public Voter Voters { get; set; }
        public int VoteId { get; set; }
        public Vote Vote { get; set; }
    }
}
