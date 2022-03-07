using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quack_api.Enums
{
    public enum ResponseErrors
    {
        /* Response codes for categorization:
         * 0-99 - System Errors
         * 100-199 User data Errors
         * 200-299 Recommendation System Errors         
         */

        // System errors
        AnExceptionOccurredInAController = 1,
        AnExceptionOccurredInTheDAL = 2,
        ChangesCouldNotBeAppliedToTheDatabase = 5,
        QuackUpdateListBadFormat = 50,
        
    }
}
