using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bug_Tracking_System.Models
{
    public class BugViewModel
    {
        public List<Bug> Bugs { get; set; }
        public SelectList Project { get; set; }
        public SelectList SubProject { get; set; }
        public SelectList Tester { get; set; }
        public SelectList Developer { get; set; }
        public string BugProject { get; set; }
        public string BugSubProject { get; set; }
        public string BugTester { get; set; }
        public string BugDeveloper { get; set; }
        public string searchString { get; set; }
        public string BugStatus { get; set; }
        public string Rating { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }
}