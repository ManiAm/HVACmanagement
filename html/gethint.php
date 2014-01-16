<?php

    ini_set('max_execution_time', 600);
	// error_reporting (E_ALL ^ E_NOTICE);
	error_reporting(E_ERROR);
	
	//get the sql query parameter
    $SQL_mote=$_GET["q"];	
	
	// get the filename
	$filename=$_GET["filename"];

    $ip= $_SERVER['REMOTE_ADDR'];

	// create commands.txt file
	$commands = "commands.txt";	
    $fh = fopen($commands, 'w') or die("can't open file");
	
	//$output = "output/" . str_replace(":","_",$ip) . ".txt";	
	$output = "output/" . $filename . ".txt";
	
	// write to file
	fprintf($fh, "%s \n", ".mode column");
	fprintf($fh, "%s \n", ".width 1 5 1 8 12 12");
	fprintf($fh, ".output %s \n", $output);	
	fprintf($fh, "%s \n", $SQL_mote);
	fprintf($fh, "%s \n", "exit");
	
	// close the file
	fclose($fh);

	// send signal to Labview to stop writing to DB
	$fp = fsockopen("127.0.0.1", 9103, $errno, $errstr, 5);	

    // echo "$errstr <br>";
	// echo "It seems that the LabVIEW is not running! <br><br>";
	
    if ($fp) 
	{
	    // send 1 to labVIEW
        fwrite($fp, "1");  
		
	    // waiting for 5 seconds for the data to buffer
	    sleep(5);		
    }
	
	// start timer
	$time_start = microtime(true);	
	
	// run the sql command
    exec('c:\WINDOWS\system32\cmd.exe /c "c:\HVAC\html\sqlite3.exe" " c:\HVAC\database\mydatabase.sqlite" " < commands.txt"');
	
	// now we compress it
	$arg = sprintf("a -mx3 %s.7z %s", $output, $output);
	exec('c:\WINDOWS\system32\cmd.exe /c "c:\HVAC\html\\7za.exe ' . $arg);
	
	// end timer
	$time_end = microtime(true);	
    $time = ($time_end - $time_start) / 60;	

    printf("Execution Time: %f minutes.", $time);
	
	printf("<br> <br> <a href='%s.7z'> Click Here to Download</a>", $output);
	
	if ($fp)
	{
	    // send 0 to labview
        fwrite($fp, "0");    
    }
?>