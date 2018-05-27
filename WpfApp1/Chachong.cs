using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfApp1
{
    class Chachong
    {
        private ObservableCollection<Solvedproblem> sps;
        private ObservableCollection<Solution> sls;
        private ObservableCollection<Classfile> cfs;
        public Solvedproblem problemfound;
        public Solution solutionfound;
        public Classfile classfilefound;
        public bool Chazhao(string name, ObservableCollection<Solvedproblem> sp)
        {
            sps = sp;
            for(int i = 0; i < sps.Count; i ++)
            {
                if(sps[i].Name == name)
                {
                    problemfound = sps[i];
                    return true;
                }
            }
            return false;
        }
        public bool Chazhao(string name, ObservableCollection<Solution> sl)
        {
            sls = sl;
            for (int i = 0; i < sls.Count; i++)
            {
                if (sls[i].Name == name)
                {
                    solutionfound = sls[i];
                    return true;
                }
            }
            return false;
        }
        public bool Chazhao(string name, ObservableCollection<Classfile> cf)
        {
            cfs = cf;
            for (int i = 0; i < cfs.Count; i++)
            {
                if (cfs[i].Name == name)
                {
                    classfilefound = cfs[i];
                    return true;
                }
            }
            return false;
        }
    }
}
