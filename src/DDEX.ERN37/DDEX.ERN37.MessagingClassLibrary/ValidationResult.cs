using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDEX.ERN37.MessagingClassLibrary
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Messages = new List<string>();
        }

        public bool IsValid
        {
            get
            {
                return Messages.Any();
            }
        }

        public IList<string> Messages { get; private set; }
    }
}
