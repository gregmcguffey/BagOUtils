using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagOUtils.ConfigurationCache;

namespace BagOUtils.Guards.Messages
{
    public static class GuardMessage
    {
        private static IStorage storage;

        static GuardMessage()
        {
            GuardMessage.storage = new Storage();
            GuardMessage.storage
                .InitiaizeStore<string>();



        }


        //-------------------------------------------------------------------------
        //
        // Message Properties
        //
        //-------------------------------------------------------------------------


        //-------------------------------------------------------------------------
        //
        // Load Messages into store
        //
        //-------------------------------------------------------------------------

        private static void LoadMessages()
        {
            var store = GuardMessage.storage.GetStore<string>();
            store.Add("", "");
        }
    }
}
