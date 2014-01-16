<html>

 <head>
 
  
<script>

function showHint(str, ts1, ts2)
{
    document.getElementById("txtHint1").innerHTML="Please Wait ... <br> 1) Executing the SQL Query. <br> 2) Writing to file. <br> 3) Compressing the file.";
	document.getElementById("txtHint2").innerHTML="It may take several minutes to prepare the file. Go and grab a Coffee :)";
	document.getElementById('loadingImage').style.visibility='visible';
	
    if (str.length==0)
    { 
       document.getElementById("txtHint1").innerHTML="Shit!";
       return;
    }
  
    if (window.XMLHttpRequest)
    {   // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp=new XMLHttpRequest();
    }
    else
    {   // code for IE6, IE5
        xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
    }
	
    xmlhttp.onreadystatechange=function()
    {
        if (xmlhttp.readyState==4 && xmlhttp.status==200)
        {
		    document.getElementById("txtHint1").innerHTML=xmlhttp.responseText;
	        document.getElementById("txtHint2").innerHTML="";
	        document.getElementById('loadingImage').style.visibility='hidden';				
		
			// var embed=document.createElement('object');
            // embed.setAttribute('type','audio/wav');
            // embed.setAttribute('data', 'c:\test.wav');
            // embed.setAttribute('autostart', true);
            // document.getElementsByTagName('body')[0].appendChild(embed);
        }
    }
	
	var filename = ts1 + "_" + ts2;
	
	// send sql query and file name to the gethint.php
    xmlhttp.open("GET","gethint.php?q=" + str + "&filename=" + filename,true);
    xmlhttp.send();
}
</script>

  
 </head>
 
 <body>
 

 <?php 
 
  ini_set('max_execution_time', 30);
  error_reporting (E_ALL ^ E_NOTICE);
 
  $server_ip= $_SERVER['SERVER_ADDR'];
  $ip= $_SERVER['REMOTE_ADDR'];
  
  printf("Web-Server IP address: %s", $server_ip);
  printf("<br> Your IP address: %s", $ip);
  
  if($_POST['formSubmit'] == "Submit") 
  {  
    // ##############
    // mote
	// ##############
	$motetempreture = 0;
    if($_POST['motetempreture'] == "T")
	{
	    $motetempreture = 1;
	}

	$motehumidity = 0;
	if($_POST['motehumidity'] == "H")
	{
	    $motehumidity = 1;
	}
	
	$motelight = 0;
	if($_POST['motelight'] == "L")
	{
	    $motelight = 1;
	}

	$moteco2 = 0;
    if($_POST['moteco2'] == "C")
	{
	    $moteco2 = 1;
	}	
	
	$nodeID = 0;	
	$nodeID_val = $_POST['nodeID'];
	if($nodeID_val != "")
	{
	    $nodeID = 1;
	}
	
	$motelocation = 0;	
	$motelocation_val = $_POST['motelocation'];
	if($motelocation_val != "")
	{
	    $motelocation = 1;
	}
	
	
	//##############
	// plc
	// #############
    $plctempreture = 0;
	if($_POST['plctempreture'] == "T")
	{
	    $plctempreture = 1;
	}	
	
	$plcco2 = 0;
	if($_POST['plcco2'] == "C")
	{
	    $plcco2 = 1;
	}
	
    $plcmotion = 0;
	if($_POST['plcmotion'] == "M")
	{
	    $plcmotion = 1;
	}

    $plcactuation = 0;
	if($_POST['plcactuation'] == "A")
	{
	    $plcactuation = 1;	
	}
	
	$plclocation = 0;	
	$plclocation_val = $_POST['plclocation'];
	if($plclocation_val != "")
	{
	    $plclocation = 1;
	}
	

	//##############
	// counter
	// #############
	$pcounteroccupancy = 0;
	if($_POST['pcounteroccupancy'] == "O")
	{
        $pcounteroccupancy = 1;
	}	
	
    //##############
	// event
	// #############
	$event = 0;
	if($_POST['event'] == "E")
	{
		$event = 1;
	}	
	
    //##############
	// timestamp
	// #############
	$timestamp1 = 0;
	$timestamp1_val = $_POST['timestamp1'];
	if($timestamp1_val != "")
	{
	    $timestamp1 = 1;
		
        $timestamp1_val = str_replace(':', '', $timestamp1_val);
		$timestamp1_val = str_replace('-', '', $timestamp1_val);
		$timestamp1_val = str_replace(' ', '', $timestamp1_val);
	}	
	
    $timestamp2 = 0;
    $timestamp2_val = $_POST['timestamp2'];
	if($timestamp2_val != "")
	{
		$timestamp2 = 1;
		
		$timestamp2_val = str_replace(':', '', $timestamp2_val);
		$timestamp2_val = str_replace('-', '', $timestamp2_val);
		$timestamp2_val = str_replace(' ', '', $timestamp2_val);
	}
  }  
  
    printf("<br><br>Your selection:<br>");
  
    // show the table of current selected values
  	echo "<table border=\"3\">";	

	      echo "<tr>";   
		  echo "<td ALIGN=CENTER>";              echo "Mote";                          echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "Temperature: $motetempreture";   echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "Humidity: $motehumidity";       echo "</td>";      
		  echo "<td ALIGN=CENTER>";              echo "Light: $motelight";             echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "CO2: $moteco2";                 echo "</td>"; 
	      echo "</tr>";		

		  echo "<tr>";   
		  echo "<td ALIGN=CENTER>";              echo "Mote nodeID";                   echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "$nodeID";                       echo "</td>"; 
	      echo "</tr>";		 

		  echo "<tr>";   
		  echo "<td ALIGN=CENTER>";              echo "Mote location";                 echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "$motelocation";                 echo "</td>"; 
	      echo "</tr>";	
		  
		  echo "<tr>";   
		  echo "<td ALIGN=CENTER>";              echo "PLC";                           echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "Tempreture: $plctempreture";    echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "CO2: $plcco2";                  echo "</td>";      
		  echo "<td ALIGN=CENTER>";              echo "Motion: $plcmotion";            echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "Actuation: $plcactuation";      echo "</td>"; 
	      echo "</tr>";	
		  
		  echo "<tr>";   
		  echo "<td ALIGN=CENTER>";              echo "PLC location";                   echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "$plclocation";                   echo "</td>"; 
	      echo "</tr>";			  
		  
		  echo "<tr>";   
		  echo "<td ALIGN=CENTER>";              echo "People Counter";                echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "Occupancy: $pcounteroccupancy"; echo "</td>"; 
	      echo "</tr>";	

		  echo "<tr>";   
		  echo "<td ALIGN=CENTER>";              echo "Events";                        echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "$event";                        echo "</td>"; 
	      echo "</tr>";	

		  echo "<tr>";   
		  echo "<td ALIGN=CENTER>";              echo "TimeStamp should be greater than";  echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "$timestamp1";                       echo "</td>"; 
	      echo "</tr>";		

		  echo "<tr>";   
		  echo "<td ALIGN=CENTER>";              echo "TimeStamp should be smaller than";  echo "</td>"; 
		  echo "<td ALIGN=CENTER>";              echo "$timestamp2";                       echo "</td>"; 
	      echo "</tr>";			  
	
	echo "</table>";
  
  // #################################################################
  // generate the sql WHERE clause for mote
  $dataTypes_mot = array();
  
  if($motetempreture == 1)  
      array_push($dataTypes_mot, "T");
  
  if($motehumidity == 1)
      array_push($dataTypes_mot, "H");
	  
  if($motelight == 1)
      array_push($dataTypes_mot, "L");
	  
  if($moteco2 == 1)
      array_push($dataTypes_mot, "C");	  
	  
  $arraySize = count($dataTypes_mot);   

  $SQL_WHERE_mote = "" ;
	  
  for ($i = 0; $i < $arraySize; $i++) 
  {
      $SQL_WHERE_mote = $SQL_WHERE_mote . " mote.sourceType = 'M' AND mote.dataType = '" . $dataTypes_mot[$i] . "'";
	  
	  // add nodeID
      if($nodeID != 0)
	  {
	      $SQL_WHERE_mote = $SQL_WHERE_mote . " AND mote.nodeID = '" . $nodeID_val . "'";	  
	  }
	  
	  // add TS1
      if($timestamp1 != 0)
	  {
	      $SQL_WHERE_mote = $SQL_WHERE_mote . " AND mote.timeStamp >= " . $timestamp1_val;	  
	  }     
	  
	  // add TS2
	  if($timestamp2 != 0)
	  {
	      $SQL_WHERE_mote = $SQL_WHERE_mote . " AND mote.timeStamp <= " . $timestamp2_val;	  
	  }
	  
	  // add mote location
	  if($motelocation != 0)
	  {
	      $SQL_WHERE_mote = $SQL_WHERE_mote . " AND mote.location = '" . $motelocation_val . "'";	  
	  }
		  
	  if($i+1 != $arraySize)
	  {
	      $SQL_WHERE_mote = $SQL_WHERE_mote . ") OR (";
	  }
	  else
	  {
	      $SQL_WHERE_mote = "WHERE (" . $SQL_WHERE_mote . ")";		  
	  }
  }
  
  $SQL_mote = "-";
  if($SQL_WHERE_mote != "")
  {
      $SQL_mote = "SELECT * FROM mote " . $SQL_WHERE_mote;	
  }
  else
  {
      $SQL_mote = "SELECT * FROM mote WHERE (1=2)" ;  
  }
  
  // #################################################################
  // generate the sql WHERE clause for plc
  $dataTypes_plc = array();
  
  if($plctempreture == 1)  
      array_push($dataTypes_plc, "T");
  
  if($plcco2 == 1)
      array_push($dataTypes_plc, "C");
	  
  if($plcmotion == 1)
      array_push($dataTypes_plc, "M");
	  
  if($plcactuation == 1)
      array_push($dataTypes_plc, "A");	  
	  
  $arraySize = count($dataTypes_plc);   

  $SQL_WHERE_plc = "" ;
	  
  for ($i = 0; $i < $arraySize; $i++) 
  {
      $SQL_WHERE_plc = $SQL_WHERE_plc . " plc.sourceType = 'P' AND plc.dataType = '" . $dataTypes_plc[$i] . "'";
	  
	  // add TS1
      if($timestamp1 != 0)
	  {
	      $SQL_WHERE_plc = $SQL_WHERE_plc . " AND plc.timeStamp >= " . $timestamp1_val;	  
	  }     
	  
	  // add TS2
	  if($timestamp2 != 0)
	  {
	      $SQL_WHERE_plc = $SQL_WHERE_plc . " AND plc.timeStamp <= " . $timestamp2_val;	  
	  }
	  
	  // add plc location
	  if($plclocation != 0)
	  {
	      $SQL_WHERE_plc = $SQL_WHERE_plc . " AND plc.location = '" . $plclocation_val . "'";	  
	  }
		  
	  if($i+1 != $arraySize)
	  {
	      $SQL_WHERE_plc = $SQL_WHERE_plc . ") OR (";
	  }
	  else
	  {
	      $SQL_WHERE_plc = "WHERE (" . $SQL_WHERE_plc . ")";		  
	  }
  }
  
  $SQL_plc = "-";
  if($SQL_WHERE_plc != "")
  {
      $SQL_plc = "SELECT * FROM plc " . $SQL_WHERE_plc;	
  }
  else
  {
      $SQL_plc = "SELECT * FROM plc WHERE (1=2)" ;  
  }
  
  // #################################################################  
    // generate the sql WHERE clause for counter
	
	$SQL_WHERE_counter = "";
	
	if($pcounteroccupancy == 1)
	{
	    $SQL_WHERE_counter = " counter.sourceType = 'C' AND counter.dataType = 'O'";
		
	    // add TS1
        if($timestamp1 != 0)
	    {
	        $SQL_WHERE_counter = $SQL_WHERE_counter . " AND counter.timeStamp >= " . $timestamp1_val;	  
	    }     
	  
	    // add TS2
	    if($timestamp2 != 0)
	    {
	        $SQL_WHERE_counter= $SQL_WHERE_counter . " AND counter.timeStamp <= " . $timestamp2_val;	  
	    }		
	}
	
	$SQL_counter = "-";
    if($SQL_WHERE_counter != "")
    {
        $SQL_counter = "SELECT * FROM counter WHERE " . $SQL_WHERE_counter;	
    }
    else
    {
        $SQL_counter = "SELECT * FROM counter WHERE (1=2)" ;  
    }
  
    // #################################################################  
    // generate the sql WHERE clause for events
	
	$SQL_WHERE_event = "";
	
	if($event == 1)
	{
	    $SQL_WHERE_event = " event.sourceType = 'E'";
		
	    // add TS1
        if($timestamp1 != 0)
	    {
	        $SQL_WHERE_event = $SQL_WHERE_event . " AND event.timeStamp >= " . $timestamp1_val;	  
	    }     
	  
	    // add TS2
	    if($timestamp2 != 0)
	    {
	        $SQL_WHERE_event = $SQL_WHERE_event . " AND event.timeStamp <= " . $timestamp2_val;	  
	    }		
	}
	
    $SQL_event = "-";
    if($SQL_WHERE_event != "")
    {
        $SQL_event = "SELECT * FROM event WHERE " . $SQL_WHERE_event;	
    }
    else
    {
        $SQL_event = "SELECT * FROM event WHERE (1=2)" ;  
    }
  
  // #################################################################  
	
	//printf("<br> <b>SQL Query for mote table:</b> %s", $SQL_mote);
	//printf("<br> <br> <b>SQL Query for plc table:</b> %s", $SQL_plc);
	//printf("<br> <br> <b>SQL Query for counter table:</b> %s", $SQL_counter);
	//printf("<br> <br> <b>SQL Query for event table:</b> %s", $SQL_event);
	
	$SQL_Query = $SQL_mote . " UNION ALL " . $SQL_plc . " UNION ALL " . $SQL_counter . " UNION ALL " . $SQL_event . " ORDER BY timeStamp ASC;";	
	printf("<br> <br> <b>Final SQL Query: </b> %s", $SQL_Query);
	
	$SQL_Query_modified = str_replace("'", "\'", $SQL_Query);
	
	// write to the log file	
	$fh2 = fopen("log.txt", 'a') or die("can't open file");
	
	$date = date('Y-m-d H:i:s');
	fprintf($fh2, "Time: %s \n", $date);
	fprintf($fh2, "%s \n", "Access to the web-interface.");
	fprintf($fh2, "IP address: %s \n\n", $ip);	
	
	// close the file
	fclose($fh2);
	
	
	?>
	
	<br>
	<br>
  
	<button type="button" onclick="showHint('<?php echo $SQL_Query_modified ?>', '<?php echo $timestamp1_val; ?>', '<?php echo $timestamp2_val; ?>')" > Run Query!</button>
	
	<p> <span id="txtHint1"></span></p>
	<p> <span id="txtHint2"></span></p>
	<img id="loadingImage" src="coffee.gif" alt="coffee" height="20%" style="visibility:hidden">
 
 </body>
 
</html>