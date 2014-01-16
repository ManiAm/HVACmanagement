using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irisys.BlackfinAPI;

namespace BlackfinAPIConsole
{
    /// <summary>
    /// Convenience class for commands with no arguments which require a confirmation message
    /// </summary>
    public abstract class ConfirmConsoleCommand : BaseConsoleCommand{
        public ConfirmConsoleCommand(String name, String help)
            : base(name, help, new String[] { "[y/n]\nAre you sure you want to " + name })
        {
        }

        /// <summary>
        /// Ask the user to confirm they want to continue, then call abstract execute function
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override String execute(Blackfin bf, String[] args)
        {
            if (args[0].ToLower().Equals("y"))
            {
                execute(bf);
            }
            return "user cancelled ("+args[0]+")";
        }

        /// <summary>
        /// The argumentless function to execute upon confirmation
        /// </summary>
        /// <param name="bf"></param>
        /// <returns></returns>
        public abstract String execute(Blackfin bf);
    }

}
