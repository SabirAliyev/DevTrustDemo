Write a WPF project using devexpress ([https://www.devexpress.com/](https://www.devexpress.com/)) or any other 3rd party library to display a table with fake data.

It should be possible to select multiple lines.
By right-clicking on a table row, a context menu should be displayed.
The context menu should contain two item menus to export
- in CSV
- in TXT

By clicking one on the item menu, the selected lines should be exported to the appropriate format: CSV or TXT.

What will be paid attention to:
1.  Code quality
2.  Usage programming patterns, etc.

Minimum requirements:
1.  Using MVVM and programming patterns, etc.
2.  Usage of Dependency Injection.


### Implementation

This project was created by using DevExpress v22.2 as was mentioned in the task.

#### What was done.
1. The Northwind Database was downloaded and installed as a data source, with ADO.NET EntityFramework (into NorthwindData project) as in documentation for DevExpress. Only three tables were used (Orders, Employees and Shippers).
2. MVVM architecture was used. Added MvvmDialogsLibrary as a separate library class to manage custom dialogs. 
3. For serialization logic for 'DevTrustDemoSerializationLibrary' was created as a separate class library.
4. As IoC containerization I used Castle Windsor. Bootstrapping container located at one entry point (class BootstrapContainer).
5. Creation of CSV and TXT files was implemented in the context menu of the Orders table. 

6. All additional functionality of the tables, created by DevExpress, were left without any changes out from the box.


#### What was not done.

1. Unit tests not created. This item is not explicitly stated in the assignment. Therefore, with a very small amount of logic, this item is omitted for the time being.
2. Models in the main project were not created because in library NorthwindData that objects already declared. In full functional application, where used DAO layer, this model must be implemented.
3. DAO (CRUD) layer was not created. Since the data is used only for reading from the prepared database.


#### Problems.

Running the program in Visual Studio in debug mode may cause the program to crash when a dialog box for selecting a file to save is displayed. This happens in my development environment. It is possible that everything will work in your case. However, in other modes everything works fine. To eliminate this problem, you can try to set 'Enable native code debugging' in the main project ('DevTrustdemoUI') settings.

#### Installation

1. Install DevExpress (minimum version 22.2).
2. Clone this repository to your local machine.
3. Set 'DevtTrustDemoUI' project as startup project.

#### My comments.
This is my first time of using the DevExpress framework. In my last big project, I used Sincfusion instead. But that was not an unpleasant experience. Implementation of MVVM pattern when it comes to dialog boxes, it can seem a bit overwhelming. But in my opinion, if I had not done this, then the implementation of the pattern would not have been complete. I partially transferred the basis of my own developments and would be happy to know how do you usually implement it. Dependency Injection was not fully implemented. I acknowledge this deficiency in advance. But with such simple functionality, I couldn't find a good way to use it in this example. I would be glad to receive any comments from you.



