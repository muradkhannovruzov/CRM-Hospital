using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ReceptionApp.Model
{
    public class Raiting
    {
        public double Score { get; private set; }
        public ObservableCollection<Vote> votes;
        public Raiting()
        {
            votes = new ObservableCollection<Vote>();
        }
        public void AddVote(Vote vote)
        {
            votes.Add(vote);
            Score = votes.Average(x => x.StarPoint);
        }
    }
}
