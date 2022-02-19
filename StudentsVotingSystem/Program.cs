using StudentsVotingSystem.Models;
using StudentsVotingSystem.Repository;
using System;

namespace StudentsVotingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext _context = new();
            ElectionRepo ElectionRepo = new ElectionRepo(_context);
            PositionRepo positionRepo = new PositionRepo(_context);
            VoteRepo voteRepo = new VoteRepo(_context, ElectionRepo);
            CandidateRepo candidateRepo = new CandidateRepo(_context, positionRepo, ElectionRepo);
            VoterRepo voterRepo = new VoterRepo(_context, ElectionRepo, positionRepo, voteRepo, candidateRepo);
            AdminRepo AdminRepo = new AdminRepo(_context, ElectionRepo, voteRepo, positionRepo);
            LoginMenu LoginMenu = new LoginMenu(_context, voterRepo, AdminRepo);
            RegistrationMenu RegistrationMenu = new RegistrationMenu(_context, voterRepo, AdminRepo);
            Menu menu = new Menu(_context, AdminRepo, LoginMenu, RegistrationMenu);
            menu.MainMenu();
        }
    }
}
