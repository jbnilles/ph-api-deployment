
# Title / Repository Name
Gaming Enviornment API

## About / Synopsis

* This is the api that connects the frontend with the DB and handles messaging and contacts.
* Project status: working/ in development




## Table of contents



> * [Title / Repository Name](#title--repository-name)
>   * [About / Synopsis](#about--synopsis)
>   * [Table of contents](#table-of-contents)
>   * [Usage](#usage)
>     * [Screenshots](#screenshots)
>     * [Features](#features)
>   * [Code](#code)
>     * [Content](#content)
>     * [To-Do](#to-do)
>     * [Bugs](#bugs)
>   * [Contributing / Reporting issues](#contributing--reporting-issues)
>   * [License](#license)


## Usage
 The API has 4 Controllers Authenticate, Contact, Game, Message. Each controller has multiple endpoints, to view endpoints go to https://phuserenvapi.azurewebsites.net/swagger/index.html


### Screenshots

### Features
Authenticate handles logging in and creating an account,
Contact handle add/ searching/ removing contacts,
Game handles game moves and game state,
Message handles all messaging,


## Code



### Content

Front End is written in JS using React/Redux and hosted by azure static web app, API is developed in C# .Net and hotsted by Azure App Service, and Database is in a SQL Database hosted by Azure SQL Database.

* Technologies used: 
    * C# / .Net
    * SQL DB
    * Hosted on Azure

Front End Code https://github.com/jbnilles/ph_env_frontend
API Code https://github.com/jbnilles/ph-api-deployment

### To Do

* Separate the game controller into a new api to decouple the game management from the user enviornment
* Better documentation of the endpoints,
* Refactor/clean up code
* Change status codes to better reflect bad api calls

### Bugs

* None Listed





## Contributing / Reporting issues

Please report any issues to the creator: Joseph Nilles at jbnilles24@gmail.com

## License

Copyright (c) <2021> <Joseph Nilles>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

