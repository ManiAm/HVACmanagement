using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irisys.BlackfinAPI;
using System.IO;
using IrisysTime;
using Irisys.BlackfinAPI.Interfaces;
using System.Net;
using System.Reflection;

namespace BlackfinAPIConsole
{
  /// <summary>
  /// This class contains all of the command definitions for the Blackfin API
  /// None of the commands should use the console (this is handled by whatever
  /// is calling the Command)
  /// This demonstrates simple use of API for string based operations
  /// </summary>
  public class BlackfinConsoleCommands
  {
    private BlackfinConsoleCommands()
    {
    }

    public class GetDeviceNameCommand : BaseConsoleCommand
    {
      public GetDeviceNameCommand()
        : base("GetDeviceName", "Gets the device name string from the connected unit")
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetDeviceName())
          return null;
        return bf.DeviceName;
      }
    }
    public class GetDeviceIDCommand : BaseConsoleCommand
    {
      public GetDeviceIDCommand()
        : base("GetDeviceID", "Gets the device ID string from the connected unit")
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetDeviceID())
          return null;
        return bf.DeviceID;
      }
    }
    public class GetSiteNameCommand : BaseConsoleCommand
    {
      public GetSiteNameCommand()
        : base("GetSiteName", "Gets the Site name string from the connected unit")
      {

      }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetSiteName())
          return null;
        return bf.SiteName;
      }
    }
    public class GetSiteIDCommand : BaseConsoleCommand
    {
      public GetSiteIDCommand()
        : base("GetSiteID", "Gets the Site ID string from the connected unit")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetSiteID())
          return null;
        return bf.SiteID;
      }
    }

    public class GetLocaleStringCommand : BaseConsoleCommand
    {
      public GetLocaleStringCommand()
        : base("GetLocaleString", "Gets the Locale string from the connected unit")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetLocaleString())
          return null;
        return bf.LocaleString;
      }
    }

    public class GetUserStringCommand : BaseConsoleCommand
    {
      public GetUserStringCommand()
        : base("GetUserString", "Gets the user string from the connected unit")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetUserString())
          return null;
        return bf.UserString;
      }
    }

    public class GetRegisterLabelsCommand : BaseConsoleCommand
    {
        public GetRegisterLabelsCommand()
            : base("GetRegisterLabels", "Gets the labels associated with each active count register on the device")
        { }

        public override string execute(Blackfin bf, string[] args)
        {
            if (!bf.GetRegisterLabels())
                return null;

            List<string> labels = bf.RegisterLabels;
            string result = "\nRegister Labels;\n";

            foreach (string label in labels)
                result += "\t" + label + "\n";

            return result;
        }
    }

    public class GetUnitTimeCommand : BaseConsoleCommand
    {
      public GetUnitTimeCommand()
        : base("GetUnitTime", "Gets the Unit Time from the connected unit")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetUnitTime())
          return null;
        return bf.UnitTime.ToString();
      }
    }

    public class GetUpTimeCommand : BaseConsoleCommand
    {
      public GetUpTimeCommand()
        : base("GetUpTime", "Get the up time of the connected device, in seconds")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetUpTime())
          return null;
        return "The device has been running for " + bf.UpTime + " seconds";
      }
    }

    public class SetDeviceNameCommand : BaseConsoleCommand
    {
      public SetDeviceNameCommand()
        : base("SetDeviceName",
            "Sets the device name string from the connected unit",
            new string[] { "Device Name" })
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.SetDeviceName(args[0]))
          return null;
        return "Set device name";
      }
    }

    public class SetDeviceIDCommand : BaseConsoleCommand
    {
      public SetDeviceIDCommand()
        : base("SetDeviceID",
            "Sets the device ID string from the connected unit",
            new string[] { "Device ID" })
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.SetDeviceID(args[0]))
          return null;

        return "Set device ID";
      }
    }

    public class SetSiteNameCommand : BaseConsoleCommand
    {
      public SetSiteNameCommand()
        : base("SetSiteName",
            "Sets the Site name string from the connected unit",
            new string[] { "Site Name" })
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.SetSiteName(args[0]))
          return null;
        return "Set site name";
      }
    }

    public class SetSiteIDCommand : BaseConsoleCommand
    {
      public SetSiteIDCommand()
        : base("SetSiteID",
            "Sets the Site ID string from the connected unit",
            new string[] { "Site ID" })
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.SetSiteID(args[0]))
          return null;
        return "Set site id";
      }
    }

    public class SetLocaleStringCommand : BaseConsoleCommand
    {
      public SetLocaleStringCommand()
        : base("SetLocaleString",
            "Sets the Locale string from the connected unit",
            new string[] { "Locale String" })
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.SetLocaleString(args[0]))
          return null;
        return "The locale string has been set";
      }
    }

    public class SetUserStringCommand : BaseConsoleCommand
    {
      public SetUserStringCommand()
        : base("SetUserString",
            "Sets the user string from the connected unit",
            new string[] { "User String" })
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.SetUserString(args[0]))
          return null;
        return "The user string has been set";
      }
    }

    public class SetUnitTimeCommand : BaseConsoleCommand
    {
      public SetUnitTimeCommand()
        : base("SetUnitTime", "Sets the Unit Time on the connected unit", new string[] { "UnitTime" })
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          DateTime t = DateTime.Parse(args[0]);
          if (!bf.SetUnitTime(t))
            return null;
          return "Set unit time";
        }
        catch (FormatException)
        {
          throw new CommandArgumentException("Incorrect Date Format");
        }
      }
    }

    public class GetDNSCommand : BaseConsoleCommand
    {
      public GetDNSCommand()
        : base("GetDNS",
            "Gets the DNS address for the specified slot (1-3)",
            new string[] { "DNS Slot (1 - 3)" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          switch (int.Parse(args[0]))
          {
            case 1:
              return bf.GetDNS1().ToString();
            case 2:
              return bf.GetDNS2().ToString();
            case 3:
              return bf.GetDNS3().ToString();
          }

        }
        catch (FormatException)
        {
        }
        catch (OverflowException)
        {
        }

        throw new CommandArgumentException("Invalid DNS slot value");
      }
    }

    public class SetDNSCommand : BaseConsoleCommand
    {
      public SetDNSCommand()
        : base("SetDNS",
            "Sets the DNS address for the specified slot (1-3)",
            new string[] { "DNS Slot (1 - 3)", "IP address of DNS" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          int slot = int.Parse(args[0]);
          IPAddress ip = IPAddress.Parse(args[1]);
          switch (slot)
          {
            case 1:
              bf.SetDNS1(ip);   // Resets the IP board, always returns false for an IP connection
              throw new ConnectionResetException ("Set DNS 1");
            case 2:
              bf.SetDNS2(ip);   // Resets the IP board, always returns false for an IP connection
              throw new ConnectionResetException("Set DNS 2");
            case 3:
              bf.SetDNS3(ip);   // Resets the IP board, always returns false for an IP connection
              throw new ConnectionResetException("Set DNS 3");
          }

        }
        catch (OverflowException)
        {}
        catch (FormatException)
        {}

        throw new CommandArgumentException("Invalid DNS slot value");
      }
    }

    public class GetDHCPEnabledCommand : BaseConsoleCommand
    {
      public GetDHCPEnabledCommand()
        : base("GetDHCPEnabled",
            "Gets a value determining whether or not DHCP is to be used by the device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetDHCPEnabled())
          return null;
        return bf.DHCPEnabled + "";
      }
    }

    public class GetIPFirmwareVersionCommand : BaseConsoleCommand
    {
      public GetIPFirmwareVersionCommand()
        : base("GetIPFirmwareVersion",
            "Gets the firmware version of the IP Board of the connected device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetIPFirmwareVersion())
          return null;
        return bf.IPFirmwareVersion;
      }
    }

    public class GetMonitorFirmwareVersionCommand : BaseConsoleCommand
    {
      public GetMonitorFirmwareVersionCommand()
        : base("GetMonitorFirmwareVersion",
            "Gets the firmware version of the DSP Board of the connected device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetMonitorFirmwareVersion())
          return null;
        return bf.MonitorFirmwareVersion;
      }
    }

    public class GetMACAddressCommand : BaseConsoleCommand
    {
      public GetMACAddressCommand()
        : base("GetMACAddress",
            "Gets the MAC address for the IP board of the connected device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetMACAddress())
          return null;
        return bf.MACAddress;
      }
    }

    public class GetIPAddressCommand : BaseConsoleCommand
    {
      public GetIPAddressCommand()
        : base("GetIPAddress",
             "Gets the static IP Address for the IP board of the connected device (not an assigned DHCP address)")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetIPAddress())
          return null;
        return bf.IPAddress.ToString();
      }
    }

    public class GetGatewayCommand : BaseConsoleCommand
    {
      public GetGatewayCommand()
        : base("GetGateway",
            "Gets the static gateway address for the IP board of the connected device (used when DHCP is disabled)")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetGateway())
          return null;
        return bf.Gateway.ToString();
      }
    }

    public class GetSubnetMaskCommand : BaseConsoleCommand
    {
      public GetSubnetMaskCommand()
        : base("GetSubnetMask",
            "Gets the static subnet mask for the IP board of the connected device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetSubnetMask())
          return null;
        return bf.SubnetMask.ToString();
      }
    }

    public class GetSerialNumberCommand : BaseConsoleCommand
    {
      public GetSerialNumberCommand()
        : base("GetSerialNumber",
            "Gets the serial number for the the connected device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetSerialNumber())
          return null;
        return SerialNumberConverter.ConvertSerialNumber(bf.SerialNumber);
      }
    }

    public class GetClientConfigEnableCommand : BaseConsoleCommand
    {
      public GetClientConfigEnableCommand()
        : base("GetClientConfigEnable",
            "Check whether client connection mode is enabled on the device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetClientConfigEnable())
          return null;
        return "Client connection mode is " + ((bf.ClientConfigEnable) ? "enabled" : "disabled");
      }
    }

    public class GetClientConfigIPCommand : BaseConsoleCommand
    {
      public GetClientConfigIPCommand()
        : base("GetClientConfigIP",
            "Gets the IP address which IP board of the connected device will connect to in client connection mode")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetClientConfigIP())
          return null;
        return bf.ClientConfigIP.ToString();
      }
    }

    public class GetClientConfigHostnameCommand : BaseConsoleCommand
    {
      public GetClientConfigHostnameCommand()
        : base("GetClientConfigHostname",
            "Gets the Hostname which IP board of the connected device will connect to in client connection mode")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetClientConfigHostname())
          return null;
        return bf.ClientConfigHostname;
      }
    }

    public class GetClientConfigPortCommand : BaseConsoleCommand
    {
      public GetClientConfigPortCommand()
        : base("GetClientConfigPort",
             "Gets the port which IP board of the connected device will connect to in client connection mode")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetClientConfigPort())
          return null;
        return bf.ClientConfigPort + "";
      }
    }

    public class GetClientConfigTimeoutCommand : BaseConsoleCommand
    {
      public GetClientConfigTimeoutCommand()
        : base("GetClientConfigTimeout",
            "Gets the period in seconds at which IP board of the connected device will attempt to connect in client connection mode")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetClientConfigTimeout())
          return null;
        return bf.ClientConfigTimeout + "";
      }
    }

    public class SetClientConfigEnableCommand : BaseConsoleCommand
    {
      public SetClientConfigEnableCommand()
        : base("SetClientConfigEnable",
            "Enable or disable the client connection mode on the device",
            new string[] { "Enabled? (Y/N)" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (args[0].ToLower().Equals("y") || args[0].ToLower().Equals("n"))
        {
          bool bEnabled = args[0].ToLower().Equals("y");
          bf.SetClientConfigEnable(bEnabled);   // Resets the IP board, always returns false for an IP connection
          throw new ConnectionResetException("Client connection mode has been " + ((bEnabled) ? "enabled" : "disabled"));
        }

        throw new CommandArgumentException("Invalid argument value");
      }
    }

    public class SetClientConfigIPCommand : BaseConsoleCommand
    {
      public SetClientConfigIPCommand()
        : base("SetClientConfigIP",
            "Sets the IP address which IP board of the connected device will connect to in client connection mode",
            new string[] { "Server IP Address" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          IPAddress ipinet = IPAddress.Parse(args[0]);
          bf.SetClientConfigIP(ipinet);   // Resets the IP board, always returns false for an IP connection
          throw new ConnectionResetException("Client connection IP has been set");
        }
        catch (FormatException)
        {
          throw new CommandArgumentException("Invalid IP Address");
        }
      }
    }

    public class SetClientConfigHostnameCommand : BaseConsoleCommand
    {
      public SetClientConfigHostnameCommand()
        : base("SetClientConfigHostname",
            "Sets the Hostname which IP board of the connected device will connect to in client connection mode",
            new string[] { "Server hostname" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        bf.SetClientConfigHostname(args[0]);   // Resets the IP board, always returns false for an IP connection
        throw new ConnectionResetException("Client connection hostname has been set");
      }
    }

    public class SetClientConfigPortCommand : BaseConsoleCommand
    {
      public SetClientConfigPortCommand()
        : base("SetClientConfigPort",
            "Sets the port which IP board of the connected device will connect to in client connection mode",
            new string[] { "Server port number" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          bf.SetClientConfigPort(ushort.Parse(args[0]));   // Resets the IP board, always returns false for an IP connection
          throw new ConnectionResetException("Client connection port has been set");
        }
        catch (FormatException)
        {
          throw new CommandArgumentException("Invalid port value");
        }
      }
    }

    public class SetClientConfigTimeoutCommand : BaseConsoleCommand
    {
      public SetClientConfigTimeoutCommand()
        : base("SetClientConfigTimeout",
            "Sets the period in seconds at which IP board of the connected device will attempt to connect in client connection mode",
            new string[] { "Timeout period (s)" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          bf.SetClientConfigTimeout(uint.Parse(args[0]));   // Resets the IP board, always returns false for an IP connection
          throw new ConnectionResetException("Client connection timeout has been set");
        }
        catch (FormatException)
        {
          throw new CommandArgumentException("Invalid timeout value");
        }
      }
    }

    public class GetCountLogPeriod : BaseConsoleCommand
    {
      public GetCountLogPeriod()
        : base("GetCountLogPeriod",
            "Gets the count log interval in seconds")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetCountLogPeriod())
          return null;
        return bf.CountLogPeriod.ToString();
      }

    }
    public class SetCountLogPeriod : BaseConsoleCommand
    {
      public SetCountLogPeriod()
        : base("SetCountLogPeriod",
            "Sets the count log interval in seconds",
            new string[] { "Log Interval (s)" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          if (!bf.SetCountLogPeriod(int.Parse(args[0])))
            return null;
        }
        catch (FormatException)
        {
          throw new CommandArgumentException("Invalid interval value");
        }
        return "Set count log interval";
      }

    }

    public class GetDeviceStatusCommand : BaseConsoleCommand
    {
      public GetDeviceStatusCommand()
        : base("GetDeviceStatus", "Gets the errors and warnings for the connected device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetDeviceStatus())
          return null;
        DeviceStatus deviceStatus = bf.DeviceStatus;

        string output = "\nError: ";
        //Errors
        foreach (DeviceLogEntry entry in deviceStatus.m_errorList)
        {
          string tmpstring = entry.timestamp.ToString();
          tmpstring += " ";
          tmpstring += entry.errorDescription;

          output += tmpstring;
        }

        output += "\nWarning: ";
        //Add warnings
        foreach (DeviceLogEntry entry in deviceStatus.m_warnList)
        {
          string tmpstring = entry.timestamp.ToString();
          tmpstring += " ";
          tmpstring += entry.errorDescription;

          output += tmpstring;
        }

        output += "\nMessages: ";
        //Add info
        foreach (DeviceLogEntry entry in deviceStatus.m_infoList)
        {
          string tmpstring = entry.timestamp.ToString();
          tmpstring += " ";
          tmpstring += entry.errorDescription;

          output += tmpstring;
        }
        return "Device Status:" + output + "\n";
      }
    }

    public class GetLastNCountsCommand : BaseConsoleCommand
    {
      public GetLastNCountsCommand()
        : base("GetLastNCounts",
            "Gets the last n counts from the device log in the connected device",
            new string[] { "N (Number of counts to retrieve)" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          if (!bf.GetLastNCounts(uint.Parse(args[0])))
            return null;
          return PrintCountsList(bf.Counts);
        }
        catch (FormatException)
        {
          throw new CommandArgumentException("Incorrect n argument");
        }
      }
    }

    public class GetCurrentCountCommand : BaseConsoleCommand
    {
      public GetCurrentCountCommand()
        : base("GetCurrentCount", "Gets the current count (not from log) from the connected device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetCurrentCount())
          return null;
        return PrintCounts(bf.Counts.First());
      }
    }

    public class GetUnitTimeZoneCommand : BaseConsoleCommand
    {
      public GetUnitTimeZoneCommand()
        : base("GetUnitTimeZone", "Gets the current Unit Time Zone from the connected device")
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetUnitTimeZone())
          return null;
        IrisysTimeZone tz = bf.UnitTimeZone;
        return tz.DisplayName;
      }
    }

    public class SetUnitTimeZoneCommand : BaseConsoleCommand
    {
      public SetUnitTimeZoneCommand()
        : base("SetUnitTimeZone", "Sets the current Unit Time Zone from the connected device", new string[]
          {
            "TimeZone Index (0-" + IrisysTimeZone.TimeZones.Count() + ")",
            "Apply DST"
          })
      {
      }

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          short index = short.Parse(args[0]);
          bool dst = bool.Parse(args[1]);
          if (index >= IrisysTimeZone.TimeZones.Count())
            throw new CommandArgumentException("Timezone index out of bounds");
          IrisysTimeZone tz = new IrisysTimeZone(IrisysTimeZone.TimeZones.ElementAt(index));
          if (tz.SupportsDST)
            tz.ApplyDST = dst;
          if (!bf.SetUnitTimeZone(tz))
            return null;
          return "Set Timezone to " + tz.TimeZoneID;
        }
        catch (FormatException)
        {
          throw new CommandArgumentException("Timezone index incorrect");
        }
      }
    }

    public class GetCountsCommand : BaseConsoleCommand
    {
      public GetCountsCommand()
        : base("GetCounts",
            "Get the count log between two specified dates",
            new string[] { "Start Time", "End Time" })
      {}

      public override string execute(Blackfin bf, string[] args)
      {
        try
        {
          DateTime startTime = DateTime.Parse(args[0]);
          DateTime endTime = DateTime.Parse(args[1]);


          if (!(bf.GetCounts(startTime, endTime)))
            return null;
          return PrintCountsList(bf.Counts);
        }
        catch (FormatException)
        {
          throw new IOException("Could not parse start or end dates");
        }
      }
    }

    public class ResetCurrentCountCommand : ConfirmConsoleCommand
    {
      public ResetCurrentCountCommand()
        : base("ResetCurrentCount", "Resets the current count in the device")
      {}

      public override string execute(Blackfin bf)
      {
        if (!bf.ResetCurrentCount())
          return null;
        return "Reset current counts";
      }
    }

    public class ResetCountLogsCommand : ConfirmConsoleCommand
    {
      public ResetCountLogsCommand()
        : base("ResetCountLogs", "Resets the count logs in the device")
      {}

      public override string execute(Blackfin bf)
      {
        if (!bf.ResetCountLogs())
          return null;
        return "Reset count logs";
      }
    }

    public class ResetDeviceStatusCommand : ConfirmConsoleCommand
    {
      public ResetDeviceStatusCommand()
        : base("ResetDeviceStatus", "Resets the diagnostics message log")
      {}

      public override string execute(Blackfin bf)
      {
        if (!bf.ResetDeviceStatus())
          return null;
        return "Reset device status";
      }
    }

    public class GetNetworkChecksumCommand : BaseConsoleCommand
    {
      public GetNetworkChecksumCommand()
        : base("GetNetworkChecksum", "Get a checksum of the settings for all devices on the network")
      { }

      public override string execute(Blackfin bf, string[] args)
      {
        if (!bf.GetNetworkChecksum())
          return null;
        return "Network Checksum: " + bf.NetworkChecksum;
      }
    }

    /// <summary>
    /// Convenience method for printing count logs
    /// </summary>
    /// <param name="counts">The list of counts to display as a string</param>
    /// <returns>The generated string</returns>
    private static string PrintCountsList(List<Count> counts)
    {
      StringBuilder o = new StringBuilder();
      foreach (Count ct in counts)
      {
        o.Append(ct.countTime.ToString());
        o.Append(" ");

        foreach (uint ctline in ct.countLines)
        {
          o.Append(ctline.ToString());
          o.Append(" ");
        }
        o.Append("\n");
      }
      return o.ToString();
    }

    private static string PrintCounts(Count c)
    {
      string tmpstring = "";
      
      foreach (uint ctline in c.countLines)
      {
         tmpstring += ctline.ToString() + "\n";
      }

      // adding the timestamp at the end
      tmpstring += c.countTime.ToString("yyyy-MM-dd HH:mm:ss") + "\n";

      // c.countTime.Year + "-" + c.countTime.Month + "-" + c.countTime.Day + " " + c.countTime.Hour + ":" + c.countTime.Minute + ":" + c.countTime.Second

      return tmpstring;
    }

    /// <summary>
    /// Returns a list of all console commands defined in this file
    /// </summary>
    /// <returns></returns>
    public static List<ConsoleCommand> GetAllConsoleCommands()
    {
      List<ConsoleCommand> Commands = new List<ConsoleCommand>();

      // add all nested classes to list
      BlackfinConsoleCommands c = new BlackfinConsoleCommands();
      foreach (Type t in c.GetType().GetNestedTypes())
      {
        if (typeof(ConsoleCommand).IsAssignableFrom(t))
        {
          ConstructorInfo i = t.GetConstructor(new Type[] { });
          if (i != null)
          {
            ConsoleCommand command = i.Invoke(new object[] { }) as ConsoleCommand;
            Commands.Add(command);
          }
        }
      }

      return Commands;
    }
  }

}
