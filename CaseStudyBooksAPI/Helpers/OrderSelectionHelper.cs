using CaseStudyBooksAPI.DAL.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.Helpers
{
    /**
     * In CartDetail component:
     * var orderHelper = {
     *  email: <>,
     *  selections: <>
     * };
     * 
     * Each element in the selections will be parsed into an OrderSelectionHelper object.
     */
    public class OrderSelectionHelper
    {
        public string ProductName { get; set; }

        public int Qty { get; set; }
        
        public Product Item { get; set; }
    }
}
