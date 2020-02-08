using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Inputs
{
    public class GetUserInput
    {
        [DefaultValue(1)]
        public int UserId { get; set; }
    }
}
