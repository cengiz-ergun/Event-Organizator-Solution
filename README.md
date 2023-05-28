## Event-Organizator-Solution
This is a REST API project using .NET 6 framework. A frontend for this project is [here](https://github.com/cengiz-ergun/Event-Organizator-Clients).

## Technologies
- NET 6.0
- Microsoft SQL Server
- Entity Framework Core
- Automapper
- Fluent Validation

## Tools
- Swagger

## Features
- JWT Authentication
- Pagination at some of the API endpoints
- Onion Architecture
- Unit of work
- Repository Pattern
- Dependency Injection

## Details
The aim of the project is to create an online event management system where users can create events and view/apply for those events.

- [X] Users register to the system by entering their first name, last name, email address, and password information.
- [X] Users who log in to the system with their email address and password can view the information they provided during registration and change their passwords on their profile pages.
- [X] During the system setup, information is defined for an admin user.
- [X] The admin user, who logs into the system, manages the necessary categories and city information for locations when creating events.
- [X] The admin user reviews the events created by users and approves or rejects them.
- [X] Users define events by entering the event name, date, description (for promotional text, details, etc.), city, address, capacity, and category. They select the category and city from predefined options in the system.
- [X] Users can cancel the event they created until 5 days before the event date. They can also update the capacity and address information until 5 days before the event.
- [ ] Users can view the predefined events in the system and participate in the ones they desire. They need to obtain tickets for this process. Tickets are free, and each ticket has a unique ticket number specific to the event and person.
- [ ] When entering the event venue, users' ticket numbers are checked by the personnel at the entrance.
- [ ] Users can view the events they organized and participated in on their own pages.
