# _Hair Salon_

#### _c# Exercise for Epicodus, 07.15.2016_

#### By _**Bo Zhao**_

## Description

_An app for a hair salon. The owner should be able to add to a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist._

## Setup/Installation Requirements

* _Set up git project for Windows_
* _Clone this repository_
* _Set up dependencies in PowerShell by typing "dnu restore"_
* _Start the kestrel web server by typing "dnx kestrel"_
* _Open your web browser of choice to localhost:5004_
* _Setup database using the following commands_
  1. _CREATE DATABASE hair_salon;_
  2. _GO_
  3. _USE hair_salon;_
  4. _GO_
  5. _CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(255));_
  6. _CREATE TABLE clients (id INT IDENTITY(1,1), client_name VARCHAR(255), stylist_id VARCHAR(255));_
  7. _GO_
* _Back up the database_

## Technologies Used

* _c#_
* _Nancy_
* _Razor_
* _html_
* _SSMS_

### License

*This software is licensed under the MIT License*

Copyright (c) 2016 **_Bo Zhao_**
