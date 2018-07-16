using Xunit;
using ContactLibrary;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace xUnitTest
{
    /*
     *  All of tests must be run together; they rely on the previous functions' modifications to the dataset
     */

    public class ContactDataIOTest
    {
        private readonly ITestOutputHelper output;
        
        public ContactDataIOTest(ITestOutputHelper output)
        {
            this.output = output;
        }

       

    }
}
