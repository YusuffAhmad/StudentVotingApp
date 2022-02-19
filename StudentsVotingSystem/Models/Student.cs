using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsVotingSystem.Models
{
    public class Student : Person
    {
        public string MatricNumber { get; set; }
        public Level Level { get; set; }
    }
}
