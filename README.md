# Event Manager Application

This project is a WPF-based desktop application developed in C# as a term project.
The purpose of the application is to manage events, participants, and attendance information in a simple but realistic way.

The application works with real-life objects such as events and participants, and allows users to mark whether participants attend specific events.
All operations are performed through the graphical user interface, and data is processed through class methods rather than simple output statements.

---

## Project Purpose

In its full-scale version, the application is intended to support extended event management features such as detailed participant profiles, attendance reports, and persistent data storage.

The submitted version focuses on the core functionality required for the course project:
- Creating and managing events
- Creating and managing participants
- Assigning participants to events
- Marking and saving attendance status for each event

This version is designed as a testing and demonstration model for the course requirements.

---

## Main Features

- Add, update, and delete events
- Add, update, and delete participants
- Select an event and view all participants
- Mark whether participants will attend the selected event
- View lists of participants who will attend and who will not attend
- Save attendance information through the application

---

## Application Structure

The project is organized into the following main parts:

- **Models**
  - Event: Represents a real-life event with title, date, and location
  - Participant: Represents a person who can attend events
  - Attendance-related structures to store participation status

- **ViewModels**
  - Handle application logic using a basic MVVM approach
  - Perform operations such as adding, updating, deleting, and saving data
  - Interact with model classes through meaningful class methods

- **Views**
  - Built with WPF and XAML
  - Provide a graphical user interface for managing data and performing actions

---

## How the Application Works

1. The user creates events and participants through the interface.
2. An event is selected from the attendance screen.
3. Participants are listed for the selected event.
4. The user marks attendance using checkboxes.
5. Attendance data is saved and can be reviewed again.

All actions result in actual operations on objects and data structures, similar to a real-world application scenario.

---

## Technologies Used

- C#
- .NET (WPF)
- MVVM design pattern (without external frameworks)

---

## Running the Application

The project can be built and run using the .NET CLI:

dotnet build
dotnet run