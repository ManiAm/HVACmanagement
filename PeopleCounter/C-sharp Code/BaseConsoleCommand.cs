using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irisys.BlackfinAPI;

namespace BlackfinAPIConsole
{
    /**
     * Convenience class to reduce verbosity of code    
     */
    public abstract class BaseConsoleCommand : ConsoleCommand{
	    private String name, help;
	    private String[] arguments;
	    public BaseConsoleCommand(String name, String help)
	    {
		    this.name = name;
		    this.help = help;
		    this.arguments = new String[]{};
	    }
	    public BaseConsoleCommand(String name, String help, String[] args)
	    {
		    this.name = name;
		    this.help = help;
		    this.arguments = args;
	    }
	    public String getName()
	    {
		    return name;
	    }
	    public String getHelp()
	    {
		    return help;
	    }
	    public String[] getArguments()
	    {
		    return arguments;
	    }
        
        public abstract string execute(Blackfin b, string[] s);
    }

}
