using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.MatchHandler
{
    public class SetMatchEntityData
    {
        public Guid Entityid { get; set; }
        public string EntityName { get; set; }

        public SetMatchEntityData(Guid entityid, string entityName)
        {
            Entityid = entityid;
            EntityName = entityName;
        }
    }
}
