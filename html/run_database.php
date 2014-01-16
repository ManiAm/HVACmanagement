

/*
	try 
    {
	    // connect to SQLite from PDO database
		$dbh = new PDO('sqlite:C:\\HVAC\\database\\mydatabase.sqlite');
    }
    catch(PDOException $e)
    {
        echo $e->getMessage();         
    }	
	
	// ################
	// first query
	// ################

    $query1 = "SELECT COUNT(mote.sourceType) FROM mote " . "WHERE ( mote.sourceType = 'mote' AND mote.dataType = 'T' AND mote.nodeID = '3' AND mote.timeStamp > '2013-01-01')" ;//$SQL_WHERE_mote;

	if($result = $dbh->query($query1))
    {
      $row = $result->fetch();
	  $NoEntries = reset($row);
	}
	
    printf("# of entries: %d", $NoEntries);

		
	
	// open file to write
	$myFile = str_replace(":","_",$ip).".txt";	
    $fh = fopen($myFile, 'w') or die("can't open file");
	
    $query2 = "SELECT * FROM mote WHERE dataType = 'T'";
	
	// start timer
	$time_start = microtime(true);	

	if($result = $dbh->query($query2))
    {
      while($row = $result->fetch())
      {
	      fprintf($fh, "%s %s %s %s %s %s \n", str_pad($row['sourceType'], 20), str_pad($row['nodeID'],20), str_pad($row['dataType'],20), str_pad($row['data'],20), str_pad($row['timeStamp'],30),str_pad($row['location'],20));
	  }
    }	
	
	fclose($fh);
	
	// end timer
	$time_end = microtime(true);	
    $time = $time_end - $time_start;	

    printf("Execution Time: %f seconds.", $time);
	
	printf("<br> <br> <a href='%s'> Click Here to Download</a>", $myFile);
	
    $dbh = null;
		*/





	*/

	
	
	/*
	echo "<table>";
	
    if($result = $dbh->query($query))
    {
      while($row = $result->fetch())
      {
	      echo "<tr>";   
		  echo "<td ALIGN=CENTER, width=10%>";  print("{$row['sourceType']}");  echo "</td>"; 
		  echo "<td ALIGN=CENTER>";  print("{$row['nodeID']}");      echo "</td>"; 
		  echo "<td ALIGN=CENTER>";  print("{$row['dataType']}");    echo "</td>";      
		  echo "<td ALIGN=CENTER>";  print("{$row['data']}");        echo "</td>"; 
		  echo "<td ALIGN=CENTER>";  print("{$row['timeStamp']}");   echo "</td>"; 
		  echo "<td ALIGN=CENTER>";  print("{$row['location']}");    echo "</td>";  
	      echo "</tr>";			 
	  }
    }
	
	echo "</table>";   
	*/