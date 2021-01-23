using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPProject.Sso.AspNetIdentity.Models
{
    public enum States
    {
        Undefined = 0,
        Active = 1,
        Inactive = 2,
        InProgress = 3,
        Hired = 4,
        Terminated = 5,
        Approved = 6,
        Rejected = 7,
        Closed = 8,
        ShortListed = 9,
        InterviewScheduled = 10
    }
}
