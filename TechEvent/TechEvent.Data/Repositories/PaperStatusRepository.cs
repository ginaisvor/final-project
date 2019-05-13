using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using TechEvent.Domain.Entities;
using TechEvent.Domain.UsefulClases;

namespace TechEvent.Data.Repositories
{
    public class PaperStatusRepository
    {
        private readonly TechEventContext context;

        public PaperStatusRepository(TechEventContext context)
        {
            this.context = context;
        }

        public bool ExistStatusInDatabase(PaperStatusEnum status)
        {
            return context.PaperStatuses.Any(s => s.Name == status.ToString());
        }

    }
}
