using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Enums
{
    public enum ItemLogType
    {
        Create,
        Update,
        Delete,
        AddChild,
        DeleteChild,
        AddParent,
        DeleteParent,
        Restore
    }
}
