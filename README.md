# Hair Salon Manager

##### Created By: _**John Murray**_

### Description:

_This app allows a hair salon employee to manage a list of stylists and clients._

### User Stories:

- As a salon employee, I need to be able to see a list of all our stylists.

- As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist.

- As an employee, I need to add new stylists to our system when they are hired.

- As an employee, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added.

### Specifications:

- Hair Salon Manager:
  - Can display a list of all stylists.
  - Can select a stylist to see their details.
  - Can select a stylist to see a list of their clients.
  - Can add new stylists to list of stylists.
  - Can add new clients to a selected stylists.
  - Will not allow clients to be added if there are no stylists.

### Setup/Installation Requirements:

- _Install MAMP_
- _Make sure ports are set to default_
- _Start server_
- _Go to phpMyAdmin and import the john_murray.sql file_
- _dotnet restore then dotnet run_
- _Navigate to (http://localhost:5000)_

### Technologies Used:

- _C#_
- _.NET_
- _HTML and CSS_
- _SQL_
- _MAMP_

### Creating Database:

- In MySQL

  - CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
  - CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT);
  - Change the collation to utf8_general_ci

### License:

_{MIT License}_

Copyright (c) 2017 **_{John Murray}_**
