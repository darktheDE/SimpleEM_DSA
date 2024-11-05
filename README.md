# 💸 Simple Expense Management (SimpleEM_DSA)

**SimpleEM_DSA** is a basic console application for managing daily expenses, created as a final project for the Data Structures and Algorithms (DSA) course of HCMUUTE. This application allows users to input expenses, view them in a sorted manner, and interact with a simple console-based UI.

---

## 📂 Repository Overview

- **Repository Name:** SimpleEM_DSA
- **Branch Management:** [darktheDE](https://github.com/darktheDE)
- **Contributors:**
  - [darktheDE (Đỗ Kiến Hưng)](https://github.com/darktheDE)
  - [phantrongphu123](https://github.com/phantrongphu123)
  - [QuangDuyReal (Nguyen Van Quang Duy)](https://github.com/QuangDuyReal)
  - [Phanqui72](https://github.com/Phanqui72)

---

## 🌟 Features

1. **Expense Entry and Tracking**  
   - Record, view, and store daily expenses in `data.txt`.
   
2. **Sorting by Time**  
   - Automatically sorts expense entries by timestamp for easy tracking.

3. **User-Friendly Console UI**  
   - Presents data in a clear, structured format.

---

## 📁 Project Structure

- **Controllers**
  - **`ExpenseController.cs`:**  
    Manages the main functionality and flow of the application, connecting models with views for displaying expenses.

- **Models**
  - **`Expense.cs`:**  
    Defines the `Expense` class, encapsulating data fields like amount, date, and category for each expense entry.
    
  - **`ExpenseManager.cs`:**  
    Handles operations on expenses such as adding, retrieving, and sorting. Acts as a core data manager.

- **Views**
  - **`ExpenseView.cs`:**  
    Contains methods to display expense data and interact with users in the console, forming the UI layer of the application.

- **Other Files**
  - **`FinalDSA.csproj`:** Project file for configurations and dependencies.
  - **`Program.cs`:** The main entry point, initializing and running the application.
  - **`data.txt`:** Stores sample expense data, including 10 pre-defined entries for demonstration.
  - **`.gitattributes` and `.gitignore`:** Standard files to manage repository settings and ignored files.

---

## 📊 Tech Stack

- **Language:** C#
- **File Handling:** Uses `.txt` files for persistent storage.
- **Data Structures:** Array-based structure for sorting and managing expense entries.

---

## 📈 Future Enhancements

- **Filtering Options:**  
   Allow users to filter expenses by category or date.

- **Graphical Reports:**  
   Implement graphical views for tracking spending trends over time.

---

## 📥 Installation & Usage

1. Clone the repository:
   ```bash
   git clone https://github.com/darktheDE/SimpleEM_DSA.git
2. Open FinalDSA.sln in Visual Studio.
3. Build and run the application from the solution to start managing your expenses.

---
**Thank you for checking out SimpleEM_DSA! 🎉**

