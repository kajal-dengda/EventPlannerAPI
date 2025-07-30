Event Planner & RSVP Tracker
A full-stack web application built with .NET Core Web API backend and Angular frontend for managing events and tracking RSVPs.
Features

Simple Authentication: Username-based login system
Event Management: Create, edit, and delete events (creator-only permissions)
Public Event Listing: Browse all upcoming events
RSVP System: Track RSVPs with maximum capacity limits
Real-time Updates: Current RSVP count vs maximum capacity
Responsive Design: Works on desktop and mobile devices
Authorization: Users can only modify events they created

Tech Stack
Backend:

.NET Core 8.0 Web API
In-memory data storage
RESTful API design
CORS enabled for frontend communication

Frontend:

Angular 17
Bootstrap 5 for styling
RxJS for reactive programming
Template-driven forms

Project Structure
EventPlannerApp/
├── Backend/
│   └── EventPlannerAPI/
│       ├── Controllers/
│       ├── Models/
│       ├── Services/
│       ├── Data/
│       └── Program.cs
└── Frontend/
    └── event-planner-ui/
        ├── src/app/
        │   ├── components/
        │   ├── services/
        │   ├── models/
        │   └── guards/
        └── package.json