using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.Helpers
{
    /**
     * In the front-end, we make a HttpPost request 
     * in CartDetail component  and send a JS object 
     * called orderHelper to back-end.
     * 
     * This class is an equivalent data wrapper for that orderHelper.
     */
    public class OrderHelper
    {
        public string Email { get; set; }

        public OrderSelectionHelper[] Selections { get; set; }
    }
}
