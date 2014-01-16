using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irisys.BlackfinAPI;

namespace BlackfinAPIConsole
{
    /// <summary>
    /// Command interface
    /// </summary>
    public interface ConsoleCommand
    {
	    /// <summary>
        /// A description of the action of the Command
	    /// </summary>
	    /// <returns>A help string</returns>
	    String getHelp();
	
	    /// <summary>
        /// The calling name of the Command
	    /// </summary>
	    /// <returns></returns>
	    String getName();
	
	    /// <summary>
        /// A list of names of arguments to be delivered as Strings upon execution
	    /// </summary>
	    /// <returns></returns>
	    String[] getArguments();
        
        /// <summary>
        /// Executes the Command
        /// </summary>
        /// <param name="bf">he Blackfin object to use</param>
        /// <param name="args">A list of string to use as command arguments</param>
        /// <returns> if there is a problem in an comms call</returns>
        String execute(Blackfin bf, String[] args);
    }
}
