﻿namespace Teamspace.Models
{
    public class QuestionAns
    {
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public int Grade { get; set; }

        public Student Student { get; set; }
        public Question Question { get; set; }

        public string StudentAns { get; set; }
    }
}
