KTH HVAC Management System
==========================

This repository contains the code for the KTH HVAC management system which is running in address http://hvac.ee.kth.se/. For more information you can refer to the corresponding Wiki at https://code.google.com/p/kth-hvac/wiki/Description_of_the_testbed?tm=6

Brief Description
-----------------

This project was part of the HVAC (Heating, Ventilation, and Air Conditioning) project aiming at developing novel occupancy estimation algorithms and advanced occupancy-based control strategies for HVAC system. We have created an experimental testbed for the HVAC in the 2nd floor of KTH Q-building which comprises 4 rooms (2 laboratories, 1 student room and 1 seminar room). The KTH campus has an HVAC system managed by a centralized SCADA system. Each building is composed by a one or several PLC units which measure and control the local HVAC system components.

Programming Languages used
--------------------------

LabVIEW: most of the project is done in LabVIEW

NesC: to program wireless sensor motes using TinyOS

C#: to program the people counter device

Html, CSS, JavaScript, php: for web development


Data Collection (Sensing)
-------------------------

We collect different data from the testbed like temperature, humidity, light, co2 level, occupancy, etc. We get this data from 4 different sources:

**Source 1:** We are getting the temperature, co2 level and binary motion from the sensors that were built-in to the building from the start by Academiska Hus. An OPC client/server was used to collect data from PLCs.

**Source 2:** We deployed a 'wireless sensor network' consisting of around 16 motes in the water tank lab and collect T, H, CO2, light level in different locations of the room. Motes were programmed accordingly to collect these data and send them to a base station (Collection Tree Protocol was used for this purpose).

**Source 3:** We installed a people counter in the water tank lab to measure the occupancy, we can say how many people enters or goes out of the room. We used the provided official API, and write a C# program to communicate with the people counter.

**Source 4:** We also receive the hourly 5-day weather forecast from the internet. We use a well-known online weather forecast web-site to read the weather data in XML format. Then we parse the received XML file and extract out desired parameters.

We put aside a powerful computer (HVAC-PC) and created a centralized SQLite database that collects data from 4 sources above.

Data Retreival
--------------

Anyone in the world can use our web-site to retrieve the data from this database. The users can download a text file and use a Matlab code to parse the data. You can access the website at: http://hvac.ee.kth.se/

We used html+CSS to design the web-page. XAMPP software is installed on our dedicated HVAC-PC and Apache HTTP server is running. We created a form that a user can fill-in. A simple JavaScript code gets the current local time and adjusts the To/From timestamp fields on the form. By clicking the submit buttom, the form is sent to the form.php on the server.

        <form method="POST" action="/form.php">

form.php collects the form data and creates a SQL query. It sends the query to the second php code in gethint.php. The duty of gethint.php is to go to the database and write the results to a text file. While gethint.php is busy with getting data from database, an animated gif is shown on the page, encouraging user to grab a cup of coffe, since this may take a while.

To get the data from the SQLite database, we used the SQLite command-line utility. This is an exe file and we run it from the gethint.php by providing it with the correct parameters. The results are written to a txt file. Then we use 7z program to compress the txt file to reduce the file size and make it ready to be downloaded by the user. A link to the text file will appear, and user can click on it to download the compressed text file.

Real-time control
-----------------

What is really cool about our testbed is that a user can use his own laptop and connect to our system remotely and receive the real-time data and send the actuation data back to the system. Currently we have 4 actuation points. We can increase/decrease the temperature in a room by controlling the heater. We can controll the ventilation system by changing the valve for fresh-air in and exhaust-air out. We can also control the air conditioning.

To do the actuation remotely, users are provided with username/password. They can use labView program or Matlab to create their control function and then send back the actuation data to the HVAC server.


