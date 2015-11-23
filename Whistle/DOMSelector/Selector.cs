using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace DOMSelector
{
    public static class Selector
    {
        public static string GetSelection(string innerHTML, string selector)
        {
            string selection = String.Empty;

            CQ domStructure = CsQuery.CQ.Create(innerHTML);
            domStructure = new CQ(selector, domStructure);

            if (domStructure.Selection.Count() > 0)
            {
                selection = domStructure.Selection.First().InnerHTML;
            }
            else
            {
                throw new NullReferenceException("Invalid selector is given. Nothing has been found.");
            }

            return selection;
        }
    }
}
