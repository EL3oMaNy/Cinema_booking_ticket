# 🎬 Cinema Ticket Booking System

A modern **C# console application** that simulates a real-world **Cinema Ticket Booking System**.  
This project demonstrates clean coding practices, file handling, and role-based menus (**Admin & Customer**) with persistent data storage.

---

## ✨ Key Features

### 👨‍💼 Admin Panel
- ➕ Add new movies with duration and seat count.  
- ❌ Delete existing movies from the system.  
- 📊 View all movies with **seat availability visualization**.

### 🎟️ Customer Panel
- 📖 Browse movies and seat availability.  
- ✅ Book seats in real-time (updates file instantly).  

---

## 🗄 Data Storage Design

All data is stored in a simple **text file**: `Movie.txt`

**Format:**
```
Title|Duration|Seats
```

**Example:**
```
Inception|120|0,1,0,0,1,0
```

- `0` → Seat is **Available**  
- `1` → Seat is **Booked**  

This ensures data **persists between program runs**.

---

## 🏗 Project Architecture

```
/CinemaBookingSystem
  ├── Program.cs       # Main entry point, menus, system flow
  ├── Movie.cs         # Movie model with properties & methods
  ├── Person.cs        # User model (Admin / Customer)
  ├── Movie.txt        # Flat-file database
  └── README.md        # Project documentation
```

The system follows a **modular design** for better maintainability.

---

## 🚀 Getting Started

### Prerequisites
- Install [.NET SDK](https://dotnet.microsoft.com/download)  
- Use **Visual Studio** or **VS Code** with C# extension.

### Run the Application
```bash
git clone https://github.com/EL3oMaNy/Cinema_booking_ticket.git
cd Cinema_booking_ticket
dotnet run
```

### Default Credentials
- **Admin** → username: `admin` | password: `1234`  
- **Customer** → username: `user` | password: `0000`  

---

## 🔮 Future Roadmap
- [ ] Add **search** and **filter** functionality for movies.  
- [ ] Switch storage from text file → **JSON / SQLite database**.  
- [ ] Implement **Update Movie Details** feature.  
- [ ] Build a **Graphical UI (WinForms/WPF)**.  

---

## 🤝 Contribution

Pull requests are welcome! For major changes, please open an issue first to discuss.  

---
# This project was developed by our team: Yehia Mohamed,Mohammed Mortada, and Mario Romany.

