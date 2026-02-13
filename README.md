# ​Virtual Port Serial Visualizer (WinForms)

## ​About the Project
​This is a real-time data visualization application developed using C# and Windows Forms.
I created this project to master asynchronous programming (Multi-threading) and to practice advanced project layering, specifically how to manage continuous data streams between an infrastructure layer (Serial Port) and a presentation layer (Dynamic Charting).

​The application simulates a real-world scenario where data is generated in a domain layer, transmitted via a serial interface, and visualized dynamically.

---

## ​Features
- ​Real-time Data Streaming: Continuous data flow using high-performance threading.
- ​Dynamic Charting: Live line chart with an auto-scrolling window.
 - ​Smart Port Management: * Dynamic discovery of available COM ports.
 - ​Real-time input validation to prevent port conflicts (identical Receiver/Sender selection).
- ​Automated UI locking during active communication for stability.
- ​Thread-Safe Buffering: Uses synchronization primitives (lock) to manage shared data safely between threads.
- ​Graceful Termination: Uses CancellationTokenSource for clean background task shutdowns.

---

## ​Architecture & Structure
​The project is organized into three distinct domains (namespaces) to maintain a professional separation of concerns:

- **​dataCalc (Domain Layer)**
  Contains the core business logic and data generation. It encapsulates how data is modeled and produced, representing the "Business Rules" of the application.

- **​dataPort (Infrastructure Layer)**
  Handles the low-level serial communication. It manages the SerialPort lifecycle, background threads for reading/writing, and thread-safe data buffering. This layer references the dataCalc domain.

- **​VPSG (Presentation Layer)**
  The main WinForms UI that orchestrates the application. It handles user interactions, performs input validation, and consumes data from the Infrastructure layer to render the chart. This layer references both dataPort and dataCalc.

---

## ​Concepts Used
- ​Asynchronous Programming: Task.Run, async/await, and CancellationToken.
- ​Multi-threading: Thread management and Invoke for thread-safe UI updates.
- ​Defensive Programming: Real-time input validation and UI state management (Locking controls).
- ​Architectural Layering: Separation of Domain, Infrastructure, and UI.
- ​Concurrency Control: Thread-safe collections and lock objects.

---

## ​What I Learned
​Through this project, I significantly improved my understanding of:
- ​Managing Producer-Consumer patterns in a multi-threaded environment.
- ​Synchronizing background data streams with UI thread components (Charting).
- ​Handling hardware-related exceptions (Serial Port access) through preemptive UI logic.
- ​Structuring a project so that the UI is completely decoupled from the data generation logic.

---

## ​Screenshots


### ​Main Dashboard: 
<img width="655" height="437" alt="Screenshot (188)" src="https://github.com/user-attachments/assets/37b85d6b-0160-4563-871b-30eeb146e1ee" />

### ​Live Chart in Action: 
<img width="651" height="421" alt="Screenshot (190)" src="https://github.com/user-attachments/assets/c9130061-190c-4737-9701-4e7130eaadfb" />

---
​
## How to Run
1. ​Virtual Ports: You need a virtual serial port pair (like COM5 & COM6) created via tools like vspd or com0com.
2. ​Open the solution file in Visual Studio.
3. ​Build and Run the application.
4. ​Select your virtual pair from the dropdowns and click START.

---

## Notes
This project was created for learning purposes and to showcase my C#.
